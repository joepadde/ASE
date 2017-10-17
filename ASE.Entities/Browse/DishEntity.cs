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
    [DbTable("Dish")]
    public class DishEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("StallID", null, true)]
        public Guid StallID { get; set; }

        [DataMember]
        [Required]
        [DbField("Name", null, true)]
        public string Name { get; set; }

		[DataMember]
		[DbField("Description", null, true)]
		public string Description { get; set; }

		[DataMember]
        [DbField("Photo", null, true)]
        public byte[] Photo { get; set; }

		[DataMember]
		[DbField("Price", null, true)]
		public float Price { get; set; }

		[DataMember]
		[DbField("OutOfOrder", null, true)]
		public bool OutOfOrder { get; set; }
    }
}
