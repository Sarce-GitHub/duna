using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_WS_TOOLS_V9;
using X3_WS_TOOLS_V9.WSX3_C9;

namespace X3_TERMINALINI
{
    public partial class xTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(cls_Tools.SendMail(Properties.Settings.Default.MAIL_FROM, Properties.Settings.Default.MAIL_TO_DDT, "prova smtp terminalini", "prova bolla", false));
            //List<string> list = new List<string>();
            //list.Add("P241001");
            //list.Add("P241002");
            //list.Add("P241005");

            //string _err = "";
            //cls_TermWS.WS_BollaDaOrdine(cls_Tools.Get_User(), "004835", "IS000", "20240724", list, out _err);

            //Response.Write(_err);



            //CAdxParamKeyValue[] _k = new CAdxParamKeyValue[1];
            //_k[0] = new CAdxParamKeyValue();
            //_k[0].key = "SDHNUM";
            //_k[0].value = "BPM24EPSS100013";

            //string _err = "";
            //CAdxCallContext cc = cls_WSX3_V9.Get_CAdxCC("ITA", "COLLAUDO");
            //CAdxResultXml x = cls_WSX3_V9.RunWSObject("admin", "admin", "http://192.168.200.13:8124/soap-generic/syracuse/collaboration/syracuse/CAdxWebServiceXmlCC", cc, "YSDH", "", WS_Object_Type.Read, _k, out _err);




            //string c = "";

            //string connectionSQL = "Data Source=" + Properties.Settings.Default.SQL_IP + ";Initial Catalog=" + Properties.Settings.Default.SQL_Catalog + ";User ID=" + Properties.Settings.Default.SQL_User + ";Password=" + Properties.Settings.Default.SQL_Psw + ";";
            //using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            //{

            //    ITMMASTER _itm = db.ITMMASTER.Where(w => w.ITMREF_0.ToUpper() == "32H035-1200X2500X060").FirstOrDefault();
            //    Response.Write("ITM. " + _itm.ITMREF_0);
            //}


            //cls_SQL _SQL = new cls_SQL();
            //List<Obj_STOCK> X = _SQL.obj_STOCK_SearchByCode("DS100", "130010-CIST");
            //Response.Write("Nr. " + X.Count.ToString());
            //Response.Write("Err. " + _SQL.error);
            //Response.Write("Check_User: " + cls_Tools.Check_User().ToString() + "<br/>");
            //Response.Write("_base: " + Obj_Cookie.Get_String("login-base") + "<br/>");
            //Response.Write("_base - d: " + cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-base"), Properties.Settings.Default.Passphrase) + "<br/>");
            //string _base = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-base"), Properties.Settings.Default.Passphrase);

            //Response.Write("_abil: " + Obj_Cookie.Get_String("login-abil") + "<br/>");
            //Response.Write("_abil - d: " + cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-abil"), Properties.Settings.Default.Passphrase) + "<br/>");
            //string _abil = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-abil"), Properties.Settings.Default.Passphrase);

            ////_utx.USR_TERM_0 != "" && _utx.USR_X3_0 != "" && _utx.FCY_0 != "" && _utx.ATTIVO_0 == 2
            //foreach (var item in _base.Split('|'))
            //{
            //    Response.Write(item + "<br/>");
            //}

            //Response.Write("----------------<br/>");
            //Obj_YTSUTX _utx = new Obj_YTSUTX(_base.Split('|'), _abil.Split('|'));
            //Response.Write(_utx.USR_TERM_0 + "<br/>");
            //Response.Write(_utx.USR_X3_0 + "<br/>");
            //Response.Write(_utx.FCY_0 + "<br/>");
            //Response.Write(_utx.ATTIVO_0 + "<br/>");
            //Response.Write("----------------<br/>");
        }
    }
}