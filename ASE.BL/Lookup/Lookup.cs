using System;
using ASE.Entities;
using ASE.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ASE.BL
{
    public class Lookup : BaseModel
    {
        public Lookup(string lookupName)
        {
            Init(lookupName);
        }
        /// <summary>
        /// Create an Instance of Lookup
        /// </summary>
        /// <param name="Key"> The Key of the Lookup</param>
        /// <param name="LanguageCode">The language Code of the Lookup</param>
        public Lookup(string LanguageCode, string lookupName)
        {
            Init(lookupName);
            this.Fields.Language = LanguageCode;
            this.Fields.LookupName = lookupName;
            LogicManager.ObjLookupLogic.FilLookupDetails(this);
        }
        public Lookup(BaseEntity ObjBaseEntity, string lookupName)
        {
            Init(lookupName);
            MapCommonFields(Fields, ObjBaseEntity);
        }
        public Lookup(LookupEntity ObjEntity, string lookupName)
        {
            Init(lookupName);
            Fields = ObjEntity;
            if (Fields.ID != null && Fields.ID != default(Guid))
            {
                IsNew = false;
            }
        }
        internal void Init(string lookupName)
        {
            _fields = ASE.Entities.LookupFactory.Create(lookupName);//  new LookupEntity();
        }

        internal bool _isNew = true;
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        private LookupEntity _fields;
        /// <summary>
        /// The DB Fields of the Lookup Object
        /// </summary>
        public LookupEntity Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        internal static LookupManager _manager = null;
        public static LookupManager Manager
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Cache["LookupManager"] == null)
                    {
                        _manager = new LookupManager();
                        HttpContext.Current.Cache["LookupManager"] = _manager;
                    }
                    else
                        _manager = HttpContext.Current.Cache["LookupManager"] as LookupManager;
                    return _manager;
                }
                else
                {
                    _manager = new LookupManager();
                    return _manager;
                }
            }

        }

        internal LookupDal _objDAL = null;
        public LookupDal ObjDAL
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Cache["LookupDal"] == null)
                    {
                        _objDAL = new LookupDal();
                        HttpContext.Current.Cache["LookupDal"] = _objDAL;
                    }
                    else
                        _objDAL = HttpContext.Current.Cache["LookupDal"] as LookupDal;
                    return _objDAL;
                }
                else
                {
                    _objDAL = new LookupDal();
                    return _objDAL;
                }
            }
        }



        /// <summary>
        /// Save "Insert/Update" the Lookup Object to DB
        /// </summary>
        public virtual void Save()
        {
            LogicManager.ObjLookupLogic.saveLookup(this);
        }
    }
}
