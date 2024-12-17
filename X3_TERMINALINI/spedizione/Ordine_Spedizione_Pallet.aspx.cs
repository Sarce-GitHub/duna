using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Spedizione_Pallet : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _BPCORD = "";
        string _BPAADD = "";
        string _SOHNUM = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;

        List<Obj_STOCK>_STOCK = new List<Obj_STOCK>();
        string UBIC = Properties.Settings.Default.SPED_Ubic;
        private string connectionSQL = "Data Source=" + Properties.Settings.Default.SQL_IP + ";Initial Catalog=" + Properties.Settings.Default.SQL_Catalog + ";User ID=" + Properties.Settings.Default.SQL_User + ";Password=" + Properties.Settings.Default.SQL_Psw + ";";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();

            string[] Arr = Obj_Cookie.Get_String("prebolla-bc").ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (Request.QueryString["PALNUM"] != null)
            {
                _BPCORD = Arr[0];
                _BPAADD = Arr[1];
                DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2), out _DATE_DA);
                DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_A);
                if (Arr.Length > 4) _SOHNUM = Arr[4];

                var ordini = _SQL.Obj_YTSORDAPE_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A);

                lbl_pallet.Text = "PALLET:" + Request.QueryString["PALNUM"];
                _SQL.obj_PALNUM_GetListStock(_USR.FCY_0, Request.QueryString["PALNUM"], out _STOCK);
                var articoliNonCompatibiliOrdine = _STOCK.Select(s => s.ITMREF_0).Except(ordini.Select(o => o.ITMREF_0));
                var outOfStockItems = new List<string>();

                if (!articoliNonCompatibiliOrdine.Any())
                {
                    outOfStockItems = CheckQtaCompatibili(ordini, _STOCK);
                    if(outOfStockItems.Any())
                    {
                        PalletNonCompatibile();
                    }
                }
                else
                {
                    PalletNonCompatibile();
                }
                CreaGriglia(articoliNonCompatibiliOrdine, outOfStockItems);

            }
        }

        private void PalletNonCompatibile()
        {
            frm_error.Text = "Articoli pallet non compatibili per la spedizione";
            btn_Conferma.Visible = false;
        }

        private void CreaGriglia(IEnumerable<string> articoliNonCompatibiliOrdine, List<string> outOfStockItems)
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            int idx = 0;
            string _c = "";

            _h = "<div class=\"row bg-head font-small\">";
            _h = _h + "<div class=\"col-4 col-md-4\">Articolo</div>";
            _h = _h + "<div class=\"col-4 col-md-4\">Lotto</div>";
            _h = _h + "<div class=\"col-4 col-md-4 text-end\">Quantità</div>";
            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;

            if (_STOCK != null && _STOCK.Any())
            {
                foreach (var _i in _STOCK) 
                {
                    _c = ((idx % 2) == 1 ? "bg-alt" : "");
                    _c = (outOfStockItems.Contains(_i.ITMREF_0) ? "bg-orange" : _c);
                    _c = (articoliNonCompatibiliOrdine.Contains(_i.ITMREF_0) ? "bg-ko" : _c);

                    //if (_i.QTYPREP_0 > 0 && _i.QTYPREP_0 < (_i.QTY_0 - _i.DLVQTY_0)) _c = "bg-att";
                    //if (_i.QTYPREP_0 > 0 && _i.QTYPREP_0 >= (_i.QTY_0 - _i.DLVQTY_0)) _c = "bg-ok";

                    _h = "<div class=\"row " + _c + " \" data-itm=\"" + _i.ITMREF_0 + "\">";
                    _h = _h + "<div class=\"col-4 col-md-4 check-pos\"><b>" + _i.ITMREF_0 + "</b></div>";
                    _h = _h + "<div class=\"col-4 col-md-4 check-pos\">" + _i.LOT_0 + "</div>";
                    _h = _h + "<div class=\"col-4 col-md-4 text-end check-pos\">" + (_i.QTYSTU_0 / _i.PCUSTUCOE_0).ToString("0.##") + "&nbsp;&nbsp;</div>";

                    _h = _h + "<div class=\"col-12 \"><hr/></div>";
                    _h = _h + "</div>";

                    _d.InnerHtml = _d.InnerHtml + _h;
                }


            }
            pan_dati.Controls.Add( _d );

        }

        private List<string> CheckQtaCompatibili(List<Obj_YTSORDAPE> ordini, List<Obj_STOCK> _STOCK)
        {
            List<string> articoliQtaNonCompatibili = new List<string>();

            foreach (var item in _STOCK)
            {
                var ordine = ordini.FirstOrDefault(w => w.ITMREF_0 == item.ITMREF_0);
                if(ordine != null )
                {
                    var qtaOrdine = ordine.QTY_0 - ordine.DLVQTY_0 - ordine.QTYPREP_0;

                    if (item.QTYPCU_0 - (item.CUMALLQTY_0 / item.PCUSTUCOE_0) > qtaOrdine)
                    {
                        articoliQtaNonCompatibili.Add(item.ITMREF_0);
                    }
                }
            }
            return articoliQtaNonCompatibili;
        }

        protected void btn_Conferma_Click(object sender, EventArgs e)
        {
            string err = "";
            var _Stock = _SQL.obj_PALNUM_GetListStock(_USR.FCY_0, Request.QueryString["PALNUM"], out _STOCK);

            using (SqlConnection conn = new SqlConnection(connectionSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (var item in _STOCK)
                    {
                        var QTY = item.QTYPCU_0;

                        foreach (Obj_YTSORDAPE _ord in _SQL.Obj_YTSORDAPE_Ordini(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, item.ITMREF_0, item.PCU_0)
                                                        .Where(x => string.IsNullOrEmpty(_SOHNUM) || x.SOHNUM_0 == _SOHNUM))

                        {

                            if (QTY > 0)//(_ord.QTY_MANC > 0 && QTY > 0)
                            {
                                //decimal _QTY_PREP = (QTY > _ord.QTY_MANC ? _ord.QTY_MANC : QTY);
                                decimal _QTY_PREP = QTY;
                                if (Cambio(_QTY_PREP, item))
                                {
                                    //bool ok = cls_TermWS.WS_AllocaDett(_ord.SOHNUM_0, _ord.SOPLIN_0.ToString(), _USR.FCY_0, item.LOT_0, item.PALNUM_0, Properties.Settings.Default.SPED_Ubic, _QTY_PREP, out err);
                                    //if(!ok) throw new Exception(err);

                                    //QTY = QTY - _ord.QTY_MANC;
                                }
                                else
                                {
                                    transaction.Rollback();
                                    return;
                                }
                            }
                        }

                    }
                    transaction.Commit();
                    Response.Redirect("Ordine_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"), false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    frm_error.Text = ex.Message;
                }
            }
        }

        private bool Cambio(decimal _QTY_PREP, Obj_STOCK item)
        {
            string _err = "";
            if(!cls_TermWS.WS_CambioStock(_USR, item.ITMREF_0, item.LOT_0, item.SLO_0, item.PCU_0, _QTY_PREP, item.STA_0, item.PALNUM_0, item.LOC_0, item.STA_0, item.PALNUM_0, Properties.Settings.Default.SPED_Ubic, out _err))
            {
                //Errore
                frm_error.Text = _err;
                return false;
            }
            return true;
        }

        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
        }
    }
}