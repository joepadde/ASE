using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.DAL
{
    public class OrderDal
    {
        public DataSet GetAll()
        {
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_GetAll");
            return dsFields;
        }

        public DataSet GetByID(Guid ID, string LanguageCode = null)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", ID);
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_GetByID", Params);
            return dsFields;
        }

        public DataSet GetByUserID(Guid UserID, string LanguageCode = null)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@UserID", UserID);
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_GetByUserID", Params);
            return dsFields;
        }

		public void ClearBasketByUserID(Guid UserID, string LanguageCode = null)
		{
			SqlParameter[] Params = new SqlParameter[1];
			Params[0] = new SqlParameter("@UserID", UserID);
			DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_ClearBasketByUserID", Params);
		}

		public DataSet GetActiveByUserID(Guid UserID, string LanguageCode = null)
		{
			SqlParameter[] Params = new SqlParameter[1];
			Params[0] = new SqlParameter("@UserID", UserID);
			DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_GetActiveByUserID", Params);
			return dsFields;
		}

		public DataSet GetByStallID(Guid StallID, string LanguageCode = null)
		{
			SqlParameter[] Params = new SqlParameter[1];
			Params[0] = new SqlParameter("@StallID", StallID);
			DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_GetByStallID", Params);
			return dsFields;
		}

		public int DeleteByID(Guid ID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", ID);

            int AfRows = SqlHelper.ExecuteNonQuery(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_Order_DeleteByID", Params);
            return AfRows;
        }
    }
}