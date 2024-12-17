using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using X3_TERMINALINI._include;

namespace X3_TERMINALINI
{
    public partial class _default : System.Web.UI.Page
    {
        public string _ver = cls_Tools.Get_Vers();
        public string _Title = Properties.Settings.Default.BaseTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cls_Tools.Check_User()) Response.Redirect("Menu.aspx");
            //
            cls_SQL _SQL = new cls_SQL();

            login_err.Text = "";
            if (Page.IsPostBack)
            {
                List<string> _V = new List<string>();
                _V.Add(Request.Form["login-user"]);
                //
                Obj_YTSUTX _utx = new Obj_YTSUTX();
                if (_SQL.Obj_YTSUTX_Load(Request.Form["login-user"], Request.Form["login-pass"], out _utx))
                {
                    //
                    if (_utx.ATTIVO_0==2)
                    {
                        _SQL.Obj_YTSLOG_Save(_utx, "LOGIN", "2", "", _V);
                        string _base = "USEOK|" + _utx.USR_X3_0 + "|" + _utx.USR_TERM_0 + "|" + _utx.FCY_0 + "|" + _utx.DESCR_0;
                        string _abil = _utx.ABIL1_0 + "|" +  _utx.ABIL2_0 + "|" + _utx.ABIL3_0 + "|" + _utx.ABIL4_0 + "|" + _utx.ABIL5_0 + "|" + _utx.ABIL6_0 + "|" + _utx.ABIL7_0 + "|" + _utx.ABIL8_0 + "|" + _utx.ABIL9_0;
                        Obj_Cookie.Set_String("login-base", cls_Crypto.EncryptString(_base, Properties.Settings.Default.Passphrase));
                        Obj_Cookie.Set_String("login-abil", cls_Crypto.EncryptString(_abil, Properties.Settings.Default.Passphrase));
                        //
                        Response.Redirect("Menu.aspx");
                    }  
                    else
                    {
                        login_err.Text = "Utente non attivo";
                        _SQL.Obj_YTSLOG_Save(_utx, "LOGIN", "1", login_err.Text, _V);
                    }
                }
                else
                {
                    login_err.Text = "Utente non valido";
                    _SQL.Obj_YTSLOG_Save(_utx, "LOGIN", "1", login_err.Text, _V);
                }
            }
        }
    }
}