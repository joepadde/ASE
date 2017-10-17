using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.Framework
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DbTable : Attribute
    {
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public DbTable(string TableName)
        {
            this._tableName = TableName;
        }
    }
}
