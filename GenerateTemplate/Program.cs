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
            const string connection = "server=localhost;database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=True";
            SqlConnection con = new SqlConnection(connection);                                                                                       
            SqlCommand cmd = new SqlCommand("select table_name from INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", con);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string pathString = @"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1";
                DirectoryInfo di = new DirectoryInfo(pathString);
                if (!di.Exists)
                {
                    di.Create();
                }

                SqlCommand cmd1 = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE \'{dr[0]}\'", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                string examplePath1 = $@"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1\{dr[0].ToString().Replace(" ", "")}.cs";
                StreamWriter sw = File.CreateText(examplePath1);
                sw.WriteLine($"public class {dr[0].ToString().Replace(" ", "")} ");
                sw.WriteLine("{\n");
                while (dr1.Read())
                {
                    string type = "";
                    if (dr1[1].ToString() == "int" || dr1[1].ToString() == "smallint" || dr1[1].ToString() == "bit" || dr1[1].ToString() == "money")
                    {
                        type = "int";
                    }
                    else
                    { type = "string"; }
                            
                    sw.WriteLine($"public {type} {dr1[0]} {{get; set;}} \n");
                }
                dr1.Close();
                sw.WriteLine("\n}");
                sw.Close();
            }
            dr.Close();
            con.Close();
        }
    }
    }

