using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASE.DAL;
using System.Data;

namespace ASE.BL
{
	public class UserManager
	{
		public Guid GetUserIdByEmail(string Email)
		{
			try
			{
				UserDal objDAL = new UserDal();
				DataSet dsFields = objDAL.GetByEmail(Email);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return Guid.Empty;
				}
				var row = dsFields.Tables[0].Rows[0];
				return Guid.Parse(row["Id"].ToString());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public string GetEmailByUserId(Guid UserId)
		{
			try
			{
				UserDal objDAL = new UserDal();
				DataSet dsFields = objDAL.GetEmailByUserID(UserId);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return string.Empty;
				}
				var row = dsFields.Tables[0].Rows[0];
				return row["Email"].ToString();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}