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
    [DbTable("Lookup_TableName")]
    public class TableNameEntity : LookupEntity
    {

        [DataMember]
        [Required]
        [DbField("IsLookup", null, true)]
        public bool IsLookup { get; set; }


    }
}
