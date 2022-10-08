using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ShoppingCarListPage();
                

            }
        }
        protected void ShoppingCarListPage()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, ProductName, Price, ProductNumber FROM ShoppingCart";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataAdapter.Fill(ds, "ShoppingCartpage");
            connection.Close();
            gvShopCartPage.DataSource = ds;
            gvShopCartPage.DataMember = "ShoppingCartpage";
            gvShopCartPage.DataBind();

            if (findItemCount(connectionString) == "0")
            {
                lblPurc.Visible = false;
                btnPurc.Visible = false;
                lblSubmitMsg.Visible = true ;
                lblSubmitMsg.Text = "Sepette Ürün Yok!";
            }
        }
        
        protected void RemoveProduct_Click(object sender, System.EventArgs e)
        {
            LinkButton lnkRowSelection = (LinkButton)sender;
            string ID = lnkRowSelection.CommandArgument;
            deleteFromShoppingCart(Int32.Parse(ID));
        }

        private void deleteFromShoppingCart(int ID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM ShoppingCart WHERE ID = @ID";

            try
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                com.Parameters.AddWithValue("@ID", ID);
                com.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                Response.Redirect(string.Format("ShoppingCartPage.aspx"), false);
            }
        }

        protected void Purchase_Click(object sender, EventArgs e)
        {
            string PurchaseNumber = "", Productname = "";
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (GridViewRow gvr in gvShopCartPage.Rows)
            {
                SqlCommand cmd = new SqlCommand("Insert Into dbo.PurchasedProducts(ProductName, Price, NumberofItems) values(@ProductName, @Price, @NumberofItems)", connection);
                cmd.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@NumberofItems", SqlDbType.VarChar));
                cmd.Parameters["@ProductName"].Value = gvr.Cells[1].Text;
                Productname = gvr.Cells[1].Text;
                cmd.Parameters["@Price"].Value = gvr.Cells[2].Text;
                cmd.Parameters["@NumberofItems"].Value = gvr.Cells[3].Text;
                PurchaseNumber = gvr.Cells[3].Text;
                cmd.ExecuteNonQuery();

                updatePurchaseNum(Productname, PurchaseNumber);
            }
            connection.Close();
            string query = "DELETE FROM ShoppingCart";
            connection.Open();
            SqlCommand com = new SqlCommand(query, connection);
            com.ExecuteNonQuery();
            connection.Close();

            ShoppingCarListPage();
            lblPurc.Visible = false;
            btnPurc.Visible = false;
            lblSubmitMsg.Visible = true;
            lblSubmitMsg.Text = "Satın alma işlemi başarıyla gerçekleşti.";
        }

        protected void updatePurchaseNum(string Productname, string PurchaseNumber)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "Update Product Set PurchaseNumber = PurchaseNumber +@PurchaseNumber WHERE Productname=@Productname";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@Productname", Productname);
            com.Parameters.AddWithValue("@PurchaseNumber", PurchaseNumber);
            com.ExecuteNonQuery();
            connection.Close();
        }

        protected void BacktoMainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("default.aspx"), false);
        }
        protected string findItemCount(string connectionString)
        {
            string count = "0";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand commandRowCount = new SqlCommand("SELECT COUNT(*) FROM ShoppingCart", con))
                {
                    commandRowCount.CommandType = CommandType.Text;
                    var countStart = (Int32)commandRowCount.ExecuteScalar();
                    count = countStart.ToString();
                }
            }
            return count;
        }
        
    }
}