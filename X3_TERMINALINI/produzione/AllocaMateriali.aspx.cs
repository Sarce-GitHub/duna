using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.produzione
{
    public partial class AllocaMateriali : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL6_0 != 2) Response.Redirect("/", true);
            if (!Page.IsPostBack)
            {
                txt_RicercaBC.Focus();
            }
        }

        protected void btn_RicercaBC_Click(object sender, EventArgs e)
        {
            pan_dati.Controls.Clear();

            string nOrdine = txt_RicercaBC.Text.Trim().ToUpper();
            bool orderExists = _SQL.IsProdOrderValid(_USR.FCY_0, nOrdine);
            if(!orderExists)
            {
                frm_error.Text = "Ordine di Produzione non trovato";
                ResetForm();
                return;
            }
            Response.Redirect("AllocaMaterialiRighe.aspx?NOrd=" + nOrdine, true);
            return;
        }

        protected void ResetForm()
        {
           txt_RicercaBC.Text = "";
            txt_RicercaBC.Focus();
        }
    }
}