using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace X3_TERMINALINI.magazzino
{
    public partial class Ricerca : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL1_0 != 2) Response.Redirect("/", true);
            //
            frm_OK.Text = "";
            frm_error.Text = "";
            if (!IsPostBack) txt_Ricerca.Focus();
        }

        //AGGIUNGERE TEXT CHANGED
        protected void txt_Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Ricerca.Focus();
        }

        protected void txt_Ricerca_TextChanged(object sender, EventArgs e)
        {
            List<Obj_STOCK> List = new List<Obj_STOCK>();
            HtmlGenericControl _div = new HtmlGenericControl("div");
            //
            if (txt_Ricerca.Text.Trim()!="")
            {
                Obj_STOCK_ETIC _e = new Obj_STOCK_ETIC(txt_Ricerca.Text.Trim().ToUpper());
                switch (txt_Tipo.SelectedValue)
                {
                    case "C":
                        //string[] ArrEt = txt_Ricerca.Text.Trim().ToUpper().Split(Properties.Settings.Default.Etic_Split.ToCharArray());
                        //string itmref= ArrEt[0];
                        List = _SQL.obj_STOCK_SearchByCode(_USR.FCY_0, _e.ITMREF);
                        break;
                    case "D":
                        List = _SQL.obj_STOCK_SearchByDesc(_USR.FCY_0, txt_Ricerca.Text.Trim());
                        break;
                    case "E":
                        List = _SQL.obj_STOCK_SearchByBarCode(_USR.FCY_0, _e.ITMREF, _e.LOT, _e.SLO);
                        break;
                    case "L":
                        List = _SQL.obj_STOCK_SearchByLocation(_USR.FCY_0, txt_Ricerca.Text.Trim().ToUpper());
                        frm_error.Text = _SQL.error;
                        break;
                    case "P":
                        List = _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_Ricerca.Text.Trim().ToUpper());
                        frm_error.Text = _SQL.error;
                        break;
                    default:
                        _div.InnerHtml = "<br/><b>SELEZIONARE UNA RICERCA</b><br/>";
                        pan_data.Controls.Add(_div);
                        return;
                }
                txt_Ricerca.Focus();
                
                string _last = "";
                string h = "";
                string header = "";
                int i = 0;
                //
                if (List.Count>0) 
                { 
                    h = h + "<br/><b>MAGAZZINO " + _USR.FCY_0 + "</b><br/>";
                    foreach (Obj_STOCK s in List.OrderBy(o => o.ITMREF_0).ThenBy(o => o.LOC_0).ThenBy(o => o.LOT_0).ThenBy(o => o.SLO_0))
                    {
                        string seakey = (!string.IsNullOrEmpty(s.SEAKEY_0) && s.SEAKEY_0 != " " && s.SEAKEY_0 != s.ITMREF_0) ? " - " + s.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                        if (_last != s.ITMREF_0)
                        {
                            i = 0;
                            _last = s.ITMREF_0;
                            h = h + "<div class=\"row bg-head flex-column\">";
                            h = h + "<div class=\"col-12 col-md-2\" style=\"white-space:nowrap\"><b>" + s.ITMREF_0 + seakey + "</b></div>";
                            h = h + "<div class=\"col-12 col-md-6 font-small\"><i>" + s.ITMDES_0 + "</i></div>";

                            h = h + "</div>";

                            if(header == "")
                            {
                                header = "<div class=\"row bg-head\">";

                                header += "<div class=\"d-flex\" style=\"flex:3\">";
                                header += "<div class=\"me-2\" style=\"flex:1\">Ubicazione</div>";
                                header += "<div class=\"me-2\" style=\"flex:1\">Lotto" + " " + Properties.Settings.Default.Etic_Split + " SLO</div>";
                                header += "<div class=\"me-2\" style=\"flex:1\">Palnum</div>";
                                header += "</div>";

                                header += "<div class=\"d-flex\" style=\"flex:1\">";
                                header += "<div class=\"me-2\" style=\"flex:1\">UM</div>";
                                header += "<div class=\"text-end\" style=\"flex:1\">Qta</div>";
                                header += "</div>";

                                header = header + "</div>";
                                h += header;
                            }
                        }

                        h = h + "<div class=\"row "+ ((i % 2)==1 ? "bg-alt" : "") +"\">";
                        //
                        if (s.QTYSTU_0>0)
                        { 
                            string slo = !string.IsNullOrEmpty(s.SLO_0) ? " " + Properties.Settings.Default.Etic_Split + " "  + s.SLO_0 : "";
                            h += "<div class=\"d-flex\" style=\"flex:3\">";
                            h += "<div class=\"me-2\" style=\"flex:1\">" + s.LOC_0 + "</div>";
                            h += "<div class=\"me-2\" style=\"flex:1;white-space:nowrap\">" + s.LOT_0 + slo + "</div>";
                            h += "<div class=\"me-2\" style=\"flex:1\">" +  s.PALNUM_0 + "</div>";
                            h += "</div>";

                            h += "<div class=\"d-flex\" style=\"flex:1\">";
                            h += "<div class=\"me-2\" style=\"flex:1\">" + s.PCU_0 + "</div>";
                            h += "<div class=\"text-end\"style=\"flex:1;white-space:nowrap\">" + s.QTYPCU_0.ToString("0.###") + " " + " (" + s.STA_0 + ")</div>";
                            h += "</div>";
                        }
                        else
                        {
                            h = h + "<div class=\"col-12\"><b>Nessuno STOCK</b></div>";
                            txt_Ricerca.Focus();
                        }
                        //
                        h = h + "</div>";
                        header = "";
                        i++;
                    }
                }
                else
                {
                    h = h + "<div class=\"col-12\"><b>Nessuno record trovato</b></div>";
                    txt_Ricerca.Focus();
                }
                _div.InnerHtml = h;
                //
                txt_Ricerca.Text = "";

            }
            pan_data.Controls.Add(_div);

        }
    }
}