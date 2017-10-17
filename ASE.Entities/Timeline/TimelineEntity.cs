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
    [DbTable("Timeline")]
    public class TimelineEntity : BaseEntity
    {
        [DataMember]
        [Required]
        [DbField("UserID", null, true)]
        public Guid UserID { get; set; }

        [DataMember]
        [Required]
        [DbField("TypeID", null, true)]
        public Guid TypeID { get; set; }

        [DataMember]
        [Required]
        [DbField("TypePlace", null, true)]
        public virtual string TypePlace { get; set; }

        [DataMember]
        [Required]
        [DbField("TypePosition", null, true)]
        public virtual string TypePosition { get; set; }

        [DataMember]
        [Required]
        [DbField("TypeField", null, true)]
        public virtual string TypeField { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DbField("StartDate", null, true)]
        public DateTime StartDate { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:dd/MM/yyyy}")]
        [DbField("EndDate", null, true)]
        public DateTime? EndDate { get; set; }

    }
}
