using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class DataAcces 
    {
        
        public static DataAcces Instance { get; set; }
        private string ConnectionString { get; set; }
        private SqlConnection Connection { get; set; }


        public bool CreateSQLConnection()
        {
            string queryString = "select * from logins";
            string connectionString = @"Server=LOCALHOST\SQLEXPRESS;Database=AutoKomisDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return false;
        }
         

        public bool GetData(ISqlComunicator SqlComunicator)
        {
            return false;
        }
        public bool AddData(ISqlComunicator SqlComunicator)
        {
            return false;
        }
        public bool ModifyData(ISqlComunicator SqlComunicator)
        {
            return false;
        }

    }
}
