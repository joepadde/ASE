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
	internal class StallLogic : BaseLogic
	{
		internal object saveStall(Stall Obj)
		{
			try
			{
				object ID = DalBase<StallEntity>.Save(Obj.Fields, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal object saveStall(Stall Obj, SqlTransaction trans)
		{
			try
			{
				object ID = DalBase<StallEntity>.Save(Obj.Fields, trans, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal Stall GetStallByIDObject(Guid ID, string LanguageCode = null)
		{
			try
			{
				StallDal objDAL = new StallDal();
				DataSet dsFields = objDAL.GetByID(ID, LanguageCode);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				return MappingManager.MapStallAsListOfBLObjects(dsFields)[0];
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Stall> GetStallByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			try
			{
				StallDal objDAL = new StallDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapStallAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Stall> GetAllStallObjects()
		{
			try
			{
				StallDal objDAL = new StallDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapStallAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<StallEntity> GetStallByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			try
			{
				StallDal objDAL = new StallDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapStallAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<StallEntity> GetAllStallEntities()
		{
			try
			{
				StallDal objDAL = new StallDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapStallAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal int DeleteStallByID(Guid ID)
		{
			StallDal ObjDal = new StallDal();
			return ObjDal.DeleteByID(ID);
		}
	}
}