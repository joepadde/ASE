using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.Entities
{
    public enum ChangeType
    {
        Insert, Update, Delete
    }
    public enum TableName
    {
        Lookup_TableName,
        Lookup_Keyword,
        Lookup_Language,
        Lookup_TimelineEntityType,
        Lookup_PipelineLevelType,
        Lookup_ResponsibilityType,
    }
    public enum EntityName
    {
        TableNameEntity,
        KeywordEntity,
        LanguageEntity,
        TimelineEntityTypeEntity,
        PipelineLevelTypeEntity,
        ResponsibilityTypeEntity,
    }
    public enum ResponsibilityTypes
    {
        Budget,
        Project,
        Executive,
        Branch,
        ProfitLoss,
        Line,
    }
    public enum UserTypes
    {
        Candidate
    }
}
