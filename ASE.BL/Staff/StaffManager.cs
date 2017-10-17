using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.BL
{
	public class StaffManager
	{
		public void DeleteStaffByID(Guid ID, string LanguageCode = null)
		{
			LogicManager.ObjStaffLogic.DeleteStaffByID(ID);
		}

		public Staff GetStaffByIDObject(Guid ID, string LanguageCode = null)
		{
			return LogicManager.ObjStaffLogic.GetStaffByIDObject(ID, LanguageCode);
		}

		public List<Staff> GetStaffByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjStaffLogic.GetStaffByUserIDObjects(UserID, LanguageCode);
		}

		public List<Staff> GetAllStaffObjects()
		{
			return LogicManager.ObjStaffLogic.GetAllStaffObjects();
		}

		public List<StaffEntity> GetStaffByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjStaffLogic.GetStaffByUserIDEntities(UserID, LanguageCode);
		}

		public List<StaffEntity> GetAllStaffEntities()
		{
			return LogicManager.ObjStaffLogic.GetAllStaffEntities();
		}
	}
}