using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.BL
{
	public class StallManager
	{
		public Stall GetStallByIDObject(Guid ID, string LanguageCode = null)
		{
			return LogicManager.ObjStallLogic.GetStallByIDObject(ID, LanguageCode);
		}

		public List<Stall> GetStallByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjStallLogic.GetStallByUserIDObjects(UserID, LanguageCode);
		}

		public List<Stall> GetAllStallObjects()
		{
			return LogicManager.ObjStallLogic.GetAllStallObjects();
		}

		public List<StallEntity> GetStallByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjStallLogic.GetStallByUserIDEntities(UserID, LanguageCode);
		}

		public List<StallEntity> GetAllStallEntities()
		{
			return LogicManager.ObjStallLogic.GetAllStallEntities();
		}
	}
}