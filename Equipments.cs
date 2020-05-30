using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Komis
{
    public class Equipments
    {
        public Guid ID { get; set; }
        public bool? ABS { get; set; }
        public bool? ElectricWindows { get; set; }
        public bool? Airbag { get; set; }
        public bool? Alu { get; set; }
        public bool? AirConditioning { get; set; }
        public bool? CD { get; set; }
        public bool? ElectricMirror { get; set; }
        public bool? FactoryAudio { get; set; }
        public bool? ESP { get; set; }
        public bool? Computer { get; set; }
        public bool? CentralLock { get; set; }
        public bool? PowerSteering { get; set; }
        public bool? Isofix { get; set; }

        public Equipments()
        {

        }
        public Equipments(Equipments equipments)
        {
            equipments.ID = Guid.NewGuid();
            this.ID = equipments.ID;
            ISqlComunicator sqlComunicator = new EquipmentDataAccess(equipments);
            DataAcces.Instance.AddData(sqlComunicator);
        }
    }

    public class EquipmentDataAccess : ISqlComunicator
    {
        private Equipments Equipments { get; set; }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }

        public EquipmentDataAccess(Equipments equipments)
        {
            this.Equipments = equipments;
        }
        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool AddData(string ProcedureName)
        {
            try
            {
                if (this.Equipments == null) throw new ArgumentNullException();
                ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@ID", Equipments.ID));
                ParamList.Add(new SqlParameter("@ABS", Equipments.ABS));
                ParamList.Add(new SqlParameter("@ElectricWindows", Equipments.ElectricWindows));
                ParamList.Add(new SqlParameter("@Airbag", Equipments.Airbag));
                ParamList.Add(new SqlParameter("@Alu", Equipments.Alu));
                ParamList.Add(new SqlParameter("@AirConditioning", Equipments.AirConditioning));
                ParamList.Add(new SqlParameter("@CD", Equipments.CD));
                ParamList.Add(new SqlParameter("@ElectricMirror", Equipments.ElectricMirror));
                ParamList.Add(new SqlParameter("@FactoryAudio", Equipments.FactoryAudio));
                ParamList.Add(new SqlParameter("@ESP", Equipments.ESP));
                ParamList.Add(new SqlParameter("@Computer", Equipments.Computer));
                ParamList.Add(new SqlParameter("@CentralLock", Equipments.CentralLock));
                ParamList.Add(new SqlParameter("@PowerSteering", Equipments.PowerSteering));
                ParamList.Add(new SqlParameter("@Isofix", Equipments.Isofix));
                this.ProcedureName = "Add_Equipments";
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
