using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.produzione
{
    public partial class Dichiarazione_Produzione : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string error = "";
        decimal tolerance = 0;
        bool manageTolerance = false;
        bool toleranceMessageAlreadyShown = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            hf_FCY.Value = _USR.FCY_0;
            if (_USR.ABIL4_0 != 2) Response.Redirect("/", true);

            frm_OK.Text = "";
            frm_error.Text = "";
            tolerance = Properties.Settings.Default.DICHIARAZIONE_PRODUZIONE_TOLERANCE_PERCENTAGE;
            manageTolerance = Properties.Settings.Default.DICHIARAZIONE_PRODUZIONE_CHECK_TOLERANCE;

            if (!IsPostBack)
            {
                txt_Ricerca.Focus();
                btn_conferma.OnClientClick = ClientScript.GetPostBackEventReference(btn_conferma, "") + "; disableButton(this);";
            }
        }

        protected void txt_Ricerca_TextChanged(object sender, EventArgs e)
        {
            pan_data.Visible = false;
            ResetHiddenFields();

            string[] Arr = txt_Ricerca.Text.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (txt_Ricerca.Text.Trim() != "")
            {
                Obj_MFGITM_ITMMASTER_PRODUZIONE s = _SQL.Obj_MFGITM_ITMMASTER_PRODUZIONE_Load(_USR.FCY_0, txt_Ricerca.Text.Trim().ToUpper(), out error);
                if (!string.IsNullOrEmpty(error))
                {
                    SearchError();
                    return;
                }

                decimal qtyToSuggest = _SQL.Obj_YCONSMAT_Load(s.MFGNUM_0, out error).USEQTY_0;
                if (!string.IsNullOrEmpty(error))
                {
                    SearchError();
                    return;
                }

                if(qtyToSuggest == 0)
                {
                    ItemsNotUnloaded();
                    return; 
                }

                if(manageTolerance)
                {
                    bool isQtyWithinTolerance = CheckQtyTolerance(s.UOMEXTQTY_0, qtyToSuggest, tolerance);
                    if (!isQtyWithinTolerance && !toleranceMessageAlreadyShown)
                    {
                       ToleranceNotMet();
                       //return;
                    }
                }

                bool showLastre = !string.IsNullOrEmpty(s.PCU_0) && s.PCU_0 != " " && s.PCU_0 != s.STU_0;
                pan_data.Visible = true;
                lbl_magazzino.Text = "MAGAZZINO " + _USR.FCY_0;
                lbl_ordine.Text = "Ordine N°: " + s.MFGNUM_0 + "; COEFF: " + s.PCUSTUCOE_0.ToString("0.###");
                lbl_materiale.Text = s.ITMREF_0 + " - " + s.ITMDES1_0;
                txt_UM.Text = showLastre ? s.PCU_0 : s.STU_0;//s.UOM_0;
                txt_qta.Text = qtyToSuggest.ToString("0.###");
                hf_MFGITMQTY.Value = s.UOMEXTQTY_0.ToString("0.###");
                //txt_qta.Text = showLastre ? Math.Ceiling(s.LASTRE).ToString("0.###") : Math.Ceiling(s.RMNEXTQTY_0).ToString("0.###");
                //txt_qta.Text = hf_QTY.Value;
                txt_qta.Focus();
                txt_Ricerca.Text = "";


                hf_MFGNUM.Value = s.MFGNUM_0;
                hf_ITMREF.Value = s.ITMREF_0;
                hf_LOT.Value = s.LOT_0;
                hf_UM.Value = txt_UM.Text;
                hf_COEFF.Value = s.PCUSTUCOE_0.ToString("0.###");
                hf_CURRENTQTY.Value = txt_qta.Text;
                //hf_STATUS.Value = txt_Stato.Text;
            }
        }

        protected void btn_conferma_Click(object sender, EventArgs e)
        {
            if (txt_qta.Text.Trim() == "")
            {
                EmptyQty();
                return;
            }

            try
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_qta.Text, out _d))
                {
                    //if (_d > 0 && _d <= decimal.Parse(txt_qta.Text.Replace(",", ".")) && _d <= decimal.Parse(lbl_disp.Text.Replace(",", ".")))
                    //{
                    //    txt_qta.Text = _d.ToString("0.##");
                    //}
                    //else
                    //{

                    //    frm_error.Text = "Quantità non congrua";
                    //    txt_qta.Text = "";
                    //    txt_qta.Focus();
                    //    return;
                    //}
                }
                else
                {
                    frm_error.Text = "Quantità non valida";
                    txt_qta.Text = "";
                    txt_qta.Focus();
                    return;
                }

                decimal QTY = decimal.Parse(txt_qta.Text);
                decimal COEFF = decimal.Parse(hf_COEFF.Value);

                if(manageTolerance)
                {
                    bool isQtyWithinTolerance = CheckQtyTolerance(decimal.Parse(hf_MFGITMQTY.Value), QTY, tolerance);
                    if (!isQtyWithinTolerance && !toleranceMessageAlreadyShown)
                    {
                        ToleranceNotMet();
                        return;
                    }
                }

                string _res = "";
                var wsDichiarazioneProduzione = cls_TermWS.WS_DichiarazioneProduzione(hf_FCY.Value, hf_MFGNUM.Value, hf_ITMREF.Value, hf_LOT.Value, hf_UM.Value, QTY, COEFF, "", out error, out _res);
                if (wsDichiarazioneProduzione)
                {
                    SaveOk(_res);
                    return;
                }
                SaveKo(error);
            }
            catch (Exception ex)
            {
                SaveKo(ex.Message);
            }
        }

        protected void txt_qta_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            toleranceMessageAlreadyShown = false;
            if (txt_qta.Text.Trim() == "")
            {             
                EmptyQty();
                return;
            }

        }

        protected bool CheckQtyTolerance(decimal qtyToCheck, decimal qtySuggested, decimal tolerance)
        {
            decimal difference = Math.Abs(qtyToCheck - qtySuggested);
            decimal allowedTolerance = qtySuggested * tolerance / 100;
            return difference <= allowedTolerance;
        }

        protected void ToleranceNotMet()
        {
            string msg = "La quantità indicata supera la tolleranza consentita del " + tolerance + "%";
            ShowAlert(msg);
            frm_error.Text = msg;
            txt_Ricerca.Text = "";
            txt_Ricerca.Focus();
            btn_conferma.Enabled = true;
            return;
        }

        protected void SearchError()
        {
            frm_error.Text = error;
            txt_Ricerca.Text = "";
            txt_Ricerca.Focus();
            return;
        }

        protected void ItemsNotUnloaded()
        {
            frm_error.Text = "Non risultano materiali scaricati";
            txt_Ricerca.Text = "";
            txt_Ricerca.Focus();
            return;

        }

        protected void EmptyQty()
        {
            frm_error.Text = "Inserire una quantità";
            txt_qta.Focus();
            return;
        }

        public void SaveOk(string wsResult)
        {
            frm_OK.Text = "Dichiarazione Effettuata: " + wsResult;
            pan_data.Visible = false;
        }

        public void SaveKo(string errMsg)
        {
            frm_error.Text = "Salvataggio Fallito: " + errMsg;
            txt_qta.Text = "";
            txt_qta.Focus();
        }

        private void ResetHiddenFields()
        {
            hf_CURRENTQTY.Value = 0.ToString("0.###");
            hf_UM.Value = "";
            hf_STATUS.Value = "";
            hf_MFGNUM.Value = "";
            hf_COEFF.Value = 0.ToString("0.###");
            hf_MFGITMQTY.Value = 0.ToString("0.###");
            toleranceMessageAlreadyShown = false;
        }
        protected void ShowAlert(string message)
        {
            string script = $"<script type='text/javascript'>alert('{message}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            toleranceMessageAlreadyShown = true;
        }
    }
}