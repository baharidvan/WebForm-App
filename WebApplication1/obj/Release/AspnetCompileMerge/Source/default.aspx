<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ana Sayfa</title>
</head>
<body>
    <h1>Ürün Listesi</h1>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="dsCategory" DataValueField="categoryName" AutoPostBack="true" Width="120px" Font-Size="11px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            <asp:ListItem Text="Tüm Kategoriler" Value="%"/>
        </asp:DropDownList>

        <asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False">
            <Columns>
               <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="30" />
               <asp:BoundField DataField="productName" HeaderText="Ürün Adı" ItemStyle-Width="250" />
               <asp:BoundField DataField="categoryName" HeaderText="Kategori" ItemStyle-Width="150" />
               <asp:BoundField DataField="price" HeaderText="Ücreti" ItemStyle-Width="30" />
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="ShowDetails" Text="Detaylar" runat="server" CommandName="FindID" CommandArgument='<%#Eval("ID")%>' OnClick="ButtonLink_Click" />
                </ItemTemplate>
               </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <span>
            <asp:LinkButton ID="showShoppingCart" Text="Sepettekileri Göster" runat="server" OnClick="showShoppingCart_Click" />
            <asp:Label ID="lblCartNum" runat="server"></asp:Label>
        </span>
        
    </div>
    <asp:SqlDataSource ID="dsCategory" runat="server" ConnectionString="<%$ConnectionStrings:Library %>" SelectCommand="Select Distinct CategoryName from Product"></asp:SqlDataSource>
    </form>
</body>
</html>
