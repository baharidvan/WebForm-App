<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsPage.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ayrıntılı Ürün Gösterimi</title>
</head>
<body>
    <h1>Ayrıntılı Ürün Gösterimi</h1>
    <form id="form1" runat="server">
    <div style="text-align:center">
       <asp:GridView ID="gvDetailsPage" runat="server" AutoGenerateColumns="false">
            <Columns>
               <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="30" />
               <asp:BoundField DataField="productName" HeaderText="Ürün Adı" ItemStyle-Width="250" />
               <asp:BoundField DataField="categoryName" HeaderText="Kategori" ItemStyle-Width="150" />
               <asp:BoundField DataField="description" HeaderText="Ürün Hakkında Açıklama" ItemStyle-Width="400" />
               <asp:BoundField DataField="price" HeaderText="Ücreti" ItemStyle-Width="30" />
               <asp:BoundField DataField="purchaseNumber" HeaderText="Satın Alınma Sayısı" ItemStyle-Width="30" />
               <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="AddtoCart" Text="Sepete Ekle" runat="server" CommandName="FindID" CommandArgument='<%#Eval("ID")%>' OnClick="addToCart_Click" />
                    <asp:Label ID="lblPurchase" runat="server" Text="Satın almak istediğiniz adeti giriniz:  " Visible="false"></asp:Label>
                    <asp:TextBox ID="txtPurchase" style="width:50px;" runat="server" Visible="false"></asp:TextBox>
                    <asp:Button ID="btnSubmit" runat="server" Text="Onayla" Visible="false" CommandName="FindID" CommandArgument='<%#Eval("ID")+";"+Eval("productName")+";"+Eval("categoryName")+";"+Eval("price")%>' OnClick="btnSubmit_Click"/>
                </ItemTemplate>
               </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br/>
        <asp:LinkButton ID="backToProducts" Text="Ürün Listesine Dön" runat="server" OnClick="ReturnLink_Click" />
    </div>
    </form>
</body>
</html>
