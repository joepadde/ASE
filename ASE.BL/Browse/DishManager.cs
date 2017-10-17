using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.BL
{
	public class DishManager
	{
		public void DeleteDishByID(Guid ID, string LanguageCode = null)
		{
			LogicManager.ObjDishLogic.DeleteDishByID(ID);
		}

		public Dish GetDishByIDObject(Guid ID, string LanguageCode = null)
		{
			return LogicManager.ObjDishLogic.GetDishByIDObject(ID, LanguageCode);
		}

		public List<Dish> GetDishByStallIDObjects(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjDishLogic.GetDishByStallIDObjects(UserID, LanguageCode);
		}

		public List<Dish> GetAllDishObjects()
		{
			return LogicManager.ObjDishLogic.GetAllDishObjects();
		}

		public List<DishEntity> GetDishByStallIDEntities(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjDishLogic.GetDishByStallIDEntities(UserID, LanguageCode);
		}

		public List<DishEntity> GetAllDishEntities()
		{
			return LogicManager.ObjDishLogic.GetAllDishEntities();
		}
	}
}