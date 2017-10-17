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
    [DbTable("Timeline_PipelineLevel")]
    public class PipelineLevelEntity : TimelineExtension
    {
        [DataMember]
        [Required]
        [DbField("LevelID", null, true)]
        public Boolean LevelID { get; set; }
    }
}
