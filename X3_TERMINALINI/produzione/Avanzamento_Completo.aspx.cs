using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.produzione
{
	public partial class Avanzamento_Completo : System.Web.UI.Page
	{
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string error = "";


        protected void Page_Load(object sender, EventArgs e)
		{
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            hf_FCY.Value = _USR.FCY_0;
            if (_USR.ABIL4_0 != 2) Response.Redirect("/", true); 

            frm_OK.Text = "";
            frm_error.Text = "";
            if (!IsPostBack) txt_Ricerca.Focus();

        }

        protected void txt_Ricerca_TextChanged(object sender, EventArgs e)
        {
            pan_data.Visible = false;
            ResetHiddenFields();

            string[] Arr = txt_Ricerca.Text.Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if(Arr.Length != 3)
            {
                frm_error.Text = "Formato barcode non valido";
                txt_Ricerca.Text = "";
                txt_Ricerca.Focus();
                return;
            }

            hf_ODP.Value = !string.IsNullOrEmpty(Arr[0]) ? Arr[0] : "";
            hf_QTY.Value = !string.IsNullOrEmpty(Arr[1]) ? decimal.Parse(Arr[1]).ToString("0.###") : "";
            hf_PALNUM.Value = !string.IsNullOrEmpty(Arr[2]) ? Arr[2] : "";

            if (txt_Ricerca.Text.Trim() != "")
            {
                Obj_MFGITM_ITMMASTER_PRODUZIONE s = _SQL.Obj_MFGITM_ITMMASTER_PRODUZIONE_Load(_USR.FCY_0, hf_ODP.Value.Trim().ToUpper(), out error);
            
                if(!string.IsNullOrEmpty(error))
                {
                    frm_error.Text = error;
                    txt_Ricerca.Text = "";
                    txt_Ricerca.Focus();
                    return;
                }
                
                bool showLastre = !string.IsNullOrEmpty(s.PCU_0) && s.PCU_0 != s.STU_0;
                pan_data.Visible = true;
                lbl_magazzino.Text = "MAGAZZINO " + _USR.FCY_0;
                lbl_ordine.Text = "Ordine N°: " + s.MFGNUM_0 + "; COEFF: " + s.PCUSTUCOE_0.ToString("0.#####");
                lbl_materiale.Text = s.ITMREF_0 + " - " + s.ITMDES1_0;
                txt_UM.Text = showLastre ? s.PCU_0 : s.STU_0;
                txt_PALNUM.Text = hf_PALNUM.Value;
                //txt_qta.Text = showLastre ? Math.Ceiling(s.LASTRE).ToString("0.###") : Math.Ceiling(s.RMNEXTQTY_0).ToString("0.###");
                txt_qta.Text = hf_QTY.Value;
                txt_qta.Focus();
                txt_Ricerca.Text = "";


                hf_MFGNUM.Value = s.MFGNUM_0;
                hf_ITMREF.Value = s.ITMREF_0;
                hf_LOT.Value = s.LOT_0;
                hf_UM.Value = txt_UM.Text;
                hf_COEFF.Value = s.PCUSTUCOE_0.ToString("0.#####");
                hf_STATUS.Value = txt_Stato.Text;
                hf_CURRENTQTY.Value = txt_qta.Text;

            }
        }

        protected void btn_conferma_Click(object sender, EventArgs e)
        {
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
                decimal COEFF = decimal.Parse(hf_COEFF.Value);

                //if (QTY == decimal.Parse(hf_CURRENTQTY.Value)) 
                //{
                //    frm_error.Text = "Quantità non modificata";
                //    txt_qta.Focus();
                //    return;
                //}
                string _res = "";
                var wsAvanzamentoCompleto = cls_TermWS.WS_AvanzamentoCompleto(hf_FCY.Value, hf_MFGNUM.Value, hf_ITMREF.Value, hf_LOT.Value, hf_UM.Value, QTY, COEFF, hf_PALNUM.Value, out error, out _res);
                if (wsAvanzamentoCompleto)
                {
                    frm_OK.Text = "Dichiarazione Effettuata: " + _res;
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

        }

        private void ResetHiddenFields()
        {
            hf_CURRENTQTY.Value = 0.ToString("0.###");
            hf_UM.Value = "";
            hf_STATUS.Value = "";
            hf_MFGNUM.Value = "";
            hf_COEFF.Value = 0.ToString("0.#####");
            hf_PALNUM.Value = "";
            hf_QTY.Value = 0.ToString("0.###");
        }

    }
}