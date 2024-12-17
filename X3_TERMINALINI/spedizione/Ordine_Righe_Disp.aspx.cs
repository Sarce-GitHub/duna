using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Righe_Disp : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/Menu.aspx", true);
            string _ITM = "";
            if (Request.QueryString["ITM"] == null) Response.Redirect("Ordine_Righe.aspx", true);
            _ITM = Request.QueryString["ITM"].ToString();
            string _SAU = "";
            if (Request.QueryString["SAU"] == null) Response.Redirect("Ordine_Righe.aspx", true);
            _SAU = Request.QueryString["SAU"].ToString();


            string h = "";
            int i = 0;
            HtmlGenericControl _div = new HtmlGenericControl("div");
            List<Obj_STOCK> List = _SQL.obj_STOCK_SearchByITMPCU(_USR.FCY_0, _ITM, _SAU);
            //
            if (List.Count > 0)
            {
                foreach (Obj_STOCK s in List.OrderBy(o => o.ITMREF_0).ThenBy(o => o.LOC_0).ThenBy(o => o.LOT_0).ThenBy(o => o.SLO_0))
                {
                    h =  "<div class=\"row bg-head\">";
                    h = h + "<div class=\"col-12 col-md-2\"><b>" + s.ITMREF_0 + "</b></div>";
                    h = h + "<div class=\"col-12 col-md-6 font-small\"><i>" + s.ITMDES_0 + "</i></div>";
                    h = h + "</div>";

                    h = h + "<div class=\"row font-small " + ((i % 2) == 1 ? "bg-alt" : "") + "\">";
                    //
                    h = h + "<div class=\"col-3 col-md-2\">" + s.LOC_0 + "</div>";
                    h = h + "<div class=\"col-5 col-md-2\">" + (s.LOT_0 + " " + s.SLO_0 + " " + s.PALNUM_0).Trim() + "</div>";
                    h = h + "<div class=\"col-4 col-md-2 text-end\">" + s.QTYSTU_0.ToString("0.###") + " " + s.STU_0 + " (" + s.STA_0 + ")</div>";
                    //
                    h = h + "</div>";
                    i++;
                    _div.InnerHtml = _div.InnerHtml + h;
                }
            }
            else
            {
                h =  "<div class=\"col-12\"><b>Nessuno record trovato</b></div>";
                _div.InnerHtml = _div.InnerHtml + h;
            }
            
            pan_data.Controls.Add(_div);
        }

        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
        }
    }
}