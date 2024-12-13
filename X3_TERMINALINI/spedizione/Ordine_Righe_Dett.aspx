<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ordine_Righe_Dett.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Righe_Dett" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <asp:HiddenField ID="HF_Tipo_P" runat="server" />
    <h4>Preparazione</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Articolo/Lotto</i></span><br />
            <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control" BackColor="LightGray"></asp:Label>
        </div>

        <div class="col-12 col-md-6">
            <span class="font-small"><i>Ubicazione/Pallet di Partenza</i></span><br />
            <asp:TextBox runat="server" ID="txt_Partenza" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Partenza_TextChanged"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 col-md-3">
            <span class="font-small"><i>UM</i></span><br />
            <asp:Label runat="server" ID="lbl_UM" CssClass="form-control base-label" value="*"></asp:Label>
        </div>
        <div class="col-4 col-md-3">
            <span class="font-small"><i>Stato</i></span><br />
            <asp:Label runat="server" ID="lbl_Stato" CssClass="form-control base-label" value="*"></asp:Label>
        </div>
         <div class="col-4 col-md-3">
            <span class="font-small"><i>Quantità Rich.</i></span><br />
            <asp:Label runat="server" ID="lbl_richiesto" CssClass="form-control base-label" value="*"></asp:Label>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Quantità Disp.</i></span><br />
            <asp:Label runat="server" ID="lbl_disp" CssClass="form-control base-label" value="*"></asp:Label>
        </div>
        <div class="col-6 col-md-3">
            <span class="font-small"><i>Quantità Prel.</i></span><br />
            <asp:TextBox runat="server" ID="txt_Quantita" Enabled="false" CssClass="form-control" ></asp:TextBox>
        </div>


    </div>
    <div class="row">
        <div class="col-12 col-md-2 mb-3" style="margin-top: 30px">
            <asp:Button runat="server" ID="btt_Esegui" CssClass="form-control btn btn-primary modal-check" Text="Esegui" OnClick="btt_Esegui_Click"></asp:Button>
        </div>
    </div>

    <asp:Label runat="server" ID="lbl_Alert" CssClass="text-danger"></asp:Label>
    <div class="row">
        <div class="col-4 col-md-4" style="margin-top: 30px">
            <asp:Button runat="server" ID="btn_Conferma" CssClass="form-control btn btn-success modal-check" Text="Conferma" OnClick="btn_Conferma_Click"></asp:Button>
        </div>
        <div class="col-4 col-md-4" style="margin-top: 30px">
            <asp:Button runat="server" ID="btn_Annulla" CssClass="form-control btn btn-danger modal-check" Text="Annulla" OnClick="btn_Annulla_Click"></asp:Button>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button  runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" OnClick="btn_Indietro_Click" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
