<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/_include/Menu.Master" CodeBehind="Ordine_Righe_Ordine_Singolo.aspx.cs" Inherits="X3_TERMINALINI.spedizione.Ordine_Righe_Ordine_Singolo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
     <h4>Preparazione</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row" style="margin-bottom:20px">
        <div class="col-8 col-md-5">
            <span class="font-small"><i>Pallet</i></span><br />
            <asp:TextBox runat="server" ID="txt_Pallet" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Pallet_TextChanged"></asp:TextBox>
        </div>
        <div class="col-4 col-md-1">
            <span class="font-small"><i>&nbsp;</i></span><br />
            <asp:Button runat="server" ID="btn_Pallet" CssClass="form-control chk-wait" BackColor="Red" ForeColor="White" Text="Reset" OnClick="btn_Pallet_Click" />
        </div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Etichetta</i></span><br />
            <asp:TextBox runat="server" ID="txt_Etichetta" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Etichetta_TextChanged"></asp:TextBox>
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_dati"></asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="Ordine.aspx" UseSubmitBehavior="false"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
    <script>
        
        $(".check-pos").click(function () {
            document.location.href = "Ordine_Righe_Disp.aspx?ITM=" + $(this).closest('.row').attr("data-itm") + "&SAU=" +$(this).closest('.row').attr("data-sau"); 
        });
    </script>
</asp:Content>
