﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using X3_TERMINALINI.magazzino;

namespace X3_TERMINALINI.spedizione
{
    public partial class Ordine_Spedizione_Righe : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _BPCORD = "";
        string _BPAADD = "";
        string _SOHNUM = "";
        DateTime _DATE_DA = DateTime.MinValue;
        DateTime _DATE_A = DateTime.MinValue;
        int _PACCHIDASPARARE = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL3_0 != 2) Response.Redirect("/", true);
            if (Request.QueryString["BC"] == null) Response.Redirect("Ordine_Spedizione.aspx", true);

            string[] Arr = Request.QueryString["BC"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (Arr.Length != 4 && Arr.Length != 5)
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }

            //
            _BPCORD = Arr[0];
            _BPAADD = Arr[1];
            if (!DateTime.TryParse(Arr[2].Substring(0, 4) + "-" + Arr[2].Substring(4, 2) + "-" + Arr[2].Substring(6, 2), out _DATE_DA))
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }
            if (!DateTime.TryParse(Arr[3].Substring(0, 4) + "-" + Arr[3].Substring(4, 2) + "-" + Arr[3].Substring(6, 2), out _DATE_A))
            {
                Response.Redirect("Ordine_Spedizione.aspx", true);
                return;
            }
            if (Arr.Length > 4) _SOHNUM = Arr[4];

            lbl_ClienteCod.Text = _BPCORD;
            lbl_ClienteAdd.Text = _BPAADD + " - " + _SQL.Obj_BPADDRESS_DESC(_BPCORD, _BPAADD);
            lbl_ClienteData.Text = _DATE_DA.ToString("dd/MM/yy") +" - " + _DATE_A.ToString("dd/MM/yy");
            Obj_Cookie.Set_String("prebolla-bc", Request.QueryString["BC"].Trim().ToUpper());

            //CONTEGGIO BARRE DA SPARARE
            if(!string.IsNullOrEmpty(_SOHNUM))
            {
                _PACCHIDASPARARE = _CountPacchiDaSparare(_SOHNUM, _USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A);
            }
            else
            {
                frm_error.Text = "N° Ordine non presente";
            }

            lbl_PacchiTot.Text = _PACCHIDASPARARE.ToString();
            Ricerca();
            btn_Tutto.PostBackUrl = "~/spedizione/Ordine_Spedizione_Righe_Conferma.aspx?BC=" + Request.QueryString["BC"].Trim().ToUpper();
            btn_Parziale.PostBackUrl = "~/spedizione/Ordine_Spedizione_Righe_Rimuovi.aspx?BC=" + Request.QueryString["BC"].Trim().ToUpper();
        }

        private void Ricerca()
        {
            string _PN = "*";
            string _PN_ITM = "*";
            pan_dati.Controls.Clear();
            //List<Obj_YTSORDINEAPE> Lista = _SQL.Obj_YTSORDINEAPE_Spedizione(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A, true).ToList();
            List< Obj_YTSALLORD> Lista = _SQL.Obj_YTSALLORD_Lista(_USR.FCY_0, _BPCORD, _BPAADD, _DATE_DA, _DATE_A)
                                         .Where(x => x.LOC_0 == Properties.Settings.Default.SPED_Ubic && (string.IsNullOrEmpty(_SOHNUM) || x.VCRNUM_0 == _SOHNUM))
                                            .OrderBy(x =>
                                            {
                                                if (long.TryParse(x.PALNUM_0, out long parsedValue))
                                                {
                                                    return parsedValue;
                                                }
                                                else
                                                {
                                                    return long.MaxValue; // TO ENSURE NON PARSABLE VALUES GO LAST
                                                }
                                            })

                                         .ToList();

            lbl_pacchiPreparati.Text = Lista.Count.ToString();

            bool allPackagesReady = CheckAllPackagesAreReady(Lista.Count);
            if (!allPackagesReady)
            {
                SpedizioneIncompleta();
            }
            else 
            {
                CounterPallet.Attributes["class"] = "text-success";
            }

            if (Lista.Count > 0)
            {
                lbl_ClienteCod.Text = Lista[0].BPCORD_0;
                HtmlGenericControl _d = new HtmlGenericControl();

                string _h = "";
                int idx = 0;
                string _c = "";

                _h = "<div class=\"row bg-head font-small\">";
                _h = _h + "<div class=\"col-1\">PN</div>";
                _h = _h + "<div class=\"col-7\">Articolo</div>";
                _h = _h + "<div class=\"col-4 text-end\">Qta Ord.</div>";

                _h = _h + "</div>";
                _d.InnerHtml = _d.InnerHtml + _h;

                foreach (Obj_YTSALLORD _i in Lista)
                {
                    if (_PN!= _i.PALNUM_0)
                    {
                        _PN_ITM = "";
                        _PN = _i.PALNUM_0;
                        _d.InnerHtml = _d.InnerHtml + "<div class=\"row bg-ok\"><div class=\"col-12\"><b><i>" + _PN + "</i></b></div></div>";
                    }
                    //
                    if (_PN_ITM!= _i.PALNUM_0 + "-" +_i.ITMREF_0)
                    {
                        _PN_ITM = _i.PALNUM_0 + "-" + _i.ITMREF_0;
                        //
                        Obj_STOCK OUT_Obj = new Obj_STOCK();
                        if ( _SQL.obj_PALNUM_GetStock(_USR.FCY_0, _i.PALNUM_0, _i.ITMREF_0, out OUT_Obj))
                        { 
                            //
                            _c = ((idx % 2) == 1 ? "bg-alt" : "");
                            _h = "<div class=\"row " + _c + " \">";
                            _h = _h + "<div class=\"col-1\">&nbsp;</div>";
                            _h = _h + "<div class=\"col-7\"><b>" + _i.ITMREF_0 + "</b></div>";
                            _h = _h + "<div class=\"col-4 text-end\"><b>" + OUT_Obj.QTYPCU_0.ToString("0.##") + " " + _i.PCU_0 + "</b>&nbsp;&nbsp;</div>";
                            _h = _h + "</div>";
                            _d.InnerHtml = _d.InnerHtml + _h;
                        }
                    }
                }

                pan_dati.Controls.Add(_d);
            }

        }

        private int _CountPacchiDaSparare(string nOrdine, string FCY, string BPCORD, string BPADD, DateTime dataDa, DateTime dataA)
        {
            try
            {
                return _SQL.CountPacchiDaSpedire(nOrdine, FCY, BPCORD, BPADD, dataDa, dataA);
            }
            catch(Exception e) 
            {
                frm_error.Text = e.Message;
                return -1;
            }
        }

        private bool CheckAllPackagesAreReady(int pacchiPreparati)
        {
            return pacchiPreparati == _PACCHIDASPARARE;
        }

        protected void SpedizioneIncompleta()
        {
            frm_error.Text = "Completare la lettura dei bancali per procedere";
            //btn_Tutto.Enabled = false;
        }
    }
}