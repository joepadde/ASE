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

        #region  Mapping Order Functions
        internal static void FillOrderDetails(DataSet dsFields, Order Obj)
        {
            DataRow row = dsFields.Tables[0].Rows[0];

            Obj.IsNew = false;
            FillOrderEntityObject(Obj.Fields, row);
        }

        internal static List<Order> MapOrderAsListOfBLObjects(DataSet dsFields)
        {
            List<Order> lst = new List<Order>();
            Order BLObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                BLObject = new Order();
                BLObject.IsNew = false;
                FillOrderEntityObject(BLObject.Fields, row);
                lst.Add(BLObject);

            }

            return lst;
        }

        internal static List<OrderEntity> MapOrderAsListOfEntities(DataSet dsFields)
        {
            List<OrderEntity> lst = new List<OrderEntity>();
            OrderEntity entityObject;

            foreach (DataRow row in dsFields.Tables[0].Rows)
            {
                entityObject = new OrderEntity();
                FillOrderEntityObject(entityObject, row);
                lst.Add(entityObject);
            }

            return lst;
        }

        private static void FillOrderEntityObject(OrderEntity Fields, DataRow row)
        {
            //Base
            Fields.ID = (row["ID"] != DBNull.Value) ? (Guid?)row["ID"] : null;

            //OrderEntity
            Fields.UserID = (Guid) row["UserId"];
			Fields.StallID = (Guid) row["StallId"];
			Fields.DishID = (Guid) row["DishId"];
			Fields.Quantity = (int) row["Quantity"];
			Fields.OrderedTime = DateTime.Parse(row["OrderedTime"].ToString());
			Fields.PickupTime = DateTime.Parse(row["PickupTime"].ToString());
			bool? tmp = Convert.IsDBNull(row["Status"]) ? null : (bool?)row["Status"];
			Fields.Status = tmp;
		}
        #endregion

    }
}