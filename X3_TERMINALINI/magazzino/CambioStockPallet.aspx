<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="CambioStockPallet.aspx.cs" Inherits="X3_TERMINALINI.magazzino.CambioStockPallet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <div class="row">
        <div class="col-12">
             <h4>Cambio Ubicazione Pallet</h4>
            <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
            <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
        </div>
    </div>

     <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Etichetta Pallet</i></span><br />
            <asp:TextBox runat="server" ID="txt_etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_etichetta_TextChanged"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <span class="font-small"><i>Dati Pallet</i></span><br />
            <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control base-label font-small" value="*"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Ubicazione Destinazione</i></span><br />
            <asp:TextBox runat="server" ID="txt_destinazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_destinazione_TextChanged"></asp:TextBox>
        </div>
    </div>
    <div class="row" >
<%--        <div class="col-12 col-md-2" style="margin-top:30px">
            <asp:Button runat="server" ID="btt_Esegui" CssClass="form-control btn btn-primary modal-check" Text="Esegui" OnClick="btt_Esegui_Click" ></asp:Button>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
     <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?STK" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
