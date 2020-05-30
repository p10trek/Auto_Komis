using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Logika interakcji dla klasy CarDetails.xaml
    /// </summary>
    public partial class CarDetails : Window, INotifyPropertyChanged
    {

        private DataTable TempResult { get; set; }
        private Guid carID;

        private Car car;
        public Car Car
        {
            get
            {
                if (car == null)
                {
                    car = new Car();
                }
                return car;
            }
            set
            {
                car = value;
                OnPropertyChanged("Car");
            }
        }
        private Image image;
        public Image Image
        {
            get
            {
                if (image == null)
                {
                    image = new Image();
                }
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        private short imageCounter;
        public short ImageCounter
        {
            get
            {
                if (imageCounter == 0)
                {
                    imageCounter = 1;
                }
                return imageCounter;
            }
            set
            {
                if (imageCounter > 5)
                {
                    value = 0;
                }
                if (imageCounter < 1)
                {
                    value = 5;
                }
                imageCounter = value;
            }
        }
        public CarDetails(Guid CarID)
        {
            InitializeComponent();
            DataContext = this;
            carID = CarID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ISqlComunicator sqlComunicator = new CarDetailsDataAccess(carID);
                TempResult = DataAcces.Instance.GetData(sqlComunicator);
                this.Car = new Car(TempResult.Rows[0].Field<Guid>("carID"))
                {
                    Brand = $"Brand: {TempResult.Rows[0].Field<string>("Brand")}",
                    Model = $"Model: {TempResult.Rows[0].Field<string>("Model")}",
                    Price = $"Price: {TempResult.Rows[0].Field<decimal>("Price")}",

                };
                BitmapImage bitmapSource = LoadImage(TempResult.Rows[0].Field<byte[]>("Image1"));
                this.Image = new Image { Source = bitmapSource };
            }
            catch (Exception ex)
            {
                throw ex;
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
        private static BitmapImage LoadImage(byte[] imageData)
        {
            try
            {


                if (imageData == null || imageData.Length == 0) return null;
                var image = new BitmapImage();
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            ImageCounter += 1;
            if (LoadImage(TempResult.Rows[0].Field<byte[]>($"Image{ImageCounter}")) == null)
            {
                ImageCounter -= 1;
            }
            else
            {
                BitmapImage bitmapSource = LoadImage(TempResult.Rows[0].Field<byte[]>($"Image{ImageCounter}"));
                this.Image = new Image { Source = bitmapSource };
            }

        }
        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            ImageCounter -= 1;
            if (LoadImage(TempResult.Rows[0].Field<byte[]>($"Image{ImageCounter}")) == null)
            {
                ImageCounter += 1;
            }
            else
            {
                BitmapImage bitmapSource = LoadImage(TempResult.Rows[0].Field<byte[]>($"Image{ImageCounter}"));
                this.Image = new Image { Source = bitmapSource };
            }

        }
    }

    public class CarDetailsDataAccess : ISqlComunicator
    {
        private Guid CarID { get; set; }
        public CarDetailsDataAccess(Guid CarID)
        {
            this.CarID= CarID;
        }
        public string ProcedureName { get; set; }
        public string QueryString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<SqlParameter> ParamList { get; set; }
        public bool AddData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool GetData(string ProcedureName)
        {
            this.ProcedureName = "Get_Car_Details";
            ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CarID", CarID));
            return true;
        }

        public bool ModifyData(string ProcedureName)
        {
            throw new NotImplementedException();
        }
    }


}
