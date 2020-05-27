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
        string QueryString { get; set; }
        List<SqlParameter> ParamList { get; set; }
        bool GetData(string ProcedureName = null);
        bool AddData(string ProcedureName = null);
        bool ModifyData(string ProcedureName);
    }
}
