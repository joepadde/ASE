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
    public class TimelineExtension : BaseEntity
    {

        [DataMember]
        [Required]
        [DbField("EntityID", null, true)]
        public Guid EntityID { get; set; }
 
    }
}
