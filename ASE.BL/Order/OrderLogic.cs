using System;
using ASE.Entities;
using ASE.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ASE.BL
{
	internal class OrderLogic : BaseLogic
	{
		internal object saveOrder(Order Obj)
		{
			try
			{
				object ID = DalBase<OrderEntity>.Save(Obj.Fields, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal object saveOrder(Order Obj, SqlTransaction trans)
		{
			try
			{
				object ID = DalBase<OrderEntity>.Save(Obj.Fields, trans, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal Order GetOrderByIDObject(Guid ID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetByID(ID, LanguageCode);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				return MappingManager.MapOrderAsListOfBLObjects(dsFields)[0];
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal void ClearBasket(Guid UserID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				objDAL.ClearBasketByUserID(UserID, LanguageCode);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Order> GetOrderByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapOrderAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Order> GetOrderByStallIDObjects(Guid StallID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetByStallID(StallID, LanguageCode);
				return MappingManager.MapOrderAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Order> GetAllOrderObjects()
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapOrderAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<OrderEntity> GetOrderByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapOrderAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<OrderEntity> GetActiveOrderByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetActiveByUserID(UserID, LanguageCode);
				return MappingManager.MapOrderAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<OrderEntity> GetAllOrderEntities()
		{
			try
			{
				OrderDal objDAL = new OrderDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapOrderAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal int DeleteOrderByID(Guid ID)
		{
			OrderDal ObjDal = new OrderDal();
			return ObjDal.DeleteByID(ID);
		}
	}
}