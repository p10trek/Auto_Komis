using System;
using System.Collections.Generic;
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
