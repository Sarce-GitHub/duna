using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace X3_TERMINALINI
{
    public class cls_SQL
    {

        #region VARIABLE

        private string connectionSQL = string.Empty;
        public string error = string.Empty;
        //
        public cls_SQL()
        {
            this.connectionSQL = "Data Source="+ Properties.Settings.Default.SQL_IP +";Initial Catalog="+ Properties.Settings.Default.SQL_Catalog +";User ID="+ Properties.Settings.Default.SQL_User +";Password="+ Properties.Settings.Default.SQL_Psw +";";
        }

        #endregion

        #region YTSUTX

        // LOAD Obj_Ytsutx
        public bool Obj_YTSUTX_Load(string IN_USR_TERM, string IN_PSW, out Obj_YTSUTX OUT_item)
        {
            // Inizializzazione
            OUT_item = new Obj_YTSUTX();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    YTSUTX item = db.YTSUTX.Where(w => w.USR_TERM_0== IN_USR_TERM && w.PSW_0== IN_PSW).FirstOrDefault();
                    // Se esiste
                    if (item != null)
                    {
                        OUT_item = new Obj_YTSUTX()
                        {
                            USR_TERM_0 = item.USR_TERM_0,
                            PSW_0 = item.PSW_0,
                            USR_X3_0 = item.USR_X3_0,
                            DESCR_0 = item.DESCR_0,
                            ATTIVO_0 = item.ATTIVO_0,
                            FCY_0 = item.FCY_0,
                            ABIL1_0 = item.ABIL1_0,
                            ABIL2_0 = item.ABIL2_0,
                            ABIL3_0 = item.ABIL3_0,
                            ABIL4_0 = item.ABIL4_0,
                            ABIL5_0 = item.ABIL5_0,
                            ABIL6_0 = item.ABIL6_0,
                            ABIL7_0 = item.ABIL7_0,
                            ABIL8_0 = item.ABIL8_0,
                            ABIL9_0 = item.ABIL9_0
                        };
                        return true;
                    }
                    else
                    {
                        error = "Record non trovato.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        #endregion

        #region YTSLOG

        // LOAD Obj_Ytslog
        public void Obj_YTSLOG_Save(Obj_YTSUTX IN_USR, string IN_FUNZIONE, string IN_ESITO, string IN_MESSAGGIO, List<string> IN_VARS)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    YTSLOG _l = new YTSLOG()
                    {
                        UPDTICK_0 = 1,
                        USR_X3_0 = IN_USR.USR_X3_0,
                        USR_TERM_0 = IN_USR.USR_TERM_0,
                        FUNZIONE_0 = IN_FUNZIONE,
                        ESITO_0 = IN_ESITO,
                        MESSAGGIO_0 = IN_MESSAGGIO,
                        VARIABILE_0 = (IN_VARS.Count > 0 ? IN_VARS[0] : ""),
                        VARIABILE_1 = (IN_VARS.Count > 1 ? IN_VARS[1] : ""),
                        VARIABILE_2 = (IN_VARS.Count > 2 ? IN_VARS[2] : ""),
                        VARIABILE_3 = (IN_VARS.Count > 3 ? IN_VARS[3] : ""),
                        VARIABILE_4 = (IN_VARS.Count > 4 ? IN_VARS[4] : ""),
                        VARIABILE_5 = (IN_VARS.Count > 5 ? IN_VARS[5] : ""),
                        VARIABILE_6 = (IN_VARS.Count > 6 ? IN_VARS[6] : ""),
                        VARIABILE_7 = (IN_VARS.Count > 7 ? IN_VARS[7] : ""),
                        VARIABILE_8 = (IN_VARS.Count > 8 ? IN_VARS[8] : ""),
                        VARIABILE_9 = (IN_VARS.Count > 9 ? IN_VARS[9] : ""),
                        VARIABILE_10 = (IN_VARS.Count > 10 ? IN_VARS[10] : ""),
                        VARIABILE_11 = (IN_VARS.Count > 11 ? IN_VARS[11] : ""),
                        VARIABILE_12 = (IN_VARS.Count > 12 ? IN_VARS[12] : ""),
                        VARIABILE_13 = (IN_VARS.Count > 13 ? IN_VARS[13] : ""),
                        VARIABILE_14 = (IN_VARS.Count > 14 ? IN_VARS[14] : ""),
                        CREDATTIM_0 = DateTime.UtcNow,
                        UPDDATTIM_0 = DateTime.UtcNow,
                        AUUID_0 = new byte[] { 0x0 },
                        CREUSR_0 = IN_USR.USR_X3_0,
                        UPDUSR_0 = IN_USR.USR_X3_0
                    };
                    db.YTSLOG.InsertOnSubmit(_l);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
            }
        }

        #endregion

        #region STOLOC

        public bool obj_STOLOC_Check(string IN_STOFCY, string IN_LOC)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOLOC item = db.STOLOC.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC.Trim().ToUpper() && w.LOCTYP_0 != "NAV").FirstOrDefault();
                    // Se esiste
                    return (item != null);

                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_STOLOC_Navetta_Check(string IN_STOFCY, string IN_LOC)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOLOC item = db.STOLOC.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC.Trim().ToUpper() && w.LOCTYP_0 == "NAV").FirstOrDefault();
                    // Se esiste
                    return (item != null);

                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_STOLOC_Presped_Check(string IN_STOFCY, string IN_LOC)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOLOC item = db.STOLOC.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC.Trim().ToUpper() && w.LOCTYP_0 == "PRE").FirstOrDefault();
                    // Se esiste
                    return (item != null);

                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_STOLOC_Sped_Check(string IN_STOFCY, string IN_LOC)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOLOC item = db.STOLOC.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC.Trim().ToUpper() && w.LOCTYP_0 == "SPE").FirstOrDefault();
                    // Se esiste
                    return (item != null);

                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }



        public bool obj_STOLOC_Load(string IN_STOFCY, string IN_LOC, out obj_STOLOC OUT_Obj)
        {
            // Inizializzazione
            OUT_Obj = new obj_STOLOC();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOLOC item = db.STOLOC.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0== IN_LOC).FirstOrDefault();
                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = new obj_STOLOC
                        {
                            STOFCY_0 = item.STOFCY_0,
                            LOC_0 = item.LOC_0,
                            WRH_0 = item.WRH_0,
                            OCPCOD_0 = item.OCPCOD_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            LOCNUMFMT_0 = item.LOCNUMFMT_0,
                            PPSSEQ_0 = item.PPSSEQ_0,
                            MONITMFLG_0 = item.MONITMFLG_0,
                            DEDFLG_0 = item.DEDFLG_0,
                            REAFLG_0 = item.REAFLG_0,
                            FRGMGTMOD_0 = item.FRGMGTMOD_0,
                            TEMLTI_0 = item.TEMLTI_0,
                            FILMGTFLG_0 = item.FILMGTFLG_0,
                            AUZSST_0 = item.AUZSST_0,
                            AVADAT_0 = item.AVADAT_0,
                            AVAHOU_0 = item.AVAHOU_0,
                            ALLUSR_0 = item.ALLUSR_0,
                            ALLDAT_0 = item.ALLDAT_0,
                            ALLHOU_0 = item.ALLHOU_0,
                            MAXAUZWEI_0 = item.MAXAUZWEI_0,
                            WID_0 = item.WID_0,
                            HEI_0 = item.HEI_0,
                            DTH_0 = item.DTH_0,
                            LOKSTA_0 = item.LOKSTA_0,
                            CUNLOKFLG_0 = item.CUNLOKFLG_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            MAXQTYPCU_0 = item.MAXQTYPCU_0
                        };
                        //
                        return true;
                    }
                    else
                    {
                        error = "";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }


        #endregion

        #region STOCK

        public bool obj_PALNUM_Check(string IN_STOFCY, string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM).FirstOrDefault();
                    // Se esiste
                    return (item != null);
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_PALNUM_Check(string IN_STOFCY, string IN_PALNUM, string IN_LOC, string IN_BPCORD, string IN_BPAADD)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.LOC_0 == IN_LOC).FirstOrDefault();
                    if (item != null)
                    {
                        // MANCA CONTROLLO CHE SIA DI QUESTO IN_BPCORD, IN_BPAADD
                        return true;
                    }
                    // Non esiste
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_PALNUM_PRESPED_Check(string IN_STOFCY, string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.LOCTYP_0 == "PRE").FirstOrDefault();
                    // Se esiste
                    return (item != null);
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_PALNUM_SPED_Check(string IN_STOFCY, string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.LOCTYP_0 == "SPE").FirstOrDefault();
                    // Se esiste
                    return (item != null);
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_PALNUM_NAV_Check(string IN_STOFCY, string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.LOCTYP_0 == "NAV").FirstOrDefault();
                    // Se esiste
                    return (item != null);
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }




        public bool obj_PALNUM_Check_State(string IN_STOFCY, string IN_PALNUM, string IN_LOC)
        {
            // Inizializzazione
            string error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    var items = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM);
                    if (items.Any(w => w.LOC_0 == IN_LOC || w.STA_0 != "A"))
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_PALNUM_Check_Preparato(string IN_STOFCY, string IN_PALNUM, string IN_LOC)
        {
            // Inizializzazione
            string error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    var items = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM);
                    return items.Any(w => w.LOC_0 == IN_LOC && w.STA_0 == "A");
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }


        public bool obj_PALNUM_GetStock(string IN_STOFCY, string IN_PALNUM, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            error = "";
            OUT_Obj = new Obj_STOCK();
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM).FirstOrDefault();
                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = obj_STOCK_Load(item);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
            }
            //
            return false;
        }

        public bool obj_PALNUM_GetStock(string IN_STOFCY, string IN_PALNUM, string IN_ITMREF, string IN_LOT, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            error = "";
            OUT_Obj = new Obj_STOCK();
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    STOCK item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.LOT_0 == IN_LOT && w.ITMREF_0 == IN_ITMREF).FirstOrDefault();
                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = obj_STOCK_Load(item);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
            }
            //
            return false;
        }

        public bool obj_PALNUM_GetListStock(string IN_STOFCY, string IN_PALNUM, out List<Obj_STOCK> OUT_Obj)
        {
            // Inizializzazione
            error = "";
            OUT_Obj = new List<Obj_STOCK>();
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    var items = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM);
                    // Se esiste
                    if (items != null && items.Any())
                    {
                        foreach(var item in items)
                        {
                            OUT_Obj.Add(obj_STOCK_Load(item));
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
            }
            //
            return false;
        }

        public bool obj_PALNUM_GetStock(string IN_STOFCY, string IN_PALNUM, string IN_ITMREF, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            error = "";
            OUT_Obj = new Obj_STOCK();
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // 
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.PALNUM_0 == IN_PALNUM && w.ITMREF_0 == IN_ITMREF).ToList())
                    {
                        if (OUT_Obj.ITMREF_0 == "")
                        {
                            OUT_Obj = obj_STOCK_Load(item);
                        }
                        else
                        {
                            OUT_Obj.QTYPCU_0 = OUT_Obj.QTYPCU_0 + item.QTYPCU_0;
                            OUT_Obj.QTYSTU_0 = OUT_Obj.QTYSTU_0 + item.QTYSTU_0;
                        }
                    }

                    // Se esiste
                    return (OUT_Obj.ITMREF_0 != "");


                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
            }
            //
            return false;
        }

        public string obj_STOCK_Check(string IN_STOFCY, string IN_ITMREF, string IN_LOT, string IN_SLO, string IN_LOCSPED, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // LETTURA STOCK
                    STOCK item = new STOCK();
                    item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.ITMREF_0 == IN_ITMREF && w.LOT_0 == IN_LOT && w.SLO_0 == IN_SLO && w.LOC_0 != IN_LOCSPED).FirstOrDefault();

                    // Se esiste
                    if (item != null)
                    {
                        // CONTROLLO CHE PCU SIA COERENTE
                        List<YTSORDAPE> YTSORDAPE = db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_STOFCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == item.ITMREF_0).OrderBy(o=>o.SOHNUM_0).ThenBy(o=>o.SOPLIN_0).ThenBy(o => o.SOQSEQ_0).ToList();
                        if (YTSORDAPE.Count == 0)
                        {
                            return "Articolo " + IN_ITMREF + " non richiesto per questa spedizione";
                        }
                        else
                        {
                            bool _ITM_PRES = false;
                            decimal _TOT_T = 0;
                            decimal _TOT_P = 0;
                            string K_SOH = "";
                            List <string> PALLET = new List<string>();
                            foreach (YTSORDAPE _y in YTSORDAPE)
                            {
                                if (K_SOH != _y.SOHNUM_0 + "-" + _y.SOPLIN_0.ToString("0") + "_" + _y.SOQSEQ_0.ToString("0"))
                                {
                                    K_SOH = _y.SOHNUM_0 + "-" + _y.SOPLIN_0.ToString("0") + "_" + _y.SOQSEQ_0.ToString("0");
                                    _TOT_T = _TOT_T + _y.QTY_0 - _y.DLVQTY_0;
                                }
                                
                                //TODO:aggiungere PALNUM a YTSORDAPE
                                if (_y.YPCU_0 == item.PCU_0 && !PALLET.Contains(_y.PALNUM_0))
                                {
                                    _ITM_PRES = true;
                                    _TOT_P = _TOT_P + _y.YQTYPCU_0;
                                    PALLET.Add(_y.PALNUM_0);
                                }
                            }
                            //
                            if (!_ITM_PRES) return "Articolo non presente con PCU coerente (" + item.PCU_0 + ")";
                            //
                            //COMMENTATO PER PERMETTERE DI PRELEVARE ANCHE SE TUTTO PREPARATO
                            //if (_TOT_P >= _TOT_T)
                            //    return "Articolo già preparato completamente";
                            else
                                return "";
                        }
                    }
                    else
                    {
                        return "ARTICOLO/LOTTO non trovato a magazzino";
                    }

                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return "ERRORE: " + ex.Message;
            }
        }
        
        public bool obj_STOCK_Load(string IN_STOFCY, string IN_LOC, string IN_ITMREF, string IN_LOT, string IN_SLO, string IN_PALNUM, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            OUT_Obj = new Obj_STOCK();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // LETTURA STOCK
                    STOCK item = new STOCK();
                    item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC && w.ITMREF_0 == IN_ITMREF && w.LOT_0 == IN_LOT && w.SLO_0 == IN_SLO && w.PALNUM_0 == IN_PALNUM).FirstOrDefault();

                    // VERIFICA ARTICOLO SINGOLO SE GESTITO A LOTTO
                    if (item == null)
                    {
                        ITMMASTER _it = db.ITMMASTER.Where(w => w.ITMREF_0 == IN_ITMREF).FirstOrDefault();
                        //_it.LOTMGTCOD_0 _it.STOMGTCOD_0
                    }

                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };

                        //
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_STOCK_Load(string IN_STOFCY, string IN_LOC, string IN_ITMREF, string IN_LOT, string IN_SLO, string IN_PALNUM, string IN_STA, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            OUT_Obj = new Obj_STOCK();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // LETTURA STOCK
                    STOCK item = new STOCK();
                    item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOC_0 == IN_LOC && w.ITMREF_0 == IN_ITMREF && w.LOT_0 == IN_LOT && w.SLO_0 == IN_SLO && w.PALNUM_0 == IN_PALNUM && w.STA_0 == IN_STA).FirstOrDefault();

                    // VERIFICA ARTICOLO SINGOLO SE GESTITO A LOTTO
                    if (item == null)
                    {
                        ITMMASTER _it = db.ITMMASTER.Where(w => w.ITMREF_0 == IN_ITMREF).FirstOrDefault();
                        //_it.LOTMGTCOD_0 _it.STOMGTCOD_0
                    }

                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0,
                            SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                        };

                        //
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public bool obj_STOCK_Load_TYP(string IN_STOFCY, string IN_LOCTYP, string IN_ITMREF, string IN_LOT, string IN_SLO, string IN_PALNUM, out Obj_STOCK OUT_Obj)
        {
            // Inizializzazione
            OUT_Obj = new Obj_STOCK();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Stock
                    STOCK item = new STOCK();
                    item = db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.LOCTYP_0 == IN_LOCTYP && w.ITMREF_0 == IN_ITMREF && w.LOT_0 == IN_LOT && w.SLO_0 == IN_SLO && w.PALNUM_0 == IN_PALNUM).FirstOrDefault();

                    // Se esiste
                    if (item != null)
                    {
                        OUT_Obj = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };

                        //
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public Obj_STOCK obj_STOCK_Load(STOCK item)
        {
            // Inizializzazione
            Obj_STOCK OUT_Obj = new Obj_STOCK();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    if (item != null)
                    {
                        OUT_Obj = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            CUMALLQTY_0 = item.CUMALLQTY_0,
                            CUMALLQTA_0 = item.CUMALLQTA_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };
                        //
                        return OUT_Obj;
                    }
                    else
                    {
                        return OUT_Obj;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_STOCK();
            }
        }

        public List<Obj_STOCK> obj_STOCK_SearchByCode(string IN_STOFCY, string IN_ITMREF, int IN_Max = 100)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    ITMMASTER _itm = db.ITMMASTER.Where(w => w.ITMREF_0.ToUpper() == IN_ITMREF.ToUpper()).FirstOrDefault();
                    if (_itm != null)
                    {
                        error = "ARTICOLO TROVATO";
                    }
                    else
                    {
                        error = "ARTICOLO PARZIALE";
                    }

                    if (_itm != null)
                    {
                        foreach (STOCK item in db.STOCK.Where(x => x.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && x.ITMREF_0.ToUpper() == IN_ITMREF.ToUpper()).ToList()) // && LOC_0 == IN_LOC0
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = item.STOFCY_0,
                                STOCOU_0 = item.STOCOU_0,
                                ITMREF_0 = item.ITMREF_0,
                                LOT_0 = item.LOT_0,
                                SLO_0 = item.SLO_0,
                                BPSLOT_0 = item.BPSLOT_0,
                                PALNUM_0 = item.PALNUM_0,
                                CTRNUM_0 = item.CTRNUM_0,
                                STA_0 = item.STA_0,
                                LOC_0 = item.LOC_0,
                                LOCTYP_0 = item.LOCTYP_0,
                                LOCCAT_0 = item.LOCCAT_0,
                                WRH_0 = item.WRH_0,
                                SERNUM_0 = item.SERNUM_0,
                                RCPDAT_0 = item.RCPDAT_0,
                                PCU_0 = item.PCU_0,
                                PCUSTUCOE_0 = item.PCUSTUCOE_0,
                                QTYPCU_0 = item.QTYPCU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                QTYSTUACT_0 = item.QTYSTUACT_0,
                                PCUORI_0 = item.PCUORI_0,
                                ITMDES_0 = _itm.ITMDES1_0,
                                STU_0 = _itm.STU_0,
                                SEAKEY_0 = _itm.SEAKEY_0
                            };
                            OUT_Obj.Add(_s);
                        }

                        //
                        if (OUT_Obj.Count == 0)
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = IN_STOFCY.ToUpper(),
                                ITMREF_0 = IN_ITMREF.ToUpper(),
                                ITMDES_0 = _itm.ITMDES1_0,
                                STU_0 = _itm.STU_0
                            };
                            OUT_Obj.Add(_s);
                        }
                    }
                    else
                    {
                        //Lettura
                        foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.ITMREF_0.ToUpper().Contains(IN_ITMREF.ToUpper())).Take(IN_Max).ToList())
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = item.STOFCY_0,
                                STOCOU_0 = item.STOCOU_0,
                                ITMREF_0 = item.ITMREF_0,
                                LOT_0 = item.LOT_0,
                                SLO_0 = item.SLO_0,
                                BPSLOT_0 = item.BPSLOT_0,
                                PALNUM_0 = item.PALNUM_0,
                                CTRNUM_0 = item.CTRNUM_0,
                                STA_0 = item.STA_0,
                                LOC_0 = item.LOC_0,
                                LOCTYP_0 = item.LOCTYP_0,
                                LOCCAT_0 = item.LOCCAT_0,
                                WRH_0 = item.WRH_0,
                                SERNUM_0 = item.SERNUM_0,
                                RCPDAT_0 = item.RCPDAT_0,
                                PCU_0 = item.PCU_0,
                                PCUSTUCOE_0 = item.PCUSTUCOE_0,
                                QTYPCU_0 = item.QTYPCU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                QTYSTUACT_0 = item.QTYSTUACT_0,
                                PCUORI_0 = item.PCUORI_0,
                                ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                                STU_0 = item.ITMMASTER.STU_0
                            };

                            //
                            OUT_Obj.Add(_s);
                        }

                    }

                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public List<Obj_STOCK> obj_STOCK_SearchByDesc(string IN_STOFCY, string IN_ITMDES, int IN_Max = 100)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (ITMMASTER _i in db.ITMMASTER.Where(w => w.ITMDES1_0.ToUpper().Contains(IN_ITMDES.ToUpper())).ToList())
                    {
                        // Lettura
                        foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.ITMREF_0 == _i.ITMREF_0).Take(IN_Max).ToList())
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = item.STOFCY_0,
                                STOCOU_0 = item.STOCOU_0,
                                ITMREF_0 = item.ITMREF_0,
                                LOT_0 = item.LOT_0,
                                SLO_0 = item.SLO_0,
                                BPSLOT_0 = item.BPSLOT_0,
                                PALNUM_0 = item.PALNUM_0,
                                CTRNUM_0 = item.CTRNUM_0,
                                STA_0 = item.STA_0,
                                LOC_0 = item.LOC_0,
                                LOCTYP_0 = item.LOCTYP_0,
                                LOCCAT_0 = item.LOCCAT_0,
                                WRH_0 = item.WRH_0,
                                SERNUM_0 = item.SERNUM_0,
                                RCPDAT_0 = item.RCPDAT_0,
                                PCU_0 = item.PCU_0,
                                PCUSTUCOE_0 = item.PCUSTUCOE_0,
                                QTYPCU_0 = item.QTYPCU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                QTYSTUACT_0 = item.QTYSTUACT_0,
                                PCUORI_0 = item.PCUORI_0,
                                ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                                STU_0 = item.ITMMASTER.STU_0,
                                SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                            };
                            //
                            OUT_Obj.Add(_s);
                        }
                        if (OUT_Obj.Count> IN_Max) return OUT_Obj;
                    }



                    //
                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public List<Obj_STOCK> obj_STOCK_SearchByLocation(string IN_STOFCY, string IN_LOC, int IN_Max = 100)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    STOLOC _itm = db.STOLOC.Where(w => w.LOC_0.ToUpper() == IN_LOC.ToUpper()).FirstOrDefault();
                    if (_itm == null)
                    {
                        error = "UBICAZIONE MANCANTE";
                        return new List<Obj_STOCK>();
                    }

                    //if (_itm != null)
                    //{
                        foreach (STOCK item in db.STOCK.Where(x => x.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && x.LOC_0.ToUpper() == IN_LOC.ToUpper()).ToList()) 
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = item.STOFCY_0,
                                STOCOU_0 = item.STOCOU_0,
                                ITMREF_0 = item.ITMREF_0,
                                LOT_0 = item.LOT_0,
                                SLO_0 = item.SLO_0,
                                BPSLOT_0 = item.BPSLOT_0,
                                PALNUM_0 = item.PALNUM_0,
                                CTRNUM_0 = item.CTRNUM_0,
                                STA_0 = item.STA_0,
                                LOC_0 = item.LOC_0,
                                LOCTYP_0 = item.LOCTYP_0,
                                LOCCAT_0 = item.LOCCAT_0,
                                WRH_0 = item.WRH_0,
                                SERNUM_0 = item.SERNUM_0,
                                RCPDAT_0 = item.RCPDAT_0,
                                PCU_0 = item.PCU_0,
                                PCUSTUCOE_0 = item.PCUSTUCOE_0,
                                QTYPCU_0 = item.QTYPCU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                QTYSTUACT_0 = item.QTYSTUACT_0,
                                PCUORI_0 = item.PCUORI_0,
                                ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                                STU_0 = item.ITMMASTER.STU_0,
                                SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                            };
                            OUT_Obj.Add(_s);
                        }

                        //
                        if (OUT_Obj.Count == 0)
                        {
                            Obj_STOCK _s = new Obj_STOCK
                            {
                                STOFCY_0 = IN_STOFCY.ToUpper(),
                                ITMREF_0 = "UBICAZIONE "+ IN_LOC,//IN_ITMREF.ToUpper(),
                                ITMDES_0 = "",//_itm.ITMDES1_0,
                                STU_0 = "",//_itm.STU_0
                            };
                            OUT_Obj.Add(_s);
                        }
                    //}
                    //else
                    //{
                        //Lettura
                        //foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.ITMREF_0.ToUpper().Contains(IN_ITMREF.ToUpper())).Take(IN_Max).ToList())
                        //{
                        //    Obj_STOCK _s = new Obj_STOCK
                        //    {
                        //        STOFCY_0 = item.STOFCY_0,
                        //        STOCOU_0 = item.STOCOU_0,
                        //        ITMREF_0 = item.ITMREF_0,
                        //        LOT_0 = item.LOT_0,
                        //        SLO_0 = item.SLO_0,
                        //        BPSLOT_0 = item.BPSLOT_0,
                        //        PALNUM_0 = item.PALNUM_0,
                        //        CTRNUM_0 = item.CTRNUM_0,
                        //        STA_0 = item.STA_0,
                        //        LOC_0 = item.LOC_0,
                        //        LOCTYP_0 = item.LOCTYP_0,
                        //        LOCCAT_0 = item.LOCCAT_0,
                        //        WRH_0 = item.WRH_0,
                        //        SERNUM_0 = item.SERNUM_0,
                        //        RCPDAT_0 = item.RCPDAT_0,
                        //        PCU_0 = item.PCU_0,
                        //        PCUSTUCOE_0 = item.PCUSTUCOE_0,
                        //        QTYPCU_0 = item.QTYPCU_0,
                        //        QTYSTU_0 = item.QTYSTU_0,
                        //        QTYSTUACT_0 = item.QTYSTUACT_0,
                        //        PCUORI_0 = item.PCUORI_0,
                        //        ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                        //        STU_0 = item.ITMMASTER.STU_0
                        //    };

                        //    //
                        //    OUT_Obj.Add(_s);
                        //}

                    //}

                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        
        public List<Obj_STOCK> obj_STOCK_SearchByPalnum(string IN_STOFCY, string IN_PALNUM, int IN_Max = 100)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    STOCK _itm = db.STOCK.Where(w => w.PALNUM_0.ToUpper() == IN_PALNUM.ToUpper()).FirstOrDefault();
                    if (_itm == null)
                    {
                        error = "PALETTA MANCANTE";
                        return new List<Obj_STOCK>();
                    }

                    foreach (STOCK item in db.STOCK.Where(x => x.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && x.PALNUM_0.ToUpper() == IN_PALNUM.ToUpper()).ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0,
                            SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                        };
                        OUT_Obj.Add(_s);
                    }

                    //
                    if (OUT_Obj.Count == 0)
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = IN_STOFCY.ToUpper(),
                            ITMREF_0 = "PALETTA " + IN_PALNUM.Trim().ToUpper(),//IN_ITMREF.ToUpper(),
                            ITMDES_0 = "",//_itm.ITMDES1_0,
                            STU_0 = "",//_itm.STU_0
                        };
                        OUT_Obj.Add(_s);
                    }
                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }


        public List<Obj_STOCK> obj_STOCK_SearchByBarCode(string IN_STOFCY, string IN_ITMREF, string IN_LOT, string IN_SLO, int IN_Max = 100)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0 == IN_STOFCY && w.ITMREF_0 == IN_ITMREF && w.LOT_0 == IN_LOT && w.SLO_0 == IN_SLO).Take(IN_Max).ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0,
                            SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                        };
                        //
                        OUT_Obj.Add(_s);
                    }
                    //
                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public List<Obj_STOCK> obj_STOCK_SearchByPalnum(string IN_STOFCY, string IN_PALNUM)
        {
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    STOCK _itm = db.STOCK.Where(w => w.PALNUM_0.ToUpper() == IN_PALNUM.ToUpper()).FirstOrDefault();
                    if (_itm == null)
                    {
                        error = "PALETTA MANCANTE";
                        return new List<Obj_STOCK>();
                    }

                    // Inizializzazione
                    List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
                    error = "";
                    //Lettura
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.PALNUM_0.ToUpper()==IN_PALNUM).ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0,
                            SEAKEY_0 = item.ITMMASTER.SEAKEY_0
                        };

                        //
                        OUT_Obj.Add(_s);
                    }

                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public List<Obj_STOCK> obj_STOCK_TipoUbicazione(string IN_STOFCY, string IN_LOCTYP)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    //Lettura
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.LOCTYP_0.ToUpper() == IN_LOCTYP.ToUpper()).ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };

                        //
                        OUT_Obj.Add(_s);
                    }
                    return OUT_Obj;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public List<Obj_STOCK> obj_STOCK_SearchByITMPCU(string IN_STOFCY, string IN_ITMREF, string IN_PCU)
        {
            // Inizializzazione
            List<Obj_STOCK> OUT_Obj = new List<Obj_STOCK>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    //Lettura
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.ITMREF_0.ToUpper()==IN_ITMREF.ToUpper() && w.PCU_0.ToUpper() == IN_PCU.ToUpper() && w.STA_0=="A").ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };

                        //
                        OUT_Obj.Add(_s);
                    }

                }

                return OUT_Obj;
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOCK>();
            }
        }

        public Obj_STOCK obj_STOCK_CheckPick(string IN_STOFCY, string IN_ITMREF, string IN_PCU, string IN_LOC, string IN_LOT, string IN_SLO, string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    //Lettura
                    foreach (STOCK item in db.STOCK.Where(w => w.STOFCY_0.ToUpper() == IN_STOFCY.ToUpper() && w.ITMREF_0.ToUpper() == IN_ITMREF.ToUpper() && w.PCU_0.ToUpper() == IN_PCU.ToUpper() && w.LOC_0==IN_LOC && w.LOT_0==IN_LOT && w.SLO_0==IN_SLO && w.PALNUM_0==IN_PALNUM && w.STA_0=="A").ToList())
                    {
                        Obj_STOCK _s = new Obj_STOCK
                        {
                            STOFCY_0 = item.STOFCY_0,
                            STOCOU_0 = item.STOCOU_0,
                            ITMREF_0 = item.ITMREF_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            BPSLOT_0 = item.BPSLOT_0,
                            PALNUM_0 = item.PALNUM_0,
                            CTRNUM_0 = item.CTRNUM_0,
                            STA_0 = item.STA_0,
                            LOC_0 = item.LOC_0,
                            LOCTYP_0 = item.LOCTYP_0,
                            LOCCAT_0 = item.LOCCAT_0,
                            WRH_0 = item.WRH_0,
                            SERNUM_0 = item.SERNUM_0,
                            RCPDAT_0 = item.RCPDAT_0,
                            PCU_0 = item.PCU_0,
                            PCUSTUCOE_0 = item.PCUSTUCOE_0,
                            QTYPCU_0 = item.QTYPCU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            QTYSTUACT_0 = item.QTYSTUACT_0,
                            PCUORI_0 = item.PCUORI_0,
                            ITMDES_0 = item.ITMMASTER.ITMDES1_0,
                            STU_0 = item.ITMMASTER.STU_0
                        };

                        //
                        return _s;
                    }

                }

                return new Obj_STOCK();
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_STOCK();
            }
        }

        public Obj_STOCK_ETIC obj_STOCK_SEAKEY(string IN_ITMREF)
        {
            // Inizializzazione
            error = "";
            Obj_STOCK_ETIC _e = new Obj_STOCK_ETIC();
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    ITMMASTER _it = db.ITMMASTER.Where(w => w.SEAKEY_0 == IN_ITMREF).FirstOrDefault();
                    if (_it != null)
                    {
                        _e.ITMREF = _it.ITMREF_0;
                        _e.LOT = Properties.Settings.Default.Abil_SEAKEY_LOT.ToUpper().Trim();
                        _e.SEAKEY = _it.SEAKEY_0;
                    }
                    return _e;
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_STOCK_ETIC();
            }
        }

        #endregion

        #region ITMMASTER

        public Obj_ITMMASTER obj_ITMMASTER_Load(string IN_ITMREF)
        {
            // Inizializzazione
            Obj_ITMMASTER OUT_Obj = new Obj_ITMMASTER();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    ITMMASTER _i = db.ITMMASTER.Where(w => w.ITMREF_0 == IN_ITMREF).FirstOrDefault();
                    if (_i != null)
                    {
                        OUT_Obj = new Obj_ITMMASTER
                        {
                            CPY_0 = _i.CPY_0,
                            ITMREF_0 = _i.ITMREF_0,
                            ITMDES1_0 = _i.ITMDES1_0,
                            STU_0 = _i.STU_0,
                            LOTMGTCOD_0 = _i.LOTMGTCOD_0
                        };
                        //
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Articolo non trovato";
                        return new Obj_ITMMASTER();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_ITMMASTER();
            }
        }

        public Obj_ITMMASTER obj_ITMMASTER_LoadGestLot(string IN_ITMREF)
        {
            using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            {
                // Inizializzazione
                Obj_ITMMASTER OUT_Obj = new Obj_ITMMASTER();
                ITMMASTER _i = db.ITMMASTER.Where(w => w.ITMREF_0 == IN_ITMREF || w.SEAKEY_0 == IN_ITMREF).FirstOrDefault();
                if (_i != null)
                {
                    OUT_Obj = new Obj_ITMMASTER
                    {
                        CPY_0 = _i.CPY_0,
                        ITMREF_0 = _i.ITMREF_0,
                        ITMDES1_0 = _i.ITMDES1_0,
                        STU_0 = _i.STU_0,
                        LOTMGTCOD_0 = _i.LOTMGTCOD_0
                    };
                    //
                    return OUT_Obj;
                }
                else
                {
                    error = "Articolo non trovato";
                    return new Obj_ITMMASTER();
                }
            }
        }


        #endregion

        #region BPADDRESS

        public string Obj_BPADDRESS_DESC(string IN_BPANUM, string IN_BPAADD)
        {
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    BPADDRESS _b = db.BPADDRESS.Where(w => w.BPANUM_0 == IN_BPANUM && w.BPAADD_0 == IN_BPAADD ).FirstOrDefault();
                    if (_b != null) return _b.BPADES_0 + " - " + _b.CTY_0 + " ("+ _b.CRY_0 +")";
                    return "";
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return "";
            }
        }

        #endregion

        #region SPEDIZIONI ORDINI - ORDINI APERTI

        // BOOL Obj_Ytsordineape
        public bool Obj_YTSORDINEAPE_Any(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, bool IN_SPED)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    return db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && (!IN_SPED || w.QTYPREP_0 > 0)).Any();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Clienti(string IN_FCY, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0}).Select(g => new { g.Key}).ToList())
                    {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0
                        };
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.BPCNAM_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }

        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Indirizzi(string IN_FCY, string IN_BPCORD, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0, n.BPAADD_0 }).Select(g => new { g.Key }).ToList())
                    {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0,
                            BPAADD_0 = item.Key.BPAADD_0
                        };
                        _i.BPAADDDES_0 = Obj_BPADDRESS_DESC(_i.BPCORD_0, _i.BPAADD_0);
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.BPAADD_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }

        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Date(string IN_FCY, string IN_BPCORD, string IN_BPCADD, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPCADD && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0, n.BPAADD_0, n.SHIDAT_0 }).Select(g => new { g.Key }).ToList())
                    {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0,
                            BPAADD_0 = item.Key.BPAADD_0,
                            SHIDAT_0 = item.Key.SHIDAT_0
                        };
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.SHIDAT_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }

        // LIST Obj_Ytsordineape
        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Lista(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            Obj_YTSORDINEAPE _YTSORDINEAPE = new Obj_YTSORDINEAPE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _itm = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date).GroupBy(n => new { n.ITMREF_0, n.SAU_0 }).Select(g => new { g.Key.ITMREF_0, g.Key.SAU_0 }).ToList();
                    foreach (var itm in _itm)
                    {
                        List<string> _PALNUM = new List<string>();
                        _YTSORDINEAPE = new Obj_YTSORDINEAPE();
                        List<YTSORDINEAPE> XData = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == itm.ITMREF_0 && w.SAU_0 == itm.SAU_0).OrderBy(o=>o.SOHNUM_0).ThenBy(o=>o.SOPLIN_0).ThenBy(o => o.SOQSEQ_0).ToList();

                        //
                        string _OLD_KEY = "";
                        string _OLD_SOH = "";
                        foreach (YTSORDINEAPE item in XData)
                        {
                            if (_OLD_KEY != item.SALFCY_0 + "-" + item.BPCORD_0 + "-" + item.BPCNAM_0 + "-" + item.BPAADD_0 + "-" + item.ITMREF_0 + "-"  + item.SAU_0)
                            {
                                _OLD_KEY = item.SALFCY_0 + "-" + item.BPCORD_0 + "-" + item.BPCNAM_0 + "-" + item.BPAADD_0 + "-" + item.ITMREF_0 + "-" +  item.SAU_0;
                                _YTSORDINEAPE = new Obj_YTSORDINEAPE()
                                {
                                    SALFCY_0 = item.SALFCY_0,
                                    BPCORD_0 = item.BPCORD_0,
                                    BPCNAM_0 = item.BPCNAM_0,
                                    BPAADD_0 = item.BPAADD_0,
                                    SHIDAT_0 = item.SHIDAT_0,
                                    ITMREF_0 = item.ITMREF_0,
                                    QTY_0 = item.QTY_0,
                                    DLVQTY_0 = item.DLVQTY_0,
                                    SAU_0 = item.SAU_0,
                                    DLVQTYSTU_0 = item.DLVQTYSTU_0,
                                    QTYSTU_0 = item.QTYSTU_0,
                                    STU_0 = item.STU_0,
                                    ITMDES_0 = item.ITMDES_0,
                                    QTYPREP_0 = item.QTYPREP_0,
                                    NrRighe = 1
                                };
                                _OLD_SOH = item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0;
                            }
                            else
                            {
                                if (_OLD_SOH != item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0)
                                {
                                    _OLD_SOH = item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0;
                                    _YTSORDINEAPE.QTYSTU_0 = _YTSORDINEAPE.QTYSTU_0 + item.QTYSTU_0;
                                    _YTSORDINEAPE.QTY_0 = _YTSORDINEAPE.QTY_0 + item.QTY_0;
                                    _YTSORDINEAPE.DLVQTYSTU_0 = _YTSORDINEAPE.DLVQTYSTU_0 + item.DLVQTYSTU_0;
                                    _YTSORDINEAPE.DLVQTY_0 = _YTSORDINEAPE.DLVQTY_0 + item.DLVQTY_0;
                                    _YTSORDINEAPE.NrRighe++;
                                }
                                _YTSORDINEAPE.QTYPREP_0 = _YTSORDINEAPE.QTYPREP_0 + item.QTYPREP_0;
                            }

                        }
                        Lista.Add(_YTSORDINEAPE);






                        //foreach (YTSORDINEAPE item in XData)
                        //{
                        //    if (_YTSORDINEAPE.ITMREF_0 != "")
                        //    {
                        //        _YTSORDINEAPE.QTYSTU_0 = _YTSORDINEAPE.QTYSTU_0 + item.QTYSTU_0;
                        //        _YTSORDINEAPE.QTY_0 = _YTSORDINEAPE.QTY_0 + item.QTY_0;
                        //        _YTSORDINEAPE.DLVQTYSTU_0 = _YTSORDINEAPE.DLVQTYSTU_0 + item.DLVQTYSTU_0;
                        //        _YTSORDINEAPE.DLVQTY_0 = _YTSORDINEAPE.DLVQTY_0 + item.DLVQTY_0;
                        //        _YTSORDINEAPE.QTYPREP_0 = _YTSORDINEAPE.QTYPREP_0 + item.QTYPREP_0;
                        //        _YTSORDINEAPE.NrRighe++;
                        //    }
                        //    else
                        //    {
                        //        _YTSORDINEAPE = new Obj_YTSORDINEAPE()
                        //        {
                        //            SOHNUM_0 = item.SOHNUM_0,
                        //            SALFCY_0 = item.SALFCY_0,
                        //            BPCORD_0 = item.BPCORD_0,
                        //            BPCNAM_0 = item.BPCNAM_0,
                        //            BPAADD_0 = item.BPAADD_0,
                        //            SHIDAT_0 = item.SHIDAT_0,
                        //            SOPLIN_0 = item.SOPLIN_0,
                        //            ITMREF_0 = item.ITMREF_0,
                        //            QTY_0 = item.QTY_0,
                        //            DLVQTY_0 = item.DLVQTY_0,
                        //            LOT_0 = item.LOT_0,
                        //            SLO_0 = item.SLO_0,
                        //            PALNUM_0 = item.PALNUM_0,
                        //            QTYPREP_0 = item.QTYPREP_0,
                        //            SAU_0 = item.SAU_0,
                        //            DLVQTYSTU_0 = item.DLVQTYSTU_0,
                        //            QTYSTU_0 = item.QTYSTU_0,
                        //            STU_0 = item.STU_0,
                        //            ITMDES_0 = item.ITMDES_0,
                        //            SDHNUM_0 = item.SDHNUM_0,
                        //            NrRighe = 1
                        //        };
                        //        //// SOMMA QTYPREP PER PALLET DIFFERENTI
                        //        //if (!_PALNUM.Contains(item.PALNUM_0) && item.PALNUM_0!="")
                        //        //{
                        //        //    _PALNUM.Add(item.PALNUM_0);
                        //        //    _YTSORDINEAPE.QTYPREP_0 = item.QTYPREP_0;
                        //        //}


                        //    }
                        //}


                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }

        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Ordini(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, string IN_ITMREF, string IN_SAU)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            Obj_YTSORDINEAPE _YTSORDINEAPE = new Obj_YTSORDINEAPE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    List<YTSORDINEAPE> XData = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == IN_ITMREF && w.SAU_0 == IN_SAU).ToList();
                    foreach (YTSORDINEAPE item in XData)
                    {
                        _YTSORDINEAPE = new Obj_YTSORDINEAPE()
                        {
                            SOHNUM_0 = item.SOHNUM_0,
                            SALFCY_0 = item.SALFCY_0,
                            BPCORD_0 = item.BPCORD_0,
                            BPCNAM_0 = item.BPCNAM_0,
                            BPAADD_0 = item.BPAADD_0,
                            SHIDAT_0 = item.SHIDAT_0,
                            SOPLIN_0 = item.SOPLIN_0,
                            SOQSEQ_0 = item.SOQSEQ_0,
                            ITMREF_0 = item.ITMREF_0,
                            QTY_0 = item.QTY_0,
                            DLVQTY_0 = item.DLVQTY_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            PALNUM_0 = item.PALNUM_0,
                            QTYPREP_0 = item.QTYPREP_0,
                            SAU_0 = item.SAU_0,
                            DLVQTYSTU_0 = item.DLVQTYSTU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            STU_0 = item.STU_0,
                            ITMDES_0 = item.ITMDES_0,
                            SDHNUM_0 = item.SDHNUM_0
                        };
                        Lista.Add(_YTSORDINEAPE);
                    }
                    return Lista.OrderBy(o=>o.SHIDAT_0).ThenBy(o => o.SOHNUM_0).ThenBy(o =>o.SOPLIN_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }


        // LIST Obj_Ytsordineape
        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Dett(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, bool IN_SOLO_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {

                    List<YTSORDINEAPE> _data = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD &&  w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.QTYPREP_0 > 0).OrderBy(x => x.PALNUM_0).ThenBy(x => x.ITMREF_0).ToList();
                    if (IN_SOLO_SPED) _data = _data.Where(w => w.SDHNUM_0 == "").ToList();

                    foreach (YTSORDINEAPE item in _data)
                    {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SOHNUM_0 = item.SOHNUM_0,
                            SALFCY_0 = item.SALFCY_0,
                            BPCORD_0 = item.BPCORD_0,
                            BPCNAM_0 = item.BPCNAM_0,
                            BPAADD_0 = item.BPAADD_0,
                            SHIDAT_0 = item.SHIDAT_0,
                            SOPLIN_0 = item.SOPLIN_0,
                            SOQSEQ_0 = item.SOQSEQ_0,
                            ITMREF_0 = item.ITMREF_0,
                            QTY_0 = item.QTY_0,
                            DLVQTY_0 = item.DLVQTY_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            PALNUM_0 = item.PALNUM_0,
                            QTYPREP_0 = item.QTYPREP_0,
                            SAU_0 = item.SAU_0,
                            DLVQTYSTU_0 = item.DLVQTYSTU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            STU_0 = item.STU_0,
                            ITMDES_0 = item.ITMDES_0,
                            SDHNUM_0 = item.SDHNUM_0

                        };
                        Lista.Add(_i);
                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }



        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Spedizione(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, bool IN_SOLO_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    List<string> _PALNUM = new List<string>();
                    List<YTSORDINEAPE> _data = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.QTYPREP_0 > 0).OrderBy(x => x.PALNUM_0).ThenBy(x => x.ITMREF_0).ToList();
                    if (IN_SOLO_SPED) _data = _data.Where(w => w.SDHNUM_0 == "").ToList();

                    foreach (YTSORDINEAPE item in _data)
                    {
                        if (!_PALNUM.Contains(item.PALNUM_0)) _PALNUM.Add(item.PALNUM_0);
                    }


                    _data = db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && _PALNUM.Contains(w.PALNUM_0) && w.QTYPREP_0 > 0).OrderBy(x => x.PALNUM_0).ThenBy(x => x.ITMREF_0).ToList();
                    if (IN_SOLO_SPED) _data = _data.Where(w => w.SDHNUM_0 == "").ToList();
                    foreach (YTSORDINEAPE item in _data)
                    {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SOHNUM_0 = item.SOHNUM_0,
                            SALFCY_0 = item.SALFCY_0,
                            BPCORD_0 = item.BPCORD_0,
                            BPCNAM_0 = item.BPCNAM_0,
                            BPAADD_0 = item.BPAADD_0,
                            SHIDAT_0 = item.SHIDAT_0,
                            SOPLIN_0 = item.SOPLIN_0,
                            SOQSEQ_0 = item.SOQSEQ_0,
                            ITMREF_0 = item.ITMREF_0,
                            QTY_0 = item.QTY_0,
                            DLVQTY_0 = item.DLVQTY_0,
                            LOT_0 = item.LOT_0,
                            SLO_0 = item.SLO_0,
                            PALNUM_0 = item.PALNUM_0,
                            QTYPREP_0 = item.QTYPREP_0,
                            SAU_0 = item.SAU_0,
                            DLVQTYSTU_0 = item.DLVQTYSTU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            STU_0 = item.STU_0,
                            ITMDES_0 = item.ITMDES_0,
                            SDHNUM_0 = item.SDHNUM_0

                        };
                        Lista.Add(_i);
                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }


        // LIST Obj_Ytsordineape
        public Obj_YTSORDINEAPE Obj_YTSORDINEAPE_Articolo(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, string IN_ITMREF)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    Obj_YTSORDINEAPE _YTSORDINEAPE = new Obj_YTSORDINEAPE();
                    List<string> _PALNUM = new List<string>();
                    foreach (YTSORDINEAPE item in db.YTSORDINEAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0==IN_ITMREF).ToList())
                    {
                        if (_YTSORDINEAPE.ITMREF_0 != "")
                        {
                            _YTSORDINEAPE.QTYSTU_0 = _YTSORDINEAPE.QTYSTU_0 + item.QTYSTU_0;
                            _YTSORDINEAPE.QTY_0 = _YTSORDINEAPE.QTY_0 + item.QTY_0;
                            _YTSORDINEAPE.DLVQTYSTU_0 = _YTSORDINEAPE.DLVQTYSTU_0 + item.DLVQTYSTU_0;
                            _YTSORDINEAPE.DLVQTY_0 = _YTSORDINEAPE.DLVQTY_0 + item.DLVQTY_0;
                            _YTSORDINEAPE.QTYPREP_0 = _YTSORDINEAPE.QTYPREP_0 + item.QTYPREP_0;
                            _YTSORDINEAPE.NrRighe++;
                        }
                        else
                        {
                            _YTSORDINEAPE = new Obj_YTSORDINEAPE()
                            {
                                SOHNUM_0 = item.SOHNUM_0,
                                SALFCY_0 = item.SALFCY_0,
                                BPCORD_0 = item.BPCORD_0,
                                BPCNAM_0 = item.BPCNAM_0,
                                BPAADD_0 = item.BPAADD_0,
                                SHIDAT_0 = item.SHIDAT_0,
                                SOPLIN_0 = item.SOPLIN_0,
                                SOQSEQ_0 = item.SOQSEQ_0,
                                ITMREF_0 = item.ITMREF_0,
                                QTY_0 = item.QTY_0,
                                DLVQTY_0 = item.DLVQTY_0,
                                LOT_0 = item.LOT_0,
                                SLO_0 = item.SLO_0,
                                PALNUM_0 = item.PALNUM_0,
                                QTYPREP_0 = item.QTYPREP_0,
                                SAU_0 = item.SAU_0,
                                DLVQTYSTU_0 = item.DLVQTYSTU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                STU_0 = item.STU_0,
                                ITMDES_0 = item.ITMDES_0,
                                SDHNUM_0 = item.SDHNUM_0,
                                NrRighe = 1
                            };

                            //// SOMMA QTYPREP PER PALLET DIFFERENTI
                            //if (!_PALNUM.Contains(item.PALNUM_0) && item.PALNUM_0 != "")
                            //{
                            //    _PALNUM.Add(item.PALNUM_0);
                            //    _YTSORDINEAPE.QTYPREP_0 = item.QTYPREP_0;
                            //}
                        }
                    }
                    return _YTSORDINEAPE;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new Obj_YTSORDINEAPE();
            }
        }

        // LIST Obj_Ytsordineape
        public List<Obj_YTSORDINEAPE> Obj_YTSORDINEAPE_Gruppo(string IN_FCY, string IN_SRC)
        {
            // Inizializzazione
            List<Obj_YTSORDINEAPE> Lista = new List<Obj_YTSORDINEAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDINEAPE.Where(w => w.SALFCY_0== IN_FCY && w.SOHNUM_0.Contains(IN_SRC) || w.BPCNAM_0.Contains(IN_SRC)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPAADD_0, n.BPCNAM_0, n.SOHNUM_0, n.SHIDAT_0 }).Select(g => new  { g.Key, qty = g.Max(row => row.QTYPREP_0) }).ToList())
                {
                        Obj_YTSORDINEAPE _i = new Obj_YTSORDINEAPE()
                        {
                            SOHNUM_0 = item.Key.SOHNUM_0,
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0,
                            BPAADD_0 = item.Key.BPAADD_0,
                            SHIDAT_0 = item.Key.SHIDAT_0,
                            QTYPREP_0 = item.qty
                        };
                        Lista.Add(_i);
                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDINEAPE>();
            }
        }
        #endregion

        #region YTSORDAPE

        public bool Obj_YTSORDAPE_Any(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, bool IN_SPED)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    return db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && (!IN_SPED || w.QTYPREP_0 > 0)).Any();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public List<Obj_YTSORDAPE> Obj_YTSORDAPE_Lista(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A)
        {
            // Inizializzazione
            List<Obj_YTSORDAPE> Lista = new List<Obj_YTSORDAPE>();
            Obj_YTSORDAPE _YTSORDAPE = new Obj_YTSORDAPE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _itm = db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date).GroupBy(n => new { n.ITMREF_0, n.SAU_0 }).Select(g => new { g.Key.ITMREF_0, g.Key.SAU_0 }).ToList();
                    foreach (var itm in _itm)
                    {
                        List<string> _PALNUM = new List<string>();
                        _YTSORDAPE = new Obj_YTSORDAPE();
                        List<YTSORDAPE> XData = db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == itm.ITMREF_0 && w.SAU_0 == itm.SAU_0).OrderBy(o => o.SOHNUM_0).ThenBy(o => o.SOPLIN_0).ThenBy(o => o.SOQSEQ_0).ToList();

                        //
                        string _OLD_KEY = "";
                        string _OLD_SOH = "";
                        foreach (YTSORDAPE item in XData)
                        {
                            if (_OLD_KEY != item.SALFCY_0 + "-" + item.BPCORD_0 + "-" + item.BPCNAM_0 + "-" + item.BPAADD_0 + "-" + item.ITMREF_0 + "-" + item.SAU_0)
                            {
                                _OLD_KEY = item.SALFCY_0 + "-" + item.BPCORD_0 + "-" + item.BPCNAM_0 + "-" + item.BPAADD_0 + "-" + item.ITMREF_0 + "-" + item.SAU_0;
                                _YTSORDAPE = new Obj_YTSORDAPE()
                                {
                                    SALFCY_0 = item.SALFCY_0,
                                    BPCORD_0 = item.BPCORD_0,
                                    BPCNAM_0 = item.BPCNAM_0,
                                    BPAADD_0 = item.BPAADD_0,
                                    SHIDAT_0 = item.SHIDAT_0,
                                    ITMREF_0 = item.ITMREF_0,
                                    QTY_0 = item.QTY_0,
                                    DLVQTY_0 = item.DLVQTY_0,
                                    SAU_0 = item.SAU_0,
                                    DLVQTYSTU_0 = item.DLVQTYSTU_0,
                                    QTYSTU_0 = item.QTYSTU_0,
                                    STU_0 = item.STU_0,
                                    ITMDES_0 = item.ITMDES_0,
                                    QTYPREP_0 = item.QTYPREP_0,
                                    PALNUM_0 = item.PALNUM_0,
                                    YPCU_0 = item.YPCU_0,
                                    YQTYPCU_0 = item.YQTYPCU_0,
                                    YPCUSTUCOE_0 = item.YPCUSTUCOE_0,
                                    SOHNUM_0 = item.SOHNUM_0,
                                    LOC_0 = item.LOC_0,
                                    NrRighe = 1
                                };
                                _OLD_SOH = item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0;
                            }
                            else
                            {
                                if (_OLD_SOH != item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0)
                                {
                                    _OLD_SOH = item.SOHNUM_0 + "_" + item.SOPLIN_0 + "_" + item.SOQSEQ_0;
                                    _YTSORDAPE.QTYSTU_0 = _YTSORDAPE.QTYSTU_0 + item.QTYSTU_0;
                                    _YTSORDAPE.QTY_0 = _YTSORDAPE.QTY_0 + item.QTY_0;
                                    _YTSORDAPE.DLVQTYSTU_0 = _YTSORDAPE.DLVQTYSTU_0 + item.DLVQTYSTU_0;
                                    _YTSORDAPE.DLVQTY_0 = _YTSORDAPE.DLVQTY_0 + item.DLVQTY_0;
                                    _YTSORDAPE.QTYPREP_0 = _YTSORDAPE.QTYPREP_0 + item.QTYPREP_0;
                                    _YTSORDAPE.NrRighe++;
                                }
                            }

                        }
                        Lista.Add(_YTSORDAPE);
                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDAPE>();
            }
        }

        public List<Obj_YTSORDAPE> Obj_YTSORDAPE_Ordini(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, string IN_ITMREF, string IN_PCU)
        {
            // Inizializzazione
            List<Obj_YTSORDAPE> Lista = new List<Obj_YTSORDAPE>();
            Obj_YTSORDAPE _YTSORDAPE = new Obj_YTSORDAPE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    List<YTSORDAPE> XData = db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == IN_ITMREF && w.YPCU_0 == IN_PCU).ToList();
                    foreach (YTSORDAPE item in XData)
                    {
                        _YTSORDAPE = new Obj_YTSORDAPE()
                        {
                            SOHNUM_0 = item.SOHNUM_0,
                            SALFCY_0 = item.SALFCY_0,
                            BPCORD_0 = item.BPCORD_0,
                            BPCNAM_0 = item.BPCNAM_0,
                            BPAADD_0 = item.BPAADD_0,
                            SHIDAT_0 = item.SHIDAT_0,
                            SOPLIN_0 = item.SOPLIN_0,
                            SOQSEQ_0 = item.SOQSEQ_0,
                            ITMREF_0 = item.ITMREF_0,
                            QTY_0 = item.QTY_0,
                            DLVQTY_0 = item.DLVQTY_0,
                            SAU_0 = item.SAU_0,
                            DLVQTYSTU_0 = item.DLVQTYSTU_0,
                            QTYSTU_0 = item.QTYSTU_0,
                            STU_0 = item.STU_0,
                            ITMDES_0 = item.ITMDES_0,
                            QTYPREP_0 = item.QTYPREP_0,
                            YPCU_0 = item.YPCU_0,
                            YPCUSTUCOE_0 = item.YPCUSTUCOE_0,
                            LOC_0 = item.LOC_0,
                            PALNUM_0= item.PALNUM_0
                        };
                        Lista.Add(_YTSORDAPE);
                    }
                    return Lista.OrderBy(o => o.SHIDAT_0).ThenBy(o => o.SOHNUM_0).ThenBy(o => o.SOPLIN_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDAPE>();
            }
        }

        public bool Obj_YTSORDAPE_Ordine_Singolo_Any(string IN_FCY, string IN_SOHNUM, bool IN_SPED)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    return db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.SOHNUM_0 == IN_SOHNUM && (!IN_SPED || w.QTYPREP_0 > 0)).Any();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return false;
            }
        }

        public string Obj_YTSORDAPE_Ordine_Singolo_BC(string IN_FCY, string IN_SOHNUM, bool IN_SPED, out string bc)
        {
            // Inizializzazione
            error = "";
            bc = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var query = db.YTSORDAPE
                                    .Where(w => w.SALFCY_0 == IN_FCY && w.SOHNUM_0 == IN_SOHNUM && (!IN_SPED || w.QTYPREP_0 > 0))
                                    .Select(w => new
                                    {
                                        w.BPCORD_0,
                                        w.BPAADD_0,
                                        SHIDAT = w.SHIDAT_0
                                    })
                                    .ToList();

                    if (query.Any())
                    {
                        var groupedResult = query
                                            .GroupBy(g => new { g.BPCORD_0, g.BPAADD_0 })
                                            .Select(g => new
                                            {
                                                BPCORD = g.Key.BPCORD_0,
                                                BPAADD = g.Key.BPAADD_0,
                                                MinSHIDAT = g.Min(x => x.SHIDAT.ToString("yyyyMMdd")),
                                                MaxSHIDAT = g.Max(x => x.SHIDAT.ToString("yyyyMMdd"))
                                            })
                                            .ToList();

                        bc = string.Join("|", groupedResult.Select(r => $"{r.BPCORD}|{r.BPAADD}|{r.MinSHIDAT:yyyy-MM-dd 00:00:00}|{r.MaxSHIDAT:yyyy-MM-dd 00:00:00}|{IN_SOHNUM}"));
                    }
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
            }
            return bc;
        }


        public Obj_YTSORDAPE Obj_YTSORDAPE_Articolo(string IN_FCY, string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, string IN_ITMREF)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    Obj_YTSORDAPE _YTSORDAPE = new Obj_YTSORDAPE();
                    List<string> _PALNUM = new List<string>();
                    foreach (YTSORDAPE item in db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.SHIDAT_0.Date >= IN_DT_DA.Date && w.SHIDAT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == IN_ITMREF).ToList())
                    {
                        if (_YTSORDAPE.ITMREF_0 != "")
                        {
                            _YTSORDAPE.QTYSTU_0 = _YTSORDAPE.QTYSTU_0 + item.QTYSTU_0;
                            _YTSORDAPE.QTY_0 = _YTSORDAPE.QTY_0 + item.QTY_0;
                            _YTSORDAPE.DLVQTYSTU_0 = _YTSORDAPE.DLVQTYSTU_0 + item.DLVQTYSTU_0;
                            _YTSORDAPE.DLVQTY_0 = _YTSORDAPE.DLVQTY_0 + item.DLVQTY_0;
                            _YTSORDAPE.QTYPREP_0 = _YTSORDAPE.QTYPREP_0 + item.QTYPREP_0;
                            _YTSORDAPE.NrRighe++;
                        }
                        else
                        {
                            _YTSORDAPE = new Obj_YTSORDAPE()
                            {
                                SOHNUM_0 = item.SOHNUM_0,
                                SALFCY_0 = item.SALFCY_0,
                                BPCORD_0 = item.BPCORD_0,
                                BPCNAM_0 = item.BPCNAM_0,
                                BPAADD_0 = item.BPAADD_0,
                                SHIDAT_0 = item.SHIDAT_0,
                                SOPLIN_0 = item.SOPLIN_0,
                                SOQSEQ_0 = item.SOQSEQ_0,
                                ITMREF_0 = item.ITMREF_0,
                                QTY_0 = item.QTY_0,
                                DLVQTY_0 = item.DLVQTY_0,
                                QTYPREP_0 = item.QTYPREP_0,
                                SAU_0 = item.SAU_0,
                                DLVQTYSTU_0 = item.DLVQTYSTU_0,
                                QTYSTU_0 = item.QTYSTU_0,
                                STU_0 = item.STU_0,
                                ITMDES_0 = item.ITMDES_0,
                                YPCU_0 = item.YPCU_0,
                                YQTYPCU_0 = item.YQTYPCU_0,
                                YPCUSTUCOE_0 = item.YPCUSTUCOE_0,
                                LOC_0 = item.LOC_0,
                                NrRighe = 1
                            };

                            //// SOMMA QTYPREP PER PALLET DIFFERENTI
                            //if (!_PALNUM.Contains(item.PALNUM_0) && item.PALNUM_0 != "")
                            //{
                            //    _PALNUM.Add(item.PALNUM_0);
                            //    _YTSORDAPE.QTYPREP_0 = item.QTYPREP_0;
                            //}
                        }
                    }
                    return _YTSORDAPE;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new Obj_YTSORDAPE();
            }
        }

        public List<Obj_YTSORDAPE> Obj_YTSORDAPE_Date(string IN_FCY, string IN_BPCORD, string IN_BPCADD, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDAPE> Lista = new List<Obj_YTSORDAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPCADD && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0, n.BPAADD_0, n.SHIDAT_0 }).Select(g => new { g.Key }).ToList())
                    {
                        Obj_YTSORDAPE _i = new Obj_YTSORDAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0,
                            BPAADD_0 = item.Key.BPAADD_0,
                            SHIDAT_0 = item.Key.SHIDAT_0
                        };
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.SHIDAT_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDAPE>();
            }
        }

        public List<Obj_YTSORDAPE> Obj_YTSORDAPE_Indirizzi(string IN_FCY, string IN_BPCORD, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDAPE> Lista = new List<Obj_YTSORDAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && w.BPCORD_0 == IN_BPCORD && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0, n.BPAADD_0 }).Select(g => new { g.Key }).ToList())
                    {
                        Obj_YTSORDAPE _i = new Obj_YTSORDAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0,
                            BPAADD_0 = item.Key.BPAADD_0
                        };
                        _i.BPAADDDES_0 = Obj_BPADDRESS_DESC(_i.BPCORD_0, _i.BPAADD_0);
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.BPAADD_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDAPE>();
            }
        }

        public List<Obj_YTSORDAPE> Obj_YTSORDAPE_Clienti(string IN_FCY, bool IN_SPED)
        {
            // Inizializzazione
            List<Obj_YTSORDAPE> Lista = new List<Obj_YTSORDAPE>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    foreach (var item in db.YTSORDAPE.Where(w => w.SALFCY_0 == IN_FCY && (!IN_SPED || w.QTYPREP_0 > 0)).GroupBy(
                                n => new { n.SALFCY_0, n.BPCORD_0, n.BPCNAM_0 }).Select(g => new { g.Key }).ToList())
                    {
                        Obj_YTSORDAPE _i = new Obj_YTSORDAPE()
                        {
                            SALFCY_0 = item.Key.SALFCY_0,
                            BPCORD_0 = item.Key.BPCORD_0,
                            BPCNAM_0 = item.Key.BPCNAM_0
                        };
                        Lista.Add(_i);
                    }
                    return Lista.OrderBy(o => o.BPCNAM_0).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSORDAPE>();
            }
        }



        #endregion

        #region SPEDIZIONI ORDINI - ORDINI PREPARATI

        public string GetFirstAvailablePalnum(short periode)
        {
            using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            {
                var pal = db.AVALNUM
                    .Where(a => a.CODNUM_0 == "YPLT" && a.PERIODE_0 == periode).FirstOrDefault();

                string toReturn = pal != null ? pal.VALEUR_0.ToString("#") : "No record found";
                //string toReturn = pal.FirstOrDefault().VALEUR_0.ToString("#");

                if (pal != null)
                {
                    pal.VALEUR_0 += 1; // Increment the value by 1
                    db.SubmitChanges();  // Save the changes to the database
                }

                return toReturn;
               
            }
        }
        public int CountPacchiDaSpedire(string nOrdine, string SALFCY, string BPCORD, string BPADD, DateTime dataDa, DateTime dataA)
        {
            using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            {
                try
                {
                    var ordiniPreparati = db.YTSORDAPE
                        .Where(a => a.SOHNUM_0 == nOrdine 
                            && a.SALFCY_0 == SALFCY
                            && a.BPCORD_0 == BPCORD
                            && a.BPAADD_0 == BPADD
                            && a.SHIDAT_0 >= dataDa && a.SHIDAT_0 <= dataA)
                        .Select(a => new { a.SOHNUM_0, a.SHIDAT_0, a.ITMREF_0 });

                    return ordiniPreparati.Count();
                }
                catch(Exception ex)
                {
                    throw;
                }

                return -1;
            }
        }


        //// LOAD Obj_Ytsprepord
        //public List<Obj_YTSPREPORD> Obj_YTSPREPORD_List(string IN_BPCORD, string IN_BPAADD, DateTime IN_DT)
        //{
        //    // Inizializzazione
        //    List<Obj_YTSPREPORD> OUT_Lista = new List<Obj_YTSPREPORD>();
        //    error = "";
        //    // Gestione Errore
        //    try
        //    {
        //        // DB
        //        using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
        //        {
        //            // Lettura Admin

        //            // Se esiste
        //            foreach(YTSPREPORD item in db.YTSPREPORD.Where(w => w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.DT_0 == IN_DT).ToList())
        //            {
        //                Obj_YTSPREPORD _i = new Obj_YTSPREPORD()
        //                {
        //                    UPDTICK_0 = item.UPDTICK_0,
        //                    BPCORD_0 = item.BPCORD_0,
        //                    BPAADD_0 = item.BPAADD_0,
        //                    DT_0 = item.DT_0,
        //                    PALNUM_0 = item.PALNUM_0
        //                };
        //                OUT_Lista.Add( _i );
        //            }
        //            return OUT_Lista;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Errore Generico
        //        error = ex.Message;
        //        return new List<Obj_YTSPREPORD>();
        //    }
        //}

        // LOAD Obj_Ytsprepord
        public bool Obj_YTSPREPORD_Save(string IN_USR, string IN_BPCORD, string IN_BPAADD, string IN_SOHNUM, int IN_SOPLIN, int IN_SOQSEQ_0, DateTime IN_DT, string IN_PALNUM, string LOT_0, decimal IN_QTY, string ITMREF)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    // Lettura Admin
                    YTSPREPORD _x = db.YTSPREPORD.Where(w => w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.DT_0 == IN_DT && w.SOHNUM_0 == IN_SOHNUM && w.SOPLIN_0 == IN_SOPLIN && w.SOQSEQ_0 == IN_SOQSEQ_0 && w.PALNUM_0 == IN_PALNUM && w.LOT_0 == LOT_0 && w.ITMREF_0 == ITMREF).FirstOrDefault();
                    if (_x != null)
                    {
                        _x.UPDTICK_0 = _x.UPDTICK_0 + 1;
                        _x.UPDDATTIM_0 = DateTime.Now;
                        _x.UPDUSR_0 = IN_USR;
                        _x.QTYPREP_0 = _x.QTYPREP_0 + IN_QTY;
                    }
                    else
                    {
                        YTSPREPORD _o = new YTSPREPORD()
                        {
                            UPDTICK_0 = 0,
                            BPCORD_0 = IN_BPCORD,
                            BPAADD_0 = IN_BPAADD,
                            DT_0 = IN_DT,
                            SOHNUM_0 = IN_SOHNUM,
                            SOPLIN_0 = IN_SOPLIN,
                            SOQSEQ_0 = IN_SOQSEQ_0,
                            PALNUM_0 = IN_PALNUM,
                            ITMREF_0 = ITMREF,
                            QTYPREP_0 = IN_QTY,
                            YLETTO_0 = 1,
                            CREDATTIM_0 = DateTime.Now,
                            UPDDATTIM_0 = DateTime.Parse("1753-01-01"),
                            AUUID_0 = new byte[] { 0x0 },
                            CREUSR_0 = IN_USR,
                            UPDUSR_0 = "",
                            LOT_0 = LOT_0
                        };
                        db.YTSPREPORD.InsertOnSubmit(_o);
                    }
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                throw new Exception(error);
                return false;
            }
        }

        public Obj_YTSPREPORDTEST Obj_YTSPREPORD_TEST(string IN_BPCORD, string IN_BPAADD, DateTime IN_DT_DA, DateTime IN_DT_A, string IN_ITMREF, string IN_SOHNUM)
        {
            // Inizializzazione
            List<Obj_YTSPREPORDTEST> Lista = new List<Obj_YTSPREPORDTEST>();
            Obj_YTSPREPORDTEST _YTSORDINEAPE = new Obj_YTSPREPORDTEST();
            error = "";
            // Gestione Errore
            try
            {

                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    Obj_YTSPREPORDTEST toReturn = new Obj_YTSPREPORDTEST();
                    var _itm = db.YTSPREPORD.Where(w => w.BPCORD_0 == IN_BPCORD && w.BPAADD_0 == IN_BPAADD && w.DT_0.Date >= IN_DT_DA.Date && w.DT_0.Date <= IN_DT_A.Date && w.ITMREF_0 == IN_ITMREF && w.SOHNUM_0 == IN_SOHNUM).FirstOrDefault();
                    
                    if(_itm != null)
                    {
                        toReturn.SOHNUM_0 = _itm.SOHNUM_0;
                        toReturn.QTYPREP_0 = _itm.QTYPREP_0;
                        toReturn.ITMREF_0 = _itm.ITMREF_0;

                    }
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new Obj_YTSPREPORDTEST();
            }
        }


        public void Obj_YTSPREPORD_Del_PALNUM(string IN_PALNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    db.YTSPREPORD.DeleteAllOnSubmit(db.YTSPREPORD.Where(w => w.PALNUM_0 == IN_PALNUM).ToList());
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
            }
        }

        #endregion

        #region STOALL
        public Obj_STOALL Obj_STOALL_Lista(string IN_SOHNUM, int IN_SOPLIN, int IN_SOQSEQ, string IN_ITMREF)
        {
            // Inizializzazione
            List<Obj_STOALL> Lista = new List<Obj_STOALL>();
            Obj_STOALL _STOALL = new Obj_STOALL();
            error = "";
            // Gestione Errore
            try
            {

                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    Obj_STOALL toReturn = new Obj_STOALL();
                    var _itm = db.STOALL.Where(w => w.VCRNUM_0 == IN_SOHNUM && w.VCRLIN_0 == IN_SOPLIN && w.VCRSEQ_0 == IN_SOQSEQ && w.ITMREF_0 == IN_ITMREF).FirstOrDefault();

                    if (_itm != null)
                    {
                        toReturn.VCRNUM_0 = _itm.VCRNUM_0;
                        toReturn.QTYSTU_0 = _itm.QTYSTU_0;
                        toReturn.ITMREF_0 = _itm.ITMREF_0;
                        toReturn.VCRLIN_0 = _itm.VCRLIN_0;
                        toReturn.VCRSEQ_0 = _itm.VCRSEQ_0;

                    }
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new Obj_STOALL();
            }
        }


        public List<Obj_YTSALLORD> Obj_YTSALLORD_Lista(string IN_FCY_0, string IN_BPCORD, string IN_BPAADD, DateTime IN_DATE_DA, DateTime IN_DATE_A)
        {
            // Inizializzazione
            List<Obj_YTSALLORD> Lista = new List<Obj_YTSALLORD>();
            Obj_YTSALLORD _YTSALLORD = new Obj_YTSALLORD();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    List<Obj_YTSALLORD> toReturn = new List<Obj_YTSALLORD>();
                    var query = from y in db.YTSALLORD
                                where 
                                  y.SALFCY_0 == IN_FCY_0
                                  && y.BPCORD_0 == IN_BPCORD
                                  && y.BPAADD_0 == IN_BPAADD
                                  && y.SHIDAT_0 >= IN_DATE_DA
                                  && y.SHIDAT_0 <= IN_DATE_A

                                orderby y.PALNUM_0
                                select new Obj_YTSALLORD
                                {
                                    SALFCY_0 = y.SALFCY_0,
                                    STOCOU_0 = y.STOCOU_0,
                                    ITMREF_0 = y.ITMREF_0,
                                    LOT_0    = y.LOT_0,
                                    SLO_0    = y.SLO_0,
                                    QTYSTU_0 = y.QTYSTU_0,
                                    BPCORD_0 = y.BPCORD_0,
                                    BPAADD_0 = y.BPAADD_0,
                                    SHIDAT_0 = y.SHIDAT_0,
                                    SOPLIN_0 = y.SOPLIN_0,
                                    SOQSEQ_0 = y.SOQSEQ_0,
                                    PALNUM_0 = y.PALNUM_0,
                                    VCRNUM_0 = y.VCRNUM_0,
                                    SAU_0    = y.SAU_0,
                                    STU_0    = y.STU_0,
                                    PCU_0 = y.PCU_0,
                                    QTYPCU_0 = y.QTYPCU_0,
                                    LOC_0 = y.LOC_0
                                };

                        toReturn = query.ToList();
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSALLORD>();
            }
        }

        public List<Obj_YTSALLORD> Obj_YTSALLORD_Lista(string IN_FCY_0, string IN_BPCORD, string IN_BPAADD, DateTime IN_DATE_DA, DateTime IN_DATE_A, string IN_PALNUM)
        {
            // Inizializzazione
            List<Obj_YTSALLORD> Lista = new List<Obj_YTSALLORD>();
            Obj_YTSALLORD _YTSALLORD = new Obj_YTSALLORD();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    List<Obj_YTSALLORD> toReturn = new List<Obj_YTSALLORD>();
                    var query = from y in db.YTSALLORD
                                where
                                  y.SALFCY_0 == IN_FCY_0
                                  && y.BPCORD_0 == IN_BPCORD
                                  && y.BPAADD_0 == IN_BPAADD
                                  && y.SHIDAT_0 >= IN_DATE_DA
                                  && y.SHIDAT_0 <= IN_DATE_A
                                  && y.PALNUM_0 == IN_PALNUM
                                orderby y.PALNUM_0
                                select new Obj_YTSALLORD
                                {
                                    SALFCY_0 = y.SALFCY_0,
                                    STOCOU_0 = y.STOCOU_0,
                                    ITMREF_0 = y.ITMREF_0,
                                    LOT_0 = y.LOT_0,
                                    SLO_0 = y.SLO_0,
                                    QTYSTU_0 = y.QTYSTU_0,
                                    BPCORD_0 = y.BPCORD_0,
                                    BPAADD_0 = y.BPAADD_0,
                                    SHIDAT_0 = y.SHIDAT_0,
                                    SOPLIN_0 = y.SOPLIN_0,
                                    SOQSEQ_0 = y.SOQSEQ_0,
                                    PALNUM_0 = y.PALNUM_0,
                                    VCRNUM_0 = y.VCRNUM_0,
                                    SAU_0 = y.SAU_0,
                                    STU_0 = y.STU_0
                                };

                    toReturn = query.ToList();
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_YTSALLORD>();
            }
        }


        #endregion

        #region SPEDIZIONI PREBOLLE - PREBOLLE

        // LIST Obj_STOPREH
        public List<Obj_STOPREH> Obj_STOPREH_Lista(string IN_FCY, string IN_USR, string IN_RIC)
        {
            // Inizializzazione
            List<Obj_STOPREH> Lista = new List<Obj_STOPREH>();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var result = from h in db.STOPREH
                                 join d in db.STOPRED on h.PRHNUM_0 equals d.PRHNUM_0
                                 join c in db.BPCUSTOMER on h.BPCORD_0 equals c.BPCNUM_0
                                 where h.DLVFLG_0 < 3 && (d.ALLTYP_0 == 1 || d.ALLTYP_0 == 5) && (h.PREUSR_0 == "" || h.PREUSR_0==IN_USR) && (IN_RIC=="" || h.PRHNUM_0.Contains(IN_RIC))
                                 group new { h, d } by new { h.STOFCY_0, h.PRHNUM_0, h.DLVFLG_0, h.PREUSR_0, h.BPCORD_0, c.BPCNAM_0 } into grouped
                                 select new
                                 {
                                     STOFCY_0 = grouped.Key.STOFCY_0,
                                     PRHNUM_0 = grouped.Key.PRHNUM_0,
                                     DLVFLG_0 = grouped.Key.DLVFLG_0,
                                     PREUSR_0 = grouped.Key.PREUSR_0,
                                     BPCNUM_0 = grouped.Key.BPCORD_0,
                                     BPCNAM_0 = grouped.Key.BPCNAM_0,
                                     RIGHE_TOT = grouped.Count(),
                                     RIGHE_PREP = grouped.Sum(x => x.d.FLGVT_0 > 0 ? 1 : 0)
                                 };

                    foreach (var _i in result)
                    {
                        Obj_STOPREH _s = new Obj_STOPREH()
                        {
                            DLVFLG_0 = _i.DLVFLG_0.ToString(),
                            PREUSR_0 = _i.PREUSR_0,
                            PRHNUM_0 = _i.PRHNUM_0,
                            STOFCY_0 = _i.STOFCY_0,
                            BPCNUM_0 = _i.BPCNUM_0,
                            BPCNAM_0 = _i.BPCNAM_0,
                            RIGHE_PREP = _i.RIGHE_PREP,
                            RIGHE_TOT = _i.RIGHE_TOT
                        };
                        Lista.Add(_s);

                    }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new List<Obj_STOPREH>();
            }
        }

        public Obj_STOPREH Obj_STOPREH_Load(string IN_FCY, string IN_USR, string IN_PRHNUM)
        {
            // Inizializzazione
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    Obj_STOPREH _obj = new Obj_STOPREH();
                    STOPREH _s = db.STOPREH.Where(w => w.STOFCY_0 == IN_FCY && w.PRHNUM_0 == IN_PRHNUM && (w.PREUSR_0 == "" || w.PREUSR_0 == IN_USR)).FirstOrDefault();

                    if (_s!=null) 
                    {
                        _obj = new Obj_STOPREH()
                        {
                            DLVFLG_0 = _s.DLVFLG_0.ToString(),
                            PREUSR_0 = _s.PREUSR_0,
                            PRHNUM_0 = _s.PRHNUM_0,
                            STOFCY_0 = _s.STOFCY_0,
                            BPCNUM_0 = _s.BPCORD_0
                        };
                        //
                        foreach (STOPRED _d in _s.STOPRED)
                        {
                            Obj_STOPRED _STOPRED = new Obj_STOPRED()
                            {
                                FLGVT = _d.FLGVT_0,
                                ITMDES = _d.ITMDES1_0,
                                ITMREF = _d.ITMREF_0,
                                PRELIN = _d.PRELIN_0,
                                QTYSTU = _d.QTYSTU_0,
                                SEQ = _d.SEQ_0
                            };
                            _obj._STOPRED.Add(_STOPRED);
                        }

                    }
                    return _obj;
                }
            }
            catch (Exception ex)
            {
                //Log Errore Generico
                error = ex.Message;
                return new Obj_STOPREH();
            }
        }

        #endregion

        #region PRODUZIONE
        #region MFGITM
        public Obj_MFGITM_ITMMASTER_PRODUZIONE Obj_MFGITM_ITMMASTER_PRODUZIONE_Load(string IN_FCY, string IN_MFGNUM, out string error)
        {
            // Inizializzazione
            Obj_MFGITM_ITMMASTER_PRODUZIONE OUT_Obj = new Obj_MFGITM_ITMMASTER_PRODUZIONE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _i = from p in db.MFGITM
                             join i in db.ITMMASTER on p.ITMREF_0 equals i.ITMREF_0
                             where p.MFGNUM_0 == IN_MFGNUM && p.MFGFCY_0 == IN_FCY
                             select new Obj_MFGITM_ITMMASTER_PRODUZIONE
                             {
                                 MFGFCY_0 = p.MFGFCY_0,
                                 MFGNUM_0 = p.MFGNUM_0,
                                 LOT_0 = p.LOT_0,
                                 PCU_0 = i.PCU_0,
                                 PCUSTUCOE_0 = i.PCUSTUCOE_0,
                                 ITMREF_0 = p.ITMREF_0,
                                 ITMDES1_0 = i.ITMDES1_0,
                                 UOM_0 = p.UOM_0,
                                 STU_0 = i.STU_0,
                                 EXTQTY_0 = p.EXTQTY_0,
                                 RMNEXTQTY_0 = p.RMNEXTQTY_0,
                                 UOMEXTQTY_0 = p.UOMEXTQTY_0,
                             };
                    OUT_Obj = _i.FirstOrDefault();

                    if (OUT_Obj != null)
                    {
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Ordine di produzione non trovato";
                        return new Obj_MFGITM_ITMMASTER_PRODUZIONE();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_MFGITM_ITMMASTER_PRODUZIONE();
            }
        }

        #endregion

        #region MFGMAT
        public Obj_MFGMAT_ITMMASTER_PRODUZIONE Obj_MFGMAT_ITMMASTER_PRODUZIONE_Load(string IN_FCY, string IN_MFGNUM, string IN_ITMREF, string IN_LOT, out string error)
        {
            // Inizializzazione
            Obj_MFGMAT_ITMMASTER_PRODUZIONE OUT_Obj = new Obj_MFGMAT_ITMMASTER_PRODUZIONE();
            error = "";
            // Gestione Errore
            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _i = from m in db.MFGMAT
                             join i in db.ITMMASTER on m.ITMREF_0 equals i.ITMREF_0
                             where m.MFGNUM_0 == IN_MFGNUM && m.MFGFCY_0 == IN_FCY && m.ITMREF_0 == IN_ITMREF //&& m.LOT_0 == IN_LOT
                             select new Obj_MFGMAT_ITMMASTER_PRODUZIONE
                             {
                                 MFGFCY_0 = m.MFGFCY_0,
                                 MFGNUM_0 = m.MFGNUM_0,
                                 MFGLIN_0 = m.MFGLIN_0,
                                 LOT_0 = m.LOT_0,
                                 ITMREF_0 = m.ITMREF_0,
                                 ITMDES1_0 = i.ITMDES1_0,
                                 STU_0 = m.STU_0,
                                 RETQTY_0 = m.RETQTY_0,
                                 USEQTY_0 = m.USEQTY_0,
                                 BOMOPE_0 = m.BOMOPE_0,
                                 BOMSEQ_0 = m.BOMSEQ_0,
                                 PCUSTUCOE_0 = i.PCUSTUCOE_0,
                                 PCU_0 = i.PCU_0,
                                 TSICOD_0 = i.TSICOD_0,
                                 TSICOD_3 = i.TSICOD_3,
                                 LOC_0 =  i.STOCK.LOC_0
                             };
                    OUT_Obj = _i.FirstOrDefault();

                    if (OUT_Obj != null)
                    {
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Ordine di produzione non trovato";
                        return new Obj_MFGMAT_ITMMASTER_PRODUZIONE();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new Obj_MFGMAT_ITMMASTER_PRODUZIONE();
            }
        }

        public List<Obj_Righe_Allocazione_Odp> Obj_MFGMAT_Load_Order_Material_List(string IN_FCY, string IN_MFGNUM, out string error)
        {
            List<Obj_Righe_Allocazione_Odp> OUT_Obj = new List<Obj_Righe_Allocazione_Odp>();
            error = "";

            try
            {
                // DB
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _i = from m in db.MFGMAT
                             join s in db.STOCK on m.ITMREF_0 equals s.ITMREF_0
                             where m.MFGNUM_0 == IN_MFGNUM && m.MFGFCY_0 == IN_FCY
                             select new Obj_Righe_Allocazione_Odp
                             { 
                                 MFGFCY_0 = m.MFGFCY_0,
                                 MFGNUM_0 = m.MFGNUM_0,
                                 MFGLIN_0 = m.MFGLIN_0,
                                 ITMREF_0 = m.ITMREF_0,
                                 STU_0 = m.STU_0,
                                 RETQTY_0 = m.RETQTY_0,
                                 USEQTY_0 = m.USEQTY_0,
                                 LOC_0 = s.LOC_0
                             };
                    OUT_Obj = _i.ToList();

                    if (OUT_Obj != null)
                    {
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Materiale non trovato per quest'ordine";
                        return new List<Obj_Righe_Allocazione_Odp>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Errore Generico
                error = ex.Message;
                return new List<Obj_Righe_Allocazione_Odp>();
            }

        }


        public bool IsProdOrderValid(string IN_FCY, string IN_MFGNUM)
        {
            using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            {
                return db.MFGMAT.Where(w => w.MFGNUM_0 == IN_MFGNUM).Any();
            }

        }

        public Obj_YSCARMAT Obj_YSCARMAT_Load(string IN_FCY, string IN_MFGNUM, string IN_ITMREF, string IN_LOT, out string error)
            {
                // Inizializzazione
                Obj_YSCARMAT OUT_Obj = new Obj_YSCARMAT();
                error = "";
                // Gestione Errore
                try
                {
                    // DB
                    using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                    {
                        var _i = from m in db.YSCARMAT
                                 join i in db.ITMMASTER on m.ITMREF_0 equals i.ITMREF_0
                                 where m.MFGNUM_0 == IN_MFGNUM && m.MFGFCY_0 == IN_FCY && m.ITMREF_0 == IN_ITMREF && m.LOT_0 == IN_LOT
                                 select new Obj_YSCARMAT
                                 {
                                     MFGFCY_0 = m.MFGFCY_0,
                                     MFGNUM_0 = m.MFGNUM_0,
                                     MFGLIN_0 = m.MFGLIN_0,
                                     ITMREF_0 = m.ITMREF_0,
                                     STU_0 = m.STU_0,
                                     RETQTY_0 = m.RETQTY_0,
                                     USEQTY_0 = m.USEQTY_0,
                                     QTYSTU_0 = m.QTYSTU_0,
                                     DISPO_0 = m.DISPO_0,
                                     BOMOPE_0 = m.BOMOPE_0,
                                     BOMSEQ_0 = m.BOMSEQ_0,
                                     ITMDES1_0 = i.ITMDES1_0,
                                     PCUSTUCOE_0 = i.PCUSTUCOE_0,
                                     PCU_0 = i.PCU_0,
                                     TSICOD_0 = i.TSICOD_0,
                                     TSICOD_3 = i.TSICOD_3,
                                     LOT_0 = m.LOT_0,
                                     LOC_0 = m.LOC_0
                                 };
                        OUT_Obj = _i.FirstOrDefault();

                        if (OUT_Obj != null)
                        {
                            return OUT_Obj;
                        }
                        else
                        {
                            error = "Materiale non trovato";
                            return new Obj_YSCARMAT();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log Errore Generico
                    error = ex.Message;
                    return new Obj_YSCARMAT();
                }
            }

        public bool IsBarcodeAlreadyRead(string barcode)
        {
            using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
            {
                return db.YTSBARCODE.Where(w => w.BARCODE_0 == barcode).Any();
            }

        }

        public Obj_YCONSMAT Obj_YCONSMAT_Load(string nOrdine, out string error)
        {
            Obj_YCONSMAT OUT_Obj = new Obj_YCONSMAT();
            error = "";

            try
            {
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _i = from m in db.YCONSMAT
                             where m.MFGNUM_0 == nOrdine
                             select new Obj_YCONSMAT
                             {
                                 MFGNUM_0 = m.MFGNUM_0,
                                 USEQTY_0 = m.USEQTY_0,
                                 STDQTY_0 = m.STDQTY_0,
                                 STUCOE_0 = m.STUCOE_0
                             };
                    OUT_Obj = _i.FirstOrDefault();

                    if (OUT_Obj != null)
                    {
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Materiale non trovato";
                        return new Obj_YCONSMAT();
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return new Obj_YCONSMAT();
            }
        }

        public Obj_YCONSCAR Obj_YCONSCAR_Load(string nOrdine, out string error)
        {
            Obj_YCONSCAR OUT_Obj = new Obj_YCONSCAR();
            error = "";

            try
            {
                using (DBClassesDataContext db = new DBClassesDataContext(connectionSQL))
                {
                    var _i = from m in db.YCONSCAR
                             where m.MFGNUM_0 == nOrdine
                             select new Obj_YCONSCAR
                             {
                                 MFGNUM_0 = m.MFGNUM_0,
                                 AVA_PREV = m.AVA_PREV_0,
                                 AVA_TOT = m.AVA_TOT_0,
                             };
                    OUT_Obj = _i.FirstOrDefault();

                    if (OUT_Obj != null && OUT_Obj.DIFFERENZAAVANZAMENTO <= 0)
                    {
                        return OUT_Obj;
                    }
                    else
                    {
                        error = "Risultano materiali non scaricati";
                        return new Obj_YCONSCAR();
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return new Obj_YCONSCAR() { };
            }

        }

        #endregion
        #endregion

    }


}