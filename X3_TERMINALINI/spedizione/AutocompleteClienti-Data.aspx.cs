using System;
using System.Collections;
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
    public partial class AutoCompleteClienti_Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string term = "";
            bool sped = false;
            if (Request.QueryString["Term"] != null) term = Request.QueryString["Term"].ToString();
            sped = Request.QueryString["SPED"] != null;
            var _USR = cls_Tools.Get_User();
            string Out_Json = "";
            cls_SQL _SQL = new cls_SQL();
            var lista = _SQL.Obj_YTSORDAPE_Clienti(_USR.FCY_0, sped);
            Out_Json = "[";
            foreach (Obj_YTSORDAPE item in lista.Where(i => i.BPCNAM_0.Contains(term.ToUpper()) || i.BPCORD_0.Contains(term.ToUpper())))
            {               
                Out_Json += "{\"value\":\"" + Rep(item.BPCORD_0) + "\",\"label\":\"" + Rep(item.BPCNAM_0) + "\"},";
                
            }
            if (Out_Json != "") Out_Json = Out_Json.Substring(0, Out_Json.Length - 1);
            if (Out_Json != "") Out_Json = Out_Json + "]";
            Response.Clear();
            Response.Buffer = false;

            Response.Write(Out_Json);
        }

        private string Rep(string In_Value)
        {
            string x = In_Value;
            x = x.Replace("\"", "");
            return x;
        }
    }
}