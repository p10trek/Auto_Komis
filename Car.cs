using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class Car
    {
        public Guid ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        
        public string EquipmentString { get; set; }
        public Guid? EquipmentID { get; set; }

        public Guid? TechnicalDetails { get; set; }
        public Guid? ImagesID { get; set; }
        public string TechnicalDetailsString { get; set; }
        public string Price { get; set; }
        public Car()
        {

        }
        public Car(Guid GuidID)
        {
            this.ID = GuidID;
            ISqlComunicator sqlComunicator = new CarDataAccess(GuidID);
            sqlComunicator.ProcedureName = "get_Car_TechDetails";
            DataTable tempTable = DataAcces.Instance.GetData(sqlComunicator);
            foreach (DataColumn column in tempTable.Columns)
            {

                if (tempTable.Rows[0][column.ColumnName].ToString() != "")
                {
                    this.TechnicalDetailsString += $"{column.ColumnName}: {tempTable.Rows[0][column.ColumnName]},";
                }
            };
            this.TechnicalDetailsString = this.TechnicalDetailsString?.Remove(this.TechnicalDetailsString.Length - 1, 1) == null
                ? "No information in the database" : this.TechnicalDetailsString?.Remove(this.TechnicalDetailsString.Length - 1, 1);
            

            sqlComunicator = new CarDataAccess(GuidID);
            sqlComunicator.ProcedureName = "Get_Car_Equipments";
            tempTable = DataAcces.Instance.GetData(sqlComunicator);
            foreach (DataColumn column in tempTable.Columns)
            {

                if (tempTable.Rows[0][column.ColumnName].ToString() != "false")
                {
                    this.EquipmentString += $"{column.ColumnName},";
                }
            };
            this.EquipmentString = this.EquipmentString?.Remove(this.EquipmentString.Length - 1, 1) == null
               ? "No information in the database" : this.EquipmentString?.Remove(this.EquipmentString.Length - 1, 1);
        }

    }
    public class CarDataAccess : ISqlComunicator
    {
        Guid CarID { get; set; }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public CarDataAccess(Guid ID)
        {
            this.CarID = ID;
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
                ParamList.Add(new SqlParameter("@CarID", CarID));
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
