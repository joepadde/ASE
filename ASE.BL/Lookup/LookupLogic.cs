using System;
using ASE.Entities;
using ASE.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ASE.BL
{
    internal class LookupLogic : BaseLogic
    {

        // This function Call the DalBase save in order to handle insert and update according to the object IsNew flag
        internal object saveLookup(Lookup ObjLookup)
        {
            object ID = null;

            SaveLookupByEntityType(ObjLookup, ID);

            ObjLookup.IsNew = false;

            return ID;

        }

        // This function take transa object in order to save Lookup in sql transaction object used when there dependency on other objects
        internal object saveLookup(Lookup ObjLookup, SqlTransaction trans)
        {
            try
            {
                object ID = null;
                SaveLookupByEntityType(ObjLookup, ID);
                ObjLookup.IsNew = false;

                return ID;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //This Function for getting all Lookup business model objects 
        internal List<Lookup> GetAllLookupObjects(string Language, string LookupName, string PageURL = null)
        {
			return null;

        }

        internal List<LookupEntity> GetAllLookupEntities(string Language, string LookupName, string PageURL = null)
        {
			return null;

        }

		//This Function to fill all the data of passed Key 
		/// <summary>
		/// fill all the data of passed Key,it fetchs the cache at first to retreive from and if no object in the cache it gets it from DB
		/// </summary>
		/// <param name="ObjLookup"></param>
		internal void FilLookupDetails(Lookup ObjLookup)
		{
		}


        internal int DeleteLookupByID(Guid ID, string LookUpName)
        {
            //base.NotifyUsers(this.GetDocumentByIDObject(ID), ChangeType.Delete, Entity.Document);
            LookupDal ObjDal = new LookupDal();
            int deletedID = ObjDal.DeleteByID(ID, LookUpName);


            return deletedID;
        }

        internal void SaveLookupByEntityType(Lookup ObjLookup, object ID)
        {
            if (ObjLookup.Fields is LanguageEntity)
            {
                ID = DalBase<LanguageEntity>.Save(ObjLookup.Fields as LanguageEntity, ObjLookup.IsNew);
                //base.NotifyUsers(ObjLookup.Fields, ObjLookup.IsNew ? ChangeType.Insert : ChangeType.Update, EntityName.LanguageEntity);
            }

            //if (ObjLookup.Fields is TableNameEntity)
            //{
            //    ID = DalBase<TableNameEntity>.Save(ObjLookup.Fields as TableNameEntity, ObjLookup.IsNew);
            //    base.NotifyUsers(ObjLookup.Fields, ObjLookup.IsNew ? ChangeType.Insert : ChangeType.Update, EntityName.TableNameEntity);
            //}
        }
    }
}
