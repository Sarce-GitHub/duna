using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_WS_TOOLS_V9.WSX3_C9;
using X3_WS_TOOLS_V9;

namespace X3_TERMINALINI.spedizione
{
    public partial class GestionePresped : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        bool CARICO = true;
        bool managesSeakey = Properties.Settings.Default.Manage_SEAKEY == "True";


        protected void Page_Load(object sender, EventArgs e)
        {
            // Controllo Utente
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/", true);
            // Reset etichette ok/Errore
            frm_OK.Text = "";
            frm_error.Text = "";
            //

            if (!IsPostBack) txt_Partenza.Focus();

            CARICO = Request.QueryString["TIPO"] == "Carico";
            if (CARICO)
            {

                lbl_title.Text = "Carico Prespedizione";
                lbl_Destinazione.Text = "Ubicazione Prespedizione da Caricare";
                lbl_Destinazione_SC.Text = "Ubicazione Prespedizione da Caricare";
                lbl_Partenza.Text = "Pallet da Caricare";
                lbl_Partenza_SC.Text = "Ubicazione/Pallet di Partenza";
            }
            else
            {
                div_navetta_da_scaricare.Visible = true;
                txt_navetta_da_scaricare.Focus();
                //div_navetta_da_scaricare_SC.Visible = true;
                //txt_navetta_da_scaricare_SC.Focus();
                lbl_title.Text = "Scarico Prespedizione";
                lbl_Destinazione.Text = "Ubicazione di Destinazione";
                lbl_Destinazione_SC.Text = "Ubicazione di Destinazione";
                lbl_Partenza.Text = "Pallet da Scaricare";
                lbl_Partenza_SC.Text = "Navetta da Scaricare";
            }

            GestisciOperazioneNavetta();
        }
        protected void GestisciOperazioneNavetta(object sender, EventArgs e)
        {
            GestisciOperazioneNavetta();
        }

        #region BANCALI
        protected void txt_navetta_da_scaricare_TextChanged(object sender, EventArgs e)
        {
            if (txt_navetta_da_scaricare.Text.Trim() == "")
            {
                frm_error.Text = "Inserire una Navetta";
                txt_navetta_da_scaricare.Text = "";
                txt_navetta_da_scaricare.Focus();
                txt_Destinazione.Text = "";
                txt_Destinazione.Enabled = true;
                txt_Partenza.Text = "";
                lbl_Articolo.Text = "";
                return;
            }

            if (!_SQL.obj_STOLOC_Presped_Check(_USR.FCY_0, txt_navetta_da_scaricare.Text.Trim().ToUpper()))
            {
                //UBICAZIONE VERIFICATA
                txt_navetta_da_scaricare.Text = "";
                //txt_navetta_da_scaricare_SC.Text = "";
                txt_navetta_da_scaricare.Focus();
                frm_error.Text = "Ubicazione Prespedizione Inesistente";
            }
            else
            {
                txt_navetta_da_scaricare.Text = txt_navetta_da_scaricare.Text.Trim().ToUpper();
                //txt_navetta_da_scaricare_SC.Text = txt_navetta_da_scaricare.Text.Trim().ToUpper();
                //txt_navetta_da_scaricare_SC.Enabled = false;
                lbl_Articolo.Text = "";
                txt_Destinazione.Text = "";
                txt_Destinazione.Enabled = true;
                txt_Destinazione.Focus();
                txt_Partenza.Text = "";
            }


        }

        protected void txt_Partenza_TextChanged(object sender, EventArgs e)
        {
            if (txt_Partenza.Text.Trim() == "")
            {
                frm_error.Text = "Inserire un Pallet";
                txt_Partenza.Text = "";
                lbl_Articolo.Text = "";
                txt_Partenza.Focus();
                return;
            }

            lbl_Articolo.Text = "";
            frm_error.Text = "";
            if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
            {
                //PALLET VERIFICATA
                txt_Partenza.Text = txt_Partenza.Text.Trim().ToUpper();
                string _s = "";
                string _h = "";
                foreach (Obj_STOCK item in _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_Partenza.Text))
                {
                    //CHECK PALLET GIA' PRESENTE SU UBICAZIONE PRESPEDIZIONE
                    if (CARICO && item.LOC_0 == txt_Destinazione.Text.Trim().ToUpper())
                    {
                        frm_error.Text = "Pallet già presente In Prespedizione";
                        lbl_Articolo.Text = "";
                        txt_Partenza.Text = "";
                        txt_Partenza.Focus();
                        return;
                    }

                    if (!CARICO && item.LOC_0 != txt_navetta_da_scaricare.Text.Trim().ToUpper())
                    {
                        frm_error.Text = "Pallet non presente in Prespedizione";
                        lbl_Articolo.Text = "";
                        txt_Partenza.Text = "";
                        txt_Partenza.Focus();
                        return;
                    }

                    if (_s == "") _s = "Ubicazione: <b>" + item.LOC_0 + "</b><br/>";
                    if (_h == "")
                    {
                        _h = "<div class=\"d-flex justify-content-between\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">ITMREF</b><b style=\"flex:1;\">LOTTO</b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><b style=\"margin-left:auto;flex:1;text-align:end\">UM</b><b class=\"ms-1\" style=\"margin-left:auto;flex:1;text-align:end\">QTA</b></div><br/></div>";
                        _s = _s + _h;
                    }
                    string lot = !string.IsNullOrEmpty(item.LOT_0) && item.LOT_0 != " " ? item.LOT_0 : "";

                    //_s = _s + "<b>" + item.ITMREF_0 + " - " + item.LOT_0 + "</b> - " + item.STU_0 + " " + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")<br/>";
                    _s = _s + "<div class=\"d-flex justify-content-between tabella-dati\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">" + item.ITMREF_0 + "</b><b style=\"flex:1;\">" + lot + "</b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><span style=\"margin-left:auto;flex:1;text-align:end\">" + item.STU_0 + "</span><span class=\"ms-1 qta-cell\" style=\"margin-left:auto;flex:1;text-align:end\">" + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span></div><br/></div>";
                }

                lbl_Articolo.Text = _s;
                //txt_Partenza.Focus();
                btt_EseguieContinua.Focus();
            }
            else
            {
                frm_error.Text = "Pallet " + txt_Partenza.Text + " non censito in " + _USR.FCY_0;
                txt_Partenza.Text = "";
                txt_Partenza.Focus();
            }
        }

        protected void txt_Destinazione_TextChanged(object sender, EventArgs e)
        {
            if (!CARICO && txt_navetta_da_scaricare.Text.Trim() == "")
            {
                frm_error.Text = "Inserire Navetta da Scaricare";
                txt_Destinazione.Text = "";
                txt_navetta_da_scaricare.Focus();
                return;
            }

            HF_Tipo_D.Value = "";
            if (txt_Destinazione.Text.Trim() != "")
            {
                //CHECK UBICAZIONE
                if (CARICO ? _SQL.obj_STOLOC_Presped_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()) : _SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
                {
                    //UBICAZIONE VERIFICATA
                    txt_Destinazione.Text = txt_Destinazione.Text.Trim().ToUpper();
                    HF_Tipo_D.Value = "U";
                    txt_Tipo_Operazione.Enabled = false;
                    txt_Destinazione.Enabled = false;
                    txt_Partenza.Focus();

                }

                // CHECK
                if (HF_Tipo_D.Value == "")
                {
                    frm_error.Text = CARICO ? "Ubicazione Prespedizione Inesistente" : "Inserita Ubicazione Inesistente";
                    txt_Destinazione.Text = "";
                    txt_Destinazione.Focus();
                }
            }
        }

        protected void btn_Annulla_Click(object sender, EventArgs e)
        {
            txt_Tipo_Operazione.Enabled = true;
            ResetBancali();
        }

        protected void btt_EseguieContinua_Click(object sender, EventArgs e)
        {
            frm_error.Text = "";
            RunCambioStock();
        }

        private void RunCambioStock()
        {
            if (txt_Partenza.Text.Trim() == "")
            {
                frm_error.Text = "Pallet non inserita";
                return;
            }
            if (txt_Destinazione.Text.Trim() == "")
            {
                frm_error.Text = CARICO ? "Ubicazione Presped non inserita" : "Destinazione non Inserita";
                return;
            }
            //
            //if (!_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione.Text.Trim().ToUpper()))
            //{
            //    frm_error.Text = "Ubicazione " + txt_Destinazione.Text + " non censita in " + _USR.FCY_0;
            //    txt_Destinazione.Text = "";
            //    txt_Destinazione.Focus();
            //    return;
            //}

            bool ok = true;
            foreach (Obj_STOCK _stock in _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_Partenza.Text))
            {
                bool ret = true;
                List<Xml_Data> src_Data = new List<Xml_Data>();
                src_Data.Add(new Xml_Data("IN_USR_X3", _USR.USR_X3_0, ""));
                src_Data.Add(new Xml_Data("IN_USR_TERM", _USR.USR_TERM_0, ""));
                src_Data.Add(new Xml_Data("IN_ITMREF", _stock.ITMREF_0, ""));
                src_Data.Add(new Xml_Data("IN_STOFCY", _USR.FCY_0, ""));
                src_Data.Add(new Xml_Data("IN_LOT", _stock.LOT_0, ""));
                src_Data.Add(new Xml_Data("IN_SLO", _stock.SLO_0, ""));
                src_Data.Add(new Xml_Data("IN_UBI_P", _stock.LOC_0, ""));
                src_Data.Add(new Xml_Data("IN_UBI_D", txt_Destinazione.Text.Trim().ToUpper(), ""));
                src_Data.Add(new Xml_Data("IN_STA_P", _stock.STA_0, ""));
                src_Data.Add(new Xml_Data("IN_STA_D", _stock.STA_0, ""));
                src_Data.Add(new Xml_Data("IN_QTY", _stock.QTYSTU_0.ToString("0.###"), "Decimal"));
                src_Data.Add(new Xml_Data("IN_PCU", _stock.STU_0, ""));

                src_Data.Add(new Xml_Data("IN_PALNUM_P", _stock.PALNUM_0, ""));
                src_Data.Add(new Xml_Data("IN_PALNUM_D", _stock.PALNUM_0, ""));
                src_Data.Add(new Xml_Data("IN_VCRNUM", "", ""));


                CAdxResultXml result = new CAdxResultXml();
                string error = "";
                try
                {

                    if (cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YTS_CMBSTK", "GRP1", src_Data, out error, out result))
                    {
                        XElement element = XElement.Parse(result.resultXml);
                        XElement GRP1 = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP1");
                        if (GRP1 != null)
                        {
                            IEnumerable<XElement> _testata = GRP1.Elements();
                            if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") != 1)
                            {
                                error = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                                ret = false;
                            }

                        }
                        else
                        {
                            error = "ERRORE WS GENERICO";
                            ret = false;
                        }
                    }
                    else
                    {
                        error = error + "<br/>Nessun risultato";
                        if (result.messages.Length > 0) error = error + "<br/>" + result.messages[0].message;
                        ret = false;
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    ret = false;
                }
                //
                if (error != "")
                {
                    frm_error.Text = frm_error.Text + error + "<br/>";
                }

                ok = ok & ret;
            }
            //
            if (ok)
            {
                frm_OK.Text = "Cambio effettuato";
                //txt_Destinazione.Text = "";
                txt_Partenza.Text = "";
                lbl_Articolo.Text = "";
                txt_Partenza.Focus();
            }
        }



        #endregion

        #region SCATOLE
        protected void txt_Etichetta_SC_TextChanged(object sender, EventArgs e)
        {
            lbl_Articolo.Text = "";
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();
            txt_Destinazione.Text = "";
            txt_Etichetta_SC.Focus();

            //LF01377AAB-1234
            if (txt_Etichetta_SC.Text.Trim() != "")
            {

                // Estrazione dati Etichetta
                Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Etichetta_SC.Text.Trim().ToUpper());
                string _PALNUM = "";
                string _LOC = txt_Partenza_SC.Text;
                HF_ITMREF.Value = "";
                //
                Obj_STOCK _STK = new Obj_STOCK();
                if (HF_Tipo_P.Value == "P")
                {
                    _PALNUM = txt_Partenza_SC.Text.Trim();
                    if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PALNUM, _Etic.ITMREF, _Etic.LOT, out _STK))
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
                if (_STK.LOC_0 == _LOC)
                {
                    string seakey = !string.IsNullOrEmpty(_STK.SEAKEY_0) && _STK.SEAKEY_0 != " " ? " - " + _STK.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
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
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
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
                    HF_ITMREF.Value = _STK.ITMREF_0;
                    lbl_Articolo.Text = "<b>" + _STK.ITMREF_0 + seakey + "</b><br/>" + _STK.ITMDES_0 + "</b><br/>" + _STK.LOC_0;
                    lbl_Stato_R.Text = _STK.STA_0;
                    lbl_UM_R.Text = _STK.PCU_0;
                    //txt_Quantita.Text = _STK.QTYPCU_0.ToString("0.##");
                    txt_Quantita_Disp_R.Text = _STK.QTYPCU_0.ToString("0.##");
                    HF_QTY_R.Value = _STK.QTYPCU_0.ToString("0.##");
                    qtyFound = true;
                }

                txt_Quantita.Focus();

                if (!qtyFound)
                {
                    frm_error.Text = "Articolo non trovato. " + _SQL.error;
                    txt_Etichetta_SC.Text = "";
                    //lbl_Articolo.Text = "";
                    //ResetCampiQuantita();
                    //ResetCampiQuantitaDerivati();
                    //EmptyCampiQuantita();
                    //txt_Destinazione.Text = "";
                    txt_Etichetta_SC.Focus();
                }
            }
            else
            {
                frm_error.Text = "Inserire un Articolo";
                lbl_Articolo.Text = "";
                ResetCampiQuantita();
                ResetCampiQuantitaDerivati();
                EmptyCampiQuantita();
                txt_Destinazione.Text = "";
                txt_Etichetta_SC.Focus();
            }
        }

        protected void txt_Quantita_TextChanged(object sender, EventArgs e)
        {
            if (txt_Quantita.Text.Trim() != "")
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_Quantita.Text, out _d))
                {
                    if (_d > 0 && _d <= decimal.Parse(HF_QTY_A.Value))
                    {
                        txt_Quantita.Text = _d.ToString("0.##");
                        txt_Quantita_Q.Enabled = false;
                        txt_Quantita_R.Enabled = false;
                        btt_EseguieContinua_SC.Focus();
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
                        btt_EseguieContinua_SC.Focus();
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
                        btt_EseguieContinua_SC.Focus();
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

        protected void txt_Destinazione_SC_TextChanged(object sender, EventArgs e)
        {
            if (!CARICO)
            {
                txt_Etichetta_SC.Text = "";
                ResetCampiQuantita();
                ResetCampiQuantitaDerivati();
                EmptyCampiQuantita();
                if (txt_Destinazione_SC.Text == "") txt_Destinazione_SC.Focus();
            }

            HF_Tipo_D.Value = "";
            if (txt_Destinazione_SC.Text.Trim() != "")
            {
                //CHECK UBICAZIONE
                if (CARICO ? _SQL.obj_STOLOC_Presped_Check(_USR.FCY_0, txt_Destinazione_SC.Text.Trim().ToUpper()) : _SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione_SC.Text.Trim().ToUpper()))
                {
                    //UBICAZIONE VERIFICATA
                    txt_Destinazione_SC.Text = txt_Destinazione_SC.Text.Trim().ToUpper();
                    HF_Tipo_D.Value = "U";

                    if (CARICO)
                    {
                        txt_Tipo_Operazione.Enabled = false;
                        txt_Destinazione_SC.Enabled = false;
                        txt_Partenza_SC.Enabled = true;
                        txt_Partenza_SC.Focus();
                        LoadDatiNavetta(txt_Destinazione_SC.Text.Trim());
                    }
                    else
                    {
                        txt_Destinazione_SC.Enabled = true;
                        txt_Etichetta_SC.Focus();
                    }
                    //LoadDatiNavetta();
                }

                // CHECK
                if (HF_Tipo_D.Value == "")
                {
                    frm_error.Text = CARICO ? "Ubicazione Prespedizione Inesistente" : "Inserita Ubicazione Inesistente";
                    txt_Destinazione_SC.Text = "";
                    txt_Destinazione_SC.Focus();
                }
            }
        }

        protected void txt_Partenza_SC_TextChanged(object sender, EventArgs e)
        {
            HF_Tipo_P.Value = "";

            txt_Etichetta_SC.Text = "";
            lbl_Articolo.Text = "";
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();

            if (txt_Partenza_SC.Text.Trim() != "")
            {
                //CHECK UBICAZIONE
                if (CARICO ? _SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Partenza_SC.Text.Trim().ToUpper()) : _SQL.obj_STOLOC_Presped_Check(_USR.FCY_0, txt_Partenza_SC.Text.Trim().ToUpper()))
                {
                    //UBICAZIONE VERIFICATA
                    txt_Partenza_SC.Text = txt_Partenza_SC.Text.Trim().ToUpper();
                    if (!CARICO)
                    {
                        LoadDatiNavetta(txt_Partenza_SC.Text.Trim());
                        txt_Partenza_SC.Enabled = false;
                    }
                    HF_Tipo_P.Value = "U";
                }
                else
                {

                    //UBICAZIONE INESISTENTE, PROVO IL PALNUM
                    if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Partenza_SC.Text.Trim().ToUpper()))
                    {
                        //PALLET VERIFICATA
                        txt_Partenza_SC.Text = txt_Partenza_SC.Text.Trim().ToUpper();
                        HF_Tipo_P.Value = "P";
                    }
                }

                // CHECK
                if (HF_Tipo_P.Value == "")
                {
                    frm_error.Text = CARICO ? "Ubicazione/Pallet non trovato" : "Ubicazione Prespedizione Inesistente";
                    if (!CARICO)
                    {
                        txt_Etichetta_SC.Text = "";
                        lbl_Articolo.Text = "";
                        ResetCampiQuantita();
                        ResetCampiQuantitaDerivati();
                        EmptyCampiQuantita();
                        txt_Destinazione_SC.Text = "";
                        txt_Partenza_SC.Text = "";
                        txt_Partenza_SC.Focus();
                    }
                    else
                    {
                        txt_Partenza_SC.Focus();
                    }
                }
                else
                {
                    if (CARICO)
                    {
                        txt_Etichetta_SC.Focus();
                    }
                    else
                    {
                        txt_Destinazione_SC.Focus();
                        txt_Tipo_Operazione.Enabled = false;
                    }
                }
            }
            else
            {
                //txt_Etichetta_SC.Text = "";
                //lbl_Articolo.Text = "";
                //ResetCampiQuantita();
                //ResetCampiQuantitaDerivati();
                //EmptyCampiQuantita();
                //txt_Destinazione_SC.Text = "";
                txt_Partenza_SC.Focus();
            }
        }

        protected void btn_Annulla_SC_Click(object sender, EventArgs e)
        {
            txt_Tipo_Operazione.Enabled = true;
            ResetScatole();
        }

        protected void btn_Reset_SC_Click(object sender, EventArgs e)
        {
            txt_Tipo_Operazione.Enabled = true;
            ResetNavettaScatole();
        }

        protected void btt_EseguieContinua_SC_Click(object sender, EventArgs e)
        {
            frm_error.Text = "";
            RunCambioStockSC();
        }

        private void RunCambioStockSC()
        {
            if (string.IsNullOrEmpty(txt_Quantita.Text) && string.IsNullOrEmpty(txt_Quantita_Q.Text) && string.IsNullOrEmpty(txt_Quantita_R.Text))
            {
                frm_error.Text = "Inserire una Quantità";
                txt_Quantita.Focus();
                return;
            }

            HF_Tipo_D.Value = "";
            if (txt_Destinazione_SC.Text.Trim() != "")
            {
                //if (txt_Destinazione_SC.Text == txt_Partenza.Text)
                //{
                //    frm_error.Text = "Stessa Ubicazione non Autorizzata";
                //    txt_Destinazione_SC.Text = "";
                //    txt_Destinazione.Focus();
                //    return;
                //}

                //CHECK UBICAZIONE
                if (CARICO ? _SQL.obj_STOLOC_Presped_Check(_USR.FCY_0, txt_Destinazione_SC.Text.Trim().ToUpper()) : _SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Destinazione_SC.Text.Trim().ToUpper()))
                {
                    //UBICAZIONE VERIFICATA
                    txt_Destinazione_SC.Text = txt_Destinazione_SC.Text.Trim().ToUpper();
                    HF_Tipo_D.Value = "U";
                }

                // CHECK
                //RunCambioStock();
                Obj_STOCK _Stock = new Obj_STOCK();
                string _UBI_P = "";
                string _UBI_D = "";
                string _PAL_P = "";
                string _PAL_D = "";
                string _PCU = "";
                // Esecuzione Cambio stock
                string _err = "";
                Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_Etichetta_SC.Text.Trim().ToUpper());
                _Etic.ITMREF = HF_ITMREF.Value;
                string _QTY = RecuperaQuantita();
                string _STA = RecuperaStato();

                if (HF_Tipo_P.Value == "U")
                {
                    _UBI_P = txt_Partenza_SC.Text.ToUpper().Trim();
                    //_PAL_P = " ";    //test

                    if (_SQL.obj_STOCK_Load(_USR.FCY_0, _UBI_P, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", _STA, out _Stock)) _PCU = _Stock.PCU_0.Trim();
                }
                //
                if (HF_Tipo_D.Value == "U") _UBI_D = txt_Destinazione_SC.Text.ToUpper().Trim();

                // PALLET
                if (HF_Tipo_P.Value == "P")
                {
                    _PAL_P = txt_Partenza_SC.Text.ToUpper().Trim();
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
                    _PAL_D = txt_Destinazione_SC.Text.ToUpper().Trim();
                    if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PAL_D, _Etic.ITMREF, _Etic.LOT, out _Stock))
                    {
                        _UBI_D = _Stock.LOC_0;
                    }
                }


                if (!cls_TermWS.WS_CambioStock(_USR, HF_ITMREF.Value, _Etic.LOT, _Etic.SLO, _PCU, decimal.Parse(_QTY), _STA, _PAL_P, _UBI_P, _STA, _PAL_D, _UBI_D, out _err))
                {
                    //Errore
                    frm_error.Text = _err;
                    txt_Destinazione_SC.Focus();
                }
                else
                {
                    // OK
                    if (CARICO)
                    {
                        LoadDatiNavetta(txt_Destinazione_SC.Text.Trim().ToUpper());
                    }
                    else
                    {
                        LoadDatiNavetta(txt_Partenza_SC.Text.Trim().ToUpper());
                    }

                    frm_OK.Text = "Spostamento Confermato";
                    txt_Etichetta_SC.Text = "";
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
                    if (CARICO)
                    {
                        txt_Partenza_SC.Text = "";
                        txt_Partenza_SC.Focus();
                    }
                    else
                    {
                        txt_Destinazione_SC.Text = "";
                        txt_Destinazione_SC.Focus();

                    }
                }
            }
        }

        private void LoadDatiNavetta(string IN_LOCATION)
        {

            //PALLET VERIFICATA
            string _s = "";
            string _h = "";
            string seakeyHeader = managesSeakey ? "SEAKEY" : "";
            if (_h == "")
            {
                _h = "<div class=\"d-flex justify-content-between\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">PALNUM</b><b class=\"ms-2\" style=\"flex:1;\">ITMREF</b><b class=\"ms-2\" style=\"flex:1;\">LOTTO</b><b class=\"ms-2\" style=\"flex:1;\">" + seakeyHeader + "</b></div><div class=\"d-flex\" class=\"ms-2\" style=\"flex:1;white-space:nowrap\"><b style=\"margin-left:auto;flex:1;text-align:end\">UM</b><b class=\"ms-2\" style=\"margin-left:auto;flex:1;text-align:end\">QTA</b></div><br/></div>";
                _s = _s + _h;
            }


            foreach (Obj_STOCK item in _SQL.obj_STOCK_SearchByLocation(_USR.FCY_0, IN_LOCATION).OrderBy(o => o.PALNUM_0).ThenBy(o => o.ITMREF_0).ThenBy(o => o.LOT_0))
            {
                string seakey = (!string.IsNullOrEmpty(item.SEAKEY_0) && item.SEAKEY_0 != " " && item.SEAKEY_0 != item.ITMREF_0 && managesSeakey) ? item.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                string lot = !string.IsNullOrEmpty(item.LOT_0) && item.LOT_0 != " " ? item.LOT_0 : "";
                string palnum = !string.IsNullOrEmpty(item.PALNUM_0) && item.PALNUM_0 != " " ? item.PALNUM_0 : "";// "&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;";
                //_s = _s + "<div class=\"d-flex justify-content-between\"><b>" + palnum + " - " + item.ITMREF_0 + lot + seakey + "</b><span style=\"margin-left:auto\">" + item.STU_0 + " " + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span><br/></div>";
                _s = _s + "<div class=\"d-flex justify-content-between tabella-dati\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\">" + "<b style=\"flex:1;\">" + item.PALNUM_0 + "</b><b class=\"ms-2\" style=\"flex:1;\">" + item.ITMREF_0 + "</b><b class=\"ms-2\" style=\"flex:1;\">" + lot + "</b><b class=\"ms-2\" style=\"flex:1;\">" + seakey + " </b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><span style=\"margin-left:auto;flex:1;text-align:end\">" + item.STU_0 + "</span><span class=\"ms-2 qta-cell\" style=\"margin-left:auto;flex:1;text-align:end\">" + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span></div><br/></div>";

            }
            lbl_Dati_Prespedizione.Text = _s;
        }


        #endregion

        #region HELPERS
        private void ResetScatole()
        {
            //txt_navetta_da_scaricare_SC.Text = "";
            //txt_navetta_da_scaricare_SC.Enabled = true;
            txt_Etichetta_SC.Text = "";
            EmptyCampiQuantita();
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            lbl_Dati_Prespedizione.Text = "";
            if (CARICO)
            {
                txt_Partenza_SC.Text = "";
                txt_Partenza_SC.Enabled = true;
                txt_Partenza_SC.Focus();
            }
            else
            {
                txt_Destinazione_SC.Text = "";
                txt_Destinazione_SC.Enabled = true;
                txt_Destinazione_SC.Focus();

            }
        }

        private void ResetNavettaScatole()
        {
            ResetScatole();
            if (CARICO)
            {
                txt_Destinazione_SC.Text = "";
                txt_Partenza_SC.Text = "";
                txt_Destinazione_SC.Enabled = true;
                txt_Destinazione_SC.Focus();
            }
            else
            {
                txt_Destinazione_SC.Text = "";
                txt_Partenza_SC.Text = "";
                txt_Partenza_SC.Enabled = true;
                txt_Partenza_SC.Focus();

            }
        }


        private void ResetBancali()
        {
            txt_navetta_da_scaricare.Text = "";
            txt_navetta_da_scaricare.Enabled = true;
            txt_Destinazione.Text = "";
            txt_Destinazione.Enabled = true;
            txt_Partenza.Text = "";
            lbl_Articolo.Text = "";
        }

        private void ResetCampiQuantita()
        {
            txt_Quantita.Enabled = true;
            txt_Quantita_Q.Enabled = true;
            txt_Quantita_R.Enabled = true;

            txt_Quantita.Focus();
        }

        private string RecuperaStato()
        {
            if (txt_Quantita.Text != "")
            {
                return lbl_Stato.Text;
            }
            else if (txt_Quantita_Q.Text != "")
            {
                return lbl_Stato_Q.Text;
            }
            else if (txt_Quantita_R.Text != "")
            {
                return lbl_Stato_R.Text;
            }
            else
            {
                txt_Quantita.Focus();
                return frm_error.Text = "Nessuna Quantità Compilata";
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
                return frm_error.Text = "Nessuna Quantità Compilata";
            }
        }

        protected void GestisciOperazioneNavetta()
        {
            switch (txt_Tipo_Operazione.SelectedValue)
            {
                case "CambioUbicazionePallet":
                    //ResetBancali();
                    CaricaPallet.Visible = true;
                    CaricaScatole.Visible = false;

                    if (CARICO)
                    {
                        txt_Destinazione.Focus();
                    }
                    else
                    {
                        txt_navetta_da_scaricare.Focus();
                    }
                    break;
                case "AssemblaPallet":
                    CaricaScatole.Visible = true;
                    CaricaPallet.Visible = false;


                    if (CARICO)
                    {
                        div_scarico_sc_col.CssClass += " flex-column";
                        txt_Destinazione_SC.Focus();
                        div_btn_sc_carico.Visible = true;
                        div_btn_sc_scarico.Visible = false;
                    }
                    else
                    {
                        div_scarico_sc_col.CssClass += " flex-column-reverse";
                        txt_Partenza_SC.Focus();
                        div_btn_sc_carico.Visible = false;
                        div_btn_sc_scarico.Visible = true;

                    }

                    break;
            }
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

        #endregion
    }
}