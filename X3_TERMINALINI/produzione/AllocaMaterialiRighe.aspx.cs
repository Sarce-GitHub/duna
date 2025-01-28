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
        Dictionary<string, decimal> _ETICHETTE = new Dictionary<string, decimal>();
        string _err = "";
        string _res = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!cls_Tools.Check_User()) return;
            _USR = cls_Tools.Get_User();
            if (_USR.ABIL4_0 != 2 || _USR.ABIL5_0 != 2) Response.Redirect("AllocaMateriali.aspx", true);
            if (Request.QueryString["NOrd"] == null) Response.Redirect("AllocaMateriali.aspx", true);
            if (!Page.IsPostBack)
            {
                //txt_RicercaBC.Focus();
            }
            string[] Arr = Request.QueryString["NOrd"].Trim().ToUpper().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            _MFGNUM = Arr[0];
            lbl_ordine.Text = _MFGNUM;
            Ricerca();
        }

        protected void txt_Etichetta_TextChanged(object sender, EventArgs e)
        {
            frm_error.Text = "";
            if (txt_Etichetta.Text.Trim() != "")
            {
                if (!_ETICHETTE.ContainsKey(txt_Etichetta.Text.Trim().ToUpper()))
                {
                    frm_error.Text = "Etichetta non trovata";
                }

                if (_ETICHETTE[txt_Etichetta.Text.Trim().ToUpper()] <= 0)
                {
                    frm_error.Text = "Materiale già del tutto allocato";
                }

                if (!string.IsNullOrEmpty(frm_error.Text))
                {
                    FormReset();
                    return;
                }

                txt_Qta.Text = _ETICHETTE[txt_Etichetta.Text.Trim().ToUpper()].ToString("0.######");
                txt_Qta.Enabled = true;
                txt_Qta.Text = _ETICHETTE[txt_Etichetta.Text.Trim().ToUpper()].ToString("0.######");
                txt_Qta.Focus();
                btn_Alloca.Enabled = true;
                return;

            }
            FormReset();
        }

        protected void btn_Alloca_Click(object sender, EventArgs e)
        {
            Obj_STOCK_ETIC _e = new Obj_STOCK_ETIC(txt_Etichetta.Text.Trim().ToUpper());
            var wsRes = cls_TermWS.WS_AllocaMateriali(_USR.FCY_0, _MFGNUM, 0.ToString(), _e.ITMREF, _e.LOT, decimal.Parse(txt_Qta.Text), "", out _err, out _res); //TODO: ADD CHECK
            frm_error.Text = _err;
            frm_OK.Text = _res;
            FormReset();
        }

        protected void Ricerca()
        {
            var list = _SQL.Obj_MFGMAT_Load_Order_Material_List(_USR.FCY_0, _MFGNUM, out _err);
            var substring = list
                .GroupBy(x => $"{x.ITMREF_0}{(string.IsNullOrEmpty(x.LOT_0) ? "" : $"{Properties.Settings.Default.Etic_Split}{x.LOT_0}")}")
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        LOC_0 = g.Select(x => x.LOC_0).Distinct().ToArray(),
                        MATERIALEDAALLOCARE_0 = g.First().MATERIALEDAALLOCARE
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
            _h = _h + "<div class=\"col-8 col-md-8\">Etichetta</div>";
            //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Ord.</div>";
            //_h = _h + "<div class=\"col-4 col-md-2 text-end\">Qta Prep.</div>";
            _h = _h + "<div class=\"col-4 col-md-4 text-end\">Qta. Allocabile</div>";

            _h = _h + "</div>";
            _d.InnerHtml = _d.InnerHtml + _h;


            foreach (var _i in substring)
            {
                _ETICHETTE[_i.Key] = _i.Value.MATERIALEDAALLOCARE_0;
                string bgColor = "";

                _c = ((idx % 2) == 1 ? "bg-alt" : "");
                var j = _i;

                //_c = "bg-light-blue";
                if (_i.Value.MATERIALEDAALLOCARE_0 == 0) _c = "bg-ok";
                if (_i.Value.MATERIALEDAALLOCARE_0 < 0) _c = "bg-light-grey";



                _h = "<div class=\"row " + _c + " \" data-itm=\"" + _i.Key + "\">";
                _h += "<div class=\"col-8 col-md-8 check-pos\"><b>" + _i.Key + "</b></div>";
                //_h += "<div class=\"col-12 col-md-4 font-small check-pos\">" + _i.ITMDES_0 + " (" + _i.NrRighe.ToString("0") + ")" + " <span class=\"fw-bold\">pal: " + _i.PALNUM_0 + "</span></div>";
                //_h += "<div class=\"col-4 col-md-2 text-end check-pos\">" + (_i.YQTYPCU_0).ToString("0.##") + " " + _i.YPCU_0 + "&nbsp;&nbsp;</div>";
                //
                //string qtyPrep = (_i.QTYPREP_0 / _i.YPCUSTUCOE_0).ToString("0.##");
                //    _h += "<div class=\"col-4 col-md-2 text-end check-pos\"><b>" + qtyPrep + " " + _i.YPCU_0 + "</b>&nbsp;&nbsp;</div>";

                _h += "<div class=\"col-4 col-md-4 text-end check-pos\"><i>" + _i.Value.MATERIALEDAALLOCARE_0.ToString("0.######") + "</i>&nbsp;&nbsp;</div>";

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


                _h += "<div class=\"col-12 \"><hr/></div>";
                _h += "</div>";
                //
                _d.InnerHtml = _d.InnerHtml + _h;
            }

            pan_dati.Controls.Add(_d);
            txt_Etichetta.Focus();
        }

        public void FormReset()
        {
            txt_Etichetta.Text = "";
            txt_Qta.Text = "";
            txt_Qta.Enabled = false;
            btn_Alloca.Enabled = false;
            txt_Etichetta.Focus();
        }
    }
}
