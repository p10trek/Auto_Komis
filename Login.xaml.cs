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
    public partial class Login : Window, ISqlComunicator
    {
        private string Position { get; set; }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        public bool GetData(string ProcedureName)
        {
            try
            {
                this.ProcedureName = "Sign_In";
                QueryString = "Select * from Logins";
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@Login", User.Text));
                ParamList.Add(new SqlParameter("@Pass", Password.Password));
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable result = DataAcces.Instance.GetData((ISqlComunicator)this);
            if (result.Rows[0].Field<string>("Result") == "Passed")
            {
                SalesList salesList = new SalesList();
                salesList.Show();
                this.Close();
            }
            //byte[] data = Encoding.GetEncoding(1252).GetBytes(Password.Password);
            //var sha = new SHA256Managed();
            //byte[] hash = sha.ComputeHash(data);
        }
    }
}
