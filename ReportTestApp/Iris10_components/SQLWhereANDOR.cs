using System;
using System.Collections.Generic;
using System.Linq;

namespace IrisWeb
{
    public static class SqlWhereAndorOptions
    {
        readonly static Dictionary<SqlWhereAndor, string> andorLookup = new Dictionary<SqlWhereAndor, string>()
        {
            { SqlWhereAndor.And, "AND" },
            { SqlWhereAndor.OR, "OR" },
        };

        public enum SqlWhereAndor
        {
            And = 0,
            OR = 1,
        }

        public static string GetSqlwhereandor(this SqlWhereAndor c)
        {
            string sql = null;
            if (andorLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }
    }
}
