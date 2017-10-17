using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.BL
{
	public class OrderManager
	{

		public void ClearBasket(Guid UserID, string LanguageCode = null)
		{
			LogicManager.ObjOrderLogic.ClearBasket(UserID, LanguageCode);
		}

		public void DeleteOrderByID(Guid ID, string LanguageCode = null)
		{
			LogicManager.ObjOrderLogic.DeleteOrderByID(ID);
		}

		public Order GetOrderByIDObject(Guid ID, string LanguageCode = null)
		{
			return LogicManager.ObjOrderLogic.GetOrderByIDObject(ID, LanguageCode);
		}

		public List<Order> GetOrderByStallIDObjects(Guid StallID, string LanguageCode = null)
		{
			return LogicManager.ObjOrderLogic.GetOrderByStallIDObjects(StallID, LanguageCode);
		}

		public List<OrderEntity> GetOrderByStallIDEntities(Guid StallID, string LanguageCode = null)
		{
			var list = LogicManager.ObjOrderLogic.GetOrderByStallIDObjects(StallID, LanguageCode);
			var entities = new List<OrderEntity>();
			foreach (var item in list)
				entities.Add(item.Fields);
			return entities;
		}

		public List<Order> GetOrderByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjOrderLogic.GetOrderByUserIDObjects(UserID, LanguageCode);
		}

		public List<Order> GetAllOrderObjects()
		{
			return LogicManager.ObjOrderLogic.GetAllOrderObjects();
		}

		public List<OrderEntity> GetOrderByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjOrderLogic.GetOrderByUserIDEntities(UserID, LanguageCode);
		}

		public List<OrderEntity> GetActiveOrderByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			return LogicManager.ObjOrderLogic.GetActiveOrderByUserIDEntities(UserID, LanguageCode);
		}

		public List<OrderEntity> GetAllOrderEntities()
		{
			return LogicManager.ObjOrderLogic.GetAllOrderEntities();
		}
	}
}