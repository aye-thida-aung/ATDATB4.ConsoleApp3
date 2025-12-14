using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB4.ConsoleApp3;

internal class AdoDotNetSample
{
    private SqlConnectionStringBuilder sb;
    //private SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder
    //{
    //    DataSource = ".",
    //    InitialCatalog = "Batch4MiniPOS",
    //    UserID = "sa",
    //    Password = "sasa@123",
    //    TrustServerCertificate = true
    //};

    public AdoDotNetSample()
    {
        sb = new SqlConnectionStringBuilder();
        sb.DataSource = "."; // server name
        sb.InitialCatalog = "Batch4MiniPOS"; // database name
        sb.UserID = "sa";
        sb.Password = "12345";
        sb.TrustServerCertificate = true;
    }

    //public AdoDotNetSample(SqlConnectionStringBuilder sb)
    //{
    //    this.sb = sb;
    //}

    public void Read()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        SqlCommand cmd = new SqlCommand("select * from tbl_product", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt); // execute

        connection.Close();

        // table > row > column
        foreach (DataRow dr in dt.Rows)
        {
            int porductId = Convert.ToInt32(dr["ProductId"]);
            string productName = Convert.ToString(dr["ProductName"]);
            decimal price = Convert.ToDecimal(dr["Price"]);
            Console.WriteLine("Product ID : " + porductId);

            Console.WriteLine("Product Name : " + productName);
            Console.WriteLine($"Price : {price.ToString("n0")}");
        }
    }

    public void Edit()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        SqlCommand cmd = new SqlCommand("select * from tbl_product where productid = 1", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt); // execute

        connection.Close();

        // 1 or 0 - success / fail

        if (dt.Rows.Count == 0) // > 0 ?
            return;

        DataRow dr = dt.Rows[0];

        string productName = Convert.ToString(dr["ProductName"]);
        decimal price = Convert.ToDecimal(dr["Price"]);
        Console.WriteLine("Product Name : " + productName);
        Console.WriteLine($"Price : {price.ToString("n0")}");
    }

    public void Create()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Price]
           ,[Quantitty]
           ,[IsDelete]
           ,[CreatedDateTime])
     VALUES
           ('Pineapple'
           ,4000
           ,30
           ,0
           ,GETDATE())";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Saving Successful." : "Saving Failed.";
        Console.WriteLine(message);
    }

    public void Update()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"UPDATE Tbl_Product
                     SET ProductName = 'Updated Apple Apple',
                         Price = 5000
                     WHERE ProductId = 1";

        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Update Successful." : "Update Failed.";
        Console.WriteLine(message);
    }


    public void Delete()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"UPDATE Tbl_Product
                     SET IsDelete = 1
                     WHERE ProductId = 6";

        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Delete Successful." : "Delete Failed.";
        Console.WriteLine(message);
    }

}