using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI._include
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        public string _ver = cls_Tools.Get_Vers();
        public string _Title = Properties.Settings.Default.BaseTitle;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!cls_Tools.Check_User()) Response.Redirect("/");

            frm_top_ora.Text = DateTime.Now.ToString("HH:mm");
        }
    }
}