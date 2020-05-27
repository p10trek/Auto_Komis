using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy EmployeeAdding.xaml
    /// </summary>
    public partial class EmployeeAdding : Window, INotifyPropertyChanged
    {

        private Employee Employee { get; set; }
        private ObservableCollection<string> comboBoxItems;
        public ObservableCollection<string> ComboBoxItems
        {
            get
            {
                if(comboBoxItems == null)
                {
                    comboBoxItems = new ObservableCollection<string>();
                }
                return comboBoxItems;
            }
            set
            {
                comboBoxItems = value;
                OnPropertyChanged("ComboBoxItems");   
            }
        }
        

        public EmployeeAdding()
        {
            InitializeComponent();
            DataContext = this;
            ISqlComunicator sqlComunicator = new AddingEmployeeDataAccess();
            DataTable result = DataAcces.Instance.GetData(sqlComunicator);
            ComboBoxItems = new ObservableCollection<string>(result.Rows.OfType<DataRow>().Select(row=>row.Field<string>("Name").ToString()));
            
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Employee = new Employee();
                if (LogBox.Text.Length >= 3 && LogBox.Text.Length <= 20)
                    Employee.UserLogin = LogBox.Text;
                else
                {
                    MessageBox.Show("Incorrect Login (Length should be between 3-20");
                    throw new Exception();
                }
                if ((NamBox.Text.Length >= 3 && NamBox.Text.Length <= 20))
                    Employee.Name = NamBox.Text;
                else
                {
                    MessageBox.Show("Incorrect Name (Length should be between 3-20");
                    throw new Exception();
                }
                if ((SurBox.Text.Length >= 3 && SurBox.Text.Length <= 20))
                    Employee.Surname = SurBox.Text;
                else
                {
                    MessageBox.Show("Incorrect Surname (Length should be between 3-20");
                    throw new Exception();
                }
                if ((SurBox.Text.Length >= 3 && SurBox.Text.Length <= 20))
                    Employee.PositionName = PosComBox.Text;
                else
                {
                    MessageBox.Show("Incorrect Position name (Length should be between 3-20");
                    throw new Exception();
                }
                if (
                    DisBox.Text.Where(c => char.IsDigit(c)).Count() == DisBox.Text.Length
                    &&
                    Convert.ToInt64(DisBox.Text) < 100
                    &&
                    Convert.ToInt64(DisBox.Text) >= 0
                    )
                    Employee.DiscountLevel = Convert.ToInt16(DisBox.Text);
                else
                {
                    MessageBox.Show("Incorrect Discount Level (should be between <0;100))");
                    throw new Exception();
                }
                if (PassBox.Password == PassBox1.Password
                   &&
                   PassBox.Password.Length >= 3 && PassBox.Password.Length <= 20)
                    Employee.Password = PassBox.Password;
                else
                {
                    MessageBox.Show("Incorrect Password name (Length should be between 3-20 and Passwords fields schould have same values");
                    throw new Exception();
                }
                ISqlComunicator sqlComunicator = new AddingEmployeeDataAccess(Employee);
                DataAcces.Instance.AddData(sqlComunicator);
                Employee = null;

            }
            catch(Exception ex)
            {
                Employee = null;
                return;
            }

               
        }
    }
    public class AddingEmployeeDataAccess : ISqlComunicator
    {
        private Employee Employee { get; set; }
        string PositionName { get; set; }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public AddingEmployeeDataAccess()
        {
            this.ProcedureName = "Get_Position_List";
        }
        public AddingEmployeeDataAccess(Employee employee)
        {
            this.Employee = employee;
        }
        public bool GetData(string ProcedureName)
        {
            try
            {
                if (ProcedureName != null)
                {
                    this.ProcedureName = ProcedureName;
                }
                ParamList = new List<SqlParameter>();
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
                if (this.Employee == null) throw new ArgumentNullException();
                this.ProcedureName = "Add_Employee";
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@Login",Employee.UserLogin));
                ParamList.Add(new SqlParameter("@Name", Employee.Name));
                ParamList.Add(new SqlParameter("@Password", Employee.Password));
                ParamList.Add(new SqlParameter("@Position", Employee.PositionName));
                ParamList.Add(new SqlParameter("@Surname", Employee.Surname));
                ParamList.Add(new SqlParameter("@DiscountLevel", Employee.DiscountLevel));
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
