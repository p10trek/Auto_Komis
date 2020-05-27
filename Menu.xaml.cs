using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Auto_Komis
{
    /// <summary>
    /// Logika interakcji dla klasy Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SalesList salesList = new SalesList();
            salesList.Show();
            Window.GetWindow(this).Close();
        }
        private void Car_List_Button_Click(object sender, RoutedEventArgs e)
        {
            CarList CarList = new CarList();
            CarList.Show();
            Window.GetWindow(this).Close();
        }
        private void Employee_List_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList EmployeeList = new EmployeeList();
            EmployeeList.Show();
            Window.GetWindow(this).Close();
        }
        private void Customer_List_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerList CustomerList = new CustomerList();
            CustomerList.Show();
            Window.GetWindow(this).Close();
        }

        private void Add_Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAdding employeeAdding = new EmployeeAdding();
            employeeAdding.Show();
            Window.GetWindow(this).Close();
        }
    }
}
