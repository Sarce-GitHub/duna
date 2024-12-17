using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace X3_TERMINALINI.ricevimento
{
    public partial class Ricevimento : System.Web.UI.Page
    {
        // Variabili generali
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();

        /// <summary>
        /// Caricamento della pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Controllo Utente
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL1_0 != 2) Response.Redirect("/", true);
            // Reset etichette ok/Errore
            frm_OK.Text = "";
            frm_error.Text = "";
            // Se non Post pack
            if (!Page.IsPostBack)
            {
                // Reset Form
                ResetForm();
                // Ricalcolo Elenco
                Elenco(); 
                txt_Ricerca.Focus();
            }
        }

        /// <summary>
        /// Reset variabili e campi Form
        /// </summary>
        private void ResetForm()
        {
            div_Ric.Visible = true;
            div_Det.Visible = false;
            //
            txt_Quantita.Text = "";
            txt_UM.Text = "";
            txt_Destinazione.Text = "";
            lbl_Articolo.Text = "";
            lbl_Stato.Text = "";
            txt_Ricerca.Text = "";
            //
            HF_Etic.Value = "";
            HF_Tipo.Value = "";
            HF_Qta.Value = "";
            HF_Ubi_P.Value = "";
        }

        /// <summary>
        /// Funzione tasto Indietro differenziato per pannello
        /// </summary>
        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            if (div_Ric.Visible)
                Response.Redirect("/Menu.aspx?RIC", true);
            else
                Response.Redirect("/Ricevimento/Ricevimento.aspx", true);
        }
       
        /// <summary>
        /// Funzione di caricamento elenco dati in ricezione
        /// </summary>
        private void Elenco()
        {
            // Valiebili Locali
            string _last = "";
            int i = 0;
            // Estrazuibe Lista per Tipo Ubicazione
            List<Obj_STOCK> List = _SQL.obj_STOCK_TipoUbicazione(_USR.FCY_0, Properties.Settings.Default.RIC_TipoUbic);
            HtmlGenericControl _div = new HtmlGenericControl("div");
            // VErifica di almeno un elemento
            if (List.Count > 0)
            {
                // Ciclo per elementi da sistemare
                foreach (Obj_STOCK s in List.OrderBy(o => o.LOC_0).ThenBy(o => o.ITMREF_0).ThenBy(o => o.LOT_0).ThenBy(o => o.SLO_0))
                {
                    string h = "";
                    // Barra divisoria per Ubicazione
                    if (_last != s.LOC_0)
                    {
                        i = 0;
                        _last = s.LOC_0;
                        h = h + "<div class=\"row bg-head\">";
                        h = h + "<div class=\"col-12\"><b>" + s.LOC_0 + "</b></div>";
                        h = h + "</div>";
                    }
                    // Dati Esposti
                    h = h + "<div class=\"row font-small " + ((i % 2) == 1 ? "bg-alt" : "") + "\">";
                    //
                    h = h + "<div class=\"col-3 col-md-2\"><b>" + s.ITMREF_0 + "</b></div>";
                    h = h + "<div class=\"col-3 col-md-2\">" + (s.LOT_0 + " " + s.SLO_0).Trim() + "</div>";
                    h = h + "<div class=\"col-3 col-md-2\"><b>" + s.PALNUM_0.Trim() + "</b></div>";
                    h = h + "<div class=\"col-3 col-md-2 text-end\">" + s.QTYPCU_0.ToString("0.###") + " " + s.STU_0 + " (" + s.STA_0 + ")</div>";
                    h = h + "<div class=\"col-12 col-md-6\"><i>" + s.ITMDES_0 + "</i></div>";
                    //
                    h = h + "</div>";
                    i++;
                    _div.InnerHtml = _div.InnerHtml + h;
                }
            }
            else
            {
                // Nessun record
                _div.InnerHtml = "<div class=\"col-12\"><b>Nessuno materiale trovato in "+ Properties.Settings.Default.RIC_TipoUbic + "</b></div>";
            }
            
            //
            pan_data.Controls.Add(_div);
        }

        /// <summary>
        /// Modifica Campo Ricerca - Etichetta
        /// </summary>
        protected void txt_Ricerca_TextChanged(object sender, EventArgs e)
        {
            // Estrazione dati Etichetta
            Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Ricerca.Text.Trim().ToUpper());
            // Stock
            Obj_STOCK _STK = new Obj_STOCK();
            // Estrazione dati per Articolo
            _SQL.obj_STOCK_Load_TYP(_USR.FCY_0, Properties.Settings.Default.RIC_TipoUbic, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", out _STK);
            // SE STOCK
            if (_STK.LOCTYP_0 == Properties.Settings.Default.RIC_TipoUbic)
            {
                HF_Tipo.Value = "I";
            }
            else
            {
                // NO Articolo - Estrazione dati per Pallet
                if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, txt_Ricerca.Text.Trim().ToUpper(), out _STK))
                {
                    if (_STK.LOCTYP_0 == Properties.Settings.Default.RIC_TipoUbic)
                    {
                        HF_Tipo.Value = "P";
                    }
                }
            }

            // Se Tipo Impostato - OK
            txt_Quantita.Enabled = true;
            if (HF_Tipo.Value!="")
            {
                div_Ric.Visible = false;
                div_Det.Visible = true;
                lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + "</b><br/>" + _STK.ITMDES_0;
                txt_Quantita.Text = _STK.QTYPCU_0.ToString("0.###");
                if (HF_Tipo.Value == "P") txt_Quantita.Enabled = false;
                HF_Qta.Value = _STK.QTYPCU_0.ToString("0.###");
                txt_UM.Text = _STK.PCU_0;
                lbl_Stato.Text = _STK.STA_0;
                HF_Ubi_P.Value = _STK.LOC_0;
                HF_Etic.Value = txt_Ricerca.Text.Trim().ToUpper();
                txt_Destinazione.Focus();
            }
            else
            {
                // Etichetta non trovata ne come articlo ne come pallet
                frm_error.Text = "Etichetta "+ txt_Ricerca.Text + " non trovato in " + Properties.Settings.Default.RIC_TipoUbic;
                if (_SQL.error!="") frm_error.Text = frm_error.Text + " - " + _SQL.error;
                txt_Ricerca.Text = "";
                Elenco();
                txt_Ricerca.Focus();
            }
        }

        /// <summary>
        /// Controllo Quantità
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txt_Quantita_TextChanged(object sender, EventArgs e)
        {
            decimal _q = 0;
            // NUMERICO
            if (!decimal.TryParse(txt_Quantita.Text, out _q))
            {
                frm_error.Text = "Quantità non numerica";
                txt_Quantita.Text = "";
                txt_Quantita.Text = decimal.Parse(HF_Qta.Value).ToString("0.###");
                txt_Quantita.Focus();
                return;
            }
            // TRA 0 e MASSIMO
            if (_q<=0 || _q> decimal.Parse(HF_Qta.Value))
            {
                frm_error.Text = "Quantità non corretta";
                txt_Quantita.Text = "";
                txt_Quantita.Text = decimal.Parse(HF_Qta.Value).ToString("0.###");
                txt_Quantita.Focus();
                return;
            }

            txt_Destinazione.Focus();

        }

        /// <summary>
        /// Azione dopo inserimento Ubicazione
        /// </summary>
        protected void txt_Destinazione_TextChanged(object sender, EventArgs e)
        {
            // Se non Vuoto
            if (txt_Destinazione.Text.Trim() != "")
            {
                //CHECK UBICAZIONE
                if (_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                {
                    //UBICAZIONE VERIFICATA
                    txt_Destinazione.Text = txt_Destinazione.Text.Trim().ToUpper();
                }
                else
                {
                    // Ubicazione non trovata
                    frm_error.Text = "Ubicazione "+ txt_Destinazione.Text + " non trovata";
                    txt_Destinazione.Text = "";
                    txt_Destinazione.Focus();
                    return;
                }

                //
                string _err = "";
                // Estrazione Etichetta
                Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC();    
                string _PALNUM = "";
                if (HF_Tipo.Value == "I")
                {
                    // CAMBIO STOCK ITMREF/LOT/SLO
                    _Etic = new Obj_STOCK_ETIC(HF_Etic.Value);
                }
                else
                {
                    // CAMBIO STOCK PALNUM
                    Obj_STOCK _STK = new Obj_STOCK();
                    if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, HF_Etic.Value, out _STK))
                    {
                        _Etic.ITMREF = _STK.ITMREF_0.Trim();
                        _Etic.LOT = _STK.LOT_0.Trim();
                        _Etic.SLO=_STK.SLO_0.Trim();
                        _PALNUM = HF_Etic.Value;
                    }

                }
                // Esecuzione Cambio stock
                if (!cls_TermWS.WS_CambioStock(_USR, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, txt_UM.Text.Trim(), decimal.Parse(txt_Quantita.Text),lbl_Stato.Text, _PALNUM, HF_Ubi_P.Value, lbl_Stato.Text, _PALNUM, txt_Destinazione.Text, out _err))
                {
                    //Errore
                    frm_error.Text = _err;
                    txt_Destinazione.Focus();
                }
                else
                {
                    // OK
                    frm_OK.Text = "Spostamento Confermato";
                    ResetForm();
                    Elenco();
                    txt_Ricerca.Focus();
                }
            }
        }
    }
}