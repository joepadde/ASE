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

	public class Dish : BaseModel
	{

		public Dish()
		{
			Init();
			InitBaseFields();
		}

		public Dish(BaseEntity ObjBaseEntity)
		{
			Init();
			MapCommonFields(Fields, ObjBaseEntity);
		}

		public Dish(DishEntity ObjEntity)
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
				Fields = new DishEntity();
			if (Fields.ID == null)
				Fields.ID = Guid.NewGuid();
		}

		private bool _isNew = true;
		public bool IsNew
		{
			get { return _isNew; }
			set { _isNew = value; }
		}

		private DishEntity _fields;
		public DishEntity Fields
		{
			get { return _fields; }
			set { _fields = value; }
		}

		private static DishManager _manager = null;
		public static DishManager Manager
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["DishManager"] == null)
					{
						_manager = new DishManager();
						HttpContext.Current.Cache["DishManager"] = _manager;
					}
					else
						_manager = HttpContext.Current.Cache["DishManager"] as DishManager;
					return _manager;
				}
				else
				{
					_manager = new DishManager();
					return _manager;
				}
			}

		}

		private DishDal _objDAL = null;
		public DishDal ObjDAL
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["DishDal"] == null)
					{
						_objDAL = new DishDal();
						HttpContext.Current.Cache["DishDal"] = _objDAL;
					}
					else
						_objDAL = HttpContext.Current.Cache["DishDal"] as DishDal;
					return _objDAL;
				}
				else
				{
					_objDAL = new DishDal();
					return _objDAL;
				}
			}
		}

		public void Save()
		{
			LogicManager.ObjDishLogic.saveDish(this);
		}

	}
}