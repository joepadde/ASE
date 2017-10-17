using ASE.Entities;
using ASE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.BL
{
    public class TimelineManager
    {
        public Timeline GetTimelineByIDObject(Guid ID, string LanguageCode = null)
        {
            return LogicManager.ObjTimelineLogic.GetTimelineByIDObject(ID, LanguageCode);
        }

        public List<Timeline> GetTimelineByUserIDObjects(Guid UserID, string LanguageCode = null)
        {
            return LogicManager.ObjTimelineLogic.GetTimelineByUserIDObjects(UserID, LanguageCode);
        }

        public List<Timeline> GetAllTimelineObjects()
        {
            return LogicManager.ObjTimelineLogic.GetAllTimelineObjects();
        }

        public List<TimelineEntity> GetTimelineByUserIDEntities(Guid UserID, string LanguageCode = null)
        {
            return LogicManager.ObjTimelineLogic.GetTimelineByUserIDEntities(UserID, LanguageCode);
        }

        public List<TimelineEntity> GetAllTimelineEntities()
        {
            return LogicManager.ObjTimelineLogic.GetAllTimelineEntities();
        }
    }
}