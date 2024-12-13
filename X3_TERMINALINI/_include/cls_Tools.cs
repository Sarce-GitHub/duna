﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Web;
using System.Xml.Linq;
using X3_WS_TOOLS_V9;
using X3_WS_TOOLS_V9.WSX3_C9;

namespace X3_TERMINALINI
{
    public class cls_Tools
    {
        public static string Get_Vers()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return Properties.Settings.Default.BaseVers + "." + version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString("00000");
        }

        public static WS_Param Get_WS_Param()
        {
            WS_Param _ws = new WS_Param()
            {
                WS_Lan = Properties.Settings.Default.WS_Lan,
                WS_Pool = Properties.Settings.Default.WS_Pool,
                WS_Pws = Properties.Settings.Default.WS_Pws,
                WS_Url = Properties.Settings.Default.WS_URL,
                WS_User = Properties.Settings.Default.WS_User
            };
            //
            return _ws;
        }

        public static bool Check_User()
        {
            string _base = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-base"), Properties.Settings.Default.Passphrase);
            string _abil = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-abil"), Properties.Settings.Default.Passphrase);
            //
            Obj_YTSUTX _utx = new Obj_YTSUTX(_base.Split('|'), _abil.Split('|'));
            if (_utx.USR_TERM_0 != "" && _utx.USR_X3_0 != "" && _utx.FCY_0 != "" && _utx.ATTIVO_0 == 2) return true;
            return false;
        }

        public static Obj_YTSUTX Get_User()
        {
            string _base = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-base"), Properties.Settings.Default.Passphrase);
            string _abil = cls_Crypto.DecryptString(Obj_Cookie.Get_String("login-abil"), Properties.Settings.Default.Passphrase);
            //
            Obj_YTSUTX _utx = new Obj_YTSUTX(_base.Split('|'), _abil.Split('|'));
            return _utx;
        }

        public static string SendMail(string IN_from, string IN_to, string IN_Subject, string IN_Body, bool IN_IsBodyHtml)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(IN_from);
                //
                foreach (string itm in IN_to.Split(';'))
                {
                    if (itm != "") message.To.Add(itm);
                }

                message.SubjectEncoding = Encoding.ASCII;
                message.Subject = IN_Subject;
                message.Body = IN_Body;
                message.IsBodyHtml = IN_IsBodyHtml;

                SmtpClient client = new SmtpClient(X3_TERMINALINI.Properties.Settings.Default.MAIL_SMTP);
                client.UseDefaultCredentials = true;
                if (X3_TERMINALINI.Properties.Settings.Default.MAIL_SMTP_USER!="")
                    client.Credentials = new System.Net.NetworkCredential(X3_TERMINALINI.Properties.Settings.Default.MAIL_SMTP_USER, X3_TERMINALINI.Properties.Settings.Default.MAIL_SMTP_PASSWORD);
                
                //client.EnableSsl = true;
                client.Port = X3_TERMINALINI.Properties.Settings.Default.MAIL_SMTP_PORT;
                client.Send(message);
                client = null;
                message = null;
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class cls_TermWS
    {
         
        public static bool WS_CambioStock(Obj_YTSUTX _USR, string _ITMREF, string _LOT, string _SLO, string _PCU, decimal _QTY, string _STA_P, string _PALNUM_P, string _UBI_P, string _STA_D, string _PALNUM_D, string _UBI_D, out string _err)
        {


            _err = "";
            List<Xml_Data> src_Data = new List<Xml_Data>();
            src_Data.Add(new Xml_Data("IN_USR_X3", _USR.USR_X3_0, ""));
            src_Data.Add(new Xml_Data("IN_USR_TERM", _USR.USR_TERM_0, ""));
            src_Data.Add(new Xml_Data("IN_STOFCY", _USR.FCY_0, ""));
            src_Data.Add(new Xml_Data("IN_ITMREF", _ITMREF, ""));
            src_Data.Add(new Xml_Data("IN_LOT", _LOT, ""));
            src_Data.Add(new Xml_Data("IN_SLO", _SLO, ""));
            src_Data.Add(new Xml_Data("IN_UBI_P", _UBI_P, ""));
            src_Data.Add(new Xml_Data("IN_UBI_D", _UBI_D, ""));
            src_Data.Add(new Xml_Data("IN_STA_P", _STA_P, ""));
            src_Data.Add(new Xml_Data("IN_STA_D", _STA_D, ""));
            src_Data.Add(new Xml_Data("IN_QTY", _QTY.ToString("0.###"), "Decimal"));
            src_Data.Add(new Xml_Data("IN_PCU", _PCU, ""));
            src_Data.Add(new Xml_Data("IN_PALNUM_P", _PALNUM_P, ""));
            src_Data.Add(new Xml_Data("IN_PALNUM_D", _PALNUM_D, ""));
            src_Data.Add(new Xml_Data("IN_VCRNUM", "", ""));

            CAdxResultXml result = new CAdxResultXml();
            try
            {

                if (cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YTS_CMBSTK", "GRP1", src_Data, out _err, out result))
                {
                    XElement element = XElement.Parse(result.resultXml);
                    XElement GRP1 = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP1");
                    if (GRP1 != null)
                    {
                        IEnumerable<XElement> _testata = GRP1.Elements();
                        if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") == 1)
                        {
                            return true;
                        }
                        else
                        {
                            _err = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                            return false;
                        }

                    }
                    else
                    {
                        _err = "ERRORE WS GENERICO";
                        return false;
                    }
                }
                else
                {
                    _err = _err + "<br/>Nessun risultato";
                    if (result.messages.Length > 0) _err = _err + "<br/>" + result.messages[0].message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                return false;
            }

        }

        //Subprog SDH_SOH2SDH(IN_USR_X3, IN_USR_TERM, IN_FCY, IN_BPCORD, IN_BPAADD, IN_DATE, IN_PALNUM, OUT_OK, OUT_MESSAGE)
        public static bool WS_BollaDaOrdine(Obj_YTSUTX _USR, string _BPCORD, string _BPAADD, List<string> _PALNUM, out string _err)
        {


            _err = "";
            List<Xml_Data> src_Data = new List<Xml_Data>();
            src_Data.Add(new Xml_Data("IN_USR_X3", _USR.USR_X3_0, ""));
            src_Data.Add(new Xml_Data("IN_USR_TERM", _USR.USR_TERM_0, ""));
            src_Data.Add(new Xml_Data("IN_FCY", _USR.FCY_0, ""));
            src_Data.Add(new Xml_Data("IN_BPCORD", _BPCORD, ""));
            src_Data.Add(new Xml_Data("IN_BPAADD", _BPAADD, ""));


            List<List<Xml_Data>> src_PALNUM = new List<List<Xml_Data>>();
            foreach (string _p in _PALNUM)
            {
                if (_p.Trim() != "")
                {
                    List<Xml_Data> src_item = new List<Xml_Data>();
                    src_item.Add(new Xml_Data("IN_PALNUM", _p.Trim().ToUpper(), ""));
                    src_PALNUM.Add(src_item);
                }
            }


            CAdxResultXml result = new CAdxResultXml();
            try
            {

                if (cls_WS.CallWS_TAB_GetResult(cls_Tools.Get_WS_Param(), "YTS_SO2SD", "GRP1", src_Data, "GRP2", src_PALNUM, out _err, out result))
                {
                    XElement element = XElement.Parse(result.resultXml);
                    XElement GRP = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP3");
                    if (GRP != null)
                    {
                        IEnumerable<XElement> _testata = GRP.Elements();
                        if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") == 1)
                        {
                            _err = cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                            return true;
                        }
                        else
                        {
                            _err = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                            return false;
                        }

                    }
                    else
                    {
                        _err = "ERRORE WS GENERICO";
                        return false;
                    }
                }
                else
                {
                    _err = _err + "<br/>Nessun risultato";
                    if (result.messages.Length > 0) _err = _err + "<br/>" + result.messages[0].message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                return false;
            }

        }


        public static bool WS_AllocaDett(string _DOC, string _RIGA, string _SEQ, string _FCY, string _LOT, string _PALNUM, string _LOC, decimal _QTY, out string _err)
        {
            _err = "";
            List<Xml_Data> src_Data = new List<Xml_Data>();
            src_Data.Add(new Xml_Data("IN_DOC", _DOC, ""));
            src_Data.Add(new Xml_Data("IN_RIGA", _RIGA, ""));
            src_Data.Add(new Xml_Data("IN_SEQ", _SEQ, ""));
            src_Data.Add(new Xml_Data("IN_STOFCY", _FCY, ""));
            src_Data.Add(new Xml_Data("IN_LOTTO", _LOT, ""));
            src_Data.Add(new Xml_Data("IN_PALNUM", _PALNUM, ""));
            src_Data.Add(new Xml_Data("IN_QTY", _QTY.ToString("0.###"), "Decimal"));
            src_Data.Add(new Xml_Data("IN_UBI", _LOC, ""));
            src_Data.Add(new Xml_Data("IN_BAR", " ", ""));

            CAdxResultXml result = new CAdxResultXml();
            try
            {

                //if (cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YALLOCA_BP", "GRP1", src_Data, out _err, out result))
                //{
                //    XElement element = XElement.Parse(result.resultXml);
                //    XElement GRP1 = cls_WSX3_V9.Find_Elem(element, "GRP", "ID", "GRP1");
                //    if (GRP1 != null)
                //    {
                //        IEnumerable<XElement> _testata = GRP1.Elements();
                //        if (cls_WSX3_V9.GetNodeValue_Int(_testata, "OUT_OK") == 1)
                //        {
                //            return true;
                //        }
                //        else
                //        {
                //            _err = "ERRORE WS - " + cls_WSX3_V9.GetNodeValue_String(_testata, "OUT_MESSAGE");
                //            return false;
                //        }

                //    }
                //    else
                //    {
                //        _err = "ERRORE WS GENERICO";
                //        return false;
                //    }
                //}
                //else
                //{
                //    _err = _err + "<br/>Nessun risultato";
                //    if (result.messages.Length > 0) _err = _err + "<br/>" + result.messages[0].message;
                //    throw new Exception(_err);
                //}

                bool ok = cls_WS.CallWS_GetResult(cls_Tools.Get_WS_Param(), "YALLOCA_BP", "GRP1", src_Data, out _err, out result);
                if (!ok) 
                {
                    _err = _err + "<br/>Nessun risultato";
                    if (result.messages.Length > 0) _err = _err + "<br/>" + result.messages[0].message;
                    throw new Exception(_err);
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                _err = ex.Message;
                return false;
            }

        }




    }


    #region Obj_Cookie

    public class Obj_Cookie
    {

        public static void Set_String(string IN_Nome, string IN_Value, int IN_Days = 1)
        {
            HttpCookie aCookie = new HttpCookie(IN_Nome.ToString());
            aCookie.Value = IN_Value;
            aCookie.Expires = DateTime.Now.AddDays(IN_Days);
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }

        public static string Get_String(string IN_Nome)
        {
            try
            {
                HttpCookie aCookie = HttpContext.Current.Request.Cookies.Get(IN_Nome);
                if (aCookie!= null) return aCookie.Value.ToString();
                return "";
            }
            catch (Exception)
            {
                return "";
            }

        }
    }

    #endregion

}