using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.DAL
{
    public class LookupDal
    {
        public DataSet GetLookup(string Language, string LookupName, string PageURL = null)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@Language", Language);
            Params[1] = new SqlParameter("@LookupName", LookupName);
            Params[2] = new SqlParameter("@PageUrl", PageURL);

            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Lookup_GetAll", Params);
            return dsFields;
        }

        //public DataSet Insert(Guid UserID,string Name,string Description,byte[] Data)
        //{
        //    SqlParameter[] Params = new SqlParameter[6];
        //    Params[0] = new SqlParameter("@FK_UserID", UserID);
        //    Params[1] = new SqlParameter("@Name", Name);
        //    Params[2] = new SqlParameter("@Description", Description);
        //    Params[3] = new SqlParameter("@Data", Data);
        //    Params[4] = new SqlParameter("@CreatedBy", default(Guid));
        //    Params[5] = new SqlParameter("@ModifiedBy", default(Guid));

        //    Params[3].SqlDbType = SqlDbType.VarBinary;
        //    DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Documents_Insert", Params);
        //    return dsFields;
        //}

        //public DataSet Update(Guid ID,Guid UserID, string Name, string Description, byte[] Data)
        //{
        //    SqlParameter[] Params = new SqlParameter[6];
        //    Params[0] = new SqlParameter("@FK_UserID", UserID);
        //    Params[1] = new SqlParameter("@Name", Name);
        //    Params[2] = new SqlParameter("@Description", Description);
        //    Params[3] = new SqlParameter("@Data", Data);
        //    Params[4] = new SqlParameter("@ModifiedBy", default(Guid));
        //    Params[5] = new SqlParameter("@ID",ID);

        //    Params[3].SqlDbType = SqlDbType.VarBinary;
        //    DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Documents_Update", Params);
        //    return dsFields;
        //}
        //public int DeleteByID(Guid ID)
        //{
        //    SqlParameter[] Params = new SqlParameter[1];
        //    Params[0] = new SqlParameter("@ID", ID);

        //    int AfRows = SqlHelper.ExecuteNonQuery(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Documents_DeleteByID", Params);
        //    return AfRows;
        //}

        public int DeleteByID(Guid ID, string LookUpName)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@ID", ID);
            Params[1] = new SqlParameter("@LookUpName", LookUpName);

            int AfRows = SqlHelper.ExecuteNonQuery(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Lookup_DeleteByID", Params);
            return AfRows;
        }
    }
}
