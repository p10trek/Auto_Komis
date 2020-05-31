using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class Customer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Phone { get; set; }
        public string Identy { get; set; }
        public Customer()
        {

        }
        public Customer(string identy)
        {

            CustomerDataAcces sqlComunicator = new CustomerDataAcces(identy);
            this.ID = (Guid) DataAcces.Instance.GetData(sqlComunicator).Rows[0][0];
        }
        public Customer(Customer customer)
        {
            CustomerDataAcces sqlComunicator = new CustomerDataAcces(customer);
            DataAcces.Instance.AddData(sqlComunicator);
        }
    }
    public class CustomerDataAcces : ISqlComunicator
    {
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        private Customer Customer { get; set; }
        public CustomerDataAcces(string identy)
        {
            this.Customer = new Customer();
            this.Customer.Identy = identy;
        }
        public CustomerDataAcces(Customer customer)
        {
            this.Customer = customer;
        }
        public bool GetData(string ProcedureName)
        {
            try
            {
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@Identy", this.Customer.Identy));
                this.ProcedureName = "Get_Customer";
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddData(string ProcedureName)
        {
            try
            {
                if (ProcedureName != null)
                {
                    this.ProcedureName = ProcedureName;
                }
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@ID", this.Customer.ID));
                ParamList.Add(new SqlParameter("@Name", this.Customer.Name));
                ParamList.Add(new SqlParameter("@Surname", this.Customer.Surname));
                ParamList.Add(new SqlParameter("@City", this.Customer.City));
                ParamList.Add(new SqlParameter("@Street", this.Customer.Street));
                ParamList.Add(new SqlParameter("@Number", this.Customer.Number));
                ParamList.Add(new SqlParameter("@Phone", this.Customer.Phone));
                this.ProcedureName = "Add_Customer";
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModifyData(string ProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}
