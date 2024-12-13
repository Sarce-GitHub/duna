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
    public partial class Ordine_Spedizione_Disimpegno_Righe : System.Web.UI.Page
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
            }
        }

        private void Ricerca()
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            int idx = 0;
            string _c = "";


            var listOrdine = _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A)
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
                        _h += "<div class=\"col-4 text-end check-pos qta-cell\">" + item.QTYSTU_0.ToString("0.##") + " " + item.STU_0 + "&nbsp;&nbsp;</div>";
                        _h += "</div>";

                        _d.InnerHtml += _h;
                    }

                    _d.InnerHtml += "<div class=\"col-12 \"><hr/></div>";
                }
            }
            pan_dati.Controls.Add(_d);
        }


        protected void txt_Pallet_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            txt_Pallet.Text = txt_Pallet.Text.Trim().ToUpper();
            // ESISTENZA PALLET
            if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Pallet.Text.Trim().ToUpper()))
            {
                // ESISTENZA PALLET IN SPEDIZIONE
                if(_SQL.obj_PALNUM_Check_Preparato(_USR.FCY_0, txt_Pallet.Text.Trim().ToUpper(), Properties.Settings.Default.SPED_Ubic))
                {
                    Response.Redirect("Ordine_Spedizione_Disimpegno_Pallet.aspx?PALNUM=" + txt_Pallet.Text.Trim().ToUpper());
                    return;
                }
                frm_error.Text = "PALLET NON RISULTA IN UBICAZIONE SPEDIZIONE";
                return;
            }
            frm_error.Text = "PALLET NON ESISTENTE";
            txt_Pallet.Text = "";
            //Obj_Cookie.Set_String("prebolla-palnum", txt_Pallet.Text);
        }
    }
}