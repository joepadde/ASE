using ASE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.BL
{
    public class LookupManager
    {
        /// <summary>
        /// Get a List all Lookup Objects
        /// </summary>
        /// <returns>List all Lookup Objects</returns>
        public List<Lookup> GetAllLookupsObjects(string LanguageCode, string LookupName, string PageURL = null)
        {
            return LogicManager.ObjLookupLogic.GetAllLookupObjects(LanguageCode, LookupName, PageURL);
        }

        public List<LookupEntity> GetAllLookupsEntities(string LanguageCode, string LookupName, string PageURL = null)
        {
            return LogicManager.ObjLookupLogic.GetAllLookupEntities(LanguageCode, LookupName, PageURL);
        }

        public int DeleteLookupByID(Guid ID, string LookUpName)
        {
            return LogicManager.ObjLookupLogic.DeleteLookupByID(ID, LookUpName);
        }
    }
}
