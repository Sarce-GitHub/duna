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
    public partial class Ordine_Spedizione_Disimpegno_Pallet : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _BPCORD = "";
        string _BPAADD = "";
        string _PALNUM = "";
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

                var ordini = _SQL.Obj_YTSORDAPE_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A);

                lbl_pallet.Text =  Request.QueryString["PALNUM"];
                _PALNUM = Request.QueryString["PALNUM"];
                _SQL.obj_PALNUM_GetListStock(_USR.FCY_0, Request.QueryString["PALNUM"], out _STOCK);
                var articoliNonCompatibiliOrdine = _STOCK.Select(s => s.ITMREF_0).Except(ordini.Select(o => o.ITMREF_0));
                var outOfStockItems = new List<string>();

                //if (!articoliNonCompatibiliOrdine.Any())
                //{
                //    outOfStockItems = CheckQtaCompatibili(ordini, _STOCK);
                //    if(outOfStockItems.Any())
                //    {
                //        PalletNonCompatibile();
                //    }
                //}
                //else
                //{
                //    PalletNonCompatibile();
                //}
                //CreaGriglia(articoliNonCompatibiliOrdine, outOfStockItems);
                Ricerca();

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

                    _h = "<div class=\"row tabella-dati " + _c + " \" data-itm=\"" + _i.ITMREF_0 + "\">";
                    _h = _h + "<div class=\"col-4 col-md-4 check-pos\"><b>" + _i.ITMREF_0 + "</b></div>";
                    _h = _h + "<div class=\"col-4 col-md-4 check-pos\">" + _i.LOT_0 + "</div>";
                    _h = _h + "<div class=\"col-4 col-md-4 text-end check-pos qta-cell\">" + _i.QTYSTU_0.ToString("0.##") + " " + _i.STU_0 + "&nbsp;&nbsp;</div>";

                    _h = _h + "<div class=\"col-12 \"><hr/></div>";
                    _h = _h + "</div>";

                    _d.InnerHtml = _d.InnerHtml + _h;
                }


            }
            pan_dati.Controls.Add( _d );

        }

        private void Ricerca()
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            int idx = 0;
            string _c = "";


            var listOrdine = _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, _PALNUM)
                .OrderBy(x => x.PALNUM_0)
                .ThenBy(x => x.VCRNUM_0)
                .ThenBy(x => x.ITMREF_0)
                .ThenBy(x => x.SHIDAT_0)
                .GroupBy(x => x.PALNUM_0)
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(y => y.VCRNUM_0)
                            .ToDictionary(
                                y => y.Key,
                                y => y.Select(z => new { z.ITMREF_0, z.SHIDAT_0, z.SAU_0, z.STU_0, z.QTYSTU_0 }).ToList()
                            )
                );

            foreach (var kvp in listOrdine)
            {
                var palnum = kvp.Key;
                var innerDict = kvp.Value;

                _h = "<div class=\"row bg-head\">";
                _h = _h + "<div class=\"col-12 col-md-6 fw-bold\">" + palnum + "</div>";
                //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Ord.</div>";
                //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Prep.</div>";
                //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Residua.</div>";

                _h = _h + "</div>";
                _d.InnerHtml = _d.InnerHtml + _h;


                //idx = 0;
                foreach (var innerKvp in innerDict)
                {
                    var vcrnum = innerKvp.Key;
                    var items = innerKvp.Value;

                    _h = "<div class=\"row " + _c + " \" data-itm=\"" + palnum + "\">";
                    _h += "<div class=\"col-12 col-md-2 check-pos\"><b>" + vcrnum + "</b></div>";
                    _h += "</div>";

                    _d.InnerHtml += _h;

                    foreach (var item in items)
                    {
                        _h = "<div class=\"row tabella-dati " + _c + "\" data-itm=\"" + vcrnum + "\">";
                        _h += "<div class=\"col-0 col-md-1 font-small check-pos\"></div>";
                        _h += "<div class=\"col-4 font-small check-pos\">" + item.ITMREF_0 + "</div>";
                        _h += "<div class=\"col-3 font-small check-pos\">" + item.SHIDAT_0.ToString("dd/MM/yyyy") + "</div>";
                        _h += "<div class=\"col-4 text-end check-pos qta-cell\">" + item.QTYSTU_0.ToString("0.##") + " " + item.SAU_0 +  "&nbsp;&nbsp;</div>";
                        _h += "</div>";

                        _d.InnerHtml += _h;
                    }

                    _d.InnerHtml += "<div class=\"col-12 \"><hr/></div>";
                }
            }
            pan_dati.Controls.Add(_d);
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

                    if (item.QTYPCU_0 - item.CUMALLQTY_0 > qtaOrdine)
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
                        foreach (Obj_YTSALLORD _ord in _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, _PALNUM))
                        {
                            var QTYDISALL = _ord.QTYSTU_0 * -1;
                            if (cls_TermWS.WS_AllocaDett(_ord.VCRNUM_0, _ord.SOPLIN_0.ToString(), _ord.SOQSEQ_0.ToString(), _USR.FCY_0, _ord.LOT_0, _ord.PALNUM_0, item.LOC_0, QTYDISALL, out err))
                            {
                                Obj_STOCK _stock = new Obj_STOCK()
                                { 
                                    ITMREF_0 = _ord.ITMREF_0,
                                    LOT_0 = _ord.LOT_0,
                                    STA_0 = item.STA_0,
                                    PALNUM_0 = _ord.PALNUM_0,
                                    LOC_0 = item.LOC_0,
                                    SLO_0 = _ord.SLO_0,
                                    PCU_0 = item.PCU_0,
                                };


                                bool ok = Cambio(_ord.QTYSTU_0, _stock);
                                if(!ok) throw new Exception(err);
                            }
                            else
                            {
                                transaction.Rollback();
                                return;
                            }
                        }

                    }
                    transaction.Commit();
                    Response.Redirect("Ordine_Spedizione_Disimpegno_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"), false);
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
            if(!cls_TermWS.WS_CambioStock(_USR, item.ITMREF_0, item.LOT_0, item.SLO_0, item.PCU_0, _QTY_PREP, item.STA_0, item.PALNUM_0, item.LOC_0, item.STA_0, item.PALNUM_0, txt_destinazione.Text.Trim().ToUpper(), out _err))
            {
                //Errore
                frm_error.Text = _err;
                return false;
            }
            return true;
        }

        protected void txt_destinazione_TextChanged(object sender, EventArgs e)
        {
            try
            {
                frm_error.Text = "";
                btn_Conferma.Visible = false;
                txt_destinazione.Text = txt_destinazione.Text.Trim().ToUpper();
                bool _ok = true;
                if(txt_destinazione.Text.Trim() == "")
                {
                    txt_destinazione.Focus();
                    _ok = false;
                    return;
                }

                if (!_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_destinazione.Text))
                {
                    frm_error.Text = "Ubicazione " + txt_destinazione.Text + " non censita in " + _USR.FCY_0;
                    txt_destinazione.Text = "";
                    txt_destinazione.Focus();
                    _ok = false;
                    return;
                }

                if(txt_destinazione.Text == Properties.Settings.Default.SPED_Ubic)
                {
                    frm_error.Text = "Ubicazione " + txt_destinazione.Text + " non valida";
                    txt_destinazione.Text = "";
                    txt_destinazione.Focus();
                    _ok = false;
                    return;
                }

                if (_ok) btn_Conferma.Visible = true;
                btn_Conferma.Visible = true;

                btn_Conferma.Focus();
            }
            catch (Exception ex)
            {
                frm_error.Text = ex.Message;
            }
        }

        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Spedizione_Disimpegno_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
        }
    }
}