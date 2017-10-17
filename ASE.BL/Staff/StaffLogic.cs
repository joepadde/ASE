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
	internal class StaffLogic : BaseLogic
	{
		internal object saveStaff(Staff Obj)
		{
			try
			{
				object ID = DalBase<StaffEntity>.Save(Obj.Fields, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal object saveStaff(Staff Obj, SqlTransaction trans)
		{
			try
			{
				object ID = DalBase<StaffEntity>.Save(Obj.Fields, trans, Obj.IsNew);
				Obj.IsNew = false;

				return ID;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal Staff GetStaffByIDObject(Guid ID, string LanguageCode = null)
		{
			try
			{
				StaffDal objDAL = new StaffDal();
				DataSet dsFields = objDAL.GetByID(ID, LanguageCode);
				if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				return MappingManager.MapStaffAsListOfBLObjects(dsFields)[0];
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Staff> GetStaffByUserIDObjects(Guid UserID, string LanguageCode = null)
		{
			try
			{
				StaffDal objDAL = new StaffDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapStaffAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<Staff> GetAllStaffObjects()
		{
			try
			{
				StaffDal objDAL = new StaffDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapStaffAsListOfBLObjects(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<StaffEntity> GetStaffByUserIDEntities(Guid UserID, string LanguageCode = null)
		{
			try
			{
				StaffDal objDAL = new StaffDal();
				DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
				return MappingManager.MapStaffAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal List<StaffEntity> GetAllStaffEntities()
		{
			try
			{
				StaffDal objDAL = new StaffDal();
				DataSet dsFields = objDAL.GetAll();
				return MappingManager.MapStaffAsListOfEntities(dsFields);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		internal int DeleteStaffByID(Guid ID)
		{
			StaffDal ObjDal = new StaffDal();
			return ObjDal.DeleteByID(ID);
		}
	}
}