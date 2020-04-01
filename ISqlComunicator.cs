using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public interface ISqlComunicator
    {
        string ProcedureName { get; set; }
        bool GetData(string ProcedureName);
        bool AddData(string ProcedureName);
        bool ModifyData(string ProcedureName);
    }
}
