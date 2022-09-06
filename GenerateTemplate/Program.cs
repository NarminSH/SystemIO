using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace GenerateTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connection = "server=localhost;database=Northwind;Trusted_Connection=True";
            SqlConnection con = new SqlConnection(connection);                                                                                       
        


            SqlCommand cmd = new SqlCommand("select table_name from INFORMATION_SCHEMA.TABLES ", con);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            List<object> tableNames = new List<object>();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tableNames.Add(dr[0]);

            }

            dr.Close();
            con.Close();

            string pathString = @"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1";
            DirectoryInfo di = new DirectoryInfo(pathString);
            
            if (di.Exists)
            {
                Console.WriteLine("That path exists already.");
            }
            else
            {
                di.Create();
                Console.WriteLine("The directory was created successfully.");
            }
            foreach (var item in tableNames)
            {

               string path = System.IO.Path.Combine(pathString, $"{item}.cs");

                Console.WriteLine("Path to my file: {0}\n", path);
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                    path = @"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1";
                }
                else
                {
                    Console.WriteLine("File \"{0}\" already exists.", item);
                }

            }


        }
    }
}
