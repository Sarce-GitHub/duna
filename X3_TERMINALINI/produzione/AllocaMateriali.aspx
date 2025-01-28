<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="AllocaMateriali.aspx.cs" Inherits="X3_TERMINALINI.produzione.AllocaMateriali" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Allocazione Materiali</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

    <div class="row" style="margin-bottom:20px">
        <div class="col-12"><b>Ricerca per Ordine di Produzione</b></div>
        <div class="col-8 col-md-6">
            <span class="font-small"><i>Lettura N°Ordine</i></span><br />
            <asp:TextBox runat="server" ID="txt_RicercaBC" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-4 col-md-2">
            <span class="font-small">&nbsp;</span><br />
            <asp:Button runat="server" ID="btn_RicercaBC" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_RicercaBC_Click" Text="Ricerca"></asp:Button>
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?PROD" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">

</asp:Content>
