<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ordine_Spedizione_Pallet.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Spedizione_Pallet" MasterPageFile="~/_include/Menu.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
     <h4>Preparazione Pallet</h4>
     <div class="col-12 col-md-2">
         <span class="font-small">&nbsp;</span><br />
         <asp:Button runat="server" ID="btn_Conferma" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Conferma_Click" Text="Conferma Pallet"></asp:Button>
     </div>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>

    <div class="row" style="margin-bottom:20px">
        <div class="col-12 col-md-6">
        <asp:Label runat="server" ID="lbl_pallet" ForeColor="DarkGreen"></asp:Label>
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" OnClick="btn_Indietro_Click" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>


