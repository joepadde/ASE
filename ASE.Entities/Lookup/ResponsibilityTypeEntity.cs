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
    [DbTable("Lookup_ResponsibilityType")]
    public class ResponsibilityTypeEntity : LookupEntity
    {
        [DataMember]
        [Required]
        [DbField("[en-US_Desc]", null, true)]
        public string en_US_Desc { get; set; }

        [DataMember]
        [Required]
        [DbField("[da-DK_Desc]", null, true)]
        public string da_DK_Desc { get; set; }
    }
}
