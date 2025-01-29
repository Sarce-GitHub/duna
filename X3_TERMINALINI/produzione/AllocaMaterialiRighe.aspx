<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="AllocaMaterialiRighe.aspx.cs" Inherits="X3_TERMINALINI.produzione.AllocaMaterialiRighe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Allocazione Materiali Ordine <asp:Label runat="server" CssClass="fw-bold" ID="lbl_ordine"></asp:Label></h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

    <div class="row" style="margin-bottom:20px">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Etichetta</i></span><br />
            <asp:TextBox runat="server" ID="txt_Etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Etichetta_TextChanged"></asp:TextBox>
        </div>
        <div class="col-8 col-md-4">
            <span class="font-small"><i>Quantità</i></span><br />
            <asp:TextBox runat="server" ID="txt_Qta" CssClass="form-control chk-wait" AutoPostBack="true" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-4 col-md-2">
            <span class="font-small"><i>&nbsp;</i></span><br />
            <asp:Button runat="server" ID="btn_Alloca" CssClass="form-control chk-wait" BackColor="Orange" ForeColor="White" Text="Alloca" Enabled="false" OnClick="btn_Alloca_Click" />
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/produzione/AllocaMateriali.aspx" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server"></asp:Content>
