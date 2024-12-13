<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ricevimento.aspx.cs" Inherits="X3_TERMINALINI.ricevimento.Ricevimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Ricevimento Magazzino</h4>
    <asp:HiddenField runat="server" ID="HF_Tipo" />
    <asp:HiddenField runat="server" ID="HF_Etic" />
    <asp:HiddenField runat="server" ID="HF_Qta" />
    <asp:HiddenField runat="server" ID="HF_Ubi_P" />
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <asp:Panel runat="server" ID="div_Ric">
        <div class="row margin-row">
            <div class="col-12"><b>Ricerca per Etichetta</b></div>
            <div class="col-8 col-md-6">
                <span class="font-small"><i>Etichetta</i></span><br />
                <asp:TextBox runat="server" ID="txt_Ricerca" CssClass="form-control chk-wait" AutoPostBack="false"></asp:TextBox>
            </div>
            <div class="col-4 col-md-2">
                <span class="font-small">&nbsp;</span><br />
                <asp:Button runat="server" ID="btn_RicercaBC" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Ricerca_Click" Text="Ricerca"></asp:Button>
            </div>
        </div>

        <div class="col-6 col-md-6">
            <asp:TextBox runat="server" ID="txtAutoCodiceFornitore" CssClass="form-control input-hidden" OnTextChanged="txtAutoCodiceFornitore_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>

        <br>

        <div class="row" style="margin-bottom: 20px">
            <div class="col-12"><b>Ricerca per Selezione</b></div>
            <div class="col-12 col-md-6">
                <span class="font-small"><i>Seleziona Fornitore</i></span><br />
                <asp:TextBox runat="server" ID="txtAutoFornitore" CssClass="form-control form-autocomplete" data-label="Nome" data-autocomplete-data="txtAutoCodiceFornitore"></asp:TextBox>
            </div>
            <div class="col-12 col-md-2">
                <span class="font-small">&nbsp;</span><br />
                <asp:Button runat="server" ID="btn_Conferma" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Conferma_Click" Text="Conferma"></asp:Button>
            </div>
        </div>
        <asp:Panel runat="server" ID="pan_data"></asp:Panel>
    </asp:Panel>


    <asp:Panel runat="server" ID="div_Det">
        <div class="row">
            <div class="col-12">
                <span class="font-small"><i>Articolo</i></span><br />
                <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control base-label" value="*"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-4">
                <span class="font-small"><i>Quantità</i></span><br />
                <asp:TextBox runat="server" ID="txt_Quantita" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Quantita_TextChanged"></asp:TextBox>
            </div>
            <div class="col-4">
                <span class="font-small"><i>UM</i></span><br />
                <asp:Label runat="server" ID="txt_UM" CssClass="form-control base-label" value="*"></asp:Label>
            </div>
            <div class="col-4">
                <span class="font-small"><i>Stato</i></span><br />
                <asp:Label runat="server" ID="lbl_Stato" CssClass="form-control base-label" value="*"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-md-6">
                <span class="font-small"><i>Ubicazione di Destinazione</i></span><br />
                <asp:TextBox runat="server" ID="txt_Destinazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Destinazione_TextChanged"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" OnClick="btn_Indietro_Click" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
