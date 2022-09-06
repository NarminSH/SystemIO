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

            //List<object> tableNames = new List<object>();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
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
                //string path = Path.Combine(pathString, $"{dr[0]}.cs");
                //if (!File.Exists(path))
                //{
                //    File.Create(path);
                //    path = @"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1";
                //}
                //else
                //{
                //    Console.WriteLine("File \"{0}\" already exists.", dr[0]);
                //}

                SqlCommand cmd1 = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE \'{dr[0]}\'", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                string examplePath1 = $@"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1\{dr[0]}.cs";
                StreamWriter sw = File.CreateText(examplePath1);
                sw.WriteLine($"public class {dr[0]} ");
                sw.WriteLine("{\n");
                while (dr1.Read())
                {
                    string type = "";
                    if (dr1[1].ToString() == "nvarchar")
                    {
                        type = "string";
                    }
                    else if (dr1[1].ToString() == "int" || dr1[1].ToString() == "smallint" || dr1[1].ToString() == "bit" || dr1[1].ToString() == "money")
                    {
                        type = "int";
                    }
                    sw.WriteLine($"public {type} {dr1[0]}; \n");
                }
                dr1.Close();
                sw.WriteLine("\n}");
                sw.Close();
            }
            dr.Close();
            con.Close();



            //foreach (var item in tableNames)
            //{
            //    string path = Path.Combine(pathString, $"{item}.cs");
            //    if (!File.Exists(path))
            //    {
            //        File.Create(path);
            //        path = @"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1";
            //    }
            //    else
            //    {
            //        Console.WriteLine("File \"{0}\" already exists.", item);
            //    }

            //    SqlCommand cmd1 = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE \'{item}\'", con);
            //    SqlDataReader dr1 = cmd1.ExecuteReader();
            //    string examplePath1 = $@"C:\Users\narmin.psh\source\repos\GenerateTemplate\GenerateTemplate\NewFolder\myDir1\{item}.cs";
            //    StreamWriter sw = File.CreateText(examplePath1);
            //    sw.WriteLine($"public class {item} ");
            //    sw.WriteLine("{\n");
            //    while (dr1.Read())
            //    {
            //        string type = "";
            //        if (dr1[1].ToString() == "nvarchar")
            //        {
            //            type = "string";
            //        }
            //        else if (dr1[1].ToString() == "int" || dr1[1].ToString() == "smallint" || dr1[1].ToString() == "bit" || dr1[1].ToString() == "money")
            //        {
            //            type = "int";
            //        }
            //        sw.WriteLine($"public {type} {dr1[0]}; \n");
            //        sw.Close();
            //    }
            //    dr1.Close();
            //    sw.WriteLine("\n}");
            //    sw.Close();
            //    Console.WriteLine("Path to my file: {0}\n", path);
            //}
            con.Close();
        }


    }
    }

