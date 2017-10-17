using System;
using ASE.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ASE.Entities
{
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        [DbField("ID", null, true)]
        public virtual Guid? ID { get; set; }
    }
}
