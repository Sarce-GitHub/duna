<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine_Righe_Disp.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Righe_Disp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
     <h4>Ricerca Magazzino</h4>
    <asp:Panel runat="server" ID="pan_data"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
     <asp:Button  runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" OnClick="btn_Indietro_Click" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
