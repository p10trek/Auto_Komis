using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            connection.Open();
            return connection;
        }


        public DataTable GetData(ISqlComunicator SqlComunicator)
        {
            SqlComunicator.GetData();
            return ExecuteProcedure(SqlComunicator);
        }
        public DataTable AddData(ISqlComunicator SqlComunicator)
        {
            SqlComunicator.AddData();
            return ExecuteProcedure(SqlComunicator);
        }
        public DataTable ModifyData(ISqlComunicator SqlComunicator)
        {
            SqlComunicator.ModifyData();
            return ExecuteProcedure(SqlComunicator);
        }

        public DataTable ExecuteProcedure(ISqlComunicator SqlComunicator)
        {
            using (SqlConnection connection = CreateSQLConnection())
            {

                SqlCommand command = new SqlCommand(SqlComunicator.ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (SqlComunicator.ParamList != null)
                {
                    foreach (SqlParameter param in SqlComunicator.ParamList)
                    {
                        command.Parameters.Add(param);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    resultTable = new DataTable();
                    resultTable.Load(reader);
                    
                    if(resultTable.Columns[0].ColumnName == "Succes")
                    {
                        MessageBox.Show(resultTable.Rows[0][0].ToString());
                    }
                    else if (resultTable.Columns[0].ColumnName == "Error")
                    {
                        MessageBox.Show(resultTable.Rows[0][0].ToString());
                    }
                    

                }
                finally
                {
                    reader.Close();
                }
            }
            return resultTable;
        }

    }
}
