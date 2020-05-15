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
    /// Logika interakcji dla klasy SalesMenu.xaml
    /// </summary>
    public partial class SalesMenu : Window, ISqlComunicator
    {
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Employee Employee { get; set; }
        public SalesMenu()
        {
            InitializeComponent();
        }

        public string ProcedureName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string QueryString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<SqlParameter> ParamList { get; set; }
        public bool AddData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool ModifyData(string ProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}
