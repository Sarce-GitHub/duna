using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using X3_WS_TOOLS_V9;
using X3_WS_TOOLS_V9.WSX3_C9;

namespace X3_TERMINALINI
{
    public class cls_DataX3
    {
        public string error = string.Empty;

        #region STOCK


        #endregion
    }


    public static class cls_WS
    {
        #region WS

        public static bool CallWS_GRP(WS_Param _par, string WS_Name, string WS_GRP, List<Xml_Data> WS_Data, bool WS_NoCheck, out string OUT_Errore)
        {
            OUT_Errore = "";
            try
            {
                string Err = "";
                CAdxCallContext cc = cls_WSX3_V9.Get_CAdxCC(_par.WS_Lan, _par.WS_Pool);
                CAdxResultXml res = cls_WSX3_V9.Get_WS_XML(_par.WS_User, _par.WS_Pws, _par.WS_Url, cc, GRP_Type.GRP, WS_GRP, WS_Data, WS_Name, out Err);
                // CONTROLLO SE ERRORE 
                if (Err == "")
                {
                    // CONTROLLO STATUS WS 
                    if (res.status == 1)
                    {
                        if (WS_NoCheck) return true;
                        // CONTROLLO CAMPO RITORNO WS
                        int OK = cls_WSX3_V9.Get_SingleData_Int(res.resultXml, "GRP1", "OUT_OK");
                        string MESSAGE = cls_WSX3_V9.Get_SingleData_String(res.resultXml, "GRP1", "OUT_MSG");
                        if (MESSAGE == "") MESSAGE = cls_WSX3_V9.Get_SingleData_String(res.resultXml, "GRP1", "OUT_ERR");

                        // SE OK
                        if (OK == 1)
                        {
                            return true;
                        }
                        else
                        {
                            Err = MESSAGE;
                            // RECUPERO MESSAGGIO ERRORE WS
                            foreach (var item in res.messages)
                            {
                                if (item.message != "Connessione a database obsoleta.") Err = Err + " " + item.message;
                            }
                            // ESPONGO
                            OUT_Errore = "WS in Errore. " + Err;
                            return false;
                        }
                    }
                    else
                    {
                        // RECUPERO MESSAGGIO ERRORE WS
                        foreach (var item in res.messages)
                        {
                            if (item.message != "Connessione a database obsoleta.") Err = Err + " " + item.message;
                        }
                        // ESPONGO
                        OUT_Errore = "WS in Errore. " + Err;
                        return false;
                    }
                }
                else
                {
                    // ESPONGO 
                    OUT_Errore = "WS in Errore. " + Err;
                    return false;
                }
            }
            catch (Exception ex)
            {
                OUT_Errore = "WS in Errore. " + ex.Message;
                return false;
            }
        }

        public static bool CallWS_GetResult(WS_Param _par, string WS_Name, string WS_GRP, List<Xml_Data> WS_Data, out string OUT_Errore, out CAdxResultXml res)
        {
            OUT_Errore = "";
            res = new CAdxResultXml();
            try
            {
                string Err = "";
                CAdxCallContext cc = cls_WSX3_V9.Get_CAdxCC(_par.WS_Lan, _par.WS_Pool);
                res = cls_WSX3_V9.Get_WS_XML(_par.WS_User, _par.WS_Pws, _par.WS_Url, cc, GRP_Type.GRP, WS_GRP, WS_Data, WS_Name, out Err);
                // CONTROLLO SE ERRORE 
                if (Err == "" && res.status == 1)
                {
                    return true;
                }
                else
                {
                    // ESPONGO 
                    OUT_Errore = "WS in Errore. status ("+ res.status + ") " + Err;
                    //
                    if (res.messages != null)
                    {
                        foreach (var mss in res.messages)
                        {
                            OUT_Errore = OUT_Errore + " - " + mss.message.ToString();
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                OUT_Errore = "WS Exception. " + ex.Message;
                return false;
            }
        }

        public static bool CallWS_TAB_GetResult(WS_Param _par, string WS_Name, string WS_GRP, List<Xml_Data> WS_Data, string WS_TAB, List<List<Xml_Data>> WS_Data_TAB, out string OUT_Errore, out CAdxResultXml res)
        {
            OUT_Errore = "";
            res = new CAdxResultXml();
            try
            {
                string Err = "";
                CAdxCallContext cc = cls_WSX3_V9.Get_CAdxCC(_par.WS_Lan, _par.WS_Pool);
                res = cls_WSX3_V9.Get_WS_XML_GRP_TAB(_par.WS_User, _par.WS_Pws, _par.WS_Url, cc, WS_GRP, WS_Data, WS_TAB, WS_Data_TAB, WS_Name, WS_Object_Type.Run, out Err);
                // CONTROLLO SE ERRORE 
                if (Err == "" && res.status == 1)
                {
                    return true;
                }
                else
                {
                    // ESPONGO 
                    OUT_Errore = "WS in Errore. " + Err;
                    //
                    if (res.messages != null)
                    {
                        foreach (var mss in res.messages)
                        {
                            OUT_Errore = OUT_Errore + " - " + mss.message.ToString();
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                OUT_Errore = "WS in Errore. " + ex.Message;
                return false;
            }
        }

        #endregion
    }


}