using System;
using System.Collections.Generic;
using System.Linq;

namespace IrisWeb
{
    public class SqlWhere
    {
        public SqlField Field = null;
        public object Value1 = null;
        public object Value2 = null;
        public List<object> InList = null;
        public string Comparator = "";
        public string Andor = "AND";

        //Mock SQL initialization variables
        //These are used to declare a WHERE object before the generator has been created.
        public string MockTableName = null;
        public string MockFieldName = null;
        public object MockValue = null;
        public string MockComparator = null;
        public bool MockInitialization = false;

        public List<SqlTable> Tables = new List<SqlTable>();
        public List<SqlField> Fields = new List<SqlField>();

        public SqlWhere(ref List<SqlTable> tables, ref List<SqlField> fields)
        {
            this.Tables = tables;
            this.Fields = fields; 
        }

        public SqlWhere(string tableName, string fieldName, object value, SqlWhereComparison.SqlComparer comparator)
        {
            this.MockTableName = tableName;
            this.MockFieldName = fieldName;
            this.MockValue = value;
            this.MockComparator = SqlWhereComparison.GetSqlComparor(comparator);
            MockInitialization = true;
        }

        public List<SqlWhere> InnerWheres = new List<SqlWhere>();

        public SqlWhere AddWhereParameter(string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, tableName, fieldName, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andor, SqlField field, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(andor, field, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andor, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name == fieldName.ToUpper() && field.Table.Name == tableName.ToUpper())
                {
                    return AddWhereParameter(andor, field, fieldValue, fieldValue2, comparator);
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false);
                    return AddWhereParameter(andor, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }


        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andor, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            SqlWhere newwhere = new SqlWhere(ref Tables, ref Fields);
            newwhere.Andor = SqlWhereAndorOptions.GetSqlwhereandor(andor);
            newwhere.Field = field;
            newwhere.Value1 = fieldValue1;
            newwhere.Value2 = fieldValue2;
            newwhere.Comparator = SqlWhereComparison.GetSqlComparor(comparator);
            InnerWheres.Add(newwhere);
            return newwhere;
        }
    }
}
