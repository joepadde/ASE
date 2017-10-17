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
    [DbTable("Timeline_Responsibility")]
    public class ResponsibilityEntity : TimelineExtension
    {
        [DataMember]
        [Required]
        [DbField("TypeID", null, true)]
        public Boolean LevelID { get; set; }
    }
}
