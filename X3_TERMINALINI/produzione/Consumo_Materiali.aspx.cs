﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_TERMINALINI.Properties;

namespace X3_TERMINALINI.produzione
{
    public partial class Consumo_Materiali : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string error = "";
        bool isFamigliaStatistica;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL4_0 != 2) Response.Redirect("/", true);

            frm_OK.Text = "";
            frm_error.Text = "";
            hf_FCY.Value = _USR.FCY_0;
            //if (!IsPostBack) txt_ordine.Focus();

            if (!IsPostBack)
            {
                txt_ordine.Focus();
                btn_conferma.OnClientClick = ClientScript.GetPostBackEventReference(btn_conferma, "") + "; disableButton(this);";
            }

        }

        protected void txt_ordine_TextChanged(object sender, EventArgs e)
        {
            pan_data.Visible = false;
            ResetHiddenFields();

            if (!string.IsNullOrEmpty(txt_ordine.Text) && !string.IsNullOrEmpty(txt_etichetta.Text))
            {
                Ricerca();
                return;
            }

            if (txt_ordine.Text.Trim() != "")
            {
                txt_etichetta.Focus();
            }
        }

        protected void txt_etichetta_TextChanged(object sender, EventArgs e)
        {
            pan_data.Visible = false;
            ResetHiddenFields();

            if (string.IsNullOrEmpty(txt_ordine.Text))
            {
                frm_error.Text = "Inserire un ordine";
                txt_ordine.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txt_ordine.Text.Trim()) && !string.IsNullOrEmpty(txt_etichetta.Text.Trim()))
            {
                Ricerca();
            }
        }

        protected void Ricerca()
        {
            Obj_STOCK_ETIC _e = new Obj_STOCK_ETIC(txt_etichetta.Text.Trim().ToUpper());
            hf_ITMREF.Value = _e.ITMREF.Trim().ToUpper();
            hf_LOT.Value = _e.LOT.Trim().ToUpper();

            //Obj_MFGMAT_ITMMASTER_PRODUZIONE 
            //Obj_MFGMAT_ITMMASTER_PRODUZIONE s = _SQL.Obj_MFGMAT_ITMMASTER_PRODUZIONE_Load(_USR.FCY_0, txt_ordine.Text.Trim().ToUpper(), hf_ITMREF.Value, hf_LOT.Value, out error);
            Obj_YSCARMAT s = _SQL.Obj_YSCARMAT_Load(_USR.FCY_0, txt_ordine.Text.Trim().ToUpper(), hf_ITMREF.Value, hf_LOT.Value, out error);
            if (!string.IsNullOrEmpty(error))
            {
                frm_error.Text = error;
                txt_ordine.Text = "";
                txt_etichetta.Text = "";
                txt_ordine.Focus();
                ResetHiddenFields();
                return;
            }


            isFamigliaStatistica = s.TSICOD_3 == Properties.Settings.Default.CONS_MATERIALI_TSICOD_TO_CHECK; //ex TSICOD_3

            string[] Arr = txt_etichetta.Text.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string lot = !string.IsNullOrEmpty(s.LOT_0) ? " " + Properties.Settings.Default.Etic_Split + " " + s.LOT_0 : "";
            pan_data.Visible = true;
            lbl_magazzino.Text = "MAGAZZINO " + _USR.FCY_0;
            lbl_ordine.Text = "Ordine N°: " + s.MFGNUM_0;
            lbl_materiale.Text = s.ITMREF_0 + lot + " - " + s.ITMDES1_0;
            txt_lin.Text = s.MFGLIN_0.ToString("0.###");
            txt_UM.Text = isFamigliaStatistica ? s.PCU_0 : s.STU_0;
            //txt_qta.Text = isFamigliaStatistica ? (s.RESTO / s.PCUSTUCOE_0).ToString("0.###") : s.RESTO.ToString("0.###");
            txt_qta.Text = isFamigliaStatistica ? (s.QTYSTU_0 / s.PCUSTUCOE_0).ToString("0.###") : s.QTYSTU_0.ToString("0.###");
            txt_qta.Focus();

            txt_ordine.Text = "";
            txt_etichetta.Text = "";

            hf_MFGNUM.Value = s.MFGNUM_0;
            hf_MFGLIN.Value = s.MFGLIN_0.ToString();
            hf_BOMSEQ.Value = s.BOMSEQ_0.ToString();
            hf_OPE.Value = s.BOMOPE_0.ToString();
            hf_LOT.Value = Arr.Length > 1 ? Arr[1] : "";//s.LOT_0;
            hf_ITMREF.Value = s.ITMREF_0;
            hf_STU.Value = s.STU_0;
            hf_CURRENTQTY.Value = txt_qta.Text;
            hf_DISPQTY.Value = s.DISPO_0.ToString("0.###");
            hf_COEFF.Value = isFamigliaStatistica ? s.PCUSTUCOE_0.ToString("0.###") : 0.ToString("0.###");
            hf_TSICOD.Value = s.TSICOD_3;
            hf_LOC.Value = s.LOC_0;

        }


        protected void btn_conferma_Click(object sender, EventArgs e)
        {
            isFamigliaStatistica = hf_TSICOD.Value == Properties.Settings.Default.CONS_MATERIALI_TSICOD_TO_CHECK;

            if (txt_qta.Text.Trim() == "")
            {
                frm_error.Text = "Inserire una quantità";
                txt_qta.Focus();
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
                decimal COEFF = isFamigliaStatistica ? decimal.Parse(hf_COEFF.Value) : 1;

                if(QTY * COEFF > decimal.Parse(hf_DISPQTY.Value))
                {
                    frm_error.Text = "Quantità indicata superiore a quella disponibile a magazzino";
                    txt_qta.Focus();
                    return;
                }

                //if (QTY == decimal.Parse(hf_CURRENTQTY.Value))
                //{
                //    frm_error.Text = "Quantità non modificata";
                //    txt_qta.Focus();
                //    return;
                //}
                
                string _res = "";
                //var wsConsumoMateriali = false;
                var wsConsumoMateriali = cls_TermWS.WS_ConsumoMateriali(hf_FCY.Value, hf_MFGNUM.Value, hf_ITMREF.Value, hf_LOT.Value, txt_UM.Text.Trim().ToUpper(), QTY, hf_LOC.Value, hf_MFGLIN.Value.ToString(), hf_BOMSEQ.Value.ToString(), hf_OPE.Value.ToString(), out error, out _res);
                if (wsConsumoMateriali)
                {
                    frm_OK.Text = "Consumo Effettuato: " + _res;
                    pan_data.Visible = false;
                }
                else
                {
                    frm_error.Text = "Salvataggio Fallito: " + error;
                    txt_qta.Text = "";
                    txt_qta.Focus();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                frm_error.Text = "Salvataggio Fallito: " + error;
                txt_qta.Text = "";
                txt_qta.Focus();
            }
            btn_conferma.Enabled = true;

        }


        private void ResetHiddenFields()

        {
            hf_MFGNUM.Value = "";
            hf_MFGLIN.Value = 0.ToString("0.###");
            hf_BOMSEQ.Value = 0.ToString("0.###");
            hf_OPE.Value = 0.ToString("0.###");
            hf_LOT.Value = "";
            hf_ITMREF.Value = "";
            hf_STU.Value = "";
            hf_CURRENTQTY.Value = 0.ToString("0.###");
            hf_DISPQTY.Value = 0.ToString("0.###");
            hf_COEFF.Value = 0.ToString("0.###");
            hf_TSICOD.Value = "";
            hf_LOC.Value = "";
        }
    }
}