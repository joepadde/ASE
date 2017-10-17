using System;
using ASE.Entities;
using ASE.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace ASE.BL
{

	public class Order : BaseModel
	{

		public Order()
		{
			Init();
			InitBaseFields();
		}

		public Order(Guid StallId, Guid DishId, Guid UserId)
		{
			Init();
			InitBaseFields();
			Fields.StallID = StallId;
			Fields.DishID = DishId;
			Fields.UserID = UserId;
			Fields.Quantity = 1;
		}

		public Order(BaseEntity ObjBaseEntity)
		{
			Init();
			MapCommonFields(Fields, ObjBaseEntity);
		}

		public Order(OrderEntity ObjEntity)
		{
			Init();
			Fields = ObjEntity;
			if (Fields.ID != null && Fields.ID != default(Guid))
			{
				IsNew = false;
			}
			InitBaseFields();
		}

		private void Init()
		{
			_fields = null;
		}

		private void InitBaseFields()
		{
			if (Fields == null)
				Fields = new OrderEntity();
			if (Fields.ID == null)
				Fields.ID = Guid.NewGuid();
		}

		private bool _isNew = true;
		public bool IsNew
		{
			get { return _isNew; }
			set { _isNew = value; }
		}

		private OrderEntity _fields;
		public OrderEntity Fields
		{
			get { return _fields; }
			set { _fields = value; }
		}

		private static OrderManager _manager = null;
		public static OrderManager Manager
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["OrderManager"] == null)
					{
						_manager = new OrderManager();
						HttpContext.Current.Cache["OrderManager"] = _manager;
					}
					else
						_manager = HttpContext.Current.Cache["OrderManager"] as OrderManager;
					return _manager;
				}
				else
				{
					_manager = new OrderManager();
					return _manager;
				}
			}

		}

		private OrderDal _objDAL = null;
		public OrderDal ObjDAL
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["OrderDal"] == null)
					{
						_objDAL = new OrderDal();
						HttpContext.Current.Cache["OrderDal"] = _objDAL;
					}
					else
						_objDAL = HttpContext.Current.Cache["OrderDal"] as OrderDal;
					return _objDAL;
				}
				else
				{
					_objDAL = new OrderDal();
					return _objDAL;
				}
			}
		}

		public void Save()
		{
			LogicManager.ObjOrderLogic.saveOrder(this);
		}

	}
}