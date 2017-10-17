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
    [DbTable("DishOrder")]
    public class OrderEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("UserID", null, true)]
        public Guid UserID { get; set; }

		[DataMember]
		[Required]
		[DbField("StallId", null, true)]
		public Guid StallID { get; set; }

		[DataMember]
		[Required]
		[DbField("DishID", null, true)]
		public Guid DishID { get; set; }

		[DataMember]
		[Required]
		[DbField("Quantity", null, true)]
		public int Quantity { get; set; }

		[DataMember]
		[Required]
		[DbField("OrderedTime", null, true)]
		public DateTime OrderedTime { get; set; }

		[DataMember]
		[Required]
		[DbField("PickupTime", null, true)]
		public DateTime PickupTime { get; set; }

		[DataMember]
		[Required]
		[DbField("Status", null, true)]
		public bool? Status { get; set; }
	}
}