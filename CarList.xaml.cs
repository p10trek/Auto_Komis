using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Logika interakcji dla klasy CarList.xaml
    /// </summary>
    public partial class CarList : Window, INotifyPropertyChanged
    {
        private DataView carTable;
        public DataView CarTable
        {
            get
            {
                return carTable;
            }
            set
            {
                carTable = null;
                carTable = value;
                OnPropertyChanged("CarTable");
            }
        }
        public List<Car> ListOfCars { get; set; }
        public CarList()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string guid = (((sender as DataGridRow)?.Item as DataRowView)?.Row as DataRow)?.ItemArray[0]?.ToString();
            if (!String.IsNullOrEmpty(guid))
            {
                Guid carID = new Guid(guid);
                if (carID != Guid.Empty)
                {
                    CarDetails carDetails = new CarDetails(carID);
                    carDetails.Show();
                    this.Close();
                }
            }
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID")
            {
                e.Cancel = true;
            }

        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ISqlComunicator sqlComunicator = new CarsDataAcces();
            DataTable result = DataAcces.Instance.GetData(sqlComunicator);
            if (result.Rows.Count > 0)
            {
                CarTable = new DataView();
                CarTable = result.DefaultView;
            }
            else
            {
                MessageBox.Show(result.Rows[0].Field<string>("Result"));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
    class CarsDataAcces : ISqlComunicator
    {
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public bool GetData(string ProcedureName)
        {
            try
            {
                this.ProcedureName = "Get_Car_list";
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
