<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="CambioStock.aspx.cs" Inherits="X3_TERMINALINI.magazzino.CambioStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Cambio Ubicazione Merce</h4>
    <asp:HiddenField runat="server" ID="HF_Tipo_P" />
    <asp:HiddenField runat="server" ID="HF_Tipo_D" />
    <asp:HiddenField runat="server" ID="HF_QTY_A" />
    <asp:HiddenField runat="server" ID="HF_QTY_Q" />
    <asp:HiddenField runat="server" ID="HF_QTY_R" />

    <asp:HiddenField runat="server" ID="HF_ITMREF" />
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Ubicazione/Pallet di Partenza</i></span><br />
            <asp:TextBox runat="server" ID="txt_Partenza" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Partenza_TextChanged"></asp:TextBox>
        </div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Etichetta</i></span><br />
            <asp:TextBox runat="server" ID="txt_Etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Etichetta_TextChanged" autocomplete="off"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <span class="font-small"><i>Articolo</i></span><br />
            <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control base-label" value="*"></asp:Label>
        </div>
    </div>
    <div class="row mb-3">
        <div class="col-4">
            <span class="font-small"><i>Quantità</i></span><br />
            <asp:TextBox runat="server" ID="txt_Quantita" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_TextChanged"></asp:TextBox>
        </div>
        <div class="col-4">
            <span class="font-small"><i>Qta Disp.</i></span><br />
            <asp:TextBox runat="server" ID="txt_Quantita_Disp" CssClass="form-control chk-wait bg-light-green text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
        </div>

        <div class="col-2">
            <span class="font-small"><i>UM</i></span><br />
            <asp:Label runat="server" ID="lbl_UM" CssClass="form-control base-label" value="*"></asp:Label>  
        </div>
        <div class="col-2">
            <span class="font-small"><i>Stato</i></span><br />
            <asp:Label runat="server" ID="lbl_Stato" CssClass="form-control base-label bg-light-green" value="*"></asp:Label>  
        </div>
    </div>
    <div class="row mb-3">
        <div class="col-4">
<%--            <span class="font-small"><i>Quantità Q</i></span><br />--%>
            <asp:TextBox runat="server" ID="txt_Quantita_Q" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_Q_TextChanged"></asp:TextBox>
        </div>
        <div class="col-4">
<%--            <span class="font-small"><i>Qta Disp.</i></span><br />--%>
            <asp:TextBox runat="server" ID="txt_Quantita_Disp_Q" CssClass="form-control chk-wait bg-light-yellow text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-2">
<%--            <span class="font-small"><i>UM Q</i></span><br />--%>
            <asp:Label runat="server" ID="lbl_UM_Q" CssClass="form-control base-label" value="*"></asp:Label>  
        </div>
        <div class="col-2">
<%--            <span class="font-small"><i>Stato Q</i></span><br />--%>
            <asp:Label runat="server" ID="lbl_Stato_Q" CssClass="form-control base-label bg-light-yellow" value="*"></asp:Label>  
        </div>
    </div>

    <div class="row">
        <div class="col-4">
<%--            <span class="font-small"><i>Quantità R</i></span><br />--%>
            <asp:TextBox runat="server" ID="txt_Quantita_R" CssClass="form-control chk-wait text-end" AutoPostBack="true" OnTextChanged="txt_Quantita_R_TextChanged"></asp:TextBox>
        </div>
        <div class="col-4">
<%--            <span class="font-small"><i>Qta Disp.</i></span><br />--%>
            <asp:TextBox runat="server" ID="txt_Quantita_Disp_R" CssClass="form-control chk-wait bg-light-red text-end" AutoPostBack="true" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-2">
<%--            <span class="font-small"><i>UM R</i></span><br />--%>
            <asp:Label runat="server" ID="lbl_UM_R" CssClass="form-control base-label" value="*"></asp:Label>  
        </div>
        <div class="col-2">
<%--            <span class="font-small"><i>Stato R</i></span><br />--%>
            <asp:Label runat="server" ID="lbl_Stato_R" CssClass="form-control base-label bg-light-red" value="*"></asp:Label>  
        </div>
    </div>


    <div class="row">
         <div class="col-12 col-md-6">
               <span class="font-small"><i>Ubicazione/Pallet di Destinazione</i></span><br />
            <asp:TextBox runat="server" ID="txt_Destinazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Destinazione_TextChanged"></asp:TextBox>
         </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?STK" UseSubmitBehavior="false" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>