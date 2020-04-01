using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
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
    public partial class Login : Window,ISqlComunicator
    {
        private string UserLogin { get; set; }
        private SecureString Password { get; set; }
        private string Position { get; set; }
        public string ProcedureName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Login()
        {
            InitializeComponent();
        }

        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
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
