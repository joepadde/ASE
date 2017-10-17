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

        #region  Mapping Stall Functions
        internal static void FillStallDetails(DataSet dsFields, Stall Obj)
        {
            DataRow row = dsFields.Tables[0].Rows[0];

            Obj.IsNew = false;
            FillStallEntityObject(Obj.Fields, row);
        }

        internal static List<Stall> MapStallAsListOfBLObjects(DataSet dsFields)
        {
            List<Stall> lst = new List<Stall>();
            Stall BLObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                BLObject = new Stall();
                BLObject.IsNew = false;
                FillStallEntityObject(BLObject.Fields, row);
                lst.Add(BLObject);

            }

            return lst;
        }

        internal static List<StallEntity> MapStallAsListOfEntities(DataSet dsFields)
        {
            List<StallEntity> lst = new List<StallEntity>();
            StallEntity entityObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                entityObject = new StallEntity();
                FillStallEntityObject(entityObject, row);
                lst.Add(entityObject);
            }

            return lst;
        }

        private static void FillStallEntityObject(StallEntity Fields, DataRow row)
        {
            //Base
            Fields.ID = (row["ID"] != DBNull.Value) ? (Guid?)row["ID"] : null;

            //StallEntity
            Fields.UserID = (Guid) row["UserId"];
            Fields.Name = row["Name"].ToString();
            Fields.Logo = null;
        }
        #endregion

    }
}