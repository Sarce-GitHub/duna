<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="X3_TERMINALINI.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">

    <div class="container">
        <div class="row" runat="server" id="nav_general" style="margin-bottom: 30px">
            <div class="col-12"><b>MENU' GENERALE</b></div>
            <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btt_gen_Stock">
                <asp:Button runat="server" ID="btt_gen_Stock" CssClass="form-control btn btn-primary modal-check" Text="Magazzino" PostBackUrl="Menu.aspx?STK" UseSubmitBehavior="false" />
            </div>
            <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btt_gen_Ricevimento">
                <asp:Button runat="server" ID="btt_gen_Ricevimento" CssClass="form-control btn btn-secondary modal-check" Text="Ricevimento" PostBackUrl="Menu.aspx?RIC" UseSubmitBehavior="false" />
            </div>
            <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btt_gen_Spedizione">
                <asp:Button runat="server" ID="btt_gen_Spedizione" CssClass="form-control btn btn-info modal-check" Text="Spedizioni" PostBackUrl="Menu.aspx?SPED" UseSubmitBehavior="false" />
            </div>
        </div>

        <div runat="server" id="nav_stock" style="margin-bottom: 30px">
            <div class="row">
                <div class="col-12"><b>GESTIONE STOCK</b></div>
            </div>
            <div class="row">
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_RicercaStock" CssClass="form-control btn btn-secondary modal-check" Text="Ricerca Materiale" PostBackUrl="~/magazzino/Ricerca.aspx" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_cambiostock" CssClass="form-control btn btn-primary modal-check" Text="Cambio Ubicazione Merce" PostBackUrl="~/magazzino/CambioStock.aspx" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_UbicazionePallet" CssClass="form-control btn btn-info modal-check" Text="Cambio Ubicazione Pallet" PostBackUrl="~/magazzino/CambioStockPallet.aspx" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top" runat="server" id="nav_btn_GS_entratadiversa">
                    <asp:Button runat="server" ID="btn_GS_entratadiversa" CssClass="form-control btn btn-orange modal-check" Text="Entrata Diversa" PostBackUrl="~/magazzino/EntrataDiversa.aspx" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_AsssPallet" CssClass="form-control btn btn-success modal-check" Text="Assembla Pallet" PostBackUrl="~/magazzino/AssemblaPallet.aspx" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_DisPallet" CssClass="form-control btn btn-danger  modal-check" Text="Disassembla Pallet" PostBackUrl="~/magazzino/DisassemblaPallet.aspx" UseSubmitBehavior="false" />
                </div>
            </div>

            <div runat="server" class="row" ID="div_navetta">
                <div class="col-12 col-md-4 margin-row-top" >
                    <asp:Button runat="server" ID="btn_GS_CarNavetta" CssClass="form-control btn-orange modal-check" Text="Carico Navetta" PostBackUrl="~/magazzino/GestioneNavetta.aspx?TIPO=Carico" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top">
                    <asp:Button runat="server" ID="btn_GS_ScarNavetta" CssClass="form-control btn-ok modal-check" Text="Scarico Navetta" PostBackUrl="~/magazzino/GestioneNavetta.aspx?TIPO=Scarico" UseSubmitBehavior="false" />
                </div>
            </div>

        </div>

        <div class="row" runat="server" id="nav_ricevimento" style="margin-bottom: 30px">
            <div class="col-12"><b>GESTIONE RICEVIMENTI</b></div>
            <div class="col-12 col-md-4 margin-row-top">
                <asp:Button runat="server" ID="btn_GR_ricevimento" CssClass="form-control btn btn-secondary modal-check" Text="Ricevimento Materiale" PostBackUrl="~/ricevimento/Ricevimento.aspx" UseSubmitBehavior="false" />
            </div>
        </div>

        <div class="row" runat="server" id="nav_spedizione" style="margin-bottom: 30px">
            <div class="col-12"><b>GESTIONE SPEDIZIONI</b></div>
            <div runat="server" class="row" id="div_Presped">
                <div runat="server" class="col-12 col-md-4 margin-row-top" id="div_btn_SP_CarPresped">
                    <asp:Button runat="server" ID="btn_SP_CarPresped" CssClass="form-control btn-orange modal-check" Text="Carico Prespedizione" PostBackUrl="~/spedizione/GestionePresped.aspx?TIPO=Carico" UseSubmitBehavior="false" />
                </div>
                <div runat="server" class="col-12 col-md-4 margin-row-top" id="div_btn_SP_PrepPallet">
                    <asp:Button runat="server" ID="btn_SP_PrepPallet" CssClass="form-control btn btn-danger modal-check" Text="Prepara Pallet" PostBackUrl="~/spedizione/PreparaPallet.aspx" UseSubmitBehavior="false"/>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btn_SP_Ordine">
                    <asp:Button runat="server" ID="btn_SP_Ordine" CssClass="form-control btn btn-secondary modal-check" Text="Prepara Ordine" PostBackUrl="~/spedizione/Ordine.aspx" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btn_SP_Ordine_Spedizione">
                    <asp:Button runat="server" ID="btn_SP_Ordine_Spedizione" CssClass="form-control btn btn-primary modal-check" Text="Spedisci Ordine" PostBackUrl="~/spedizione/Ordine_Spedizione.aspx" UseSubmitBehavior="false" />
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btn_SP_BollaPreparazione">
                    <asp:Button runat="server" ID="btn_SP_BollaPreparazione" CssClass="form-control btn btn-secondary modal-check" Text="Bolla Preparzione" PostBackUrl="~/spedizione/BollePreparazione.aspx" UseSubmitBehavior="false" />
                </div>
                <div class="col-12 col-md-4 margin-row-top" runat="server" id="div_btn_SP_Disimpegno">
                    <asp:Button runat="server" ID="btn_SP_Disimpegno" CssClass="form-control btn btn-success modal-check" Text="Disimpegno Bancale" PostBackUrl="~/spedizione/Ordine_Spedizione_Disimpegno.aspx" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>


        <div class="row" style="margin-top: 40px">
            <div class="col-12 col-md-4 margin-row-top" runat="server" id="nav_MenuGen">
                <asp:Button runat="server" ID="btn_MenuGen" CssClass="form-control btn btn-white modal-check" Text="Menù Generale" PostBackUrl="Menu.aspx" UseSubmitBehavior="false" />
            </div>
            <div class="col-12 col-md-4 margin-row-top">
                <asp:Button runat="server" ID="btn_Disconnetti" CssClass="form-control btn btn-warning modal-check" Text="Disconnetti" OnClick="btn_Disconnetti_Click" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <%= _foot %>
</asp:Content>
