using System;
using ASE.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ASE.Entities
{
    [DataContract]
    //[DbTable("Lookup_NotificationEvents")]
    public class LookupEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("[Key]", null, true)]
        public string Key { get; set; }

        [DataMember]
        [Required]
        [DbField("[en-US_Value]", null, true)]
        public string en_US_Value { get; set; }

        [DataMember]
        [Required]
        [DbField("[da-DK_Value]", null, true)]
        public string da_DK_Value { get; set; }


        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string LookupName { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        //[DataMember]
        //public string[] NotificationEventRoleIds { get; set; }

        //public IList<NotificationEventRoleEntity> LstNotificationEventRoles { get; set; }


    }

    public static class LookupFactory
    {
        /// <summary>
        /// Decides which class to instantiate.
        /// </summary>
        public static LookupEntity Create(string lookupName)
        {
            if (lookupName == "Lookup_TableName" || lookupName == "TableNameEntity")
            {
                return new TableNameEntity();
            }

            if (lookupName == "Lookup_Language" || lookupName == "LanguageEntity")
            {
                return new LanguageEntity();
            }

            if (lookupName == "Lookup_Keyword" || lookupName == "KeywordEntity")
            {
                return new KeywordEntity();
            }

            if (lookupName == "Lookup_TimelineEntityType" || lookupName == "TimelineEntityTypeEntity")
            {
                return new TimelineEntityTypeEntity();
            }

            if (lookupName == "Lookup_PipelineLevelType" || lookupName == "PipelineLevelTypeEntity")
            {
                return new PipelineLevelTypeEntity();
            }

            if (lookupName == "Lookup_ResponsibilityType" || lookupName == "ResponsibilityTypeEntity")
            {
                return new ResponsibilityTypeEntity();
            }

            return null;
        }
    }
}
