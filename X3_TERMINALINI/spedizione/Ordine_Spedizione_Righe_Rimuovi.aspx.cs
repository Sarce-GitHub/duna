using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using X3_TERMINALINI.magazzino;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Spedizione_Righe_Rimuovi : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _BPCORD = "";
        string _BPAADD = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;
        List<string> _PALNUM = new List<string>();
        List<string> _REMOVE_PALNUM = new List<string>();
        string _PN_ITM = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/", true);
            if (Request.QueryString["BC"] == null) Response.Redirect("Ordine_Spedizione.aspx", true);

            string[] Arr = Request.QueryString["BC"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4)
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }

            //
            _BPCORD = Arr[0];
            _BPAADD = Arr[1];
            if (!DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2),  out _DATE_DA))
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }
            if (!DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_A))
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }

            if (Request.QueryString["PAL"]!=null)
            {
                _REMOVE_PALNUM = Request.QueryString["PAL"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            lbl_ClienteCod.Text = _BPCORD;
            lbl_ClienteAdd.Text = _BPAADD + " - " + _SQL.Obj_BPADDRESS_DESC(_BPCORD, _BPAADD);
            lbl_ClienteData.Text = _DATE_DA.ToString("dd/MM/yy") +"/"+ _DATE_A.ToString("dd/MM/yy");
            _PALNUM = new List<string>();
            Obj_Cookie.Set_String("prebolla-bc", Request.QueryString["BC"].Trim().ToUpper());
            Ricerca();
            //
            btn_Indietro.PostBackUrl = "Ordine_Spedizione_Righe.aspx?BC=" + Request.QueryString["BC"];
            txt_Etichetta.Focus();
            //

        }

        private void Ricerca()
        {
            string _PN = "*";
            pan_dati.Controls.Clear();
            //List<Obj_YTSORDINEAPE> Lista = _SQL.Obj_YTSORDINEAPE_Spedizione(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, true).ToList();
            List<Obj_YTSALLORD> Lista = _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A).ToList();

            if (Lista.Count > 0)
            {
                lbl_ClienteCod.Text = Lista[0].BPCORD_0;
                HtmlGenericControl _d = new HtmlGenericControl();

                string _h = "";
                int idx = 0;
                string _c = "";

                _h = "";
                _h = _h + "<div class=\"row bg-head font-small\">";
                _h = _h + "<div class=\"col-1\">PN</div>";
                _h = _h + "<div class=\"col-7\">Articolo</div>";
                _h = _h + "<div class=\"col-4 text-end\">Qta Ord.</div>";

                _h = _h + "</div>";
                _d.InnerHtml = _d.InnerHtml + _h;

                foreach (Obj_YTSALLORD _i in Lista)
                {
                    if (_PN != _i.PALNUM_0)
                    {
                        _PN = _i.PALNUM_0;
                        _h = "<div class=\"row " + (!_REMOVE_PALNUM.Contains(_PN) ? "bg-ok" : "bg-ko") + "\"><div class=\"col-12\"><b><i>" + _PN + "</i></b></div></div>";
                        _PALNUM.Add(_PN);
                        _d.InnerHtml = _d.InnerHtml + _h;
                    }

                    //
                    if (_PN_ITM != _i.PALNUM_0 + "-" + _i.ITMREF_0)
                    {
                        _PN_ITM = _i.PALNUM_0 + "-" + _i.ITMREF_0;
                        //
                        Obj_STOCK OUT_Obj = new Obj_STOCK();
                        if (_SQL.obj_PALNUM_GetStock(_USR.FCY_0, _i.PALNUM_0, _i.ITMREF_0, out OUT_Obj))
                        {
                            string _css = "";
                            if (_REMOVE_PALNUM.Contains(_PN)) _css = "strike";
                            //
                            _c = ((idx % 2) == 1 ? "bg-alt" : "");
                            _h = "<div class=\"row " + _c + " \">";
                            _h = _h + "<div class=\"col-1 "+ _css + "\">&nbsp;</div>";
                            _h = _h + "<div class=\"col-7 "+ _css + "\"><b>" + _i.ITMREF_0 + "</b></div>";
                            _h = _h + "<div class=\"col-4  "+ _css + " text-end\"><b>" + OUT_Obj.QTYSTU_0.ToString("0.##") + " " + _i.STU_0 + "</b>&nbsp;&nbsp;</div>";
                            _h = _h + "</div>";
                            _d.InnerHtml = _d.InnerHtml + _h;
                        }
                    }
                }

                pan_dati.Controls.Add(_d);
            }

        }

        protected void btn_Tutto_Click(object sender, EventArgs e)
        {
            //YTS_SO2SD
            string _err = "";
            frm_OK.Text = "";
            frm_error.Text = "";
            foreach (var item in _REMOVE_PALNUM)
            {
                _PALNUM.Remove(item);
            }
             
            if (_PALNUM.Count > 0) { 

                if (cls_TermWS.WS_BollaDaOrdine(cls_Tools.Get_User(), _BPCORD, _BPAADD, _PALNUM, out _err))
                {
                    btn_Tutto.Enabled = false;
                    frm_OK.Text = "BOLLA INSERITA: " + _err;

                    string _body = "Nuova bolla terminalini:\r\n";
                    _body = _body + "ID BOLLA: " + _err + "\r\n";
                    _body = _body + "DATA BOLLA: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\r\n";
                    _body = _body + "CLIENTE: " + lbl_ClienteCod.Text + "\r\n";
                    _body = _body + "INDIRIZZO: " + _BPAADD + " - " + _SQL.Obj_BPADDRESS_DESC(_BPCORD, _BPAADD) + "\r\n";
                    //
                    cls_Tools.SendMail(X3_TERMINALINI.Properties.Settings.Default.MAIL_FROM, X3_TERMINALINI.Properties.Settings.Default.MAIL_TO_DDT, "Nuova bolla terminalini: " + _err, _body, false);
                }
                else
                {
                    btn_Tutto.Enabled = true;
                    frm_error.Text = "BOLLA NON INSERITA: " + _err;
                }
            }
            else
            {
                btn_Tutto.Enabled = true;
                frm_error.Text = "NESSUN PALLET DA SPEDIRE";
            }

            Ricerca();
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            string _p = txt_Etichetta.Text.Trim().ToUpper();
            
            //UBICAZIONE INESISTENTE, PROVO IL PALNUM
            if (_PALNUM.Contains(_p))
            {
                if (_REMOVE_PALNUM.Contains(_p))
                    _REMOVE_PALNUM.Remove(_p);
                else
                    _REMOVE_PALNUM.Add(_p);

                //
                string QP = "";
                if (_REMOVE_PALNUM.Count > 0)
                {
                    QP = "&PAL=";
                    foreach (string p in _REMOVE_PALNUM) { QP += p + "|"; }
                    QP = QP.Substring(0, QP.Length - 1);
                }
                Response.Redirect("Ordine_Spedizione_Righe_Rimuovi.aspx?BC=" + Request.QueryString["BC"] + QP);
            }
            else
            {
                frm_error.Text = "Pallet <b>"+ _p + "</b> non in elenco";
            }

            txt_Etichetta.Text = "";
            txt_Etichetta.Focus();
        }
    }
}