<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine_Spedizione.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Spedizione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Spedizione Ordine</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row" style="margin-bottom: 20px">
        <div class="col-12"><b>Ricerca per Barcode</b></div>
        <div class="col-8 col-md-6">
            <span class="font-small"><i>Lettura BarCode</i></span><br />
            <asp:TextBox runat="server" ID="txt_RicercaBC" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-4 col-md-2">
            <span class="font-small">&nbsp;</span><br />
            <asp:Button runat="server" ID="btn_RicercaBC" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_RicercaBC_Click" Text="Ricerca"></asp:Button>
        </div>
    </div>
    <div class="col-6 col-md-6">
        <asp:TextBox runat="server" ID="txtAutoCodiceClienteSped" CssClass="form-control input-hidden" OnTextChanged="txtAutoCodiceClienteSped_TextChanged" AutoPostBack="true"></asp:TextBox>
    </div>
    <div runat="server" class="row" style="margin-bottom: 20px" ID="div_ricerca_generica">
        <div class="col-12"><b>Ricerca per Selezione</b></div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Seleziona Cliente</i></span><br />
            <asp:TextBox runat="server" ID="txtAutoClienteSped" CssClass="form-control form-autocomplete-sped" data-label="Nome" data-autocomplete-data="txtAutoCodiceClienteSped"></asp:TextBox>
        </div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Seleziona Indirizzo</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_BPAADD" CssClass="form-control chk-wait" AutoPostBack="true" OnSelectedIndexChanged="ddl_BPAADD_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Seleziona Data DA</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_DATA_DA" CssClass="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddl_DATA_DA_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Seleziona Data A</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_DATA_A" CssClass="form-control "></asp:DropDownList>
        </div>


        <div class="col-12 col-md-2">
            <span class="font-small">&nbsp;</span><br />
            <asp:Button runat="server" ID="btn_Conferma" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Conferma_Click" Text="Conferma"></asp:Button>
        </div>

    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?SPED" UseSubmitBehavior="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
