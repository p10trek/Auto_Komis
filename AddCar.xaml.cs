using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
using Microsoft.Win32;

namespace Auto_Komis
{
    /// <summary>
    /// Logika interakcji dla klasy AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        Car Car { get; set; }
        TechnicalDetails TechnicalDetails { get; set; }
        Equipments Equipments { get; set; }
        Images Images { get; set; }
        public AddCar()
        {
            InitializeComponent();
            Car = new Car();
        }

        private void Add_Images(object sender, RoutedEventArgs e)
        {
            List<byte[]> imageList = new List<byte[]>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            //if (openFileDialog.ShowDialog() == true)
            foreach (string fileName in openFileDialog.FileNames)
            {
                imageList.Add(GetPhoto(fileName));
            }
            this.Images = new Images();
            this.Images.ID = Guid.NewGuid();
            this.Images.ImageList = imageList;
            Images images = new Images(this.Images);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Car.ID = Guid.NewGuid();
                this.Equipments = new Equipments()
                {
                    ABS = ABS.IsChecked,
                    ElectricWindows = ElWind.IsChecked,
                    Airbag = Airbag.IsChecked,
                    Alu = Alu.IsChecked,
                    AirConditioning = AirCond.IsChecked,
                    CD = CD.IsChecked,
                    ElectricMirror = Mirrors.IsChecked,
                    FactoryAudio = Audo.IsChecked,
                    ESP = ESP.IsChecked,
                    Computer = Computer.IsChecked,
                    CentralLock = CentLock.IsChecked,
                    PowerSteering = PowSter.IsChecked,
                    Isofix = Isofix.IsChecked
                };

                Equipments equipments = new Equipments(this.Equipments);

                this.TechnicalDetails = new TechnicalDetails()
                {
                    YearOfProduction = Convert.ToDateTime("01.01."+Year.Text),
                    Mileage = Convert.ToInt32(Mileage.Text),
                    EngineCapacity = Convert.ToInt32(EngCap.Text),
                    FuelType = Fuel.Text,
                    Power = Power.Text,
                    Transmission = Transmission.Text,
                    Drive = Drive.Text,
                    BodyType = Body.Text,
                    Color = Color.Text,
                    IsRegistered = Registered.IsChecked,
                    NoAccident = Accident.IsChecked,
                    Condition = Condition.Text
                };
                
                TechnicalDetails technicalDetails = new TechnicalDetails(this.TechnicalDetails);

                this.Car = new Car()
                {
                    
                    Model = Model.Text,
                    Brand = Brand.Text,
                    EquipmentID = this.Equipments.ID,
                    TechnicalDetails = this.TechnicalDetails.ID,
                    ImagesID = this.Images.ID,
                    Price = Price.Text
                };

                ISqlComunicator sqlComunicator = new AddingCarDataAccess(this.Car);
                DataAcces.Instance.AddData(sqlComunicator);
                //this.Car.TechnicalDetails = null;
            }
            catch
            {
                this.Car = new Car()
                {
                    
                    Model = Model.Text,
                    Brand = Brand.Text,
                    EquipmentID = this.Equipments.ID,
                    TechnicalDetails = this.TechnicalDetails.ID,
                    ImagesID = this.Images.ID,
                    Price = Price.Text
                };
                ISqlComunicator sqlComunicator = new AddingCarDataAccess(this.Car);
                DataAcces.Instance.ModifyData(sqlComunicator);
            }


        }
        public static byte[] GetPhoto(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] photo = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return photo;
        }
        public class AddingCarDataAccess : ISqlComunicator
        {
            private Car Car { get; set; }
            private Equipments Equipments { get; set; }
            private TechnicalDetails TechnicalDetails { get; set; }
            public string ProcedureName { get; set; }
            public string QueryString { get; set; }
            public List<SqlParameter> ParamList { get; set; }

            public AddingCarDataAccess(Car car)
            {
                this.Car = car;
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
                CultureInfo culture = new CultureInfo("en-US");
                try
                {
                    if (this.Car == null) throw new ArgumentNullException();
                    ParamList = new List<SqlParameter>();
                    ParamList.Add(new SqlParameter("@ID", Car.ID));
                    ParamList.Add(new SqlParameter("@Brand", Car.Brand));
                    ParamList.Add(new SqlParameter("@Model", Car.Model));
                    ParamList.Add(new SqlParameter("@EquipmentID", Car.EquipmentID));
                    ParamList.Add(new SqlParameter("@TechnicalDetailsID", Car.TechnicalDetails));
                    ParamList.Add(new SqlParameter("@ImagesID", Car.ImagesID));
                    ParamList.Add(new SqlParameter("@Price", Convert.ToDecimal(Car.Price,culture))
                    {
                        DbType = System.Data.DbType.Decimal,
                        Precision = 10,
                        Scale = 2
                    });
                    this.ProcedureName = "Add_Car";
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool ModifyData(string ProcedureName)
            {
                try
                {
                    ParamList = new List<SqlParameter>();

                    if (Car.TechnicalDetails != Guid.Empty)
                        ParamList.Add(new SqlParameter("@TechnicalDetailsID", Car.TechnicalDetails));
                    else
                        ParamList.Add(new SqlParameter("@TechnicalDetailsID", null));
                    if (Car.EquipmentID != Guid.Empty)
                        ParamList.Add(new SqlParameter("@EquipmentsID", Car.EquipmentID));
                    else
                        ParamList.Add(new SqlParameter("@EquipmentsID", null));
                    if (Car.ImagesID != Guid.Empty)
                        ParamList.Add(new SqlParameter("@ImagesID", Car.ImagesID));
                    else
                        ParamList.Add(new SqlParameter("@ImagesID", null));
                    if (Car.ID != Guid.Empty)
                        ParamList.Add(new SqlParameter("@CarID", Car.ID));
                    else
                        ParamList.Add(new SqlParameter("@CarID", null));
                    this.ProcedureName = "Clean_Car_Adding";
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }
}
