using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Ridistribuzione : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC();
        Obj_YTSORDAPE _Art_Ordine = new Obj_YTSORDAPE();
        Obj_STOCK _Stock = new Obj_STOCK();
        //
        string _ITEMREF = "";
        string _BPCORD = "";
        string _BPAADD = "";
        string _QTAMAG = "";
        string _LOT = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;
        //
        DateTime _DATE_COM = DateTime.MinValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/Menu.aspx", true);

            string[] Arr = Obj_Cookie.Get_String("prebolla-bc").ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            _ITEMREF = Request.QueryString["ITEMREF"];
            _QTAMAG = Request.QueryString["QTAMAG"];
            _LOT = Request.QueryString["LOT"]; 
            _BPCORD = Arr[0];
            _BPAADD = Arr[1];
            //DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2), out _DATE_DA);
            DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_DA);
            _DATE_DA = _DATE_DA.AddDays(1);
            _DATE_A = DateTime.MaxValue;

            var listOrdine = _SQL.Obj_YTSORDAPE_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A).Where(w => w.ITMREF_0 == _ITEMREF).ToList();

        }

        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Righe_Dett.aspx?LOT=" + _LOT);
        }
    }
}