using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class TechnicalDetails
    {
        public Guid ID {get ;set ;}
        public DateTime YearOfProduction {get ;set ;}
        public int Mileage {get ;set ;}
        public int EngineCapacity {get ;set ;}
        public string FuelType {get ;set ;}
        public string Power {get ;set ;}
        public string Transmission {get ;set ;}
        public string Drive {get ;set ;}
        public string BodyType {get ;set ;}
        public string Color {get ;set ;}
        public bool? IsRegistered {get ;set ;}
        public bool? NoAccident {get ;set ;}
        public string Condition {get ;set ;}
        public TechnicalDetails()
        {

        }
        public TechnicalDetails(TechnicalDetails technicalDetails)
        {
            this.ID = Guid.NewGuid();
            technicalDetails.ID = this.ID;
            ISqlComunicator sqlComunicator = new TechnicalDetailsDataAccess(technicalDetails);
            DataAcces.Instance.AddData(sqlComunicator);
        }
    }
    public class TechnicalDetailsDataAccess : ISqlComunicator
    {
        private Equipments Equipments { get; set; }
        private TechnicalDetails TechnicalDetails { get; set; }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }

        public TechnicalDetailsDataAccess(TechnicalDetails technicalDetails)
        {
            this.TechnicalDetails = technicalDetails;
        }
        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool AddData(string ProcedureName)
        {
            try
            {
                if (this.TechnicalDetails == null) throw new ArgumentNullException();
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@ID", TechnicalDetails.ID));
                ParamList.Add(new SqlParameter("@YearOfProduction", TechnicalDetails.YearOfProduction));
                ParamList.Add(new SqlParameter("@Mileage", TechnicalDetails.Mileage));
                ParamList.Add(new SqlParameter("@EngineCapacity", TechnicalDetails.EngineCapacity));
                ParamList.Add(new SqlParameter("@FuelType", TechnicalDetails.FuelType));
                ParamList.Add(new SqlParameter("@Power", TechnicalDetails.Power));
                ParamList.Add(new SqlParameter("@Transmission", TechnicalDetails.Transmission));
                ParamList.Add(new SqlParameter("@Drive", TechnicalDetails.Drive));
                ParamList.Add(new SqlParameter("@BodyType", TechnicalDetails.BodyType));
                ParamList.Add(new SqlParameter("@Color", TechnicalDetails.Color));
                ParamList.Add(new SqlParameter("@IsRegistered", TechnicalDetails.IsRegistered));
                ParamList.Add(new SqlParameter("@NoAccident", TechnicalDetails.NoAccident));
                ParamList.Add(new SqlParameter("@Condition", TechnicalDetails.Condition));
                this.ProcedureName = "Add_TechnicalDetails";
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
