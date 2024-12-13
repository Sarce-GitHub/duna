<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="PreparaPallet.aspx.cs" Inherits="X3_TERMINALINI.spedizione.PreparaPallet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
    <asp:HiddenField runat="server" ID="HF_ITMREF" />
    <asp:HiddenField runat="server" ID="HF_QTY_A" />
    <asp:HiddenField runat="server" ID="HF_QTY_Q" />
    <asp:HiddenField runat="server" ID="HF_QTY_R" />


    <div class="row">
        <div class="col-12">
             <h4>Preparazione Pallet</h4>
            <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
            <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
            <asp:HiddenField runat="server" ID="hf_LOC" />
        </div>
    </div>
     <div class="row">
        <div class="col-6 col-md-6">
            <span class="font-small"><i>Etichetta Pallet</i></span><br />
            <asp:TextBox runat="server" ID="txt_etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_etichetta_TextChanged"></asp:TextBox>
        </div>
        <div class="col-6 col-md-6" runat="server" id="div_ubicazione_pallet">
            <span class="font-small"><i>Ubicazione Pallet</i></span><br />
            <asp:TextBox runat="server" ID="lbl_ubicazione_pallet" CssClass="form-control chk-wait" Enabled="false"></asp:TextBox>
        </div>

    </div>
    <div runat="server" id="div_new">
        <div class="row">
            <asp:Label runat="server" ID="lbl_NewPallet" ForeColor="Blue"></asp:Label>
        </div>
        <div class="row">
            <div class="col-12 col-md-6">
                <span class="font-small"><i>Ubicazione Destinazione</i></span><br />
                <asp:TextBox runat="server" ID="txt_destinazione" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row" >
            <div class="col-12 col-md-2" style="margin-top:10px">
                <asp:Button runat="server" ID="btt_Nuovo_Conferma" CssClass="form-control btn btn-primary modal-check" Text="Conferma" OnClick="btt_Nuovo_Conferma_Click" ></asp:Button>
            </div>
            <div class="col-12 col-md-2" style="margin-top:10px">
                <asp:Button runat="server" ID="btt_Nuovo_Annulla" CssClass="form-control btn btn-danger modal-check" Text="Annulla" OnClick="btt_Nuovo_Annulla_Click" UseSubmitBehavior="false"></asp:Button>
            </div>
        </div>
    </div>
    <div runat="server" id="div_data">
        <div class="row">
             <div class="col-8 col-md-6">
                <span class="font-small"><i>Etichetta Articolo</i></span><br />
                <asp:TextBox runat="server" ID="txt_articolo" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_articolo_TextChanged"></asp:TextBox>
            </div>
            <div class="col-4 col-md-6">
                <span class="font-small"><i>Ubicazione Prelievo</i></span><br />
                <asp:TextBox runat="server" ID="txt_ubicazione" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_ubicazione_TextChanged"></asp:TextBox>
            </div>
        </div>

<%--        <div class="row">
            <div class="col-4 col-md-4">
                <span class="font-small"><i>Quantità Prelevata A</i></span><br />
                <asp:TextBox runat="server" ID="txt_Qta" CssClass="form-control text-end" ></asp:TextBox>
                <asp:Label runat="server" ID="lbl_Qta" CssClass="d-block text-danger w-100 fw-bold text-end"></asp:Label>
            </div>
            <div class="col-4 col-md-4">
                <span class="font-small"><i>Quantità Prelevata Q</i></span><br />
                <asp:TextBox runat="server" ID="txt_Qta_Q" CssClass="form-control text-end" ></asp:TextBox>
                <asp:Label runat="server" ID="lbl_Qta_Q" CssClass="d-block text-danger w-100 fw-bold text-end"></asp:Label>
            </div>
            <div class="col-4 col-md-4">
                <span class="font-small"><i>Quantità Prelevata R</i></span><br />
                <asp:TextBox runat="server" ID="txt_Qta_R" CssClass="form-control text-end" ></asp:TextBox>
                <asp:Label runat="server" ID="lbl_Qta_R" CssClass="d-block text-danger w-100 fw-bold text-end"></asp:Label>
            </div>
        </div>--%>

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
<%--            <div class="col-12 col-md-2" style="margin-top:10px">
                <asp:Button runat="server" ID="btt_Esegui" CssClass="form-control btn btn-primary modal-check" Text="Esegui e Termina" OnClick="btt_Esegui_Click" ></asp:Button>
            </div>--%>
            <div class="col-6 col-md-2" style="margin-top:10px">
                <asp:Button runat="server" ID="btt_EseguieContinua" CssClass="form-control btn btn-success modal-check" Text="Esegui e Continua" OnClick="btt_EseguieContinua_Click" UseSubmitBehavior="false" ></asp:Button>
            </div>
            <div class="col-6 col-md-2" style="margin-top:10px">
                <asp:Button runat="server" ID="btt_Annulla" CssClass="form-control btn btn-danger modal-check" Text="Termina" OnClick="btt_Annulla_Click" ></asp:Button>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <span class="font-small"><i>Dati Pallet</i></span><br />
                <asp:Label runat="server" ID="lbl_Articolo" CssClass="form-control base-label font-small" value="*"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
     <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?SPED" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>
