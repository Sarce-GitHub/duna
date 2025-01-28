using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _COOKIEORDINEATTUALE = "";
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
                ddl_SHIDAT_DA.Items.Clear();
                ddl_SHIDAT_DA.Enabled = false;
                ddl_SHIDAT_A.Items.Clear();
                ddl_SHIDAT_A.Enabled = false;                
            }
            Obj_Cookie.Set_String("prebolla-bc", "");
            Obj_Cookie.Set_String("prebolla-palnum", "");
            _COOKIEORDINEATTUALE = Obj_Cookie.Get_String("ordine-attuale");

            cookie_ordine_container.Visible = !string.IsNullOrEmpty(_COOKIEORDINEATTUALE);
            cookie_ordine.Text = _COOKIEORDINEATTUALE;

            div_ricerca_generica.Visible = !bool.Parse(Properties.Settings.Default.BarCode_Only);
        }

        protected void btn_RicercaBC_Click(object sender, EventArgs e)
        {
            pan_dati.Controls.Clear();

            string[] ricerca = txt_RicercaBC.Text.Trim().ToUpper().Split(Properties.Settings.Default.Etic_Split.ToCharArray());
            string nOrdine = ricerca[0];
            string pallet = ricerca.Length == 2 ? ricerca[1] : "";

            bool isOrdine = _SQL.Obj_YTSORDAPE_Ordine_Singolo_Any(_USR.FCY_0, nOrdine, false);
            if (isOrdine)
            {
                if (!string.IsNullOrEmpty(_COOKIEORDINEATTUALE) && _COOKIEORDINEATTUALE != nOrdine)
                {
                    btn_Conferma.Visible = false;
                    txt_RicercaBC.Text = "";
                    frm_error.Text = "Attenzione, sparato un ordine diverso da quello selezionato in precedenza";
                    return;
                }

                string bc = "";
                _SQL.Obj_YTSORDAPE_Ordine_Singolo_BC(_USR.FCY_0, nOrdine, false, out bc);

                if(!string.IsNullOrEmpty(pallet)) 
                {
                    bc += (Properties.Settings.Default.Etic_Split + pallet);
                }

                Response.Redirect("Ordine_Righe_Ordine_Singolo.aspx?BC=" + bc, true);
                return;
            }


            HtmlGenericControl _d = new HtmlGenericControl();
            string[] Arr = txt_RicercaBC.Text.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4 && Arr.Length != 5)
            { 
                _d.InnerHtml = "<b>Barcode non formattato correttamente</b>";
                txt_RicercaBC.Text = "";
                pan_dati.Controls.Add(_d);
                return;
            }

            //
            DateTime _dt_da = DateTime.MinValue;
            DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2),  out _dt_da);

            DateTime _dt_a = DateTime.MinValue;
            DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _dt_a);
            if (_SQL.Obj_YTSORDAPE_Any(_USR.FCY_0, Arr[0], Arr[1], _dt_da.Date, _dt_a.Date, false))
            {
                Response.Redirect("Ordine_Righe.aspx?BC=" + txt_RicercaBC.Text.Trim().ToUpper(), true);
                return;
            }
                
            _d.InnerHtml = "<b>Nessun record trovato per:<br/>Cliente: "+ Arr[0] + "<br/>Indirizzo: "+ Arr[1] + "<br/>Data: " + _dt_da.ToString("dd/MM/yyyy") + "/ " + _dt_a.ToString("dd/MM/yyyy") + "</b>";
            txt_RicercaBC.Text = "";
            pan_dati.Controls.Add(_d);
            return;

        }
        
        protected void ddl_BPAADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_SHIDAT_DA.Items.Clear();
            ddl_SHIDAT_DA.Enabled = false;
            ddl_SHIDAT_A.Items.Clear();
            ddl_SHIDAT_A.Enabled = false;
            //
            if (ddl_BPAADD.SelectedIndex > 0)
            {
                ddl_SHIDAT_DA.Items.Add(new ListItem("* Data Da", ""));
                ddl_SHIDAT_A.Items.Add(new ListItem("* Data A", ""));
                foreach (var item in _SQL.Obj_YTSORDAPE_Date(_USR.FCY_0, txtAutoCodiceCliente.Text, ddl_BPAADD.SelectedValue, false))
                {
                    ddl_SHIDAT_DA.Items.Add(new ListItem(item.SHIDAT_0.ToString("dd/MM/yyyy"), item.SHIDAT_0.ToString("yyyyMMdd")));
                    ddl_SHIDAT_A.Items.Add(new ListItem(item.SHIDAT_0.ToString("dd/MM/yyyy"), item.SHIDAT_0.ToString("yyyyMMdd")));
                }
                ddl_SHIDAT_DA.Enabled = true;
                ddl_SHIDAT_A.Enabled = true;
                ddl_SHIDAT_DA.Focus();
            }
        }

        protected void btn_Conferma_Click(object sender, EventArgs e)
        {
            if (ddl_SHIDAT_DA.SelectedIndex > 0 && ddl_SHIDAT_A.SelectedIndex > 0)
            {
                Response.Redirect("Ordine_Righe.aspx?BC=" + txtAutoCodiceCliente.Text + "|" + ddl_BPAADD.SelectedValue + "|" + ddl_SHIDAT_DA.SelectedValue + "|" + ddl_SHIDAT_A.SelectedValue, true);
            }
        }

        protected void txtAutoCodiceCliente_TextChanged(object sender, EventArgs e)
        {
            ddl_SHIDAT_DA.Items.Clear();
            ddl_SHIDAT_DA.Enabled = false;
            ddl_SHIDAT_A.Items.Clear();
            ddl_SHIDAT_A.Enabled = false;
            ddl_BPAADD.Items.Clear();
            ddl_BPAADD.Enabled = false;
            //
            
                ddl_BPAADD.Items.Add(new ListItem("* Indirizzo", ""));
                foreach (var item in _SQL.Obj_YTSORDAPE_Indirizzi(_USR.FCY_0, txtAutoCodiceCliente.Text, false))
                {
                    ddl_BPAADD.Items.Add(new ListItem(item.BPAADD_0 + " - " + item.BPAADDDES_0, item.BPAADD_0));
                }
                ddl_BPAADD.Enabled = true;
                ddl_BPAADD.Focus();
            
        }

        protected void ddl_SHIDAT_DA_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime selectedDate;
            if (DateTime.TryParseExact(ddl_SHIDAT_DA.SelectedValue, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out selectedDate))
            {
                ddl_SHIDAT_A.Items.Clear();
                ddl_SHIDAT_A.Items.Add(new ListItem("* Data A", ""));
                foreach (ListItem item in ddl_SHIDAT_DA.Items)
                {
                    DateTime itemDate;
                    if (DateTime.TryParseExact(item.Value, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out itemDate) && itemDate >= selectedDate)
                    {
                        ddl_SHIDAT_A.Items.Add(new ListItem(item.Text, item.Value));
                    }
                }
            }
        }

        protected void reset_Cookie_Click(object sender, EventArgs e)
        {
            Obj_Cookie.Set_String("ordine-attuale", "");
            cookie_ordine_container.Visible = false;
            frm_error.Text = "";
        }

    }
}