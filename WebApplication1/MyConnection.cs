using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class MyConnection
    {
        string connectionString;
        SqlConnection connection;
        public MyConnection()
        {
            connectionString = "Data Source=LENOVO-PC;Initial Catalog=eTicaret;Integrated Security=True";
            //connectionString = "Server=(localdb)\v11.0; Data Source=.\\SQLEXPRESS; AttachDbFilename=C:\\Users\\USER\\Desktop\\VeriPark Staj\\WebApplication1\\WebApplication1\\app_data\\eTicaret2.mdf; Integrated Security=SSPI;User Instance=True";
            //connectionString = "Data Source=.\\SQLEXPRESS; AttachDbFilename=C:\\Users\\USER\\Desktop\\VeriPark Staj\\WebApplication1\\WebApplication1\\app_data\\eTicaret2.mdf; Integrated Security=SSPI;User Instance=True";
            connection = new SqlConnection(connectionString);
        }
        public SqlConnection getConnection()
        {
            return connection;
        }
    
       
        public string getConnectionString()
        {
            return connectionString;
        }
        
    }
    
}