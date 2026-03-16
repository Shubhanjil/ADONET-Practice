using System;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace AdoNetConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
{
    var program = new Program();
    program.CreateTable();
    program.InsertRecord(101, "Amy", "amy@example.com", new DateTime(2017, 12, 1));
}

public void CreateTable()
{
    using (SqlConnection con = new SqlConnection(
        "Server=localhost\\SQLEXPRESS;Database=Student;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"))
    {
        string query = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='student' AND xtype='U')
                         CREATE TABLE student(
                             id INT NOT NULL PRIMARY KEY,
                             name VARCHAR(100),
                             email VARCHAR(50),
                             join_date DATE
                         )";

        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Table created (or already exists).");
    }
}

public void InsertRecord(int id, string name, string email, DateTime joinDate)
{
    using (SqlConnection con = new SqlConnection(
        "Server=localhost\\SQLEXPRESS;Database=Student;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"))
    {
        string query = "INSERT INTO student (id, name, email, join_date) VALUES (@id, @name, @email, @join_date)";
        SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@join_date", joinDate);

        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Record inserted successfully.");
    }
}
    }
}
