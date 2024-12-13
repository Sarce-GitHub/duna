<%@ Page Title="Carico Presped" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="GestionePresped.aspx.cs" Inherits="X3_TERMINALINI.spedizione.GestionePresped" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">


    <div class="row">
        <div class="col-12">
            <h4>
            <asp:Label runat="server" ID="lbl_title"></asp:Label></h4>
            <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
            <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

            <asp:HiddenField runat="server" ID="HF_Tipo_P" />
            <asp:HiddenField runat="server" ID="HF_Tipo_D" />
            <asp:HiddenField runat="server" ID="HF_QTY_A" />
            <asp:HiddenField runat="server" ID="HF_QTY_Q" />
            <asp:HiddenField runat="server" ID="HF_QTY_R" />
            <asp:HiddenField runat="server" ID="HF_ITMREF" />
            <asp:HiddenField runat="server" ID="hf_LOC" />
        </div>
    </div>

    <div class="row mb-4">
        <div class="col-12">
            <span class="font-small" runat="server" visible="true"><i>Tipo Operazione</i></span><br />
            <asp:DropDownList runat="server" ID="txt_Tipo_Operazione" CssClass="form-control" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="GestisciOperazioneNavetta">
                <asp:ListItem Text="Scatole" Value="AssemblaPallet"></asp:ListItem>
                <asp:ListItem Text="Bancali" Value="CambioUbicazionePallet"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <div runat="server" id="CaricaPallet" visible="false">
        <div class="row">
            <div class="row">
                <div class="col-12 col-md-6" runat="server" id="div_navetta_da_scaricare" visible="false">
                    <span class="font-small"><i>Navetta da scaricare</i></span><br />
                    <asp:TextBox runat="server" ID="txt_navetta_da_scaricare" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_navetta_da_scaricare_TextChanged"></asp:TextBox>
                </div>

                <div class="col-12 col-md-6">
                    <span class="font-small"><i>
                        <asp:Label runat="server" ID="lbl_Destinazione"></asp:Label></i></span><br />
                    <asp:TextBox runat="server" ID="txt_Destinazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Destinazione_TextChanged"></asp:TextBox>
                </div>
                <div class="col-12 col-md-6">
                    <span class="font-small"><i>
                        <asp:Label runat="server" ID="lbl_Partenza"></asp:Label></i></span><br />
                    <asp:TextBox runat="server" ID="txt_Partenza" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Partenza_TextChanged"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-6 col-md-2 pe-4" style="margin-top: 10px">
                <asp:Button runat="server" ID="btt_EseguieContinua" CssClass="form-control btn btn-success modal-check" Text="Esegui e Continua" UseSubmitBehavior="false" OnClick="btt_EseguieContinua_Click"></asp:Button>
            </div>
            <div class="col-6 col-md-2 ps-0 " style="margin-top: 10px">
                <asp:Button runat="server" ID="btn_Annulla" CssClass="form-control btn btn-danger modal-check" Text="Termina" OnClick="btn_Annulla_Click"></asp:Button>
            </div>
        </div>


        <div class="row">
            <div class="col-12">
                <span class="font-small"><i>Dati Pallet</i></span><br />
                <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control base-label font-small" value="*"></asp:Label>
            </div>

        </div>
    </div>

    <div runat="server" id="CaricaScatole" visible="false">
        <div runat="server" id="div_data">
            <asp:Panel runat="server" CssClass="d-flex" id="div_scarico_sc_col">
                <div class="row">
                    <div class="col-6 col-md-4">
                        <span class="font-small"><i>
                            <asp:Label runat="server" ID="lbl_Destinazione_SC"></asp:Label></i></span><br />
                        <asp:TextBox runat="server" ID="txt_Destinazione_SC" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Destinazione_SC_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-6 col-md-2 ps-0 "  runat="server" id="div_btn_sc_carico">
                        <span class="font-small"><i></i></span>
                        <br />
                        <asp:Button runat="server" ID="btn_Reset_SC_Carico" CssClass="form-control btn btn-warning modal-check" Text="Reset" OnClick="btn_Reset_SC_Click"></asp:Button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-6 col-md-4">
                        <span class="font-small"><i>
                            <asp:Label runat="server" ID="lbl_Partenza_SC"></asp:Label></i></span><br />
                        <asp:TextBox runat="server" ID="txt_Partenza_SC" CssClass="form-control chk-wait" Enabled="true" AutoPostBack="true" OnTextChanged="txt_Partenza_SC_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-6 col-md-2 ps-0" runat="server" id="div_btn_sc_scarico">
                        <span class="font-small"><i></i></span>
                        <br />
                        <asp:Button runat="server" ID="btn_Reset_SC_Scarico" CssClass="form-control btn btn-warning modal-check" Text="Reset" OnClick="btn_Reset_SC_Click"></asp:Button>
                    </div>
                </div>
            </asp:Panel>



            <div class="row">
                <div class="col-12 col-md-4">
                    <span class="font-small"><i>Etichetta Articolo</i></span><br />
                    <asp:TextBox runat="server" ID="txt_Etichetta_SC" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Etichetta_SC_TextChanged"></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-4">
                    <span class="font-small"><i>Quantità A</i></span><br />
                    <asp:TextBox runat="server" ID="txt_Quantita" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_TextChanged"></asp:TextBox>
                </div>
                <div class="col-4">
                    <span class="font-small"><i>Qta Disp.</i></span><br />
                    <asp:TextBox runat="server" ID="txt_Quantita_Disp" CssClass="form-control chk-wait bg-light-green text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
                </div>

                <div class="col-2">
                    <span class="font-small"><i>UM</i></span><br />
                    <asp:Label runat="server" ID="lbl_UM" CssClass="form-control base-label" value="*"></asp:Label>
                </div>
                <div class="col-2">
                    <span class="font-small"><i>Stato</i></span><br />
                    <asp:Label runat="server" ID="lbl_Stato" CssClass="form-control base-label bg-light-green" value="*"></asp:Label>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-4">
                    <asp:TextBox runat="server" ID="txt_Quantita_Q" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_Q_TextChanged"></asp:TextBox>
                </div>
                <div class="col-4">
                    <asp:TextBox runat="server" ID="txt_Quantita_Disp_Q" CssClass="form-control chk-wait bg-light-yellow text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-2">
                    <asp:Label runat="server" ID="lbl_UM_Q" CssClass="form-control base-label" value="*"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:Label runat="server" ID="lbl_Stato_Q" CssClass="form-control base-label bg-light-yellow" value="*"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <asp:TextBox runat="server" ID="txt_Quantita_R" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_R_TextChanged"></asp:TextBox>
                </div>
                <div class="col-4">
                    <asp:TextBox runat="server" ID="txt_Quantita_Disp_R" CssClass="form-control chk-wait bg-light-red text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-2">
                    <asp:Label runat="server" ID="lbl_UM_R" CssClass="form-control base-label" value="*"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:Label runat="server" ID="lbl_Stato_R" CssClass="form-control base-label bg-light-red" value="*"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-6 col-md-2 pe-4" style="margin-top: 10px">
                    <asp:Button runat="server" ID="btt_EseguieContinua_SC" CssClass="form-control btn btn-success modal-check" Text="Esegui e Continua" UseSubmitBehavior="false" OnClick="btt_EseguieContinua_SC_Click"></asp:Button>
                </div>
                <div class="col-6 col-md-2 ps-0 " style="margin-top: 10px">
                    <asp:Button runat="server" ID="btn_Annulla_SC" CssClass="form-control btn btn-danger modal-check" Text="Termina" OnClick="btn_Annulla_SC_Click"></asp:Button>
                </div>
            </div>


            <div class="row">
                <div class="col-12">
                    <span class="font-small"><i>Dati Prespedizione</i></span><br />
                    <asp:Label runat="server" ID="lbl_Dati_Prespedizione" CssClass="form-control base-label font-small" value="*"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <%--        <h1>asdasdasdas</h1>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?SPED" UseSubmitBehavior="false" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
