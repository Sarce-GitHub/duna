using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_WS_TOOLS_V9.WSX3_C9;
using X3_WS_TOOLS_V9;

namespace X3_TERMINALINI.magazzino
{
    public partial class CambioStockPallet : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
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
        }

        protected void txt_etichetta_TextChanged(object sender, EventArgs e)
        {
            lbl_Articolo.Text = "";
            frm_error.Text = "";
            if (!_SQL.obj_PALNUM_Check(_USR.FCY_0, txt_etichetta.Text.Trim().ToUpper()))
            {
                frm_error.Text = "Pallet " + txt_etichetta.Text + " non censito in " + _USR.FCY_0;
                txt_etichetta.Text = "";
                txt_etichetta.Focus();
                return;
            }

            var stockList = _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_etichetta.Text.Trim().ToUpper());

            if (_SQL.obj_STOLOC_Navetta_Check(_USR.FCY_0, stockList.Select(w => w.LOC_0.Trim().ToUpper()).FirstOrDefault()))
            {
                frm_error.Text = "Palnum " + txt_etichetta.Text.Trim().ToUpper() + " su ubicazione di tipo NAV non autorizzata ";
                txt_etichetta.Text = "";
                txt_etichetta.Focus();
                return;
            }


            if (_SQL.obj_STOLOC_Sped_Check(_USR.FCY_0, stockList.Select(w => w.LOC_0.Trim().ToUpper()).FirstOrDefault()))
            {
                frm_error.Text = "Palnum " + txt_etichetta.Text.Trim().ToUpper() + " su ubicazione di tipo SPED non autorizzata ";
                txt_etichetta.Text = "";
                txt_etichetta.Focus();
                return;
            }

            txt_etichetta.Text = txt_etichetta.Text.Trim().ToUpper();
            string _s = "";
            string _h = "";
            foreach (Obj_STOCK item in stockList)
            {
                string seakey = (!string.IsNullOrEmpty(item.SEAKEY_0) && item.SEAKEY_0 != " " && item.SEAKEY_0 != item.ITMREF_0 && managesSeakey) ? item.SEAKEY_0 : ""; //NB: SAGE salva uno spazio quando SEAKEY_0 è vuoto
                string lot = !string.IsNullOrEmpty(item.LOT_0) && item.LOT_0 != " " ? item.LOT_0 : "";
                string seakeyHeader = managesSeakey ? "SEAKEY" : "";

                if (_s == "") _s = "Ubicazione: <b>" + item.LOC_0 + "</b><br/>";

                if (_h == "")
                {
                    _h = "<div class=\"d-flex justify-content-between\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">ITMREF</b><b style=\"flex:1;\">LOTTO</b><b style=\"flex:1;\">" + seakeyHeader + "</b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><b style=\"margin-left:auto;flex:1;text-align:end\">UM</b><b class=\"ms-1\" style=\"margin-left:auto;flex:1;text-align:end\">QTA</b></div><br/></div>";
                    _s = _s + _h;
                }

                //_s = _s + "<div class=\"d-flex justify-content-between\"><b>" + item.ITMREF_0 + lot + seakey + "</b><span style=\"margin-left:auto\">" + item.STU_0 + " " + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span><br/></div>";
                _s = _s + "<div class=\"d-flex justify-content-between tabella-dati\"><div class=\"d-flex\" style=\"flex:3;white-space:nowrap\"><b style=\"flex:1;\">" + item.ITMREF_0 + "</b><b style=\"flex:1;\">" + lot + "</b><b style=\"flex:1;\">" + seakey + " </b></div><div class=\"d-flex\" style=\"flex:1;white-space:nowrap\"><span style=\"margin-left:auto;flex:1;text-align:end\">" + item.STU_0 + "</span><span class=\"ms-1 qta-cell\" style=\"margin-left:auto;flex:1;text-align:end\">" + item.QTYSTU_0.ToString("0.####") + " (" + item.STA_0 + ")</span></div><br/></div>";
            }
            lbl_Articolo.Text = _s;
            txt_destinazione.Focus();
        }

        protected void txt_destinazione_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            //btt_Esegui.Click += null;
            if (!CheckStolocUbic(txt_destinazione)) return;

            RunCambioStock();
        }

        private void RunCambioStock()
        {
            if (txt_etichetta.Text.Trim() == "")
            {
                frm_error.Text = "Pallet non inserita";
                return;
            }
            if (txt_destinazione.Text.Trim() == "")
            {
                frm_error.Text = "Ubicazione non inserita";
                return;
            }
            //
            if (!_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_destinazione.Text.Trim().ToUpper()))
            {
                frm_error.Text = "Ubicazione " + txt_destinazione.Text.Trim().ToUpper() + " non censita in " + _USR.FCY_0;
                txt_destinazione.Text = "";
                txt_destinazione.Focus();
                return;
            }

            if (_SQL.obj_STOLOC_Sped_Check(_USR.FCY_0, txt_destinazione.Text.Trim().ToUpper()))
            {
                frm_error.Text = "Ubicazione " + txt_destinazione.Text.Trim().ToUpper() + " di tipo SPED non autorizzata ";
                txt_destinazione.Text = "";
                txt_destinazione.Focus();
                return;
            }


            bool ok = true;
            foreach (Obj_STOCK _stock in _SQL.obj_STOCK_SearchByPalnum(_USR.FCY_0, txt_etichetta.Text))
            {
                bool ret = true;
                List<Xml_Data> src_Data = new List<Xml_Data>();
                src_Data.Add(new Xml_Data("IN_USR_X3", _USR.USR_X3_0, ""));
                src_Data.Add(new Xml_Data("IN_USR_TERM", _USR.USR_TERM_0, ""));
                src_Data.Add(new Xml_Data("IN_ITMREF", _stock.ITMREF_0, ""));
                src_Data.Add(new Xml_Data("IN_STOFCY", _USR.FCY_0, ""));
                src_Data.Add(new Xml_Data("IN_LOT", _stock.LOT_0.Trim(), ""));
                src_Data.Add(new Xml_Data("IN_SLO", _stock.SLO_0.Trim(), ""));
                src_Data.Add(new Xml_Data("IN_UBI_P", _stock.LOC_0, ""));
                src_Data.Add(new Xml_Data("IN_UBI_D", txt_destinazione.Text.Trim().ToUpper(), ""));
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
                txt_destinazione.Text = "";
                txt_etichetta.Text = "";
                lbl_Articolo.Text = "";
                txt_etichetta.Focus();
            }
           
        }

        protected void btt_Esegui_Click(object sender, EventArgs e)
        {
            RunCambioStock();
        }

        protected bool CheckStolocUbic(TextBox ubic)
        {
            if (!_SQL.obj_STOLOC_Check(_USR.FCY_0, ubic.Text.Trim().ToUpper()))
            {
                if (_SQL.obj_STOLOC_Navetta_Check(_USR.FCY_0, ubic.Text.Trim().ToString()))
                {
                    frm_error.Text = "Palnum " + txt_etichetta.Text.Trim().ToUpper() + " su ubicazione di tipo NAV non autorizzata ";
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
                frm_error.Text = "Palnum " + txt_etichetta.Text.Trim().ToUpper() + " su ubicazione di tipo SPED non autorizzata ";
                ubic.Text = "";
                ubic.Focus();
                return false;
            }
            return true;
        }
    }
}