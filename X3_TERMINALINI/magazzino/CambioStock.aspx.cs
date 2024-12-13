using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_WS_TOOLS_V9;
using X3_WS_TOOLS_V9.WSX3_C9;

namespace X3_TERMINALINI.magazzino
{
    public partial class CambioStock : System.Web.UI.Page
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
            //
            if (!IsPostBack) txt_Partenza.Focus();
        }

        /// <summary>
        /// Indicazione Ubicazione/Pallet di partenza
        /// </summary>
        protected void txt_Partenza_TextChanged(object sender, EventArgs e)
        {
            HF_Tipo_P.Value = "";

            txt_Etichetta.Text = "";
            lbl_Articolo.Text = "";
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();
            txt_Destinazione.Text = "";

            if (txt_Partenza.Text.Trim()!="")
            {
                //CHECK UBICAZIONE
                if (_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                {
                    if (_SQL.obj_STOLOC_Sped_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                    {
                        frm_error.Text = "Ubicazione " + txt_Partenza.Text.Trim().ToUpper() + " di tipo SPED non autorizzata ";
                        txt_Partenza.Text = "";
                        txt_Partenza.Focus();
                        return;
                    }

                    //UBICAZIONE VERIFICATA
                    txt_Partenza.Text = txt_Partenza.Text.Trim().ToUpper();
                    HF_Tipo_P.Value = "U";
                }
                else
                {

                    //UBICAZIONE INESISTENTE, PROVO IL PALNUM
                    if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                    {
                        if(_SQL.obj_PALNUM_SPED_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                        {
                            frm_error.Text = "Palnum " + txt_Partenza.Text.Trim().ToUpper() + " su ubicazione di tipo SPED non autorizzata ";
                            txt_Partenza.Text = "";
                            txt_Partenza.Focus();
                            return;
                        }

                        if (_SQL.obj_PALNUM_NAV_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                        {
                            frm_error.Text = "Palnum " + txt_Partenza.Text.Trim().ToUpper() + " su ubicazione di tipo NAV non autorizzata ";
                            txt_Partenza.Text = "";
                            txt_Partenza.Focus();
                            return;
                        }


                        //PALLET VERIFICATA
                        txt_Partenza.Text = txt_Partenza.Text.Trim().ToUpper();
                        HF_Tipo_P.Value = "P";
                    }
                }

                // CHECK
                if (HF_Tipo_P.Value == "")
                {
                    frm_error.Text = "Ubicazione/Pallet non trovato";
                    txt_Partenza.Text = "";
                    txt_Partenza.Focus();
                }
                else
                {
                    txt_Etichetta.Focus();
                }
            }
            else 
            {
                txt_Partenza.Focus();
            }
        }

        /// <summary>
        /// Indicazione Etichetta 
        /// </summary>
        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            lbl_Articolo.Text = "";
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();
            txt_Destinazione.Text = "";
            txt_Etichetta.Focus();

            //LF01377AAB-1234
            if (txt_Etichetta.Text.Trim() != "")
            {

                // Estrazione dati Etichetta
                Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Etichetta.Text.Trim().ToUpper());
                string _PALNUM = "";
                string _LOC = txt_Partenza.Text;
                HF_ITMREF.Value = "";
                //
                Obj_STOCK _STK = new Obj_STOCK();
                if (HF_Tipo_P.Value == "P")
                {
                    _PALNUM = txt_Partenza.Text.Trim();
                    if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PALNUM, _Etic.ITMREF, _Etic.LOT,  out _STK))
                    {
                        _Etic.ITMREF = _STK.ITMREF_0;
                        _Etic.LOT = _STK.LOT_0;
                        _Etic.SLO = _STK.SLO_0;
                        _LOC = _STK.LOC_0;
                    }
                }
                //
                bool qtyFound = false;
                _STK = new Obj_STOCK();
                _SQL.obj_STOCK_Load(_USR.FCY_0, _LOC, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, _PALNUM, "A", out _STK);
                //
                if (_STK.LOC_0== _LOC)
                {
                    string seakey = !string.IsNullOrEmpty(_STK.SEAKEY_0) && _STK.SEAKEY_0 != " " ? " - " + _STK.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                    string lot = !string.IsNullOrEmpty(_STK.LOT_0) && _STK.LOT_0 != " " ? " " + " " + Properties.Settings.Default.Etic_Split + " " + " " + _STK.LOT_0 : "";
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + lot + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
                    lbl_Stato.Text = _STK.STA_0;
                    lbl_UM.Text = _STK.PCU_0;
                    //txt_Quantita.Text = _STK.QTYPCU_0.ToString("0.##");
                    txt_Quantita_Disp.Text = _STK.QTYPCU_0.ToString("0.##");
                    HF_QTY_A.Value = _STK.QTYPCU_0.ToString("0.##");
                    qtyFound = true;
                }

                _STK = new Obj_STOCK();
                _SQL.obj_STOCK_Load(_USR.FCY_0, _LOC, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, _PALNUM, "Q", out _STK);
                //
                if (_STK.LOC_0 == _LOC)
                {
                    string seakey = !string.IsNullOrEmpty(_STK.SEAKEY_0) && _STK.SEAKEY_0 != " " ? " - " + _STK.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                    string lot = !string.IsNullOrEmpty(_STK.LOT_0) && _STK.LOT_0 != " " ? " " + " " + Properties.Settings.Default.Etic_Split + " " + " " + _STK.LOT_0 : "";
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + lot + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
                    lbl_Stato_Q.Text = _STK.STA_0;
                    lbl_UM_Q.Text = _STK.PCU_0;
                    //txt_Quantita.Text = _STK.QTYPCU_0.ToString("0.##");
                    txt_Quantita_Disp_Q.Text = _STK.QTYPCU_0.ToString("0.##");
                    HF_QTY_Q.Value = _STK.QTYPCU_0.ToString("0.##");
                    qtyFound = true;
                }

                _STK = new Obj_STOCK();
                _SQL.obj_STOCK_Load(_USR.FCY_0, _LOC, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, _PALNUM, "R", out _STK);
                //
                if (_STK.LOC_0 == _LOC)
                {
                    string seakey = !string.IsNullOrEmpty(_STK.SEAKEY_0) && _STK.SEAKEY_0 != " " ? " - " + _STK.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                    string lot = !string.IsNullOrEmpty(_STK.LOT_0) && _STK.LOT_0 != " " ? " " + " " + Properties.Settings.Default.Etic_Split + " " + " " + _STK.LOT_0 : "";
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + lot + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
                    lbl_Stato_R.Text = _STK.STA_0;
                    lbl_UM_R.Text = _STK.PCU_0;
                    //txt_Quantita.Text = _STK.QTYPCU_0.ToString("0.##");
                    txt_Quantita_Disp_R.Text = _STK.QTYPCU_0.ToString("0.##");
                    HF_QTY_R.Value = _STK.QTYPCU_0.ToString("0.##");
                    qtyFound = true;
                }

                txt_Quantita.Focus();

                if(!qtyFound)
                {
                    frm_error.Text = "Articolo non trovato. " + _SQL.error;
                    txt_Etichetta.Text = "";
                    txt_Etichetta.Focus();
                }
            }
            else
            {
                frm_error.Text = "Inserire un Articolo";
            }
        }

        protected void txt_Quantita_TextChanged(object sender, EventArgs e)
        {
            if (txt_Quantita.Text.Trim() != "")
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_Quantita.Text, out _d))
                {
                    if (_d>0 && _d<= decimal.Parse(HF_QTY_A.Value))
                    {
                        txt_Quantita.Text = _d.ToString("0.##");
                        txt_Quantita_Q.Enabled = false;
                        txt_Quantita_R.Enabled = false;
                        txt_Destinazione.Focus();
                    }
                    else
                    {
                        frm_error.Text = "Quantità non congrua";
                        txt_Quantita.Text = "";
                        txt_Quantita.Focus();
                    }
                }
                else
                {
                    frm_error.Text = "Quantità non valida";
                    txt_Quantita.Text = "";
                    txt_Quantita.Focus();
                }
            }
            else
            {
                ResetCampiQuantita();
            }
        }

        private void ResetCampiQuantita()
        {
            txt_Quantita.Enabled = true;
            txt_Quantita_Q.Enabled = true;
            txt_Quantita_R.Enabled = true;

            txt_Quantita.Focus();
        }

        private void ResetCampiQuantitaDerivati()
        {
            txt_Quantita_Disp.Text = "";
            txt_Quantita_Disp_Q.Text = "";
            txt_Quantita_Disp_R.Text = "";
            lbl_UM.Text = "";
            lbl_UM_Q.Text = "";
            lbl_UM_R.Text = "";
            lbl_Stato.Text = "";
            lbl_Stato_Q.Text = "";
            lbl_Stato_R.Text = "";
        }

        private void EmptyCampiQuantita()
        {
            txt_Quantita.Text = "";
            txt_Quantita_Q.Text = "";
            txt_Quantita_R.Text = "";
        }



        protected void txt_Quantita_Q_TextChanged(object sender, EventArgs e)
        {
            if (txt_Quantita_Q.Text.Trim() != "")
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_Quantita_Q.Text, out _d))
                {
                    if (_d > 0 && _d <= decimal.Parse(HF_QTY_Q.Value))
                    {
                        txt_Quantita_Q.Text = _d.ToString("0.##");
                        txt_Quantita.Enabled = false;
                        txt_Quantita_R.Enabled = false;
                        txt_Destinazione.Focus();
                    }
                    else
                    {
                        frm_error.Text = "Quantità non congrua";
                        txt_Quantita_Q.Text = "";
                        txt_Quantita_Q.Focus();
                    }
                }
                else
                {
                    frm_error.Text = "Quantità non valida";
                    txt_Quantita_Q.Text = "";
                    txt_Quantita_Q.Focus();
                }
            }
            else
            {
                ResetCampiQuantita();
            }
        }

        protected void txt_Quantita_R_TextChanged(object sender, EventArgs e)
        {
            if (txt_Quantita_R.Text.Trim() != "")
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_Quantita_R.Text, out _d))
                {
                    if (_d > 0 && _d <= decimal.Parse(HF_QTY_R.Value))
                    {
                        txt_Quantita_R.Text = _d.ToString("0.##");
                        txt_Quantita.Enabled = false;
                        txt_Quantita_Q.Enabled = false;
                        txt_Destinazione.Focus();
                    }
                    else
                    {
                        frm_error.Text = "Quantità non congrua";
                        txt_Quantita_R.Text = "";
                        txt_Quantita_R.Focus();
                    }
                }
                else
                {
                    frm_error.Text = "Quantità non valida";
                    txt_Quantita_R.Text = "";
                    txt_Quantita_R.Focus();
                }
            }
            else
            {
                ResetCampiQuantita();
            }
        }



        protected void txt_Destinazione_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_Quantita.Text) && string.IsNullOrEmpty(txt_Quantita_Q.Text) && string.IsNullOrEmpty(txt_Quantita_R.Text))
            {
                frm_error.Text = "Inserire una Quantità";
                txt_Destinazione.Text = "";
                txt_Quantita.Focus();
                return;
            }

            HF_Tipo_D.Value = "";
            if (txt_Destinazione.Text.Trim() != "")
            {
                if (txt_Destinazione.Text == txt_Partenza.Text)
                {
                    frm_error.Text = "Stessa Ubicazione non Autorizzata";
                    txt_Destinazione.Text = "";
                    txt_Destinazione.Focus();
                    return;
                }

                //CHECK UBICAZIONE
                if (_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                {
                    if (_SQL.obj_STOLOC_Sped_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                    {
                        frm_error.Text = "Ubicazione " + txt_Destinazione.Text.Trim().ToUpper() + " di tipo SPED non autorizzata ";
                        txt_Destinazione.Text = "";
                        txt_Destinazione.Focus();
                        return;
                    }

                    //UBICAZIONE VERIFICATA
                    txt_Destinazione.Text = txt_Destinazione.Text.Trim().ToUpper();
                    HF_Tipo_D.Value = "U";
                }
                else
                {

                    //UBICAZIONE INESISTENTE, PROVO IL PALNUM
                    if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                    {
                        if (_SQL.obj_PALNUM_SPED_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                        {
                            frm_error.Text = "Palnum " + txt_Destinazione.Text.Trim().ToUpper() + " su ubicazione di tipo SPED non autorizzata ";
                            txt_Destinazione.Text = "";
                            txt_Destinazione.Focus();
                            return;
                        }

                        if (_SQL.obj_PALNUM_NAV_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                        {
                            frm_error.Text = "Palnum " + txt_Destinazione.Text.Trim().ToUpper() + " su ubicazione di tipo NAV non autorizzata ";
                            txt_Destinazione.Text = "";
                            txt_Destinazione.Focus();
                            return;
                        }


                        //PALLET VERIFICATA
                        txt_Destinazione.Text = txt_Destinazione.Text.Trim().ToUpper();
                        HF_Tipo_D.Value = "P";
                    }
                }

                // CHECK
                if (HF_Tipo_D.Value == "")
                {
                    frm_error.Text = "Ubicazione/Pallet destinazione non trovato";
                }
                else
                {
                    //RunCambioStock();
                    Obj_STOCK _Stock = new Obj_STOCK();
                    string _UBI_P = "";
                    string _UBI_D = "";
                    string _PAL_P = "";
                    string _PAL_D = "";
                    string _PCU = "";
                    // Esecuzione Cambio stock
                    string _err = "";
                    Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Etichetta.Text.Trim().ToUpper());
                    _Etic.ITMREF = HF_ITMREF.Value;
                    string _QTY = RecuperaQuantita();
                    string _STA = RecuperaStato();

                    if (HF_Tipo_P.Value == "U")
                    {
                        _UBI_P = txt_Partenza.Text.ToUpper().Trim();
                        //_PAL_P = " ";    //test

                        if (_SQL.obj_STOCK_Load(_USR.FCY_0, _UBI_P, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", _STA, out _Stock)) _PCU = _Stock.PCU_0.Trim();
                    }
                    //
                    if (HF_Tipo_D.Value == "U") _UBI_D = txt_Destinazione.Text.ToUpper().Trim();
                    
                    // PALLET
                    if (HF_Tipo_P.Value == "P")
                    {
                        _PAL_P = txt_Partenza.Text.ToUpper().Trim();
                        if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PAL_P, _Etic.ITMREF, _Etic.LOT, out _Stock))
                        {
                            _UBI_P = _Stock.LOC_0;
                            _Etic.LOT = _Stock.LOT_0;
                            _Etic.SLO = _Stock.SLO_0;
                            _PCU = _Stock.PCU_0.Trim();
                        }

                    }
                    if (HF_Tipo_D.Value == "P")
                    {
                        _PAL_D = txt_Destinazione.Text.ToUpper().Trim();
                        if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PAL_D, _Etic.ITMREF, _Etic.LOT, out _Stock))
                        {
                            _UBI_D = _Stock.LOC_0;
                        }
                    }


                    if (!cls_TermWS.WS_CambioStock(_USR, HF_ITMREF.Value, _Etic.LOT, _Etic.SLO, _PCU, decimal.Parse(_QTY), _STA, _PAL_P, _UBI_P, _STA, _PAL_D, _UBI_D, out _err))
                    {
                        //Errore
                        frm_error.Text = _err;
                        txt_Destinazione.Focus();
                    }
                    else
                    {
                        // OK
                        frm_OK.Text = "Spostamento Confermato";
                        txt_Destinazione.Text = "";
                        txt_Etichetta.Text = "";
                        txt_Partenza.Text = "";
                        txt_Destinazione.Text = "";
                        //txt_Quantita.Text = "";
                        //txt_Quantita_Q.Text = "";
                        //txt_Quantita_R.Text = "";
                        //txt_Quantita_Disp.Text = "";
                        //txt_Quantita_Disp_Q.Text = "";
                        //txt_Quantita_Disp_R.Text = "";
                        //lbl_UM.Text = "";
                        //lbl_UM_Q.Text = "";
                        //lbl_UM_R.Text = "";
                        //lbl_Stato.Text = "";
                        //lbl_Stato_Q.Text = "";
                        //lbl_Stato_R.Text = "";
                        lbl_Articolo.Text = "*";
                        HF_ITMREF.Value = "";
                        HF_QTY_A.Value = "";
                        HF_QTY_Q.Value = "";
                        HF_QTY_R.Value = "";
                        HF_Tipo_D.Value = "";
                        HF_Tipo_P.Value = "";

                        ResetCampiQuantita();
                        ResetCampiQuantitaDerivati();
                        EmptyCampiQuantita();
                        txt_Partenza.Focus();
                    }

                }
            }
        }

        private string RecuperaStato()
        {
            if(txt_Quantita.Text != "")
            {
                return lbl_Stato.Text;
            }
            else if(txt_Quantita_Q.Text != "")
            {
                return lbl_Stato_Q.Text;
            }
            else if(txt_Quantita_R.Text != "")
            {
                return lbl_Stato_R.Text;
            }
            else
            {
                txt_Quantita.Focus();
                return frm_error.Text = "Nessuna Quantità compilata";
            }
        }

        private string RecuperaQuantita()
        {
            if (txt_Quantita.Text != "")
            {
                return txt_Quantita.Text;
            }
            else if (txt_Quantita_Q.Text != "")
            {
                return txt_Quantita_Q.Text;
            }
            else if (txt_Quantita_R.Text != "")
            {
                return txt_Quantita_R.Text;
            }
            else
            {
                txt_Quantita.Focus();
                return frm_error.Text = "Nessuna Quantità compilata";
            }
        }
    }
}