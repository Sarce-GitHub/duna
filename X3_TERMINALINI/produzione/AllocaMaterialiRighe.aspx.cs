using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using X3_TERMINALINI.magazzino;

namespace X3_TERMINALINI.produzione
{
    public partial class AllocaMaterialiRighe : System.Web.UI.Page
    {
        cls_SQL _SQL = new cls_SQL();
        Obj_YTSUTX _USR = new Obj_YTSUTX();
        string _MFGNUM = "";
        Dictionary<string, List<Etichetta>> _ETICHETTE = new Dictionary<string, List<Etichetta>>();
        Dictionary<string, List<string>> _UBICAZIONI = new Dictionary<string, List<string>>();
        char _SEPARATORE;
        string _err = "";
        string _res = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL6_0 != 2) Response.Redirect("AllocaMateriali.aspx", true);
            if (Request.QueryString["NOrd"] == null) Response.Redirect("AllocaMateriali.aspx", true);
            if (!Page.IsPostBack)
            {
                //txt_RicercaBC.Focus();
            }
            string[] Arr = Request.QueryString["NOrd"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            _MFGNUM = Arr[0];
            lbl_ordine.Text = _MFGNUM;
            _SEPARATORE = char.Parse(Properties.Settings.Default.Etic_Split);
            Ricerca();
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            string etichetta = txt_Etichetta.Text.Trim().ToUpper();
            string ubicazione = txt_Ubicazione.Text.Trim().ToUpper();
            string[] etichettaSplit = etichetta.Split(_SEPARATORE);

            if(etichettaSplit.Length != 2)
            {
                frm_error.Text = "Etichetta non valida";
                txt_Etichetta.Text = "";
                return;
            }

            string articolo = etichetta.Split(_SEPARATORE)[0];
            string lotto = etichetta.Split(_SEPARATORE)[1];
            frm_error.Text = "";

            if (!string.IsNullOrEmpty(etichetta))
            {
                if (!_UBICAZIONI[ubicazione].Contains(etichetta))
                {

                    frm_error.Text = "Etichetta non trovata in ubicazione " + ubicazione;
                }

                if (!string.IsNullOrEmpty(frm_error.Text))
                {
                    FormReset();
                    return;
                }

                txt_Qta.Enabled = true;
                txt_Qta.Focus();
                btn_Alloca.Enabled = true;
                return;

            }
            frm_error.Text = "Inserire etichetta";
            FormReset();
        }

        protected void txt_Ubicazione_TextChanged(object sender, EventArgs e) // TODO:aggiungere check
        {
            CheckUbic(txt_Ubicazione.Text.Trim().ToUpper());
        }

        protected void btn_Alloca_Click(object sender, EventArgs e)
        {
            Obj_STOCK_ETIC _e = new Obj_STOCK_ETIC(txt_Etichetta.Text.Trim().ToUpper());
            var wsRes = cls_TermWS.WS_AllocaMateriali(_USR.FCY_0, _MFGNUM, 0.ToString(), _e.ITMREF, _e.LOT, decimal.Parse(txt_Qta.Text), txt_Ubicazione.Text.Trim().ToUpper(), out _err, out _res); //TODO: ADD CHECK
            frm_error.Text = _err;
            frm_OK.Text = _res;
            FormReset();
        }

        protected void Ricerca()
        {
            var list = _SQL.Obj_MFGMAT_Load_Order_Material_List(_USR.FCY_0, _MFGNUM, out _err);
            var substring = list
                .GroupBy(x => x.ITMREF_0)
                .ToDictionary(
                    g => g.Key,
                    g => new Etichetta
                    {
                        //LOC_0 = g.Select(x => x.LOC_0).Distinct().ToArray(),
                        MATERIALEDAALLOCARE = g.First().MATERIALEDAALLOCARE,
                        STOCK = g.Select(x => new Stock { LOC_0 = x.LOC_0, LOT_0 = x.LOT_0, ALLOCABILEPERLOTTO = x.ALLOCABILEPERLOTTO })
                                .Distinct()
                                .Where(x => x.ALLOCABILEPERLOTTO > 0)
                                .ToArray(),
                    }
                )
                .OrderByDescending(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Value);

            pan_dati.Controls.Clear();
            HtmlGenericControl _d = new HtmlGenericControl();

            string _h = "";
            int idx = 0;
            string _c = "";

            _h = "<div class=\"row bg-head font-small\">";
            _h = _h + "<div class=\"col-8 col-md-8 fw-bold\">Articolo   </div>";
            //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Ord.</div>";
            //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Prep.</div>";
            _h = _h + "<div class=\"col-4 col-md-4 text-end\">Qta. Allocabile</div>";

            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;


            foreach (var _i in substring)
            {
                if (!_ETICHETTE.ContainsKey(_i.Key))
                {
                    _ETICHETTE[_i.Key] = new List<Etichetta>();
                }

                _ETICHETTE[_i.Key].Add(new Etichetta
                {
                    MATERIALEDAALLOCARE = _i.Value.MATERIALEDAALLOCARE,
                    STOCK = _i.Value.STOCK.Select(s => new Stock
                    {
                        LOC_0 = s.LOC_0,
                        LOT_0 = s.LOT_0,
                        ALLOCABILEPERLOTTO = s.ALLOCABILEPERLOTTO
                    })
                });
                string bgColor = "";

                _c = ((idx % 2) == 1 ? "bg-alt" : "");
                var j = _i;

                //_c = "bg-light-blue";
                if (_i.Value.MATERIALEDAALLOCARE == 0) _c = "bg-ok";
                if (_i.Value.MATERIALEDAALLOCARE < 0) _c = "bg-light-grey";

                _h = "<div class=\"row " + _c + " \" data-itm=\"" + _i.Key + "\">";
                _h += "<div class=\"col-8 col-md-8 check-pos\"><b>" + _i.Key + "</b></div>";
                //_h += "<div class=\"col-12 col-md-4 font-small check-pos\">" + _i.ITMDES_0 + " (" + _i.NrRighe.ToString("0") + ")" + " <span class=\"fw-bold\">pal: " + _i.PALNUM_0 + "</span></div>";
                //_h += "<div class=\"col-4 col-md-2 text-end check-pos\">" + (_i.YQTYPCU_0).ToString("0.##") + " " + _i.YPCU_0 + "&nbsp;&nbsp;</div>";
                //
                //string qtyPrep = (_i.QTYPREP_0 / _i.YPCUSTUCOE_0).ToString("0.##");
                //    _h += "<div class=\"col-4 col-md-2 text-end check-pos\"><b>" + qtyPrep + " " + _i.YPCU_0 + "</b>&nbsp;&nbsp;</div>";

                _h += "<div class=\"col-4 col-md-4 text-end check-pos\"><i>" + _i.Value.MATERIALEDAALLOCARE.ToString("0.######") + "</i>&nbsp;&nbsp;</div>";

                //foreach (var ordine in listaOrdiniConPallet)
                //{
                //    //string palletString = "";
                //    //foreach (var o in ordine.Value.PalletQuantities)
                //    //{
                //    //    palletString += $"{o.Key}: {o.Value.palQty}{o.Value.palUm}; ";
                //    //}

                //    bgColor = "bg-light-grey";
                //    bgColor = "bg-light-grey";
                //    if (_c == "bg-ok") bgColor = "bg-light-green";
                //    if (_c == "bg-att") bgColor = "bg-light-yellow";
                //    _h += "<div class=\"col-12 font-small" + " " + bgColor + " " + "\"><span>Ord." + ordine.Key + " " + ordine.Value.Shidat.ToString("dd/MM/yyyy") + ": " + ordine.Value.QtyTot.ToString("0.##") + ordine.Value.UM + "</span><span class=\"ms-5\">" + "</span></div> ";
                //}
                var bgColorSubheader =  _c == "bg-ok" ? "bg-light-green" : "bg-dark-subtle" ;
                var suRowHeader = "<div class=\"col-12 font-small fw-bold row" + " " + bgColorSubheader + " " + "\"><div class=\"col-4 col-md-3\">Ubicazione</div><div class=\"col-4 col-md-3\">Lotto</div><div class=\"col-4 col-md-3 text-end\">Qta</div></div> ";
                if(_i.Value.STOCK.Count() > 0)
                {
                    _h += suRowHeader;
                }

                foreach (var st in _i.Value.STOCK)
                {
                    string etichetta = _i.Key + Properties.Settings.Default.Etic_Split + st.LOT_0;
                    if (_UBICAZIONI.ContainsKey(st.LOC_0))
                    {
                        _UBICAZIONI[st.LOC_0].Add(etichetta);
                    }
                    else
                    {
                        _UBICAZIONI[st.LOC_0] = new List<string> { etichetta };
                    }
                    _h += "<div class=\"col-12 font-small row" + " " + bgColor + " " + "\"><div class=\"col-4 col-md-3\">" + st.LOC_0 + "</div><div class=\"col-4 col-md-3\">" + etichetta.Split('|')[1] + "</div><div class=\"col-4 col-md-3 text-end\">" + st.ALLOCABILEPERLOTTO.ToString("0.######") + "</div></div> ";

                }


                _h += "<div class=\"col-12 \"><hr/></div>";
                _h += "</div>";
                //ALLOCABILEPERLOTTO
                _d.InnerHtml = _d.InnerHtml + _h;
            }

            pan_dati.Controls.Add(_d);
            txt_Ubicazione.Focus();
        }

        public void FormReset()
        {
            txt_Etichetta.Text = "";
            txt_Etichetta.Enabled = false;
            txt_Qta.Text = "";
            txt_Qta.Enabled = false;
            btn_Alloca.Enabled = false;
            txt_Ubicazione.Text = "";
            txt_Ubicazione.Focus();
        }

        protected void CheckUbic(string ubic)
        {
            if (!_UBICAZIONI.ContainsKey(ubic))
            {
                frm_error.Text = "Ubicazione non disponibile";
                FormReset();
                return;
            }
            txt_Etichetta.Enabled = true;
            txt_Etichetta.Focus();
        }

        //protected bool isEtichettaValid(string etichetta, string articolo, string lotto, string ubicazione)
        //{
        //    if (_ETICHETTE.ContainsKey(articolo))
        //    {
        //        return _ETICHETTE[articolo].Any(e => e.STOCK.Any(s => s.LOT_0 == lotto && s.LOC_0 == ubicazione));
        //    }
        //    return false;

        //}

        public class Etichetta
        {
            public decimal MATERIALEDAALLOCARE { get; set; }
            public IEnumerable<Stock> STOCK { get; set; }
        }

        public class Stock
        {
            public string LOC_0 { get; set; }
            public string LOT_0 { get; set; }
            public decimal ALLOCABILEPERLOTTO { get; set; }
        }
    }
}
