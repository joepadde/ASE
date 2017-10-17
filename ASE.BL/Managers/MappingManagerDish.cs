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

        #region  Mapping Dish Functions
        internal static void FillDishDetails(DataSet dsFields, Dish Obj)
        {
            DataRow row = dsFields.Tables[0].Rows[0];

            Obj.IsNew = false;
            FillDishEntityObject(Obj.Fields, row);
        }

        internal static List<Dish> MapDishAsListOfBLObjects(DataSet dsFields)
        {
            List<Dish> lst = new List<Dish>();
            Dish BLObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                BLObject = new Dish();
                BLObject.IsNew = false;
                FillDishEntityObject(BLObject.Fields, row);
                lst.Add(BLObject);

            }

            return lst;
        }

        internal static List<DishEntity> MapDishAsListOfEntities(DataSet dsFields)
        {
            List<DishEntity> lst = new List<DishEntity>();
            DishEntity entityObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                entityObject = new DishEntity();
                FillDishEntityObject(entityObject, row);
                lst.Add(entityObject);
            }

            return lst;
        }

        private static void FillDishEntityObject(DishEntity Fields, DataRow row)
        {
            //Base
            Fields.ID = (row["ID"] != DBNull.Value) ? (Guid?)row["ID"] : null;

            //DishEntity
            Fields.StallID = (Guid) row["StallId"];
            Fields.Name = row["Name"].ToString();
			Fields.Description = row["Description"].ToString();
            Fields.Photo = null;
			Fields.Price = float.Parse(row["Price"].ToString());
			Fields.OutOfOrder = (bool) row["OutOfOrder"];
        }
        #endregion

    }
}