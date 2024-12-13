<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="EntrataDiversa.aspx.cs" Inherits="X3_TERMINALINI.magazzino.EntrataDiversa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Entrata Diversa</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Etichetta</i></span><br />
            <asp:TextBox runat="server" ID="txt_Etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Etichetta_TextChanged"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Articolo</i></span><br />
            <asp:Label runat="server" ID="txt_Articolo" CssClass="form-control" Text="" ></asp:Label>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Lotto</i></span><br />
            <asp:Label runat="server" ID="txt_Lotto" CssClass="form-control"  Text=""></asp:Label>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>SottoLotto</i></span><br />
            <asp:Label runat="server" ID="txt_SottoLotto" CssClass="form-control"  Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
         <div class="col-6 col-md-6">
            <span class="font-small"><i>Ubicazione</i></span><br />
            <asp:TextBox runat="server" ID="txt_Ubicazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Ubicazione_TextChanged"></asp:TextBox>
        </div>
        <div class="col-6 col-md-6">
            <span class="font-small"><i>Pallet</i></span><br />
            <asp:TextBox runat="server" ID="txt_Pallet" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Pallet_TextChanged"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-6 col-md-3">
            <span class="font-small"><i>UM</i></span><br />
            <asp:Label runat="server" ID="txt_UM" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Quantità</i></span><br />
            <asp:TextBox runat="server" ID="txt_Qta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Qta_TextChanged"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-12 col-md-3">
            <br />
            <asp:Button runat="server" ID="btn_conferma" CssClass="form-control btn btn-primary" Text="Conferma" OnClick="btn_conferma_Click" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
        <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?STK" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
