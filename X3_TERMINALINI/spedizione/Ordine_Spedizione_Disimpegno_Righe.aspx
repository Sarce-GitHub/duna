<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine_Spedizione_Disimpegno_Righe.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Spedizione_Disimpegno_Righe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
     <h4>Bancali Preparati</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row" style="margin-bottom:20px">
        <div class="col-8 col-md-5">
            <span class="font-small"><i>Pallet</i></span><br />
            <asp:TextBox runat="server" ID="txt_Pallet" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Pallet_TextChanged"></asp:TextBox>
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="Ordine_Spedizione_Disimpegno.aspx" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
    <script>
    </script>
</asp:Content>
