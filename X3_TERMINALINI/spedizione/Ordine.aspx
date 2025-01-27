<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Preparazione Ordine</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

    <div runat="server" class="row" ID="cookie_ordine_container" visible="false">
        <div class="col-12 col-md-8 d-flex justify-content-between">
           <%-- <span class="text-success">Ordine in preparazione: </span>--%>
            <asp:Label runat="server" ID="cookie_ordine" CssClass="fw-bold align-self-center" ForeColor="DarkGreen" style="flex:5"></asp:Label>
            <asp:Button runat="server" ID="reset_Cookie" CssClass="form-control btn btn-warning modal-check ms-3" style="flex:1" OnClick="reset_Cookie_Click" Text="Reset"></asp:Button>
        </div>

    </div>
    <div class="row" style="margin-bottom:20px">
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
    
    <div runat="server" ID="div_ricerca_generica">
        <div class="col-6 col-md-6">
            <asp:TextBox runat="server" ID="txtAutoCodiceCliente" CssClass="form-control input-hidden" OnTextChanged="txtAutoCodiceCliente_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>   
        <div class="row" style="margin-bottom:20px">
        <div class="col-12"><b>Ricerca per Selezione</b></div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Seleziona Cliente</i></span><br />
            <asp:TextBox runat="server" ID="txtAutoCliente" CssClass="form-control form-autocomplete" data-label="Nome" data-autocomplete-data="txtAutoCodiceCliente"></asp:TextBox>
        </div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Seleziona Indirizzo</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_BPAADD" CssClass="form-control chk-wait" AutoPostBack="true" OnSelectedIndexChanged="ddl_BPAADD_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-6 col-md-2">
            <span class="font-small"><i>Seleziona Data Da</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_SHIDAT_DA" CssClass="form-control" AutoPostBack="true" onSelectedIndexChanged="ddl_SHIDAT_DA_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-6 col-md-2">
            <span class="font-small"><i>Seleziona Data A</i></span><br />
            <asp:DropDownList runat="server" ID="ddl_SHIDAT_A" CssClass="form-control "></asp:DropDownList>
        </div>
        <div class="col-12 col-md-2">
            <span class="font-small">&nbsp;</span><br />
            <asp:Button runat="server" ID="btn_Conferma" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Conferma_Click" Text="Conferma"></asp:Button>
        </div>
    </div>

</div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?SPED" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">

</asp:Content>
