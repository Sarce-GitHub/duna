using System;
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
    public partial class EntrataDiversa : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL1_0 != 2) Response.Redirect("/", true);
            if (!Page.IsPostBack)
            {
                txt_Articolo.Text = "&nbsp;";
                txt_Lotto.Text = "&nbsp;";
                txt_SottoLotto.Text = "&nbsp;";
                txt_UM.Text = "&nbsp;";
            }
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            txt_Articolo.Text = "&nbsp;";
            txt_Lotto.Text = "&nbsp;";
            txt_SottoLotto.Text = "&nbsp;";
            txt_UM.Text = "&nbsp;";

            if (txt_Etichetta.Text.Trim() != "")
            {
                string[] ArrEt = txt_Etichetta.Text.ToUpper().Split(Properties.Settings.Default.Etic_Split.ToCharArray());
                //
                string _ITMREF = ArrEt[0];
                string _LOT = "";
                if (ArrEt.Length > 1) _LOT = ArrEt[1];
                string _SLO = "";
                if (ArrEt.Length > 2) _SLO = ArrEt[2];
                //
                Obj_ITMMASTER _i = _SQL.obj_ITMMASTER_Load(_ITMREF);
                if (_i.ITMREF_0 != _ITMREF)
                {
                    frm_error.Text = "Articolo non trovato";
                    return;
                }
                //
                txt_Articolo.Text = _i.ITMREF_0;
                if (_LOT!="") txt_Lotto.Text = _LOT;
                if (_SLO!="") txt_SottoLotto.Text = _SLO;
                txt_UM.Text = _i.STU_0;
            }
        }

        protected void txt_Pallet_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
        }

        protected void txt_Qta_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            decimal _d = 0;
            if (decimal.TryParse(txt_Qta.Text, out _d))
            {
                if (_d > 0)
                    txt_Qta.Text = _d.ToString("0.###");
                else
                {
                    txt_Qta.Text = "";
                    frm_error.Text = "Numero non valido";
                    txt_Qta.Focus();
                }
            }
            else
            {
                txt_Qta.Text = "";
                frm_error.Text = "Numero non valido";
                txt_Qta.Focus();
            }
        }


        protected void btn_conferma_Click(object sender, EventArgs e)
        {
            frm_error.Text = "";
            decimal _d = 0;
            if (!decimal.TryParse(txt_Qta.Text, out _d)) return;
            //
            List<Xml_Data> src_Data = new List<Xml_Data>();
            src_Data.Add(new Xml_Data("IN_STOFCY", _USR.FCY_0, ""));
            src_Data.Add(new Xml_Data("IN_ITMREF", txt_Articolo.Text, ""));
            src_Data.Add(new Xml_Data("IN_LOT", (txt_Lotto.Text != "&nbsp;" ? txt_Lotto.Text.Trim() : ""), ""));
            if (txt_SottoLotto.Text != "&nbsp;" && txt_SottoLotto.Text.Trim() != "") src_Data.Add(new Xml_Data("IN_SLO",  txt_SottoLotto.Text.Trim(), ""));
            if (txt_Pallet.Text.Trim()!="") src_Data.Add(new Xml_Data("IN_PALNUM", txt_Pallet.Text.Trim(), ""));
            src_Data.Add(new Xml_Data("IN_UBI_D", txt_Ubicazione.Text, ""));
            src_Data.Add(new Xml_Data("IN_STATO", "A", ""));
            src_Data.Add(new Xml_Data("IN_PCU", txt_UM.Text, ""));
            src_Data.Add(new Xml_Data("IN_QTYPCU", _d.ToString("0.####"), "Decimal"));

            CAdxResultXml result = new CAdxResultXml();
            string error = "";
            try
            {

                if (cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YTS_ENTDIV", "GRP1", src_Data, out error, out result))
                {
                    XElement element = XElement.Parse(result.resultXml);
                    XElement GRP1 = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP1");
                    if (GRP1 != null)
                    {
                        IEnumerable<XElement> _testata = GRP1.Elements();
                        if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") == 1)
                        {
                            frm_OK.Text = "Entrata confermata";
                            txt_Etichetta.Text = "";
                            txt_Articolo.Text = "&nbsp;";
                            txt_Lotto.Text = "&nbsp;";
                            txt_SottoLotto.Text = "&nbsp;";
                            txt_UM.Text = "&nbsp;";
                            txt_Pallet.Text = "";
                            txt_Qta.Text = "";
                            return;
                        }
                        else
                        {
                            error = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                        }

                    }
                    else
                    {
                        error = "ERRORE WS GENERICO";
                    }
                }
                else
                {
                    error = error + "<br/>Nessun risultato";
                    if (result.messages.Length > 0) error = error + "<br/>" + result.messages[0].message;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            //
            frm_error.Text = error;
        }

        protected void txt_Ubicazione_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            //CHECK UBICAZIONE
            if (_SQL.obj_STOLOC_Check(_USR.FCY_0, txt_Ubicazione.Text.Trim().ToUpper()))
            {
                //UBICAZIONE VERIFICATA
                txt_Ubicazione.Text = txt_Ubicazione.Text.Trim().ToUpper();
            }
            else
            {
                txt_Ubicazione.Text = "";
                frm_error.Text = "Ubicazione non valida";
            }
        }
    }
}