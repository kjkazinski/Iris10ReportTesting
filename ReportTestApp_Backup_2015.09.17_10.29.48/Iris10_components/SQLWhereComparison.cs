using System;
using System.Collections.Generic;
using System.Linq;

namespace IrisWeb
{
    public static class SqlWhereComparison
    {
        readonly static Dictionary<SqlComparer, string> comparerLookup = new Dictionary<SqlComparer, string>()
        {
            { SqlComparer.Equal, "=" },
            { SqlComparer.NotEqual, "<>" },
            { SqlComparer.GreaterThan, ">" },
            { SqlComparer.LessThan, "<" },
            { SqlComparer.GreaterThan | SqlComparer.Equal, ">=" },
            { SqlComparer.LessThan | SqlComparer.Equal, "<=" },
            { SqlComparer.BitwiseAnd, "&" },
            { SqlComparer.BitwiseOr, "|" },
            { SqlComparer.BitwiseExclusiveOr, "^" },
            { SqlComparer.BitwiseAnd | SqlComparer.Equal, "&=" },
            { SqlComparer.BitwiseOr | SqlComparer.Equal, "|=" },
            { SqlComparer.BitwiseExclusiveOr | SqlComparer.Equal, "^=" },
            { SqlComparer.Like, "LIKE" },
            { SqlComparer.NotLike, "NOT LIKE" },
            { SqlComparer.Between, "BETWEEN"},
            { SqlComparer.Outside, "OUTSIDE"},
            { SqlComparer.In, "IN"}
        };

        public enum SqlComparer
        {
            None = 0,
            Equal = 1,
            Like = 65,
            GreaterThan = 2,
            LessThan = 4,
            NotEqual = 6,
            NotLike = 70,
            BitwiseAnd = 8,
            BitwiseOr = 16,
            BitwiseExclusiveOr = 32,
            Partial = 64,
            Between = 100,
            Outside = 150,
            In = 200
        }

        public static string GetSqlComparor(this SqlComparer c)
        {
            string sql = null;
            if (comparerLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        public static SqlComparer ParseComparer(string s)
        {
            SqlComparer comparer = SqlComparer.None;
            foreach (char c in s)
            {
                switch (c)
                {
                    case '>':
                        comparer |= SqlComparer.GreaterThan;
                        break;

                    case '<':
                        comparer |= SqlComparer.LessThan;
                        break;

                    case '=':
                        comparer |= SqlComparer.Equal;
                        break;

                    case '&':
                        comparer |= SqlComparer.BitwiseAnd;
                        break;

                    case '|':
                        comparer |= SqlComparer.BitwiseOr;
                        break;

                    case '^':
                        comparer |= SqlComparer.BitwiseExclusiveOr;
                        break;
                }
            }

            return comparer;
        }
    }
}
