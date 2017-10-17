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
	internal class DishLogic : BaseLogic
	{
		internal object saveDish(Dish Obj)
		{
			try
			{
				object ID = DalBase<DishEntity>.Save(Obj.Fields, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal object saveDish(Dish Obj, SqlTransaction trans)
		{
			try
			{
				object ID = DalBase<DishEntity>.Save(Obj.Fields, trans, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal Dish GetDishByIDObject(Guid ID, string LanguageCode = null)
		{
			try
			{
				DishDal objDAL = new DishDal();
				DataSet dsFields = objDAL.GetByID(ID, LanguageCode);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				return MappingManager.MapDishAsListOfBLObjects(dsFields)[0];
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Dish> GetDishByStallIDObjects(Guid StallID, string LanguageCode = null)
		{
			try
			{
				DishDal objDAL = new DishDal();
				DataSet dsFields = objDAL.GetByStallID(StallID, LanguageCode);
				return MappingManager.MapDishAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Dish> GetAllDishObjects()
		{
			try
			{
				DishDal objDAL = new DishDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapDishAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<DishEntity> GetDishByStallIDEntities(Guid UserID, string LanguageCode = null)
		{
			try
			{
				DishDal objDAL = new DishDal();
				DataSet dsFields = objDAL.GetByStallID(UserID, LanguageCode);
				return MappingManager.MapDishAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<DishEntity> GetAllDishEntities()
		{
			try
			{
				DishDal objDAL = new DishDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapDishAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal int DeleteDishByID(Guid ID)
		{
			DishDal ObjDal = new DishDal();
			return ObjDal.DeleteByID(ID);
		}
	}
}