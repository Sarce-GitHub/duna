using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Spedizione_Disimpegno : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/", true);
            if (!Page.IsPostBack)
            {
                txt_RicercaBC.Focus();
                ddl_BPAADD.Items.Clear();
                ddl_BPAADD.Enabled = false;
                ddl_DATA_DA.Items.Clear();
                ddl_DATA_DA.Enabled = false;
                ddl_DATA_A.Items.Clear();
                ddl_DATA_A.Enabled = false;                
            }
            Obj_Cookie.Set_String("prebolla-bc", "");
            Obj_Cookie.Set_String("prebolla-palnum", "");
        }

        protected void btn_RicercaBC_Click(object sender, EventArgs e)
        {
            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();
            string[] Arr = txt_RicercaBC.Text.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4) 
            {
                _d.InnerHtml = "<b>Barcode non formattato correttamente</b>";
                txt_RicercaBC.Text = "";
                pan_dati.Controls.Add(_d);
                return;
            }

            //
            DateTime _dt_da = DateTime.MinValue;
            DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2), out _dt_da);

            DateTime _dt_a = DateTime.MinValue;
            DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _dt_a);

            if (_SQL.Obj_YTSORDAPE_Any(_USR.FCY_0, Arr[0], Arr[1], _dt_da.Date, _dt_a.Date, true))
            {
                Response.Redirect("Ordine_Spedizione_Disimpegno_Righe.aspx?BC=" + txt_RicercaBC.Text.Trim().ToUpper(), true);
                return;
            }
            else
            {
                _d.InnerHtml = "<b>Nessun record trovato per:<br/>Cliente: " + Arr[0] + "<br/>Indirizzo: " + Arr[1] + "<br/>Data: " + _dt_da.ToString("dd/MM/yyyy") + "/" + _dt_a.ToString("dd/MM/yyyy") + "</b>";
                txt_RicercaBC.Text = "";
                pan_dati.Controls.Add(_d);
                return;
            }

        }
      
        protected void ddl_BPAADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_DATA_DA.Items.Clear();
            ddl_DATA_DA.Enabled = false;
            ddl_DATA_A.Items.Clear();
            ddl_DATA_A.Enabled = false;
            //
            if (ddl_BPAADD.SelectedIndex > 0)
            {
                ddl_DATA_DA.Items.Add(new ListItem("* Data Consegna", ""));
                ddl_DATA_A.Items.Add(new ListItem("* Data Consegna", ""));
                foreach (var item in _SQL.Obj_YTSORDAPE_Date(_USR.FCY_0, txtAutoCodiceClienteSped.Text, ddl_BPAADD.SelectedValue, true))
                {
                    ddl_DATA_DA.Items.Add(new ListItem(item.SHIDAT_0.ToString("dd/MM/yyyy"), item.SHIDAT_0.ToString("yyyyMMdd")));
                    ddl_DATA_A.Items.Add(new ListItem(item.SHIDAT_0.ToString("dd/MM/yyyy"), item.SHIDAT_0.ToString("yyyyMMdd")));
                }
                ddl_DATA_DA.Enabled = true;
                ddl_DATA_DA.Focus();
                ddl_DATA_A.Enabled = true;
                ddl_DATA_A.Focus();
            }
        }

        protected void btn_Conferma_Click(object sender, EventArgs e)
        {
            if (ddl_DATA_DA.SelectedIndex > 0  && ddl_DATA_A.SelectedIndex > 0)
            {
                Response.Redirect("Ordine_Spedizione_Disimpegno_Righe.aspx?BC=" + txtAutoCodiceClienteSped.Text + "|" + ddl_BPAADD.SelectedValue + "|" + ddl_DATA_DA.SelectedValue + "|" + ddl_DATA_A.SelectedValue, true);
            }
        }

        protected void txtAutoCodiceClienteSped_TextChanged(object sender, EventArgs e)
        {
            ddl_DATA_DA.Items.Clear();
            ddl_DATA_DA.Enabled = false;
            ddl_DATA_A.Items.Clear();
            ddl_DATA_A.Enabled = false;
            ddl_BPAADD.Items.Clear();
            ddl_BPAADD.Enabled = false;
            //
            
                ddl_BPAADD.Items.Add(new ListItem("* Indirizzo", ""));
                foreach (var item in _SQL.Obj_YTSORDAPE_Indirizzi(_USR.FCY_0, txtAutoCodiceClienteSped.Text, true))
                {
                    ddl_BPAADD.Items.Add(new ListItem(item.BPAADD_0 + " - " + item.BPAADDDES_0, item.BPAADD_0));
                }
                ddl_BPAADD.Enabled = true;
                ddl_BPAADD.Focus();
            
        }
        protected void ddl_DATA_DA_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime selectedDate;
            if (DateTime.TryParseExact(ddl_DATA_DA.SelectedValue, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out selectedDate))
            {
                ddl_DATA_A.Items.Clear();
                ddl_DATA_A.Items.Add(new ListItem("* Data Consegna", ""));
                foreach (ListItem item in ddl_DATA_DA.Items)
                {
                    DateTime itemDate;
                    if (DateTime.TryParseExact(item.Value, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out itemDate) && itemDate >= selectedDate)
                    {
                        ddl_DATA_A.Items.Add(new ListItem(item.Text, item.Value));
                    }
                }
            }
        }

    }
}