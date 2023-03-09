using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class SqlAccess
    {
        SqlConnection connection;
        string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ForsikringsselskabDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SqlAccess()
        {
            TestConnection();
        }

        private bool TestConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = cs;
            try
            {
                //connection.Close();
                connection.Open();
                connection.Close();
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }
        public DataTable ExecuteSql(string sql)
        {
            // Open new connection.
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();

            // Create sql command.
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Exetue sql command.
            DataTable table = ExecuteCmd(cmd);

            // Close the connection.
            connection.Close();

            return table;
        }
        private DataTable ExecuteCmd(SqlCommand cmd)
        {
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            return table;
        }
    }
}
