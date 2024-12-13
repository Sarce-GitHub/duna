using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_WS_TOOLS_V9.WSX3_C9;
using X3_WS_TOOLS_V9;
using System.Runtime.InteropServices.WindowsRuntime;

namespace X3_TERMINALINI.magazzino
{
    public partial class AssemblaPallet : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        Obj_STOCK _s = new Obj_STOCK();
        bool managesSeakey = Properties.Settings.Default.Manage_SEAKEY == "True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL1_0 != 2) Response.Redirect("/", true);
            //
            frm_OK.Text = "";
            frm_error.Text = "";
            txt_etichetta.Focus();
            if (!Page.IsPostBack)
            {
                div_data.Visible = false;
                div_new.Visible = false;
                div_ubicazione_pallet.Visible = false;
                hf_LOC.Value = "";
            }
        }

        protected void txt_etichetta_TextChanged(object sender, EventArgs e)
        {
            lbl_Articolo.Text = "";
            frm_error.Text = "";
            txt_etichetta.Text = txt_etichetta.Text.Trim().ToUpper();
            Check_Etichetta();
        }

        private void Check_Etichetta()
        {

            if(!CheckPalnumUbic(txt_etichetta)) return;

            //PALLET VERIFICATA
            string _s = "";
                string _loc = "";
                string _h = "";
                foreach (Obj_STOCK item in _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_etichetta.Text).OrderBy(o => o.ITMREF_0).ThenBy(o => o.LOT_0))
                {
                    string seakeyHeader = managesSeakey ? "SEAKEY" : "";
                    if(_h == "")
                    {
                        _h = "<div class=\"d-flex justify-content-between\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">ITMREF</b><b style=\"flex:1;\">LOTTO</b><b style=\"flex:1;\">" + seakeyHeader + "</b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><b style=\"margin-left:auto;flex:1;text-align:end\">UM</b><b class=\"ms-1\" style=\"margin-left:auto;flex:1;text-align:end\">QTA</b></div><br/></div>";
                    }

                    if (_s == "")
                    {
                        hf_LOC.Value = item.LOC_0;
                        //_s = "Ubicazione: <b>" + item.LOC_0 + "</b><br/>";
                        _loc = item.LOC_0;
                    }
                    string seakey = (!string.IsNullOrEmpty(item.SEAKEY_0) && item.SEAKEY_0 != " " && item.SEAKEY_0 != item.ITMREF_0 && managesSeakey) ? item.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                    string lot = !string.IsNullOrEmpty(item.LOT_0) && item.LOT_0 != " " ? item.LOT_0 : "";

                    _s = _s + "<div class=\"d-flex justify-content-between tabella-dati\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">" + item.ITMREF_0 + "</b><b style=\"flex:1;\">" +  lot + "</b><b style=\"flex:1;\">" + seakey + " </b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><span style=\"margin-left:auto;flex:1;text-align:end\">" + item.STU_0 + "</span><span class=\"ms-1 qta-cell\" style=\"margin-left:auto;flex:1;text-align:end\">" + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span></div><br/></div>";
                }
                _h = _h + _s;
                lbl_Articolo.Text = _h;
                div_data.Visible = true;
                div_new.Visible = false;
                txt_etichetta.Enabled = false;
                lbl_ubicazione_pallet.Text = _loc;
                div_ubicazione_pallet.Visible = true;
                txt_articolo.Focus();
        }


        protected void btt_Nuovo_Conferma_Click(object sender, EventArgs e)
        {
            frm_error.Text = "";
            txt_destinazione.Text = txt_destinazione.Text.Trim().ToUpper();
            if (txt_destinazione.Text == "")
            {
                frm_error.Text = "Destinazione non indicata";
                txt_destinazione.Focus();
                return;
            }

            if(!CheckStolocUbic(txt_destinazione)) return;

            txt_etichetta.Enabled = false;
            div_data.Visible = true;
            div_new.Visible = false;
            div_ubicazione_pallet.Visible = true;
            hf_LOC.Value = txt_destinazione.Text.Trim().ToUpper();
            lbl_ubicazione_pallet.Text = hf_LOC.Value;
            lbl_Articolo.Text = "Nuovo Pallet in <b>" + hf_LOC.Value + "</b>";
            txt_articolo.Focus();
        }

        protected void btt_Nuovo_Annulla_Click(object sender, EventArgs e)
        {
            div_data.Visible = false;
            div_new.Visible = false;
            txt_etichetta.Text = "";
            txt_etichetta.Focus();
        }


        protected void btt_EseguieContinua_Click(object sender, EventArgs e)
        {
            if (!Check_Esegui()) return;
            //if (RunCambioStock(_s))
            //{
            //    txt_articolo.Text = "";
            //    txt_destinazione.Text = "";
            //    txt_Quantita.Text = "";
            //    txt_ubicazione.Text = "";
            //    Check_Etichetta();
            //}
            CambioStockAssPal();
            Check_Etichetta();
        }

        protected void btt_Annulla_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssemblaPallet.aspx");
        }

        protected void btt_Esegui_Click(object sender, EventArgs e)
        {
            if (!Check_Esegui()) return;
            if (RunCambioStock(_s)) Response.Redirect("AssemblaPallet.aspx");
        }

        private bool Check_Esegui()
        {
            frm_error.Text = "";
            //
            if (!CheckStolocUbic(txt_ubicazione)) return false;

            Obj_STOCK_ETIC _etic = new Obj_STOCK_ETIC(txt_articolo.Text.ToUpper());
            _s = new Obj_STOCK();
            if (!_SQL.obj_STOCK_Load(_USR.FCY_0, txt_ubicazione.Text.Trim().ToUpper(), _etic.ITMREF, _etic.LOT, _etic.SLO, "", out _s))
            {
                frm_error.Text = "Articolo " + txt_articolo.Text + " non trovato in " + txt_ubicazione.Text;
                txt_articolo.Text = "";
                txt_articolo.Focus();
                ResetCampiQuantita();
                ResetCampiQuantitaDerivati();
                EmptyCampiQuantita();
                return false;
            }

            return true;
        }


        protected void txt_articolo_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            txt_articolo.Text = txt_articolo.Text.Trim().ToUpper();
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();
            txt_ubicazione.Text = "";

            if (txt_articolo.Text == "")
            {
                txt_articolo.Focus();
                return;
            }
            //
            Obj_STOCK_ETIC _etic = new Obj_STOCK_ETIC(txt_articolo.Text.ToUpper());

            Obj_ITMMASTER _i = _SQL.obj_ITMMASTER_Load(_etic.ITMREF);
            if (_i.ITMREF_0!= _etic.ITMREF)
            {
                frm_error.Text = "Articolo "+ txt_articolo.Text +" non trovato";
                txt_articolo.Text = "";
                txt_articolo.Focus();
            }
            else
            {
                txt_ubicazione.Focus();
            }

            txt_ubicazione.Text = "";
        }

        protected void txt_ubicazione_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            txt_ubicazione.Text = txt_ubicazione.Text.Trim().ToUpper();
            ResetCampiQuantita();
            ResetCampiQuantitaDerivati();
            EmptyCampiQuantita();

            if (txt_ubicazione.Text == "")
            {
                txt_ubicazione.Focus();
                return;
            }
            //

            if(!CheckStolocUbic(txt_ubicazione)) return;

            //
            Obj_STOCK_ETIC _etic = new Obj_STOCK_ETIC(txt_articolo.Text.ToUpper());
            bool qtyFound = false;
            bool hasQtaA = false;
            bool hasQtaQ = false;
            bool hasQtaR = false;

            Obj_STOCK _s = new Obj_STOCK();
            _SQL.obj_STOCK_Load(_USR.FCY_0, txt_ubicazione.Text.Trim().ToUpper(), _etic.ITMREF, _etic.LOT, _etic.SLO, "", "A", out _s);
            if(_s.LOC_0 == txt_ubicazione.Text.Trim().ToUpper())
            {
                //lbl_Qta.Text = "Qta prelevabile: " + _s.QTYSTU_0.ToString("0.####");
                HF_ITMREF.Value = _s.ITMREF_0;
                lbl_Stato.Text = _s.STA_0;
                lbl_UM.Text = _s.PCU_0;
                txt_Quantita_Disp.Text = _s.QTYPCU_0.ToString("0.##");
                HF_QTY_A.Value = _s.QTYPCU_0.ToString("0.##");
                hasQtaA = true;
                qtyFound = true;
            }

            _s = new Obj_STOCK();
            _SQL.obj_STOCK_Load(_USR.FCY_0, txt_ubicazione.Text.Trim().ToUpper(), _etic.ITMREF, _etic.LOT, _etic.SLO, "", "Q", out _s);
            if (_s.LOC_0 == txt_ubicazione.Text.Trim().ToUpper())
            {
                //lbl_Qta_Q.Text = "Qta prelevabile: " + _s.QTYSTU_0.ToString("0.####");
                HF_ITMREF.Value = _s.ITMREF_0;
                lbl_Stato_Q.Text = _s.STA_0;
                lbl_UM_Q.Text = _s.PCU_0;
                txt_Quantita_Disp_Q.Text = _s.QTYPCU_0.ToString("0.##");
                HF_QTY_Q.Value = _s.QTYPCU_0.ToString("0.##");
                hasQtaQ = true;
                qtyFound = true;
            }

            _s = new Obj_STOCK();
            _SQL.obj_STOCK_Load(_USR.FCY_0, txt_ubicazione.Text.Trim().ToUpper(), _etic.ITMREF, _etic.LOT, _etic.SLO, "", "R", out _s);
            if (_s.LOC_0 == txt_ubicazione.Text.Trim().ToUpper())
            {
                //lbl_Qta_Q.Text = "Qta prelevabile: " + _s.QTYSTU_0.ToString("0.####");
                HF_ITMREF.Value = _s.ITMREF_0;
                lbl_Stato_R.Text = _s.STA_0;
                lbl_UM_R.Text = _s.PCU_0;
                txt_Quantita_Disp_R.Text = _s.QTYPCU_0.ToString("0.##");
                HF_QTY_R.Value = _s.QTYPCU_0.ToString("0.##");
                hasQtaR = true;
                qtyFound = true;
            }

            txt_Quantita.Focus();

            if (!qtyFound)
            {
                frm_error.Text = "Articolo " + txt_articolo.Text + " non trovato in " + txt_ubicazione.Text;
                txt_ubicazione.Text = "";
                txt_articolo.Text = "";
                txt_articolo.Focus();
            }

        }

        private void ResetCampiQuantita()
        {
            txt_Quantita.Enabled = true;
            txt_Quantita_Q.Enabled = true;
            txt_Quantita_R.Enabled = true;
            txt_Quantita.Focus();
        }
        protected void CambioStockAssPal()
        {
            if (string.IsNullOrEmpty(txt_Quantita.Text) && string.IsNullOrEmpty(txt_Quantita_Q.Text) && string.IsNullOrEmpty(txt_Quantita_R.Text))
            {
                frm_error.Text = "Inserire una quantità";
                txt_Quantita.Focus();
                return;
            }

            //RunCambioStock();
            //Obj_STOCK _Stock = new Obj_STOCK();
            // Esecuzione Cambio stock
            string _err = "";
            string _UBI_P = txt_ubicazione.Text.ToUpper().Trim();
            string _UBI_D = lbl_ubicazione_pallet.Text.Trim().ToUpper();
            string _PAL_D = txt_etichetta.Text.ToUpper().Trim();
            string _PAL_P = "";
            string _PCU = _s.PCU_0.Trim();

            Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC(txt_articolo.Text.Trim().ToUpper());
            _Etic.ITMREF = HF_ITMREF.Value;
            string _QTY = RecuperaQuantita();
            string _STA = RecuperaStato();


            //if (_SQL.obj_STOCK_Load(_USR.FCY_0, _UBI_P, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", _STA, out _Stock)) _PCU = _Stock.PCU_0.Trim();
            //
            if (!cls_TermWS.WS_CambioStock(_USR, HF_ITMREF.Value, _Etic.LOT, _Etic.SLO, _PCU, decimal.Parse(_QTY), _STA, _PAL_P, _UBI_P, _STA, _PAL_D, _UBI_D, out _err))
            {
                //Errore
                frm_error.Text = _err;
                div_new.Visible = false;
                btt_EseguieContinua.Focus();
            }
            else
            {
                // OK
                frm_OK.Text = "Spostamento Confermato";
                txt_articolo.Text = ""; 
                txt_ubicazione.Text = "";   
                txt_Quantita.Text = "";
                txt_Quantita_Q.Text = "";
                txt_Quantita_R.Text = "";
                txt_Quantita_Disp.Text = "";
                txt_Quantita_Disp_Q.Text = "";
                txt_Quantita_Disp_R.Text = "";
                lbl_UM.Text = "";
                lbl_UM_Q.Text = "";
                lbl_UM_R.Text = "";
                lbl_Articolo.Text = "*";
                lbl_Stato.Text = "";
                lbl_Stato_Q.Text = "";
                lbl_Stato_R.Text = "";
                HF_ITMREF.Value = "";
                HF_QTY_A.Value = "";
                HF_QTY_Q.Value = "";
                HF_QTY_R.Value = "";

                ResetCampiQuantita();
                txt_articolo.Focus();
            }
        }


        private bool RunCambioStock(Obj_STOCK _stock)
        {
            frm_error.Text = "";
            decimal q = 0;
            if (!decimal.TryParse(txt_Quantita.Text.Trim(), out q))
            {
                frm_error.Text = "Quantità non valida";
                return false;
            }
            if (q<0 || q>_stock.QTYSTU_0)
            {
                frm_error.Text = "Quantità non corretta";
                return false;
            }


            List<Xml_Data> src_Data = new List<Xml_Data>();
            src_Data.Add(new Xml_Data("IN_USR_X3", _USR.USR_X3_0, ""));
            src_Data.Add(new Xml_Data("IN_USR_TERM", _USR.USR_TERM_0, ""));
            src_Data.Add(new Xml_Data("IN_ITMREF", _stock.ITMREF_0, ""));
            src_Data.Add(new Xml_Data("IN_STOFCY", _USR.FCY_0, ""));
            src_Data.Add(new Xml_Data("IN_LOT", _stock.LOT_0, ""));
            src_Data.Add(new Xml_Data("IN_SLO", _stock.SLO_0, ""));
            src_Data.Add(new Xml_Data("IN_UBI_P", _stock.LOC_0, ""));
            src_Data.Add(new Xml_Data("IN_UBI_D", hf_LOC.Value, ""));
            src_Data.Add(new Xml_Data("IN_STA_P", _stock.STA_0, ""));
            src_Data.Add(new Xml_Data("IN_STA_D", _stock.STA_0, ""));
            src_Data.Add(new Xml_Data("IN_QTY", q.ToString("0.###"), "Decimal"));
            src_Data.Add(new Xml_Data("IN_PCU", _stock.STU_0, ""));

            src_Data.Add(new Xml_Data("IN_PALNUM_P", "", ""));
            src_Data.Add(new Xml_Data("IN_PALNUM_D", txt_etichetta.Text, ""));
            src_Data.Add(new Xml_Data("IN_VCRNUM", "", ""));


            CAdxResultXml result = new CAdxResultXml();
            try
            {
                string error = "";
                if (cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YTS_CMBSTK", "GRP1", src_Data, out error, out result))
                {
                    XElement element = XElement.Parse(result.resultXml);
                    XElement GRP1 = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP1");
                    if (GRP1 != null)
                    {
                        IEnumerable<XElement> _testata = GRP1.Elements();
                        if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") != 1)
                        {
                            frm_error.Text = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                            return false;
                        }
                        return true;

                    }
                    else
                    {
                        frm_error.Text = "ERRORE WS GENERICO";
                        return false;
                    }
                }
                else
                {
                    frm_error.Text = "<br/>Nessun risultato";
                    if (result.messages.Length > 0) error = error + "<br/>" + result.messages[0].message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                frm_error.Text = ex.Message;
                return false;
            }
            //
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
                        btt_EseguieContinua.Focus();
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
                        btt_EseguieContinua.Focus();
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
                        btt_EseguieContinua.Focus();
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

        #region HELPERS
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

        protected bool CheckPalnumUbic(TextBox palnum)
        {

            if (!_SQL.obj_PALNUM_Check(_USR.FCY_0, palnum.Text))
            {
                div_new.Visible = true;
                lbl_NewPallet.Text = "Pallet <b>" + palnum.Text.Trim().ToUpper() + "</b> non censito a magazzino.<br/>Inserire una ubicazione di destinazione e confermare.";
                txt_destinazione.Focus();
                return false;
            }

            if (_SQL.obj_PALNUM_SPED_Check(_USR.FCY_0, palnum.Text.Trim().ToUpper()))
            {
                frm_error.Text = "Palnum " + palnum.Text.Trim().ToUpper() + " su ubicazione di tipo SPED non autorizzata ";
                palnum.Text = "";
                palnum.Focus();
                return false;
            }

            if (_SQL.obj_PALNUM_NAV_Check(_USR.FCY_0, palnum.Text.Trim().ToUpper()))
            {
                frm_error.Text = "Palnum " + palnum.Text.Trim().ToUpper() + " su ubicazione di tipo NAV non autorizzata ";
                palnum.Text = "";
                palnum.Focus();
                return false;
            }
            return true;
        }

        protected bool CheckStolocUbic(TextBox ubic)
        {
            if (!_SQL.obj_STOLOC_Check(_USR.FCY_0, ubic.Text.Trim().ToUpper()))
            {
                if (_SQL.obj_STOLOC_Navetta_Check(_USR.FCY_0, ubic.Text.Trim().ToString()))
                {
                    frm_error.Text = "Ubicazione " + ubic.Text.Trim().ToString() + " di tipo NAV non autorizzata";
                    ubic.Text = "";
                    ubic.Focus();
                    return false;
                }

                frm_error.Text = "Ubicazione " + ubic.Text.Trim().ToString() + " non censita in " + _USR.FCY_0;
                ubic.Text = "";
                ubic.Focus();
                return false;
            }

            if (_SQL.obj_STOLOC_Sped_Check(_USR.FCY_0, ubic.Text.Trim().ToString()))
            {
                frm_error.Text = "Ubicazione " + ubic.Text.Trim().ToString() + " di tipo SPED non autorizzata";
                ubic.Text = "";
                ubic.Focus();
                return false;
            }
            return true;
        }




        #endregion
    }
}