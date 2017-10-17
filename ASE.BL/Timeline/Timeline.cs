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

    public class Timeline : BaseModel
    {

        public Timeline()
        {
            Init();
            InitBaseFields();
        }

        public Timeline(BaseEntity ObjBaseEntity)
        {
            Init();
            MapCommonFields(Fields, ObjBaseEntity);
        }

        public Timeline(TimelineEntity ObjEntity)
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
            if (Fields.ID == null)
            {
                Fields.ID = Guid.NewGuid();
            }
        }

        private bool _isNew = true;
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        private TimelineEntity _fields;
        public TimelineEntity Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        private static TimelineManager _manager = null;
        public static TimelineManager Manager
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Cache["TimelineManager"] == null)
                    {
                        _manager = new TimelineManager();
                        HttpContext.Current.Cache["TimelineManager"] = _manager;
                    }
                    else
                        _manager = HttpContext.Current.Cache["TimelineManager"] as TimelineManager;
                    return _manager;
                }
                else
                {
                    _manager = new TimelineManager();
                    return _manager;
                }
            }

        }

        private TimelineDal _objDAL = null;
        public TimelineDal ObjDAL
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Cache["TimelineDal"] == null)
                    {
                        _objDAL = new TimelineDal();
                        HttpContext.Current.Cache["TimelineDal"] = _objDAL;
                    }
                    else
                        _objDAL = HttpContext.Current.Cache["TimelineDal"] as TimelineDal;
                    return _objDAL;
                }
                else
                {
                    _objDAL = new TimelineDal();
                    return _objDAL;
                }
            }
        }

        public void Save()
        {
            LogicManager.ObjTimelineLogic.saveTimeline(this);
        }

    }
}