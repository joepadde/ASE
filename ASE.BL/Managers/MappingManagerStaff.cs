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

        #region  Mapping Staff Functions
        internal static void FillStaffDetails(DataSet dsFields, Staff Obj)
        {
            DataRow row = dsFields.Tables[0].Rows[0];

            Obj.IsNew = false;
            FillStaffEntityObject(Obj.Fields, row);
        }

        internal static List<Staff> MapStaffAsListOfBLObjects(DataSet dsFields)
        {
            List<Staff> lst = new List<Staff>();
            Staff BLObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                BLObject = new Staff();
                BLObject.IsNew = false;
                FillStaffEntityObject(BLObject.Fields, row);
                lst.Add(BLObject);

            }

            return lst;
        }

        internal static List<StaffEntity> MapStaffAsListOfEntities(DataSet dsFields)
        {
            List<StaffEntity> lst = new List<StaffEntity>();
            StaffEntity entityObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                entityObject = new StaffEntity();
                FillStaffEntityObject(entityObject, row);
                lst.Add(entityObject);
            }

            return lst;
        }

        private static void FillStaffEntityObject(StaffEntity Fields, DataRow row)
        {
            //Base
            Fields.ID = (row["ID"] != DBNull.Value) ? (Guid?)row["ID"] : null;

            //StaffEntity
            Fields.UserID = (Guid) row["UserId"];
            Fields.Tier = (int)row["Tier"];
        }
        #endregion

    }
}