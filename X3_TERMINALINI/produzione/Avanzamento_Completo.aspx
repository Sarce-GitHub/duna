<%@ Page Title="Avanzamento Completo" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Avanzamento_Completo.aspx.cs" Inherits="X3_TERMINALINI.produzione.Avanzamento_Completo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <h4>Avanzamento Completo</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>

    <asp:HiddenField ID="hf_FCY" runat="server" />
    <asp:HiddenField ID="hf_MFGNUM" runat="server" />
    <asp:HiddenField ID="hf_ITMREF" runat="server" />   
    <asp:HiddenField ID="hf_LOT" runat="server" />
    <asp:HiddenField ID="hf_UM" runat="server" />
    <asp:HiddenField ID="hf_COEFF" runat="server" />
    <asp:HiddenField ID="hf_STATUS" runat="server" />
    <asp:HiddenField ID="hf_CURRENTQTY" runat="server" />
    <asp:HiddenField ID="hf_ODP" runat="server" />
    <asp:HiddenField ID="hf_QTY" runat="server" />
    <asp:HiddenField ID="hf_PALNUM" runat="server" />


    <div class="row">
        <div class="col-12 col-md-10">
            <span class="font-small"><i>Ordine di Produzione</i></span><br/>
            <asp:TextBox runat="server" ID="txt_Ricerca" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Ricerca_TextChanged"></asp:TextBox>
        </div>

    </div>

    <asp:Panel runat="server" ID="pan_data" visible="false" class="mt-4">
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
            <div class="col-2 col-md-2">
                <span class="font-small"><i>UM</i></span><br />
                <asp:TextBox runat="server" ID="txt_UM" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-4 col-md-2">
                <span class="font-small"><i>PALNUM</i></span><br />
                <asp:TextBox runat="server" ID="txt_PALNUM" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>

            <div class="col-2">
                <span class="font-small"><i>Stato</i></span><br />
                <asp:DropDownList runat="server" ID="txt_Stato" CssClass="form-control" AutoPostBack="true" Enabled="false">
                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                    <asp:ListItem Text="R" Value="R"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-12 col-md-2 mt-2" style="align-self:end">
            <asp:Button runat="server" ID="btn_conferma" CssClass="form-control btn btn-warning" Text="Conferma" OnClick="btn_conferma_Click"/>
            </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?PROD" UseSubmitBehavior="false" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
