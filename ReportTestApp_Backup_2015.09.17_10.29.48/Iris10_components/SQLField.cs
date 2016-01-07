using System;
using System.Linq;

namespace IrisWeb
{
    public class SqlField
    {
        public string Name;
        public string Alias;
        public object Value;
        public SqlTable Table;
        public bool CriteriaOnly = false;
        public bool Function = false;
        public bool GroupBy = false;

        public SqlField(string fieldName, string alias, object fieldValue, SqlTable tableObject, bool function, bool groupBy)
        {
            Name = fieldName;
            Alias = alias;
            Value = fieldValue;
            Table = tableObject;
            this.Function = function;
            GroupBy = groupBy;
        }
    }
}
