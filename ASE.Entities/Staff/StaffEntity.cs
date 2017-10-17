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
    [DbTable("Staff")]
    public class StaffEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("UserID", null, true)]
        public Guid UserID { get; set; }

        [DataMember]
		[Required]
		[DbField("Tier", null, true)]
		public int Tier { get; set; }
    }
}
