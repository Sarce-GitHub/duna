<%@ Page Title="Consumo Materiali" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Consumo_Materiali.aspx.cs" Inherits="X3_TERMINALINI.produzione.Consumo_Materiali" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Consumo Materiali</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

    <asp:HiddenField ID="hf_FCY" runat="server" />
    <asp:HiddenField ID="hf_MFGNUM" runat="server" />
    <asp:HiddenField ID="hf_MFGLIN" runat="server" />
    <asp:HiddenField ID="hf_LOT" runat="server" />
    <asp:HiddenField ID="hf_ITMREF" runat="server" />
    <asp:HiddenField ID="hf_STU" runat="server" />
    <asp:HiddenField ID="hf_COEFF" runat="server" />
    <asp:HiddenField ID="hf_CURRENTQTY" runat="server" />
    <asp:HiddenField ID="hf_BOMSEQ" runat="server" />
    <asp:HiddenField ID="hf_OPE" runat="server" />
    <asp:HiddenField ID="hf_TSICOD" runat="server" />
    <asp:HiddenField ID="hf_LOC" runat="server"/>


    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Ordine di Produzione</i></span><br />
            <asp:TextBox runat="server" ID="txt_ordine" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_ordine_TextChanged"></asp:TextBox>
        </div>
        <div class="col-12 col-md-4">
            <span class="font-small"><i>Etichetta</i></span><br />
            <asp:TextBox runat="server" ID="txt_etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_etichetta_TextChanged"></asp:TextBox>
        </div>


    </div>

    <asp:Panel runat="server" ID="pan_data" Visible="false" class="mt-4">
        <asp:Label runat="server" ID="lbl_magazzino" class="fw-bold"></asp:Label>
        <div class="row flex-column bg-head">
            <asp:Label runat="server" ID="lbl_ordine" class="fw-bold"></asp:Label>
            <asp:Label runat="server" ID="lbl_materiale" class="fw-bold"></asp:Label>
        </div>

        <div class="row bg-light-grey pb-3">
            <div class="col-4 col-md-5">
                <span class="font-small"><i>QTA</i></span><br />
                <asp:TextBox runat="server" ID="txt_qta" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-4">
                <span class="font-small"><i>UM</i></span><br />
                <asp:TextBox runat="server" ID="txt_UM" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <div runat="server" class="col-3 col-md-2" visible="false">
                <span class="font-small"><i>Linea</i></span><br />
                <asp:TextBox runat="server" ID="txt_lin" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-4 col-md-2" style="align-self: end">
                <asp:Button runat="server" ID="btn_conferma" CssClass="form-control btn btn-warning" Text="Conferma" OnClick="btn_conferma_Click" />
<%--                <asp:Button runat="server" ID="btn_conferma" CssClass="form-control btn btn-warning" Text="Conferma" OnClientClick="disableButton(this);" OnClick="btn_conferma_Click" /> --%>
                </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?PROD" UseSubmitBehavior="false" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
    <script type="text/javascript">
        function disableButton(button) {
            button.disabled = true;
        }
    </script>
</asp:Content>
