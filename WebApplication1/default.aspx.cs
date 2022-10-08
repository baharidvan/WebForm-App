using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                productListPage();
            }
        }

        public class ListData
        {
            public int ID { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public double Price { get; set; }
        }
        protected void productListPage()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, ProductName, CategoryName, Price FROM Product";
            SqlCommand dataCommand = new SqlCommand(sql, connection);
            dataCommand.Connection.Open();
            SqlDataReader myReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
            DataSet ds = new DataSet();
           
            List<ListData> list = new List<ListData>();
            while (myReader.Read())
            {
                ListData data = new ListData();
                data.ID = (int)myReader["ID"];
                data.ProductName = (string)myReader["ProductName"];
                data.CategoryName = (string)myReader["CategoryName"];
                data.Price = (double)myReader["Price"];
                list.Add(data);
            }
            connection.Close();
            var sortedList = list.OrderBy(x => x.ProductName);
            gvProductList.DataSource = sortedList;
            gvProductList.DataMember = "Product_table";
            gvProductList.DataBind();
            lblCartNum.Text = "(" + findItemCount(connectionString) + ")";
        }
        protected void ButtonLink_Click(object sender, System.EventArgs e)
        {
            LinkButton lnkRowSelection = (LinkButton)sender;
            string ID = lnkRowSelection.CommandArgument;
            Response.Redirect(string.Format("DetailsPage.aspx?id={0}", ID), false);
        }

        protected void showShoppingCart_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ShoppingCartPage.aspx"), false);
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = ddlCategory.SelectedItem.Value;
            if (category == "%")
                productListPage();
            else
                dropdown(category);
        }

        protected void dropdown(string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Library"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, ProductName, CategoryName, Price FROM Product WHERE CategoryName=@CategoryName";
            SqlCommand dataCommand = new SqlCommand(sql, connection);
            dataCommand.Connection.Open();
            dataCommand.Parameters.AddWithValue("@CategoryName", category);
            SqlDataReader myReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
            DataSet ds = new DataSet();

            List<ListData> list = new List<ListData>();
            while (myReader.Read())
            {
                ListData data = new ListData();
                data.ID = (int)myReader["ID"];
                data.ProductName = (string)myReader["ProductName"];
                data.CategoryName = (string)myReader["CategoryName"];
                data.Price = (double)myReader["Price"];
                list.Add(data);
            }
            connection.Close();
            var sortedList = list.OrderBy(x => x.ProductName);
            gvProductList.DataSource = sortedList;
            gvProductList.DataMember = "Product_table";
            gvProductList.DataBind();
            lblCartNum.Text = "(" + findItemCount(connectionString) + ")";
        }

    }
}