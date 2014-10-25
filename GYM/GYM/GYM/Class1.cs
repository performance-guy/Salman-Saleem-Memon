using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GYM
{
    class Class1
    {
        static string table;
        static byte id;
        static string query;
        static string constr = "Data Source=localhost;Initial Catalog=New;Integrated Security=True";
        SqlConnection connection = new SqlConnection(constr);
        string select_Last_Added;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int temp;

        public static string Table
        {
            set { table = value; }
            get { return table; }
        }

        public static byte ID
        {
            set { id = value; }
            get { return id; }
        }

        public static string Query
        {
            set { query = value; }
            get { return query; }
        }

        public int Select_Last_Member()
        {
            Open();
            select_Last_Added = "select [Last Member] from Admin";
            cmd.CommandText = select_Last_Added;
            cmd.Connection = connection;
            reader = cmd.ExecuteReader();
            reader.Read();
            temp = (int)(reader["Last Member"]);
            reader.Close();
            Close();
            return temp;
        }

        public int Select_Last_Employee()
        {
            select_Last_Added = "select [Last Employee] from Admin";
            Open();
            cmd.CommandText = select_Last_Added;
            cmd.Connection = connection;
            reader = cmd.ExecuteReader();
            reader.Read();
            temp = (int)(reader["Last Employee"]);
            reader.Close();
            Close();
            return temp;
        }

        public int Select_Last_Machine()
        {
            select_Last_Added = "select [Last Machine] from Admin";
            Open();
            cmd.CommandText = select_Last_Added;
            cmd.Connection = connection;
            reader = cmd.ExecuteReader();
            reader.Read();
            temp = (int)(reader["Last Machine"]);
            reader.Close();
            Close();
            return temp;
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public SqlConnection Conn()
        {
            return connection;
        }
    }
}
