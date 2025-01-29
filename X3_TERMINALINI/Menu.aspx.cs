using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using X3_TERMINALINI._include;

namespace X3_TERMINALINI
{
    public partial class Menu : System.Web.UI.Page
    {
        public string _foot = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            Obj_YTSUTX _u = cls_Tools.Get_User();
            //
            nav_general.Visible = (Request.QueryString.ToString() == "");
            nav_MenuGen.Visible = (Request.QueryString.ToString() != "");
            // PULSANTI GENERALI
            div_btt_gen_Stock.Visible = (_u.ABIL1_0 == 2 && Request.QueryString.ToString() == "");
            div_btt_gen_Ricevimento.Visible = (_u.ABIL2_0 == 2 && Request.QueryString.ToString() == "");
            div_btt_gen_Spedizione.Visible = (_u.ABIL3_0 == 2 && Request.QueryString.ToString() == "");
            div_btt_gen_Produzione.Visible = ((_u.ABIL4_0 == 2 || _u.ABIL5_0 == 2 || _u.ABIL6_0 == 2) && Request.QueryString.ToString() == "");
            
            // PANNELLI MENU
            nav_stock.Visible = (_u.ABIL1_0 == 2 && Request.QueryString.ToString() == "STK");
            nav_ricevimento.Visible = (_u.ABIL2_0 == 2&& Request.QueryString.ToString() == "RIC");
            nav_spedizione.Visible = (_u.ABIL3_0 == 2 && Request.QueryString.ToString() == "SPED");
            nav_produzione.Visible = ((_u.ABIL4_0 == 2 || _u.ABIL5_0 == 2) && Request.QueryString.ToString() == "PROD");

            // PULSANTI PARTICOLARI
            nav_btn_GS_entratadiversa.Visible = Properties.Settings.Default.Abil_MAG_EntrDiv;
            div_btn_SP_BollaPreparazione.Visible = Properties.Settings.Default.SPED_BollaPrep;
            div_btn_SP_Ordine.Visible = !Properties.Settings.Default.SPED_BollaPrep;
            div_btn_SP_Ordine_Spedizione.Visible = !Properties.Settings.Default.SPED_BollaPrep;
            div_btn_SP_CarPresped.Visible = !string.IsNullOrEmpty(Properties.Settings.Default.PRESPED_Ubic);
            div_btn_SP_PrepPallet.Visible = !string.IsNullOrEmpty(Properties.Settings.Default.PRESPED_Ubic);
            //div_btn_Consumo_Materiali.Visible = (_u.ABIL5_0 == 2 && Request.QueryString.ToString() == "PROD");
            div_btn_Avanzamento_Completo.Visible = (_u.ABIL5_0 == 2 && Request.QueryString.ToString() == "PROD");
            div_btn_GS_UbicazionePallet.Visible = (_u.ABIL1_0 == 2 && bool.Parse(Properties.Settings.Default.Manage_MAG_PALLET));
            nav_Ass_Diss_Pallet.Visible = (_u.ABIL1_0 == 2 && bool.Parse(Properties.Settings.Default.Manage_MAG_PALLET));
            div_navetta.Visible = _u.ABIL1_0 == 2 && bool.Parse(Properties.Settings.Default.Manage_NAV);
            div_Presped.Visible = _u.ABIL3_0 == 2 && bool.Parse(Properties.Settings.Default.Manage_PRESPED);
            div_btn_SP_Disimpegno.Visible = _u.ABIL3_0 == 2 && bool.Parse(Properties.Settings.Default.Manage_DISIMPEGNO);
            // Etichetta Foot
            _foot = "Utente: " + _u.USR_TERM_0 + " (" + _u.FCY_0 + ")&nbsp;&nbsp;-&nbsp;&nbsp;" + cls_Tools.Get_Vers();
        }

        /// <summary>
        /// Disconnessione dal sistema - Reset Cookies d'accesso
        /// </summary>
        protected void btn_Disconnetti_Click(object sender, EventArgs e)
        {
            // Reset coockies d'accesso
            Obj_Cookie.Set_String("login-base", "", 0);
            Obj_Cookie.Set_String("login-abil", "", 0);
            //
            Response.Redirect("/");
        }
    }
}