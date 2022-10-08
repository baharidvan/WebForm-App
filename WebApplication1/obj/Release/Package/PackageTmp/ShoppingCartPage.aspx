<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartPage.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Sepetteki Ürünler</title>
</head>
<body>
    <h1>Sepetteki Ürünler</h1>
    <form id="form1" runat="server">
    <div style="text-align:center">
     <asp:GridView ID="gvShopCartPage" runat="server" AutoGenerateColumns="false">
            <Columns>
               <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="30" />
               <asp:BoundField DataField="productName" HeaderText="Ürün Adı" ItemStyle-Width="250" />
               <asp:BoundField DataField="price" HeaderText="Ücreti" ItemStyle-Width="150" />
               <asp:BoundField DataField="productNumber" HeaderText="Sepetteki adeti" ItemStyle-Width="50" />
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="RemoveProduct" Text="Sepetten Çıkar" runat="server" CommandName="FindID" CommandArgument='<%#Eval("ID")%>' OnClick="RemoveProduct_Click" />
                </ItemTemplate>
               </asp:TemplateField>
            </Columns>
     </asp:GridView>
    <span style="margin-left: 10px">
        <asp:Label ID="lblPurc" runat="server" Text="Sepetteki ürünleri satın almak için Onaylayınız: " style="margin-right: 50px"></asp:Label>
        <asp:Button ID="btnPurc" runat="server" Text="Satın Al" style="margin-right: 190px; margin-top: 16px;" Height="25px" Width="67px" OnClick="Purchase_Click"/>
    </span>
    <span>
        <asp:Label ID="lblSubmitMsg" runat="server" style="font-size:large" Visible ="false"></asp:Label>
        <asp:LinkButton ID="BacktoMainPage" Text="Anasayfaya Dön" runat="server" OnClick="BacktoMainPage_Click" />
    </span>
    </div>
    </form>
</body>
</html>
