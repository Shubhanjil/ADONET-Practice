using System;
using Microsoft.Data.SqlClient;

namespace AdoNetConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            //program.CreateTable();
            program.InsertRecord(101, "Amy", "amy@example.com", new DateTime(2017, 12, 1));
            //program.InsertRecord(102, "Dave", "dave@example.com", new DateTime(2014, 10, 3));
            program.ReadRecords();
            new Program().DeleteRecord(101);
            program.ReadRecords();
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
        public void ReadRecords()
        {
            using (SqlConnection con = new SqlConnection(
                "Server=localhost\\SQLEXPRESS;Database=Student;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"))
            {
                try
                {
                    string query = "SELECT id, name, email, join_date FROM student";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string email = reader.GetString(2);
                        DateTime joinDate = reader.GetDateTime(3);

                        Console.WriteLine($"{id} | {name} | {email} | {joinDate:yyyy-MM-dd}");
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops, something went wrong.\n" + e.Message);
                }
            }
        }
        public void DeleteRecord(int id)
        {
            using (SqlConnection con = new SqlConnection(
                "Server=localhost\\SQLEXPRESS;Database=Student;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"))
            {
                try
                {
                    string query = "DELETE FROM student WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine($"Record {id} deleted successfully.");
                    else
                        Console.WriteLine("No record found with the given ID.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops, something went wrong.\n" + e.Message);
                }
            }
        }

    }
}
