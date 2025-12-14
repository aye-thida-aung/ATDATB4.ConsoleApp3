using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATDATB4.ConsoleApp3;


internal class DapperSample
{
    private SqlConnectionStringBuilder sb;

    public DapperSample()
    {
        sb = new SqlConnectionStringBuilder();
        sb.DataSource = "."; // server name
        sb.InitialCatalog = "Batch4MiniPOS"; // database name
        sb.UserID = "sa";
        sb.Password = "12345";
        sb.TrustServerCertificate = true;
    }

    public void Read()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = "SELECT * FROM Tbl_Product WHERE IsDelete = 0";

            List<ProductDto> lst = db.Query<ProductDto>(query).ToList();

            foreach (ProductDto item in lst)
            {
                Console.WriteLine(item.ProductId);
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.Price);
                Console.WriteLine(item.Quantity);
            }
        }
    }

    public void Edit()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = "select * from tbl_product where productid = 1;";
            ProductDto item = db.Query<ProductDto>(query).FirstOrDefault()!;

            //if (item == null) return;
            if (item is null) return;

            Console.WriteLine(item.ProductId);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.Price);
            Console.WriteLine(item.Quantity);
        }
    }

    public void Create()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();

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
            int result = db.Execute(query);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }
    }

    public void Update()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();

            string query = @"UPDATE Tbl_Product
                         SET ProductName = 'Banana',
                             Price = 500,
                             Quantitty = 20
                         WHERE ProductId = 6";

            int result = db.Execute(query);

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            Console.WriteLine(message);
        }
    }


    public void Delete()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();

            string query = @"UPDATE Tbl_Product
                         SET IsDelete = 1
                         WHERE ProductId = 6";

            int result = db.Execute(query);

            string message = result > 0 ? "Delete Successful." : "Delete Failed.";
            Console.WriteLine(message);
        }
    }

}