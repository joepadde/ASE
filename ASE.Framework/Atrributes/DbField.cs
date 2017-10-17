using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.Framework
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class DbField : Attribute
    {
        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private bool _postToDB;
        public bool PostToDB
        {
            get { return _postToDB; }
            set { _postToDB = value; }
        }

        public DbField(string FieldName, string TableName, bool PostToDB)
        {
            this._fieldName = FieldName;
            this._postToDB = PostToDB;
            this._tableName = TableName;
        }
    }
}
