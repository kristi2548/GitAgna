<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="importMe.aspx.cs" Inherits="importMe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div>
         <asp:Label ID="lblTableName" runat="server">tblName</asp:Label>
        <asp:TextBox ID="txtTableName" runat="server"></asp:TextBox>
         <asp:Label ID="Label1" runat="server">Field1Filter</asp:Label>
        <asp:TextBox ID="txtField1Filter" runat="server"></asp:TextBox>
         <asp:Label ID="Label2" runat="server">Field2Filter</asp:Label>
        <asp:TextBox ID="txtField2Filter" runat="server"></asp:TextBox>
    </div>
    <div class="d-inline-block bg-warning">
        <asp:Label ID="Label4" runat="server">Status</asp:Label>
        <asp:DropDownList ID="ddlGpsStatus" runat="server"></asp:DropDownList>
    </div>
    <div>
         <asp:FileUpload ID="Upload" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Upload" />
    </div>
     <div >
        <asp:Label runat="server" id="error" >:</asp:Label>
     </div>
      <div>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label3" runat="server">Kerko Sipas fushave</asp:Label>
        <asp:TextBox ID="txtKerkoGride" runat="server" OnTextChanged="txtKerkoGride_TextChanged" AutoPostBack="true"></asp:TextBox>
        <asp:Button ID="btnFiltro" runat="server" Text="Filtro" OnClick="btnFiltro_Click" />
    </div>
    <div>
        <asp:GridView ID="dgImportedData" runat="server"></asp:GridView>
    </div>
</asp:Content>
