using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DetailsListPage();
            }
        }

        protected void DetailsListPage()
        {
            string ID = Request.QueryString["ID"];
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, ProductName, CategoryName, Description, Price, PurchaseNumber FROM Product WHERE ID = '" + ID + "'";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataAdapter.Fill(ds, "Detailspage");
            connection.Close();
            gvDetailsPage.DataSource = ds;
            gvDetailsPage.DataMember = "DetailsPage";
            gvDetailsPage.DataBind();
        }

        protected void ReturnLink_Click(object sender, System.EventArgs e)
        {
            Response.Redirect(string.Format("default.aspx"), false);
        }
        protected void addToCart_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < gvDetailsPage.Rows.Count; i++) //
            {
                TextBox txtPurchase = ((TextBox)(gvDetailsPage.Rows[i].FindControl("txtPurchase")));
                txtPurchase.Visible = true;
                Button btnSubmit = ((Button)(gvDetailsPage.Rows[i].FindControl("btnSubmit")));
                btnSubmit.Visible = true;
                Label lblPurchase = ((Label)(gvDetailsPage.Rows[i].FindControl("lblPurchase")));
                lblPurchase.Visible = true;
                LinkButton AddtoCart = ((LinkButton)(gvDetailsPage.Rows[i].FindControl("AddtoCart")));
                AddtoCart.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Button lnkRowSelection = (Button)sender;
            string ID = lnkRowSelection.CommandArgument;
            string[] commandArgs = lnkRowSelection.CommandArgument.ToString().Split(new char[] { ';' });
            string purcNum="", productName="", category="", price="";
            for (int i = 0; i < gvDetailsPage.Rows.Count; i++) //
            {
                TextBox txtPurchase = ((TextBox)(gvDetailsPage.Rows[i].FindControl("txtPurchase")));
                purcNum = txtPurchase.Text;
                productName = commandArgs[1];
                category = commandArgs[2];
                price = commandArgs[3]; 
                
            }
            if (purcNum == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Lütfen almak istediğiniz ürünün adetini giriniz!" + "');", true);
                return;
            }
            addtoCart(productName, category, float.Parse(price, CultureInfo.InvariantCulture.NumberFormat), Int32.Parse(purcNum));
            Response.Redirect(string.Format("ShoppingCartPage.aspx"), false);
        }
        public void addtoCart(string productName, string category, float price, int productNum)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Add_Product";
            cmd.Parameters.AddWithValue("@PRODUCT_NAME", productName);
            cmd.Parameters.AddWithValue("@CATEGORY", category);
            cmd.Parameters.AddWithValue("@PRICE", price);
            cmd.Parameters.AddWithValue("@COUNT", productNum);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    
    }
}