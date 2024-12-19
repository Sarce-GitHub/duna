using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace X3_TERMINALINI
{
    #region BASE
    
    public class Obj_YTSUTX
    {
        public string USR_TERM_0 { get; set; }
        public string PSW_0 { get; set; }
        public string USR_X3_0 { get; set; }
        public string DESCR_0 { get; set; }
        public int ATTIVO_0 { get; set; }
        public string FCY_0 { get; set; }
        public int ABIL1_0 { get; set; }
        public int ABIL2_0 { get; set; }
        public int ABIL3_0 { get; set; }
        public int ABIL4_0 { get; set; }
        public int ABIL5_0 { get; set; }
        public int ABIL6_0 { get; set; }
        public int ABIL7_0 { get; set; }
        public int ABIL8_0 { get; set; }
        public int ABIL9_0 { get; set; }

        public Obj_YTSUTX()
        {
            USR_TERM_0 = "";
            PSW_0 = "";
            USR_X3_0 = "";
            DESCR_0 = "";
            ATTIVO_0 = 0;
            FCY_0 = "";
            ABIL1_0 = 0;
            ABIL2_0 = 0;
            ABIL3_0 = 0;
            ABIL4_0 = 0;
            ABIL5_0 = 0;
            ABIL6_0 = 0;
            ABIL7_0 = 0;
            ABIL8_0 = 0;
            ABIL9_0 = 0;
        }

        public Obj_YTSUTX(string[] IN_Base, string[] IN_Abil)
        {
            // string _base = "USEOK|" + _utx.USR_X3_0 + "|" + _utx.USR_TERM_0 + "|" + _utx.FCY_0+ "|" + _utx.DESCR_0;
            // string _abil = _utx.ABIL1_0 + "|" + _utx.ABIL2_0 + "|" + _utx.ABIL3_0 + "|" + _utx.ABIL4_0 + "|" + _utx.ABIL5_0 + "|" + _utx.ABIL7_0 + "|" + _utx.ABIL8_0 + "|" + _utx.ABIL9_0;
            
            try
            {
                USR_X3_0 = IN_Base[1];
                USR_TERM_0 = IN_Base[2];
                FCY_0 = IN_Base[3];
                DESCR_0 = IN_Base[4];
                ATTIVO_0 = (IN_Base[0]== "USEOK" ? 2 : 1);
                //
                ABIL1_0 = int.Parse(IN_Abil[0]);
                ABIL2_0 = int.Parse(IN_Abil[1]);
                ABIL3_0 = int.Parse(IN_Abil[2]);
                ABIL4_0 = int.Parse(IN_Abil[3]);
                ABIL5_0 = int.Parse(IN_Abil[4]);
                ABIL6_0 = int.Parse(IN_Abil[5]);
                ABIL7_0 = int.Parse(IN_Abil[6]);
                ABIL8_0 = int.Parse(IN_Abil[7]);
                ABIL9_0 = int.Parse(IN_Abil[8]);
            }
            catch (Exception ex)
            {
                USR_TERM_0 = "";
                PSW_0 = "";
                USR_X3_0 = "";
                DESCR_0 = ex.Message;
                ATTIVO_0 = 0;
                FCY_0 = "";
            }

        }
    }

    public class Obj_YTSLOG
    {
        public int UPDTICK_0 { get; set; }
        public string USR_X3_0 { get; set; }
        public string USR_TERM_0 { get; set; }
        public string FUNZIONE_0 { get; set; }
        public string ESITO_0 { get; set; }
        public string MESSAGGIO_0 { get; set; }
        public string VARIABILE_0 { get; set; }
        public string VARIABILE_1 { get; set; }
        public string VARIABILE_2 { get; set; }
        public string VARIABILE_3 { get; set; }
        public string VARIABILE_4 { get; set; }
        public string VARIABILE_5 { get; set; }
        public string VARIABILE_6 { get; set; }
        public string VARIABILE_7 { get; set; }
        public string VARIABILE_8 { get; set; }
        public string VARIABILE_9 { get; set; }
        public string VARIABILE_10 { get; set; }
        public string VARIABILE_11 { get; set; }
        public string VARIABILE_12 { get; set; }
        public string VARIABILE_13 { get; set; }
        public string VARIABILE_14 { get; set; }
        public DateTime CREDATTIM_0 { get; set; }
        public DateTime UPDDATTIM_0 { get; set; }
        public byte[] AUUID_0 { get; set; }
        public string CREUSR_0 { get; set; }
        public string UPDUSR_0 { get; set; }
        public decimal ROWID { get; set; }

        public Obj_YTSLOG()
        {
            UPDTICK_0 = 0;
            USR_X3_0 = "";
            USR_TERM_0 = "";
            FUNZIONE_0 = "";
            ESITO_0 = "";
            MESSAGGIO_0 = "";
            VARIABILE_0 = "";
            VARIABILE_1 = "";
            VARIABILE_2 = "";
            VARIABILE_3 = "";
            VARIABILE_4 = "";
            VARIABILE_5 = "";
            VARIABILE_6 = "";
            VARIABILE_7 = "";
            VARIABILE_8 = "";
            VARIABILE_9 = "";
            VARIABILE_10 = "";
            VARIABILE_11 = "";
            VARIABILE_12 = "";
            VARIABILE_13 = "";
            VARIABILE_14 = "";
            CREDATTIM_0 = DateTime.MinValue;
            UPDDATTIM_0 = DateTime.MinValue;
            AUUID_0 = new byte[] { 0x0 };
            CREUSR_0 = "";
            UPDUSR_0 = "";
            ROWID = 0;
        }
    }

    #endregion

    #region STOCK

    public class obj_STOLOC
    {
        public string STOFCY_0 { get; set; }
        public string LOC_0 { get; set; }
        public string WRH_0 { get; set; }
        public int OCPCOD_0 { get; set; }
        public string LOCTYP_0 { get; set; }
        public int LOCCAT_0 { get; set; }
        public string LOCNUMFMT_0 { get; set; }
        public string PPSSEQ_0 { get; set; }
        public int MONITMFLG_0 { get; set; }
        public int DEDFLG_0 { get; set; }
        public int REAFLG_0 { get; set; }
        public int FRGMGTMOD_0 { get; set; }
        public int TEMLTI_0 { get; set; }
        public int FILMGTFLG_0 { get; set; }
        public string AUZSST_0 { get; set; }
        public DateTime AVADAT_0 { get; set; }
        public string AVAHOU_0 { get; set; }
        public string ALLUSR_0 { get; set; }
        public DateTime ALLDAT_0 { get; set; }
        public string ALLHOU_0 { get; set; }
        public decimal MAXAUZWEI_0 { get; set; }
        public int WID_0 { get; set; }
        public int HEI_0 { get; set; }
        public int DTH_0 { get; set; }
        public int LOKSTA_0 { get; set; }
        public int CUNLOKFLG_0 { get; set; }
        public string PCU_0 { get; set; }
        public decimal PCUSTUCOE_0 { get; set; }
        public decimal QTYPCU_0 { get; set; }
        public decimal MAXQTYPCU_0 { get; set; }

        public obj_STOLOC()
        {
            STOFCY_0 = "";
            LOC_0 = "";
            WRH_0 = "";
            OCPCOD_0 = 0;
            LOCTYP_0 = "";
            LOCCAT_0 = 0;
            LOCNUMFMT_0 = "";
            PPSSEQ_0 = "";
            MONITMFLG_0 = 0;
            DEDFLG_0 = 0;
            REAFLG_0 = 0;
            FRGMGTMOD_0 = 0;
            TEMLTI_0 = 0;
            FILMGTFLG_0 = 0;
            AUZSST_0 = "";
            AVADAT_0 = DateTime.MinValue;
            AVAHOU_0 = "";
            ALLUSR_0 = "";
            ALLDAT_0 = DateTime.MinValue;
            ALLHOU_0 = "";
            MAXAUZWEI_0 = 0;
            WID_0 = 0;
            HEI_0 = 0;
            DTH_0 = 0;
            LOKSTA_0 = 0;
            CUNLOKFLG_0 = 0;
            PCU_0 = "";
            PCUSTUCOE_0 = 0;
            QTYPCU_0 = 0;
            MAXQTYPCU_0 = 0;
        }

    }

    public class Obj_STOCK
    {
        public string STOFCY_0 { get; set; }
        public decimal STOCOU_0 { get; set; }
        public string OWNER_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public string ITMDES_0 { get; set; }
        private string _LOT_0;
        private string _SLO_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }

        public string SLO_0
        {
            get => _SLO_0;
            set => _SLO_0 = value?.Trim();
        }

        private string _BPSLOT_0;
        public string BPSLOT_0
        {
            get => _BPSLOT_0;
            set => _BPSLOT_0 = value?.Trim();
        }
        public string PALNUM_0 { get; set; }
        public string CTRNUM_0 { get; set; }
        public string STA_0 { get; set; }
        public string LOC_0 { get; set; }
        public string LOCTYP_0 { get; set; }
        public int LOCCAT_0 { get; set; }
        public string WRH_0 { get; set; }
        public string SERNUM_0 { get; set; }
        public DateTime RCPDAT_0 { get; set; }
        public string STU_0 { get; set; }
        public string PCU_0 { get; set; }
        public decimal PCUSTUCOE_0 { get; set; }
        public decimal QTYPCU_0 { get; set; }
        public decimal QTYSTU_0 { get; set; }
        public decimal QTYSTUACT_0 { get; set; }
        public string PCUORI_0 { get; set; }
        public decimal QTYPCUORI_0 { get; set; }
        public decimal QTYSTUORI_0 { get; set; }
        public string QLYCTLDEM_0 { get; set; }
        public decimal CUMALLQTY_0 { get; set; }
        public decimal CUMALLQTA_0 { get; set; }
        public decimal CUMWIPQTY_0 { get; set; }
        public decimal CUMWIPQTA_0 { get; set; }
        public int EDTFLG_0 { get; set; }
        public DateTime LASRCPDAT_0 { get; set; }
        public DateTime LASISSDAT_0 { get; set; }
        public DateTime LASCUNDAT_0 { get; set; }
        public int CUNLOKFLG_0 { get; set; }
        public string CUNLISNUM_0 { get; set; }
        public int EXPNUM_0 { get; set; } 
        public string ECCVALMAJ_0 { get; set; }
        public string STOFLD1_0 { get; set; }
        public string STOFLD2_0 { get; set; }
        public string PJT_0 { get; set; }
        public string LPNNUM_0 { get; set; }
        public string SEAKEY_0 { get; set; } //per etichetta

        public Obj_STOCK()
        {
            STOFCY_0 = "";
            STOCOU_0 = 0;
            OWNER_0 = "";
            ITMREF_0 = "";
            ITMDES_0 = "";
            LOT_0 = "";
            SLO_0 = "";
            BPSLOT_0 = "";
            PALNUM_0 = "";
            CTRNUM_0 = "";
            STA_0 = "";
            LOC_0 = "";
            LOCTYP_0 = "";
            LOCCAT_0 = 0;
            WRH_0 = "";
            SERNUM_0 = "";
            RCPDAT_0 = DateTime.MinValue;
            PCU_0 = "";
            STU_0 = "";
            PCUSTUCOE_0 = 0;
            QTYPCU_0 = 0;
            QTYSTU_0 = 0;
            QTYSTUACT_0 = 0;
            PCUORI_0 = "";
            QTYPCUORI_0 = 0;
            QTYSTUORI_0 = 0;
            QLYCTLDEM_0 = "";
            CUMALLQTY_0 = 0;
            CUMALLQTA_0 = 0;
            CUMWIPQTY_0 = 0;
            CUMWIPQTA_0 = 0;
            EDTFLG_0 = 0;
            LASRCPDAT_0 = DateTime.MinValue;
            LASISSDAT_0 = DateTime.MinValue;
            LASCUNDAT_0 = DateTime.MinValue;
            CUNLOKFLG_0 = 0;
            CUNLISNUM_0 = "";
            EXPNUM_0 = 0;
            ECCVALMAJ_0 = "";
            STOFLD1_0 = "";
            STOFLD2_0 = "";
            PJT_0 = "";
            LPNNUM_0 = "";
            SEAKEY_0 = "";
        }
    }

    public class Obj_STOCK_ETIC
    {
        public string ETIC { get; set; }
        public string ITMREF { get; set; }
        public string LOT { get; set; }
        public string SLO { get; set; }
        public string SEAKEY { get; set; }

        public Obj_STOCK_ETIC()
        {
            ETIC = "";
            ITMREF = "";
            LOT = "";
            SLO = "";
            SEAKEY = "";
        }

        public Obj_STOCK_ETIC(string _e)
        {
            ETIC = _e;
            string[] ArrEt = _e.Trim().ToUpper().Split(Properties.Settings.Default.Etic_Split.ToCharArray());
            ITMREF = ArrEt[0];
            LOT = (ArrEt.Length > 1 ? ArrEt[1] : "");
            SLO = (ArrEt.Length > 2 ? ArrEt[2] : "");

            cls_SQL _SQL = new cls_SQL();
            //bool Obj_ITM_GEST_LOT()
            bool gestisceLotto = _SQL.obj_ITMMASTER_LoadGestLot(ITMREF).LOTMGTCOD_0 == 3;

            // CONVERSIONE DATI ETICHETTA ZZZ - QUANDO ARRIVO DA SEAKEY
            if (Properties.Settings.Default.Abil_SEAKEY && LOT == "" && gestisceLotto)
            {
                Obj_STOCK_ETIC _x = _SQL.obj_STOCK_SEAKEY(ITMREF);
                if (_x.ITMREF!="")
                {
                    ITMREF= _x.ITMREF;
                    LOT= _x.LOT;
                }
            }
        }
    }

    #endregion

    #region YTSALLORD
    public class Obj_YTSALLORD
    {
        public string SALFCY_0 { get; set; }
        public decimal STOCOU_0 { get; set; }
        public string ITMREF_0 { get; set; }
        private string _LOT_0;
        private string _SLO_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }

        public string SLO_0
        {
            get => _SLO_0;
            set => _SLO_0 = value?.Trim();
        }

        public decimal QTYSTU_0 { get; set; }
        public string BPCORD_0 { get; set; }

        public string BPAADD_0 { get; set; }
        public DateTime SHIDAT_0 { get; set; }
        public int SOPLIN_0 { get; set; }
        public int SOQSEQ_0 { get; set; }
        public string PALNUM_0 { get; set; }

        public string VCRNUM_0 { get; set; }
        public string SAU_0 { get; set; }
        public string STU_0 { get; set; }
        public string PCU_0 { get; set; }
        public decimal QTYPCU_0 { get; set; }
        public string LOC_0 { get; set; }

        public Obj_YTSALLORD()
        {
            SALFCY_0 = "";
            STOCOU_0 = 0;
            ITMREF_0 = "";
            LOT_0 = "";
            SLO_0 = "";
            QTYSTU_0 = 0;
            BPCORD_0 = "";
            BPAADD_0 = "";
            SHIDAT_0 = DateTime.MinValue;
            SOPLIN_0 = 0;
            SOQSEQ_0 = 0;
            PALNUM_0 = "";
            VCRNUM_0 = "";
            SAU_0 = ""; 
            STU_0 = "";
            PCU_0 = "";
            QTYPCU_0 = 0;
            LOC_0 = "";
        }



    }
    #endregion

    #region STOALL
    public class Obj_STOALL
    {
        public string STOFCY_0 { get; set; }
        public decimal STOCOU_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public int SEQ_0 { get; set; }
        public int ALLTYP_0 { get; set; }
        public int VCRTYP_0 { get; set; }
        public string VCRNUM_0 { get; set; }
        public int VCRLIN_0 { get; set; }
        public int VCRSEQ_0 { get; set; }

        private string _LOT_0;
        private string _SLO_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }

        public string SLO_0
        {
            get => _SLO_0;
            set => _SLO_0 = value?.Trim();
        }

        public string STA_0 { get; set; }
        public string LOC_0 { get; set; }
        public string WRH_0 { get; set; }
        public string SERNUM_0 { get; set; }
        public decimal QTYSTU_0 { get; set; }
        public decimal QTYSTUACT_0 { get; set; }
        public string BPRNUM_0 { get; set; }
        public string BPAADD_0 { get; set; }



        public Obj_STOALL()
        {
            STOFCY_0 = "";
            STOCOU_0 = 0;
            ITMREF_0 = "";
            LOT_0 = "";
            SLO_0 = "";
            STA_0 = "";
            LOC_0 = "";
            WRH_0 = "";
            SERNUM_0 = "";
            QTYSTU_0 = 0;
            QTYSTUACT_0 = 0;
            SEQ_0 = 0;
            ALLTYP_0 = 0;
            VCRTYP_0 = 0;
            VCRNUM_0 = "";
            VCRLIN_0 = 0;
            VCRSEQ_0 = 0;
            BPRNUM_0 = "";
            BPAADD_0 = "";
        }
    }

    #endregion

    #region PREPARAZIONE

    public class Obj_YTSORDINEAPE
    {
        public string SOHNUM_0 { get; set; }
        public string SALFCY_0 { get; set; }
        public string BPCORD_0 { get; set; }
        public string BPCNAM_0 { get; set; }
        public string BPAADD_0 { get; set; }
        public string BPAADDDES_0 { get; set; }
        public DateTime SHIDAT_0 { get; set; }
        public int SOPLIN_0 { get; set; }
        public int SOQSEQ_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public string ITMDES_0 { get; set; }
        public decimal QTY_0 { get; set; }
        public decimal DLVQTY_0 { get; set; }
        public string SAU_0 { get; set; }
        public decimal QTYSTU_0 { get; set; }
        public decimal DLVQTYSTU_0 { get; set; }
        public string STU_0 { get; set; }
        public string LOT_0 { get; set; }
        public string SLO_0 { get; set; }
        public string PALNUM_0 { get; set; }
        public decimal QTYPREP_0 { get; set; }
        public string SDHNUM_0 { get; set; }
        public int NrRighe { get; set; }

        public Obj_YTSORDINEAPE()
        {

            SOHNUM_0 = "";
            SALFCY_0 = "";
            BPCORD_0 = "";
            BPCNAM_0 = "";
            BPAADD_0 = "";
            BPAADDDES_0 = "";
            SHIDAT_0 = DateTime.MinValue;
            SOPLIN_0 = 0;
            SOQSEQ_0 = 0;
            ITMREF_0 = "";
            ITMDES_0 = "";
            QTY_0 = 0;
            DLVQTY_0 = 0;
            SAU_0 = "";
            QTYSTU_0 = 0;
            DLVQTYSTU_0 = 0;
            STU_0 = "";
            LOT_0 = "";
            SLO_0 = "";
            PALNUM_0 = "";
            QTYPREP_0 = 0;
            SDHNUM_0 = "";
        }

        public decimal QTY_MANC
        {
            get
            {
                return (QTY_0 - DLVQTY_0) - QTYPREP_0;
            }
        }
    }


    public class Obj_YTSORDAPE
    {
        public string SOHNUM_0 { get; set; }
        public string SALFCY_0 { get; set; }
        public string BPCORD_0 { get; set; }
        public string BPCNAM_0 { get; set; }
        public string BPAADD_0 { get; set; }
        public string BPAADDDES_0 { get; set; }
        public DateTime SHIDAT_0 { get; set; }
        public int SOPLIN_0 { get; set; }
        public int SOQSEQ_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public string ITMDES_0 { get; set; }
        public decimal QTY_0 { get; set; }
        public decimal DLVQTY_0 { get; set; }
        public string SAU_0 { get; set; }
        public decimal QTYSTU_0 { get; set; }
        public decimal DLVQTYSTU_0 { get; set; }
        public string STU_0 { get; set; }
        public decimal QTYPREP_0 { get; set; }
        public string PALNUM_0 { get; set; }
        public int NrRighe { get; set; }
        public string YPCU_0 { get; set; }
        public decimal YQTYPCU_0 { get; set; }
        public decimal YPCUSTUCOE_0 { get; set; }
        public string LOC_0 { get; set; }

        public Obj_YTSORDAPE()
        {

            SOHNUM_0 = "";
            SALFCY_0 = "";
            BPCORD_0 = "";
            BPCNAM_0 = "";
            BPAADD_0 = "";
            BPAADDDES_0 = "";
            SHIDAT_0 = DateTime.MinValue;
            SOPLIN_0 = 0;
            SOQSEQ_0 = 0;
            ITMREF_0 = "";
            ITMDES_0 = "";
            QTY_0 = 0;
            DLVQTY_0 = 0;
            SAU_0 = "";
            QTYSTU_0 = 0;
            DLVQTYSTU_0 = 0;
            QTYPREP_0 = 0;
            STU_0 = "";
            PALNUM_0 = "";
            YPCU_0 = "";
            YQTYPCU_0 = 0;
            YPCUSTUCOE_0 = 0;
            LOC_0 = "";
        }

        public decimal QTY_MANC
        {
            get
            {
                return (QTY_0 / YPCUSTUCOE_0) - (DLVQTY_0 / YPCUSTUCOE_0) - (QTYPREP_0 / YPCUSTUCOE_0);
            }
        }
    }


    public class Obj_ITMMASTER
    {
        public string ITMREF_0 { get; set; }
        public string CPY_0 { get; set; }
        public string ITMDES1_0 { get; set; }
        public string STU_0 { get; set; }
        public int LOTMGTCOD_0 { get; set; }
        public string PCU_0 { get; set; }
        public decimal PCUSTUCOE_0 { get; set; }

        public Obj_ITMMASTER()
        {
            ITMREF_0 = "";
            CPY_0 = "";
            ITMDES1_0 = "";
            STU_0 = "";
            LOTMGTCOD_0 = 0;
            PCU_0 = "";
            PCUSTUCOE_0 = 0;
        }
    }

    public class Obj_STOPREH
    {
        public string STOFCY_0 { get; set; }
        public string PRHNUM_0 { get; set; }
        public string BCPNUM { get; set; }
        public string BPCNUM_0 { get; set; }
        public string BPCNAM_0 { get; set; }
        public string DLVFLG_0 { get; set; }
        public string PREUSR_0 { get; set; }
        public int RIGHE_TOT { get; set; }
        public int RIGHE_PREP { get; set; }

        public List<Obj_STOPRED> _STOPRED{ get; set; }

        public Obj_STOPREH()
        {
            STOFCY_0 = "";
            PRHNUM_0 = "";
            BPCNUM_0 = "";
            BPCNAM_0 = "";
            DLVFLG_0 = "";
            PREUSR_0 = "";
            RIGHE_TOT = 0;
            RIGHE_PREP = 0;
            //
            _STOPRED = new List<Obj_STOPRED>();
        }

    }

    public class Obj_STOPRED
    {
        public int PRELIN { get; set; }
        public string ITMREF { get; set; }
        public string ITMDES { get; set; }
        public string LOCTYP_P { get; set; }
        public string LOC_P { get; set; }
        public decimal STOCOU { get; set; }
        public int SEQ { get; set; }
        public string LOT { get; set; }
        public string SLO { get; set; }
        public string STA { get; set; }
        public decimal QTYSTU { get; set; }
        public string STU { get; set; }
        public decimal QTYSTU_PREL { get; set; }
        public int FLGVT { get; set; }


        public Obj_STOPRED()
        {

            PRELIN = 0;
            ITMREF = "";
            ITMDES = "";
            LOCTYP_P = "";
            LOC_P = "";
            STOCOU = 0;
            SEQ = 0;
            LOT = "";
            SLO = "";
            STA = "";
            QTYSTU = 0;
            STU = "";
            FLGVT = 0;

        }
    }

    public class Obj_YTSPREPORD
    {
        public int UPDTICK_0 { get; set; }
        public string BPCORD_0 { get; set; }
        public string BPAADD_0 { get; set; }
        public DateTime DT_0 { get; set; }
        public string PALNUM_0 { get; set; }


        public Obj_YTSPREPORD()
        {
            UPDTICK_0 = 0;
            BPCORD_0 = "";
            BPAADD_0 = "";
            DT_0 = DateTime.MinValue;
            PALNUM_0 = "";

        }


    }

    public class Obj_YTSPREPORDTEST
    {
        public int UPDTICK_0 { get; set; }
        public string BPCORD_0 { get; set; }
        public string BPAADD_0 { get; set; }
        public DateTime DT_0 { get; set; }
        public string PALNUM_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public string SOHNUM_0 { get; set; }
        public decimal QTYPREP_0 { get; set; } 


        public Obj_YTSPREPORDTEST()
        {
            UPDTICK_0 = 0;
            BPCORD_0 = "";
            BPAADD_0 = "";
            DT_0 = DateTime.MinValue;
            PALNUM_0 = "";
            SOHNUM_0 = "";
            QTYPREP_0 = 0;
        }
    }

    public class Obj_AVALNUM
    {
        public string CODNUM_0 { get; set; }
        public int PERIODE_0 { get; set; }
        public decimal VALEUR_0 { get; set; }

        public Obj_AVALNUM()
        {
            CODNUM_0 = "";
            PERIODE_0 = 0;
            VALEUR_0 = 0;
        }
    }

    #endregion

    #region PRODUZIONE
    public class Obj_MFGITM
    {
        public string MFGFCY_0 { get; set; }
        public string MFGNUM_0 { get; set; }
        private string _LOT_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }
        public string ITMREF_0 { get; set; }
        public string UOM_0 { get; set; }
        public string STU_0 { get; set; }
        public decimal EXTQTY_0 { get; set; }
        public decimal RMNEXTQTY_0 { get; set; }

        public Obj_MFGITM()
        {
           MFGFCY_0 = "";
           MFGNUM_0 = "";
           LOT_0 = "";
           ITMREF_0 = "";
           UOM_0 = "";
           STU_0 = "";
           EXTQTY_0 = 0;
           RMNEXTQTY_0 = 0;
        }

    }

    public class Obj_MFGMAT
    {
        public string MFGFCY_0 { get; set; }
        public string MFGNUM_0 { get; set; }
        public int MFGLIN_0 { get; set; }
        public string ITMREF_0 { get; set; }
        private string _LOT_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }
        public string STU_0 { get; set; }
        public decimal RETQTY_0 { get; set; }
        public decimal USEQTY_0 { get; set; }

        public Obj_MFGMAT()
        {
            MFGFCY_0 = "";
            MFGNUM_0 = "";
            MFGLIN_0 = 0;
            ITMREF_0 = "";
            LOT_0 = "";
            STU_0 = "";
            RETQTY_0 = 0;
            USEQTY_0 = 0;
        }

    }

    public class Obj_MFGMAT_ITMMASTER_PRODUZIONE
    {
        //PROPRIETA' MFGMAT
        public string MFGFCY_0 { get; set; }
        public string MFGNUM_0 { get; set; }
        public int MFGLIN_0 { get; set; }
        public string ITMREF_0 { get; set; }
        private string _LOT_0;
        public string LOT_0
        {
            get => _LOT_0;
            set => _LOT_0 = value?.Trim();
        }
        public string STU_0 { get; set; }
        public decimal RETQTY_0 { get; set; }
        public decimal USEQTY_0 { get; set; }
        public short BOMSEQ_0 { get; set; }
        public short BOMOPE_0 { get; set; }

        //PROPRIETA' ITMMASTER
        public string ITMDES1_0 { get; set; }

        public string TSICOD_0 { get; set; }

        public string PCU_0 { get; set; }
        public decimal PCUSTUCOE_0 { get; set; }

        public Obj_MFGMAT_ITMMASTER_PRODUZIONE()
        {
            //PROPRIETA' MFGMAT   
            MFGFCY_0 = "";
            MFGNUM_0 = "";
            MFGLIN_0 = 0;
            ITMREF_0 = "";
            LOT_0 = "";
            STU_0 = "";
            RETQTY_0 = 0;
            USEQTY_0 = 0;
            BOMSEQ_0 = 0;
            BOMOPE_0 = 0;

            //PROPRIETA' ITMMASTER
            ITMDES1_0 = "";
            TSICOD_0 = "";
            PCU_0 = "";
            PCUSTUCOE_0 = 0;
        }
        public decimal RESTO
        {
            get
            {
                return RETQTY_0 - USEQTY_0;
            }
        }
    }

    public class Obj_MFGITM_ITMMASTER_PRODUZIONE
    {
        //PROPRIETA' MFGITM
        public string MFGFCY_0 { get; set; }
        public string MFGNUM_0 { get; set; }
        public string LOT_0 { get; set; }
        public string ITMREF_0 { get; set; }
        public string UOM_0 { get; set; }
        public string STU_0 { get; set; }
        public decimal EXTQTY_0 { get; set; }
        public decimal RMNEXTQTY_0 { get; set; }

        //PROPRIETA' ITMMASTER
        public string ITMDES1_0 { get; set; }
        public string PCU_0 { get; set; }
        public decimal PCUSTUCOE_0 { get; set; }

        private decimal _lastre;
        public decimal LASTRE
        {
            get
            {
                return PCUSTUCOE_0 > 0 ? RMNEXTQTY_0 / PCUSTUCOE_0 : RMNEXTQTY_0;
            }
        }


        public Obj_MFGITM_ITMMASTER_PRODUZIONE()
        {
            //PROPRIETA' MFGITM
            MFGFCY_0 = "";
            MFGNUM_0 = "";
            LOT_0 = "";
            ITMREF_0 = "";
            UOM_0 = "";
            STU_0 = "";
            EXTQTY_0 = 0;
            RMNEXTQTY_0 = 0;

            //PROPRIETA' ITMMASTER
            ITMDES1_0 = "";
            PCU_0 = "";
            PCUSTUCOE_0 = 0;

        }

    }
    #endregion
}