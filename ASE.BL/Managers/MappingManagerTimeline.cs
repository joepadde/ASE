using ASE.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ASE.BL
{
    public partial class MappingManager
    {

        #region  Mapping Timeline Functions
        internal static void FillTimelineDetails(DataSet dsFields, Timeline Obj)
        {
            DataRow row = dsFields.Tables[0].Rows[0];

            Obj.IsNew = false;
            FillTimelineEntityObject(Obj.Fields, row);
        }

        internal static List<Timeline> MapTimelineAsListOfBLObjects(DataSet dsFields)
        {
            List<Timeline> lst = new List<Timeline>();
            Timeline BLObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                BLObject = new Timeline();
                BLObject.IsNew = false;
                FillTimelineEntityObject(BLObject.Fields, row);
                lst.Add(BLObject);

            }

            return lst;
        }

        internal static List<TimelineEntity> MapTimelineAsListOfEntities(DataSet dsFields)
        {
            List<TimelineEntity> lst = new List<TimelineEntity>();
            TimelineEntity entityObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                entityObject = new TimelineEntity();
                FillTimelineEntityObject(entityObject, row);
                lst.Add(entityObject);
            }

            return lst;
        }

        private static void FillTimelineEntityObject(TimelineEntity Fields, DataRow row)
        {
            //Base
            Fields.ID = (row["ID"] != DBNull.Value) ? (Guid?)row["ID"] : null;

            //TimelineEntity
            Fields.UserID = (Guid) row["UserID"];
            Fields.TypeID = (Guid) row["TypeID"];
            Fields.TypePlace = row["TypePlace"].ToString();
            Fields.TypePosition = row["TypePosition"].ToString();
            Fields.TypeField = row["TypeField"].ToString();
            Fields.StartDate = (DateTime) row["StartDate"];
            Fields.EndDate = (row["EndDate"] != DBNull.Value) ? (DateTime?)row["EndDate"] : null;
        }
        #endregion

    }
}