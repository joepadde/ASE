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

	public class Staff : BaseModel
	{

		public Staff()
		{
			Init();
			InitBaseFields();
		}

		public Staff(string Email)
		{
			Init();
			InitBaseFields();
			var _manager = new UserManager();
			Fields.UserID = _manager.GetUserIdByEmail(Email);
			Fields.Tier = 2;
		}

		public Staff(BaseEntity ObjBaseEntity)
		{
			Init();
			MapCommonFields(Fields, ObjBaseEntity);
		}

		public Staff(StaffEntity ObjEntity)
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
				Fields = new StaffEntity();
			if (Fields.ID == null)
				Fields.ID = Guid.NewGuid();
		}

		private bool _isNew = true;
		public bool IsNew
		{
			get { return _isNew; }
			set { _isNew = value; }
		}

		private StaffEntity _fields;
		public StaffEntity Fields
		{
			get { return _fields; }
			set { _fields = value; }
		}

		private static StaffManager _manager = null;
		public static StaffManager Manager
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["StaffManager"] == null)
					{
						_manager = new StaffManager();
						HttpContext.Current.Cache["StaffManager"] = _manager;
					}
					else
						_manager = HttpContext.Current.Cache["StaffManager"] as StaffManager;
					return _manager;
				}
				else
				{
					_manager = new StaffManager();
					return _manager;
				}
			}

		}

		private StaffDal _objDAL = null;
		public StaffDal ObjDAL
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Cache["StaffDal"] == null)
					{
						_objDAL = new StaffDal();
						HttpContext.Current.Cache["StaffDal"] = _objDAL;
					}
					else
						_objDAL = HttpContext.Current.Cache["StaffDal"] as StaffDal;
					return _objDAL;
				}
				else
				{
					_objDAL = new StaffDal();
					return _objDAL;
				}
			}
		}

		public void Save()
		{
			LogicManager.ObjStaffLogic.saveStaff(this);
		}

	}
}