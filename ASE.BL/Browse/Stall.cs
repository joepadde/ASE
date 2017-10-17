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

	public class Stall : BaseModel
	{

		public Stall()
		{
			Init();
			InitBaseFields();
		}

		public Stall(BaseEntity ObjBaseEntity)
		{
			Init();
			MapCommonFields(Fields, ObjBaseEntity);
		}

		public Stall(StallEntity ObjEntity)
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
				Fields = new StallEntity();
			if (Fields.ID == null)
				Fields.ID = Guid.NewGuid();
		}

		private bool _isNew = true;
		public bool IsNew
		{
			get { return _isNew; }
			set { _isNew = value; }
		}

		private StallEntity _fields;
		public StallEntity Fields
		{
			get { return _fields; }
			set { _fields = value; }
		}

		private static StallManager _manager = null;
		public static StallManager Manager
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["StallManager"] == null)
					{
						_manager = new StallManager();
						HttpContext.Current.Cache["StallManager"] = _manager;
					}
					else
						_manager = HttpContext.Current.Cache["StallManager"] as StallManager;
					return _manager;
				}
				else
				{
					_manager = new StallManager();
					return _manager;
				}
			}

		}

		private StallDal _objDAL = null;
		public StallDal ObjDAL
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["StallDal"] == null)
					{
						_objDAL = new StallDal();
						HttpContext.Current.Cache["StallDal"] = _objDAL;
					}
					else
						_objDAL = HttpContext.Current.Cache["StallDal"] as StallDal;
					return _objDAL;
				}
				else
				{
					_objDAL = new StallDal();
					return _objDAL;
				}
			}
		}

		public void Save()
		{
			LogicManager.ObjStallLogic.saveStall(this);
		}

	}
}