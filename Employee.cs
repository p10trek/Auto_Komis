using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string UserLogin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public int DiscountLevel { get; set; }
        public string Password { get; set; }
        public Employee()
        {
            EmployeeDataAcces sqlComunicator = new EmployeeDataAcces();
            ID = (Guid)DataAcces.Instance.GetData(sqlComunicator).Rows[0][0];
        }
    }
    public class EmployeeDataAcces : ISqlComunicator
    {

        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public bool GetData(string ProcedureName)
        {
            try
            {
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@Login", CurrentUserInfo.Login));
                this.ProcedureName = "Get_Employee";
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool ModifyData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

    }


  
}
