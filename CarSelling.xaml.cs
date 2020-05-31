using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Auto_Komis
{
    /// <summary>
    /// Logika interakcji dla klasy CarSelling.xaml
    /// </summary>
    public partial class CarSelling : Window
    {
        Guid CarGuid { get; set; }
        Transaction Transaction { get; set; }
        Customer Customer { get; set; }
        public CarSelling(Guid guid)
        {
            InitializeComponent();
            this.Transaction = new Transaction();
            this.Transaction.CarID = guid;
            this.Transaction.ID = Guid.NewGuid();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (String.IsNullOrEmpty(Identy.Text))
            {
                this.Customer = new Customer
                {
                    ID = Guid.NewGuid(),
                    Name = Name.Text == "" ? "No Name" : Name.Text,
                    Surname = Surname.Text == "" ? "No Surname" : Surname.Text,
                    Phone = Phone.Text == "" ? "0" : Phone.Text,
                    Street = Street.Text == "" ? "No Street" : Street.Text,
                    Number = Number.Text == "" ? "0" : Number.Text,
                    City = City.Text == "" ? "No City" : City.Text,
                };
                Customer customer = new Customer(this.Customer);
            }
            else
            {
                this.Customer = new Customer(Identy.Text);
            }
            Employee employee = new Employee();
            this.Transaction.CustomerID = this.Customer.ID;
            this.Transaction.EmployeeID = employee.ID;
            ISqlComunicator sqlComunicator = new TransactionDataAcces(this.Transaction);
            DataAcces.Instance.AddData(sqlComunicator);
            SalesList salesList = new SalesList();
            salesList.Show();
            Window.GetWindow(this).Close();
        }
    }
    class TransactionDataAcces : ISqlComunicator
    {
        Transaction Transaction { get; set; }
        public TransactionDataAcces(Transaction transaction)
        {
            this.Transaction = transaction;
        }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
        }
        
        public bool AddData(string ProcedureName)
        {
            try
            {
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@ID", this.Transaction.ID));
                ParamList.Add(new SqlParameter("@EmployeeID", this.Transaction.EmployeeID));
                ParamList.Add(new SqlParameter("@CustomerID", this.Transaction.CustomerID));
                ParamList.Add(new SqlParameter("@CarID", this.Transaction.CarID));
                this.ProcedureName = "Add_Transaction";
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
