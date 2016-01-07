using System;
using System.Linq;

namespace IrisWeb
{
    public class SqlTable
    {
        public string Name = "";
        public string JoinType = "";
        public string JoinFieldNameA = "";
        public string JoinFieldNameB = "";
        public string JoiningTable = "";
        public string JoinComparator = "=";
        public bool DoNotBracket = false;
    }
}
