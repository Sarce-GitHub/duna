using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class BollePreparazione_Righe : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _PRHNUM = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/Menu.aspx", true);
            if (Request.QueryString["PRHNUM"] == null) Response.Redirect("BollePreparazione.aspx", true);
            _PRHNUM = Request.QueryString["PRHNUM"];
            lbl_PRHNUM.Text = _PRHNUM;
            Ricerca();
        }


        private void Ricerca()
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            //int idx = 0;
            //string _c = "";

            _h = "<div class=\"row bg-head font-small\">";
            _h = _h + "<div class=\"col-7 col-md-4\">Articolo</div>";
            _h = _h + "<div class=\"col-2 col-md-2 text-end\">Ord</div>";
            _h = _h + "<div class=\"col-2 col-md-2 text-end\">Prep</div>";
            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;
            ////
            //foreach (Obj_YTSORDINEAPE _i in _SQL.Obj_YTSORDINEAPE_Lista(_USR.FCY_0, _SOHNUM).OrderBy(x => x.BPCORD_0).ToList())
            //{
            //    _c = ((idx % 2) == 1 ? "bg-alt" : "");
            //    if (_i.QTYPREP_0 > 0) _c = "bg-att";
            //    _h = "<div class=\"row " + _c + " \" data-soh=\"" + _i.SOHNUM_0 + "\" data-lin=\"" + _i.SOPLIN_0 + "\">";
            //    _h = _h + "<div class=\"col-7 col-md-4 check-ordine\"><b>" + _i.ITMREF_0 + "</b></div>";
            //    _h = _h + "<div class=\"col-2 col-md-2 text-end check-ordine\"><i>" + _i.QTYSTU_0.ToString("0.##") + "</i></div>";
            //    _h = _h + "<div class=\"col-2 col-md-2 text-end check-ordine\"><i>" + _i.QTYPREP_0.ToString("0.##") + "</i></div>";
            //    _h = _h + "<div class=\"col-1 col-md-2 check-ordine\">" + _i.STU_0 + "</div>";
            //    _h = _h + "<div class=\"col-12 col-md-4 font-small\">" + _i.ITMDES_0 + "</div>";
            //    _h = _h + "</div>";
            //    //
            //    _d.InnerHtml = _d.InnerHtml + _h;
            //}
            //
            pan_dati.Controls.Add(_d);
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {

        }
    }
}