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
    internal class TimelineLogic : BaseLogic
    {
        internal object saveTimeline(Timeline Obj)
        {
            try
            {
                object ID = DalBase<TimelineEntity>.Save(Obj.Fields, Obj.IsNew);
                Obj.IsNew = false;

                return ID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal object saveTimeline(Timeline Obj, SqlTransaction trans)
        {
            try
            {
                object ID = DalBase<TimelineEntity>.Save(Obj.Fields, trans, Obj.IsNew);
                Obj.IsNew = false;

                return ID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal Timeline GetTimelineByIDObject(Guid ID, string LanguageCode = null)
        {
            try
            {
                TimelineDal objDAL = new TimelineDal();
                DataSet dsFields = objDAL.GetByID(ID, LanguageCode);
                if (dsFields == null || dsFields.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                return MappingManager.MapTimelineAsListOfBLObjects(dsFields)[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<Timeline> GetTimelineByUserIDObjects(Guid UserID, string LanguageCode = null)
        {
            try
            {
                TimelineDal objDAL = new TimelineDal();
                DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
                return MappingManager.MapTimelineAsListOfBLObjects(dsFields);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<Timeline> GetAllTimelineObjects()
        {
            try
            {
                TimelineDal objDAL = new TimelineDal();
                DataSet dsFields = objDAL.GetAll();
                return MappingManager.MapTimelineAsListOfBLObjects(dsFields);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<TimelineEntity> GetTimelineByUserIDEntities(Guid UserID, string LanguageCode = null)
        {
            try
            {
                TimelineDal objDAL = new TimelineDal();
                DataSet dsFields = objDAL.GetByUserID(UserID, LanguageCode);
                return MappingManager.MapTimelineAsListOfEntities(dsFields);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<TimelineEntity> GetAllTimelineEntities()
        {
            try
            {
                TimelineDal objDAL = new TimelineDal();
                DataSet dsFields = objDAL.GetAll();
                return MappingManager.MapTimelineAsListOfEntities(dsFields);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal int DeleteTimelineByID(Guid ID)
        {
            TimelineDal ObjDal = new TimelineDal();
            return ObjDal.DeleteByID(ID);
        }
    }
}