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

        private static DataAcces instance;
        public static DataAcces Instance 
        { 
            get 
            {
                if (instance == null)
                {
                    instance = new DataAcces();
                }
                return instance;
            }
            private set 
            {
                instance = value;
            } 
        }
        private string ConnectionString { get; set; }

        DataTable resultTable;

        private SqlConnection CreateSQLConnection()
        {
            
            string ConnectionString = @"Server=LOCALHOST\SQLEXPRESS;Database=AutoKomisDB;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(ConnectionString);
            //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
            connection.Open();
            return connection;
        }


        public DataTable GetData(ISqlComunicator SqlComunicator)
        {
            SqlComunicator.GetData(null);
            using (SqlConnection connection = CreateSQLConnection()) 
            { 
                
                SqlCommand command = new SqlCommand(SqlComunicator.ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach(SqlParameter param in SqlComunicator.ParamList)
                {
                    command.Parameters.Add(param);
                }
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    resultTable = new DataTable();
                    resultTable.Load(reader);

                }
                finally
                {
                    reader.Close();
                } 
            }
            return resultTable;
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
