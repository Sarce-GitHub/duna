<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine_Spedizione_Righe.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Spedizione_Righe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Spedizione Ordine</h4>
        <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row">
        <div class="col-12 col-md-4">
            <i>Cliente</i><br />
            <asp:Label runat="server" ID="lbl_ClienteCod" CssClass="form-control" BackColor="LightGray"></asp:Label>
        </div>
        <div class="col-12 col-md-6">
            <i>Indirizzo</i><br />
            <asp:Label runat="server" ID="lbl_ClienteAdd" CssClass="form-control" BackColor="LightGray"></asp:Label>
        </div>
        <div class="col-12 col-md-2">
            <i>Data Spedizione</i><br />
            <asp:Label runat="server" ID="lbl_ClienteData" CssClass="form-control" BackColor="LightGray"></asp:Label>
        </div>
    </div>
    <br />
    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>

    <div class="row">
        <div class="col-6 col-md-3 margin-row-top">

           <asp:Button runat="server" ID="btn_Tutto" CssClass="form-control btn btn-primary modal-check" Text="Conferma Tutto" PostBackUrl="" UseSubmitBehavior="false" />
        </div>
        <div class="col-6 col-md-3 margin-row-top">
            <asp:Button runat="server" ID="btn_Parziale" CssClass="form-control btn btn-danger modal-check" Text="Rimuovi Pallet"  PostBackUrl="" UseSubmitBehavior="false" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button  runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/spedizione/Ordine_Spedizione.aspx" UseSubmitBehavior="false" />
</asp:Content>
