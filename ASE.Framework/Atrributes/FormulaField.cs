using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.Framework
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FormulaField : Attribute
    {
        //Formula must be on this way: ([Field1]*[Field2])/10 
        private string _formula;
        public string Formula
        {
            get { return _formula; }
            set { _formula = value; }
        }

        public FormulaField(string formula)
        {
            this._formula = formula;
        }
    }
}
