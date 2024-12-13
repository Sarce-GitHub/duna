using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class BollePreparazione : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/", true);
            txt_Ricerca.Focus();
        }

        protected void btn_Ricerca_Click(object sender, EventArgs e)
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();
            //
            string _h = "";
            int idx = 0;
            //
            _h = "<div class=\"row bg-head\">";
            _h = _h + "<div class=\"col-10 col-md-10\">Prepatazione</div>";
            _h = _h + "<div class=\"col-2 col-md-2\">Rg.</div>";
            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;
            //
            foreach (var _i in _SQL.Obj_STOPREH_Lista(_USR.FCY_0, _USR.USR_X3_0, txt_Ricerca.Text.Trim().ToUpper()))
            {
                string _c = ((idx % 2) == 1 ? "bg-alt" : "");
                if (_i.RIGHE_PREP > 0) _c = "bg-att";
                if (_i.RIGHE_PREP == _i.RIGHE_TOT) _c = "bg-orange";
                if (_i.DLVFLG_0 == "2") _c = "bg-ok";
                //
                _h = "<div class=\"row " + _c + " check-bolla\" data-prh=\"" + _i.PRHNUM_0 + "\">";
                _h = _h + "<div class=\"col-10 col-md-10\"><b>" + _i.PRHNUM_0 + "</b><br/><i>" + _i.BPCNUM_0 + " - " + _i.BPCNAM_0+ "</i></div>";
                _h = _h + "<div class=\"col-2 col-md-2\">" + _i.RIGHE_PREP.ToString() + "/"+ _i.RIGHE_TOT.ToString() + "</div>";
                _h = _h + "</div>";
                //
                idx++;
                _d.InnerHtml = _d.InnerHtml + _h;
            }
            //
            if (_d.InnerHtml == "") _d.InnerHtml = "<b>Nessuna bolla corrispondente per la ricerca</b>";
            txt_Ricerca.Text = "";
            //
            pan_dati.Controls.Add(_d);
        }
    }
}