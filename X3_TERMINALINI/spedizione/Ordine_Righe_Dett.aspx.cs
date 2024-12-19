using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Righe_Dett : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        Obj_STOCK_ETIC _Etic = new Obj_STOCK_ETIC();
        Obj_YTSORDAPE _Art_Ordine = new Obj_YTSORDAPE();
        Obj_STOCK _Stock = new Obj_STOCK();
        //
        string _BPCORD = "";
        string _BPAADD = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;
        //
        DateTime _DATE_COM = DateTime.MinValue;
        string _SOHNUM = "";

        private string connectionSQL = "Data Source=" + Properties.Settings.Default.SQL_IP + ";Initial Catalog=" + Properties.Settings.Default.SQL_Catalog + ";User ID=" + Properties.Settings.Default.SQL_User + ";Password=" + Properties.Settings.Default.SQL_Psw + ";";
        public string error = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/Menu.aspx", true);
            if (Request.QueryString["LOT"] == null) Response.Redirect("Ordine_Righe_Ordine_Singolo.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
            //LOT=CFB08671AAA-L2430005566
            
            string[] Arr = Obj_Cookie.Get_String("prebolla-bc").ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4 && Arr.Length != 5)
            {
                Response.Redirect("Ordine_Righe_Ordine_Singolo.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
                return;
            }
            //
            _BPCORD = Arr[0];
            _BPAADD = Arr[1];
            DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2), out _DATE_DA);
            DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_A);
            _Etic = new Obj_STOCK_ETIC(Request.QueryString["LOT"].ToString().Trim().ToUpper());
            _Art_Ordine = _SQL.Obj_YTSORDAPE_Articolo(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, _Etic.ITMREF);
            _SOHNUM = Arr[4];

            if (_Art_Ordine.ITMREF_0=="")
            {
                Response.Redirect("Ordine_Righe_Ordine_Singolo.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
                return;
            }
            //
            lbl_Articolo.Text = _Etic.ITMREF + " - " + _Etic.LOT;
            btn_Annulla.Visible = false;
            btn_Conferma.Visible = false;
            txt_Partenza.Focus();
        }

        protected void btn_Indietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Righe_Ordine_Singolo.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
        }

        protected void txt_Partenza_TextChanged(object sender, EventArgs e)
        {
            txt_Quantita.Enabled = false;
            HF_Tipo_P.Value = "";
            if (txt_Partenza.Text.Trim() != "")
            {
                //CHECK UBICAZIONE
                _Stock = _SQL.obj_STOCK_CheckPick(_USR.FCY_0, _Etic.ITMREF, _Art_Ordine.YPCU_0, txt_Partenza.Text.Trim().ToUpper(), _Etic.LOT, _Etic.SLO, "");
                if (_Stock.ITMREF_0== _Etic.ITMREF)
                {
                    //UBICAZIONE VERIFICATA
                    txt_Partenza.Text = txt_Partenza.Text.Trim().ToUpper();
                    HF_Tipo_P.Value = "U";
                    txt_Quantita.Enabled = true;
                }
                else
                {

                    //UBICAZIONE INESISTENTE, PROVO IL PALNUM
                    if (_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_Partenza.Text.Trim().ToUpper()))
                    {
                        //PALLET VERIFICATA
                        txt_Partenza.Text = txt_Partenza.Text.Trim().ToUpper();
                        HF_Tipo_P.Value = "P";
                        txt_Quantita.Enabled = true;
                    }
                }

                // CHECK
                if (HF_Tipo_P.Value == "")
                {
                    lbl_Stato.Text = "";
                    lbl_UM.Text = "";
                    lbl_richiesto.Text = "";
                    lbl_disp.Text = "";
                    txt_Quantita.Text = "";
                    frm_error.Text = "Ubicazione/Pallet non trovato";
                    txt_Quantita.Enabled = false;
                    txt_Partenza.Text = "";
                    txt_Partenza.Focus();
                }
                else
                {
                    //Obj_YTSORDINEAPE _o = _Art_Ordine.Where(w => w.SAU_0 == _Stock.PCU_0 && w.QTYPREP_0<(w.QTY_0 - w.DLVQTY_0)).OrderBy(o=>o.SHIDAT_0).FirstOrDefault();
                    //if (_o==null) Response.Redirect("Ordine_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"));
                    frm_error.Text = "";
                    lbl_Stato.Text = _Stock.STA_0;
                    lbl_UM.Text = _Stock.PCU_0;
                    lbl_richiesto.Text = (_Art_Ordine.YQTYPCU_0 - _Art_Ordine.DLVQTY_0 - (_Art_Ordine.QTYPREP_0) / _Art_Ordine.YPCUSTUCOE_0).ToString("0.###");
                    lbl_disp.Text = _Stock.QTYPCU_0.ToString("0.###");
                    txt_Quantita.Text = "";
                    txt_Quantita.Focus();
                }
            }
        }



        protected void btt_Esegui_Click(object sender, EventArgs e)
        {

            if (txt_Partenza.Text.Trim() != "")
            {
                decimal _d = 0;
                if (decimal.TryParse(txt_Quantita.Text, out _d))
                {
                    if (_d > 0 && _d <= decimal.Parse(lbl_richiesto.Text.Replace(",", ".")) && _d <= decimal.Parse(lbl_disp.Text.Replace(",", ".")))
                    {
                        txt_Quantita.Text = _d.ToString("0.##");
                    }
                    //else
                    //{

                    //    frm_error.Text = "Quantità non congrua";
                    //    //txt_Quantita.Text = "";
                    //    txt_Quantita.Focus();
                    //    lbl_Alert.Text = "Quantità eccedente gli ordini previsti: Aggiungere nuovi ordini?";
                    //    lbl_Alert.Visible = true;
                    //    btn_Conferma.Visible = true;
                    //    btn_Annulla.Visible = true;
                    //    return;
                    //}
                }
                else
                {

                    frm_error.Text = "Quantità non congrua";
                    txt_Quantita.Text = "";
                    txt_Quantita.Focus();

                    //GESTIONE QTA MAGGIORE DELLA RIMANENTE
                    //lbl_Alert.Text = "Quantità eccedente gli ordini previsti: Aggiungere nuovi ordini?";
                    //lbl_Alert.Visible = true;
                    //btn_Conferma.Visible = true;
                    //btn_Annulla.Visible = true;
                    return;
                }
            }
            else
            {
                frm_error.Text = "Quantità non valida";
                txt_Quantita.Text = "";
                txt_Quantita.Focus();
                return;
            }



            decimal QTY = decimal.Parse(txt_Quantita.Text);

            using (SqlConnection conn = new SqlConnection(connectionSQL))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    _Stock = _SQL.obj_STOCK_CheckPick(_USR.FCY_0, _Etic.ITMREF, _Art_Ordine.YPCU_0, txt_Partenza.Text.Trim().ToUpper(), _Etic.LOT, _Etic.SLO, "");
                    foreach (Obj_YTSORDAPE _ord in _SQL.Obj_YTSORDAPE_Ordini(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, _Etic.ITMREF, _Stock.PCU_0))
                    {
                        //if (_ord.QTY_MANC > 0 && QTY > 0)
                        //{
                        //    decimal _QTY_PREP = (QTY > _ord.QTY_MANC ? _ord.QTY_MANC : QTY);
                        //    if (Cambio(_QTY_PREP))
                        //    {
                        //        bool ok = cls_TermWS.WS_AllocaDett(_ord.SOHNUM_0, _ord.SOPLIN_0.ToString(), _USR.FCY_0, _Stock.LOT_0, Obj_Cookie.Get_String("prebolla-palnum"), Properties.Settings.Default.SPED_Ubic, _QTY_PREP, out error);
                        //        if (!ok) throw new Exception(error);

                        //        QTY = QTY - _ord.QTY_MANC;
                        //    }
                        //    else
                        //    {
                        //        transaction.Rollback();
                        //        return;
                        //    }
                        //}

                        //var QTYDISALL = _ord.QTYSTU_0 * -1;
                        var QTYDISALL = _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA,_DATE_A)
                            .Where(w => w.VCRNUM_0 == _SOHNUM 
                                    && w.SOPLIN_0 == _ord.SOPLIN_0 
                                    && w.SOQSEQ_0 == _ord.SOQSEQ_0 
                                    && w.ITMREF_0 == _ord.ITMREF_0 
                                    && w.LOT_0 == _Etic.LOT 
                                    && w.LOC_0 == txt_Partenza.Text.Trim().ToUpper())
                                    .FirstOrDefault().QTYSTU_0 * -1;

                        if (cls_TermWS.WS_AllocaDett(_ord.SOHNUM_0, _ord.SOPLIN_0.ToString(), _USR.FCY_0, _Etic.LOT, _ord.PALNUM_0, txt_Partenza.Text.Trim().ToUpper(), QTYDISALL, out error))
                        {
                            Obj_STOCK _stock = new Obj_STOCK()
                            {
                                ITMREF_0 = _ord.ITMREF_0,
                                LOT_0 = _Etic.LOT,
                                STA_0 = "A",
                                PALNUM_0 = _ord.PALNUM_0,
                                LOC_0 = txt_Partenza.Text.Trim().ToUpper(),
                                SLO_0 = _Etic.SLO,
                                PCU_0 = lbl_UM.Text.Trim().ToUpper(),
                            };
                        }
                        else
                        { 
                            throw new Exception("DISALLOCAZIONE FALLITA " + error);
                        }

                        //if (!Cambio(_ord.QTYSTU_0, _ord.SAU_0))
                        if(!Cambio(QTY))
                        {
                            throw new Exception("CAMBIO STOCK FALLITO" + error);
                        }

                        bool ok = cls_TermWS.WS_AllocaDett(_ord.SOHNUM_0, _ord.SOPLIN_0.ToString(), _USR.FCY_0, _Stock.LOT_0, Obj_Cookie.Get_String("prebolla-palnum"), Properties.Settings.Default.SPED_Ubic, (QTYDISALL * -1), out error);
                        if (!ok) throw new Exception("ALLOCAZIONE FALLITA " + error);

                    }

                    //
                    transaction.Commit();
                    Response.Redirect("Ordine_Righe.aspx?BC=" + Obj_Cookie.Get_String("prebolla-bc"), false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    return;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    error = ex.Message;
                    frm_error.Text = "Salvataggio Preparazione Fallito " + ex;
                }
            }


        }

        private bool Cambio(decimal IN_QTY)
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
            _UBI_P = txt_Partenza.Text.ToUpper().Trim();
            if (_SQL.obj_STOCK_Load(_USR.FCY_0, _UBI_P, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", out _Stock)) _PCU = _Stock.PCU_0.Trim();

            // PALLET
            if (HF_Tipo_P.Value == "P")
            {
                _PAL_P = txt_Partenza.Text.ToUpper().Trim();
                if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PAL_P, out _Stock))
                {
                    _UBI_P = _Stock.LOC_0;
                    _Etic.LOT = _Stock.LOT_0;
                    _Etic.SLO = _Stock.SLO_0;
                    _PCU = _Stock.PCU_0.Trim();
                }

            }

            _PAL_D = Obj_Cookie.Get_String("prebolla-palnum");
            _UBI_D = Properties.Settings.Default.SPED_Ubic;


            if (!cls_TermWS.WS_CambioStock(_USR, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, _PCU, IN_QTY, lbl_Stato.Text, _PAL_P, _UBI_P, lbl_Stato.Text, _PAL_D, _UBI_D, out _err))
            {
                //Errore
                frm_error.Text = _err;
                txt_Partenza.Focus();
                return false;
            }
            //
            return true;
        }

        private bool Cambio(decimal IN_QTY, string UM)
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
            _UBI_P = txt_Partenza.Text.ToUpper().Trim();
            if (_SQL.obj_STOCK_Load(_USR.FCY_0, _UBI_P, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, "", out _Stock)) _PCU = UM.Trim().ToUpper();

            // PALLET
            if (HF_Tipo_P.Value == "P")
            {
                _PAL_P = txt_Partenza.Text.ToUpper().Trim();
                if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _PAL_P, out _Stock))
                {
                    _UBI_P = _Stock.LOC_0;
                    _Etic.LOT = _Stock.LOT_0;
                    _Etic.SLO = _Stock.SLO_0;
                    _PCU = UM.Trim().ToUpper();
                }

            }

            _PAL_D = Obj_Cookie.Get_String("prebolla-palnum");
            _UBI_D = Properties.Settings.Default.SPED_Ubic;


            if (!cls_TermWS.WS_CambioStock(_USR, _Etic.ITMREF, _Etic.LOT, _Etic.SLO, _PCU, IN_QTY, lbl_Stato.Text, _PAL_P, _UBI_P, lbl_Stato.Text, _PAL_D, _UBI_D, out _err))
            {
                //Errore
                frm_error.Text = _err;
                txt_Partenza.Focus();
                return false;
            }
            //
            return true;
        }


        protected void btn_Conferma_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine_Ridistribuzione.aspx?ITEMREF=" + _Etic.ITMREF + "&QTAMAG=" +  (int.Parse(txt_Quantita.Text) - int.Parse(lbl_richiesto.Text)) + "&LOT=" + Request.QueryString["LOT"]);
        }

        protected void btn_Annulla_Click(object sender, EventArgs e)
        {
            btn_Annulla.Visible = false;
            btn_Conferma.Visible = false;
            lbl_Alert.Visible = false;
        }
    }
}