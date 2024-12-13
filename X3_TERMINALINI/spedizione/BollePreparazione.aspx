<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="BollePreparazione.aspx.cs" Inherits="X3_TERMINALINI.spedizione.BollePreparazione" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Bolla di Preparazione</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row" style="margin-bottom:20px">
        <div class="col-8 col-md-6">
            <span class="font-small"><i>Ricerca</i></span><br />
            <asp:TextBox runat="server" ID="txt_Ricerca" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-4 col-md-2">
            <span class="font-small">&nbsp;</span><br />
            <asp:Button runat="server" ID="btn_Ricerca" CssClass="form-control btn btn-secondary modal-check" OnClick="btn_Ricerca_Click" Text="Ricerca"></asp:Button>
        </div>
    </div>
    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?SPED" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
        <script>
        $(".check-bolla").click(function () {
            document.location.href ="BollePreparazione_Righe.aspx?PRHNUM=" + $(this).attr("data-prh");
        });

        </script>
</asp:Content>
