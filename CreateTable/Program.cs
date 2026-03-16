using System;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace AdoNetConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().CreateTable();
        }
        public void CreateTable()
        {
            using (SqlConnection con = new SqlConnection(
    "Server=localhost\\SQLEXPRESS;Database=Student;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"))
            {
                SqlCommand cm = new SqlCommand(
                    "CREATE TABLE student(id INT NOT NULL, name VARCHAR(100), email VARCHAR(50), join_date DATE)", con);
                con.Open();
                cm.ExecuteNonQuery();
                Console.WriteLine("Table created Successfully");
            }

        }
    }
}
