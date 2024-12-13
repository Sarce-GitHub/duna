using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using X3_TERMINALINI.magazzino;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Righe : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _BPCORD = "";
        string _BPAADD = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/Menu.aspx", true);
            if (Request.QueryString["BC"] == null) Response.Redirect("Ordine.aspx", true);

            string[] Arr = Request.QueryString["BC"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4)
            {
                Response.Redirect("Ordine.aspx", true);
                return;
            }

            //
            _BPCORD = Arr[0];
            _BPAADD = Arr[1];
            if (!DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2),  out _DATE_DA))
            {
                Response.Redirect("Ordine.aspx", true);
                return;
            }
            if (!DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_A))
            {
                Response.Redirect("Ordine.aspx", true);
                return;
            }
            Ricerca();
            Obj_Cookie.Set_String("prebolla-bc", Request.QueryString["BC"].Trim().ToUpper());

            txt_Pallet.Focus();
            if (Obj_Cookie.Get_String("prebolla-palnum") != "")
            {
                txt_Pallet.Text = Obj_Cookie.Get_String("prebolla-palnum");
                txt_Pallet.Enabled = false;
                txt_Etichetta.Focus();
            }
        }

        private void Ricerca()
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            int idx = 0;
            string _c = "";

            _h = "<div class=\"row bg-head font-small\">";
            _h = _h + "<div class=\"col-12 col-md-6\">Articolo</div>";
            _h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Ord.</div>";
            _h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Prep.</div>";
            _h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Residua.</div>";
           
            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;

            string Ult_ITMREF = "";
            decimal Utl_QTY = 0;
            bool Check_QTY = false;
            var listOrdine = _SQL.Obj_YTSORDAPE_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A).OrderBy(x => x.ITMREF_0).ThenBy(x => x.ITMREF_0).ToList();
                        
            foreach (Obj_YTSORDAPE _i in listOrdine)
            {
                string bgColor = "";
                //var qtyPreparata = _SQL.Obj_STOALL_Lista(_i.SOHNUM_0, _i.SOPLIN_0, _i.SOQSEQ_0, _i.ITMREF_0);

                var ordini = _SQL.Obj_YTSORDAPE_Ordini(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, _i.ITMREF_0, _i.SAU_0)
                .OrderBy(o => o.SHIDAT_0)
                .ThenBy(o => o.SOHNUM_0)
                .GroupBy(o => new { o.SOHNUM_0, o.QTYSTU_0, o.SAU_0, o.DLVQTYSTU_0, o.SHIDAT_0 })
                .Select(group => new
                {
                    group.Key.SOHNUM_0,
                    group.Key.QTYSTU_0,
                    group.Key.SAU_0,
                    //group.Key.PALNUM_0,
                    group.Key.DLVQTYSTU_0,
                    group.Key.SHIDAT_0,

                    Items = group.ToList()
                });

                var listaOrdiniConPallet = ordini
                .GroupBy(o => o.SOHNUM_0)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        UM = g.Select(o => o.SAU_0.ToLower()).FirstOrDefault(),
                        QtyTot = g.Sum(o => o.QTYSTU_0 - o.DLVQTYSTU_0),
                        Shidat = g.Select(o => o.SHIDAT_0).FirstOrDefault(),
                        //PalletQuantities = g
                                            //.Where(o => !string.IsNullOrEmpty(o.PALNUM_0)) //rimuovere per vedere le qta da preparare come pallet
                                            //.GroupBy(o => !string.IsNullOrEmpty(o.PALNUM_0) ? o.PALNUM_0 : "Da preparare")
                                            //.ToDictionary(
                                            //    pg => pg.Key,
                                            //    pg => new
                                            //    {
                                            //        //palQty = pg.Sum(o => o.QTYSTU_0).ToString("0.##").ToLower(),
                                            //        //palUm = pg.Select(o => o.SAU_0).FirstOrDefault().ToLower()
                                            //    }
                                            //)

                    }
                );

                _c = ((idx % 2) == 1 ? "bg-alt" : "");

                if (_i.QTYPREP_0 > 0 && _i.QTYPREP_0 < (_i.QTY_0 - _i.DLVQTY_0)) _c = "bg-att";
                if (_i.QTYPREP_0 > 0 && _i.QTYPREP_0 == (_i.QTY_0 - _i.DLVQTY_0)) _c = "bg-ok";


                _h = "<div class=\"row " + _c + " \" data-itm=\""+ _i.ITMREF_0 +"\" data-sau=\""+ _i.SAU_0 +"\">";
                _h += "<div class=\"col-12 col-md-2 check-pos\"><b>" + _i.ITMREF_0 + "</b></div>";
                _h += "<div class=\"col-12 col-md-4 font-small check-pos\">" + _i.ITMDES_0 + " ("+ _i.NrRighe.ToString("0") +")</div>";
                _h += "<div class=\"col-4 col-md-2 text-end check-pos\">" + (_i.QTYSTU_0).ToString("0.##") + " " + _i.STU_0 + "&nbsp;&nbsp;</div>";
                //
                if (Utl_QTY>0 && !Check_QTY)
                {
                    //_h += "<div class=\"col-4 col-md-2 text-end check-pos\"><b>" + (_i.QTYPREP_0 - Utl_QTY).ToString("0.##") + " " + _i.STU_0 + "</b>&nbsp;&nbsp;</div>";
                }
                else
                {
                   _h += "<div class=\"col-4 col-md-2 text-end check-pos\"><b>" + (_i.QTYPREP_0).ToString("0.##") + " " + _i.STU_0 + "</b>&nbsp;&nbsp;</div>";
                }

                _h += "<div class=\"col-4 col-md-2 text-end check-pos\"><i>" + (_i.QTYSTU_0 - _i.DLVQTYSTU_0).ToString("0.##") + " " + _i.STU_0 + "</i>&nbsp;&nbsp;</div>";

                foreach (var ordine in listaOrdiniConPallet)
                {
                    //string palletString = "";
                    //foreach (var o in ordine.Value.PalletQuantities)
                    //{
                    //    palletString += $"{o.Key}: {o.Value.palQty}{o.Value.palUm}; ";
                    //}

                    bgColor = "bg-light-grey";
                    bgColor = "bg-light-grey";
                    if (_c == "bg-ok") bgColor = "bg-light-green";
                    if (_c == "bg-att") bgColor = "bg-light-yellow";
                    _h += "<div class=\"col-12 font-small" + " " + bgColor + " " + "\"><span>Ord." + ordine.Key +  " " + ordine.Value.Shidat.ToString("dd/MM/yyyy") + ": " + ordine.Value.QtyTot.ToString("0.##") + ordine.Value.UM +"</span><span class=\"ms-5\">" + "</span></div> ";                    
                }
                
                
                _h += "<div class=\"col-12 \"><hr/></div>";
                _h += "</div>";
                //
                _d.InnerHtml = _d.InnerHtml + _h;
            }

            pan_dati.Controls.Add(_d);
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            if (txt_Pallet.Text != "")
            {
                if (txt_Etichetta.Text.Trim() != "")
                {
                    // Estrazione dati Etichetta
                    Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Etichetta.Text.Trim().ToUpper());
                    //
                    string err = _SQL.obj_STOCK_Check(_USR.FCY_0, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, Properties.Settings.Default.SPED_Ubic, _BPCORD, _BPAADD, _DATE_DA, _DATE_A);
                    if (err == "")
                    {
                        Obj_Cookie.Set_String("prebolla-palnum", txt_Pallet.Text.Trim().ToUpper());
                        Response.Redirect("Ordine_Righe_Dett.aspx?LOT=" + txt_Etichetta.Text.Trim().ToUpper());
                    }
                    else
                    {

                        frm_error.Text = err;
                        txt_Etichetta.Text = "";
                        txt_Etichetta.Focus();
                    }
                }
            }
            else
            {
                txt_Etichetta.Text = "";
                frm_error.Text = "PALLET NON INDICATO";
                txt_Pallet.Focus();
            }
        }

        protected void txt_Pallet_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            // ESISTENZA PALLET
            if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Pallet.Text.Trim().ToUpper()))
            {
                // ESISTENZA PALLET IN SPEDIZIONE
                if(_SQL.obj_PALNUM_Check_State(_USR.FCY_0, txt_Pallet.Text.Trim().ToUpper(), Properties.Settings.Default.SPED_Ubic))
                {
                    Response.Redirect("Ordine_Spedizione_Pallet.aspx?PALNUM=" + txt_Pallet.Text.Trim().ToUpper());
                }
                //if (!_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Pallet.Text.Trim(), Properties.Settings.Default.SPED_Ubic, _BPCORD, _BPAADD))
                //{
                //    frm_error.Text = "PALLET NON RISULTA IN UBICAZIONE SPEDIZIONE";
                //    txt_Pallet.Text = "";
                //    txt_Pallet.Focus();
                //    return;
                //}
            }
            //
            Obj_Cookie.Set_String("prebolla-palnum", txt_Pallet.Text);
            txt_Pallet.Enabled = false;
            txt_Etichetta.Focus();
        }

        protected void btn_Pallet_Click(object sender, EventArgs e)
        {
            Obj_Cookie.Set_String("prebolla-palnum", "");
            Response.Redirect(Request.RawUrl);
        }
    }
}