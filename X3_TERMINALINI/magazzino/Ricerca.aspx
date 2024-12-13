<%@ Page Title="" Language="C#" MasterPageFile="~/_include/Menu.Master" AutoEventWireup="true" CodeBehind="Ricerca.aspx.cs" Inherits="X3_TERMINALINI.magazzino.Ricerca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Main" runat="server">
     <h4>Ricerca Magazzino</h4>
    <asp:Label runat="server" ID="frm_error" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="frm_OK" ForeColor="DarkGreen"></asp:Label>
    <div class="row">
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Selezione Tipo Ricerca</i></span><br />
            <asp:DropDownList runat="server" ID="txt_Tipo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txt_Tipo_SelectedIndexChanged">
<%--                <asp:ListItem Text="* Seleziona una ricerca" Value="" ></asp:ListItem>--%>
                <asp:ListItem Text="Per Codice" Value="C"></asp:ListItem>
                <asp:ListItem Text="Per Descrizione" Value="D" ></asp:ListItem>
                <asp:ListItem Text="Per Etichetta" Value="E" ></asp:ListItem>
                <asp:ListItem Text="Per Ubicazione" Value="L" ></asp:ListItem>
                <asp:ListItem Text="Per Palnum" Value="P" ></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-12 col-md-6">
            <span class="font-small"><i>Ricerca</i></span><br />
            <asp:TextBox runat="server" ID="txt_Ricerca" CssClass="form-control chk-wait" AutoPostBack="true" OnTextChanged="txt_Ricerca_TextChanged"></asp:TextBox>
        </div>
    </div>

    <asp:Panel runat="server" ID="pan_data"></asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_Bottom" runat="server">
    <asp:Button runat="server" ID="btn_Indietro" CssClass="btn btn-dark modal-check" Text="Indietro" PostBackUrl="~/Menu.aspx?STK" UseSubmitBehavior="false"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
</asp:Content>