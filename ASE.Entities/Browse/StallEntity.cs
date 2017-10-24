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
    [DbTable("Stall")]
    public class StallEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("UserID", null, true)]
        public Guid UserID { get; set; }

        [DataMember]
        [Required]
        [DbField("Name", null, true)]
        public string Name { get; set; }

		[DataMember]
		[Required]
		[DbField("Description", null, true)]
		public string Description { get; set; }

		[DataMember]
		[DbField("Logo", null, true)]
		public Byte[] Logo { get; set; }

		[DataMember]
		[DbField("ContentType", null, true)]
		public string ContentType { get; set; }

	}
}
