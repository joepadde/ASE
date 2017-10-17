
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.DAL
{
    public class UserDal
    {
        public DataSet GetByEmail(string Email, string LanguageCode = null)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@Email", Email);
            DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_User_GetByEmail", Params);
            return dsFields;
        }

		public DataSet GetEmailByUserID(Guid UserID, string LanguageCode = null)
		{
			SqlParameter[] Params = new SqlParameter[1];
			Params[0] = new SqlParameter("@ID", UserID);
			DataSet dsFields = SqlHelper.ExecuteDataset(ConnectionStringManager.ConnectionString, CommandType.StoredProcedure, "SP_User_GetEmailByID", Params);
			return dsFields;
		}
	}
}