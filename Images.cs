using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Auto_Komis
{
    public class Images
    {
        public Guid ID { get; set; }
        public List<byte[]> ImageList { get; set; }
        public Images()
        {

        }
        public Images(Images images)
        {
            this.ID = images.ID;
            ISqlComunicator sqlComunicator = new ImagesDataAccess(images);
            DataAcces.Instance.AddData(sqlComunicator);
        }
    }
    class ImagesDataAccess : ISqlComunicator
    {
        Images Images { get; set; }
        public ImagesDataAccess(Images images)
        {
            this.Images = images;
        }
        public string ProcedureName { get; set; }
        public string QueryString { get; set; }
        public List<SqlParameter> ParamList { get; set; }
        public bool AddData(string ProcedureName)
        {

            if (this.Images == null) throw new ArgumentNullException();
            ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@ID",Images.ID));
            for(int i = 1; i<=5; i++ )
            {
                if (this.Images.ImageList.Count >= i)
                {
                    //ParamList.Add(new SqlParameter($"@Image{i}", this.Images.ImageList[i - 1])
                    //{
                    //    SqlDbType = SqlDbType.Image
                    //}); 
                    ParamList.Add(new SqlParameter($"@Image{i}", SqlDbType.Image, this.Images.ImageList[i - 1].Length) { Value = this.Images.ImageList[i - 1] });
               
                }
                else
                    ParamList.Add(new SqlParameter($"@Image{i}", null));
            }
            this.ProcedureName = "Add_Images";
            return true;
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
