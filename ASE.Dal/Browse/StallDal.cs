using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.DAL
{
    public class StallDal
    {
        public DataSet GetAll()
        {
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Stall_GetAll");
            return dsFields;
        }

        public DataSet GetByID(Guid ID, string LanguageCode = null)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", ID);
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Stall_GetByID", Params);
            return dsFields;
        }

        public DataSet GetByUserID(Guid UserID, string LanguageCode = null)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@UserID", UserID);
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Stall_GetByUserID", Params);
            return dsFields;
        }

        public int DeleteByID(Guid ID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", ID);

            int AfRows = SqlHelper.ExecuteNonQuery(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Stall_DeleteByID", Params);
            return AfRows;
        }
    }
}