using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Auto_Komis
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string Position { get; set; }

        public Login()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ISqlComunicator sqlComunicator = new LoginsDataAcces(User.Text, Password.Password);
            DataTable result = DataAcces.Instance.GetData(sqlComunicator);
            if (result.Rows[0].Field<string>("Result") == "Passed")
            {
                SalesList salesList = new SalesList();
                salesList.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Rows[0].Field<string>("Result"));
            }
        }
    }
    class LoginsDataAcces : ISqlComunicator
    {
        string User { get; set; }
        string Pass { get; set; }
        public LoginsDataAcces(string user, string pass)
        {
            this.User = user;
            this.Pass = pass;
        }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public bool GetData(string ProcedureName)
        {
            try
            {
                this.ProcedureName = "Sign_In";
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@Login", User));
                ParamList.Add(new SqlParameter("@Pass", Pass));
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
