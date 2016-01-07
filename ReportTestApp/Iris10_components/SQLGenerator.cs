using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace IrisWeb
{
    public class SqlGenerator
    {
        public string SqlTypeString = "";
        public string BaseTable = "";
        public bool Distinct = false;
        public List<SqlField> Fields = new List<SqlField>();
        public List<SqlTable> Tables = new List<SqlTable>();
        public List<SqlWhere> Wheres = new List<SqlWhere>();
        public List<SqlOrder> Orders = new List<SqlOrder>();

        public bool DoNotFullyQualifyFields = false;

        public bool IsAutonumberKey = false;

        /// <summary>
        /// A list of SQL Parameters to be used in the execution of a SQL Command
        /// </summary>
        public List<SqlParameter> SqlVariables = new List<SqlParameter>();

        /// <summary>
        /// Limits the return size of a SELECT statement (TOP 10000 default)
        /// </summary>
        public int SelectStatementLimit = 10000;

        /// <summary>
        /// Initializes a basic SQLGenerator instance.
        /// </summary>
        public SqlGenerator(SqlTypes sqlType, string baseTableName)
        {
            BuildSQLGen(sqlType, baseTableName);
        }

        public SqlGenerator(SqlTypes sqlType, string baseTableName, SqlTable[] optionalTables = null)
        {
            BuildSQLGen(sqlType, baseTableName, optionalTables);
        }

        public SqlGenerator(SqlTypes sqlType, string baseTableName, bool distinct)
        {
            Distinct = distinct;
            BuildSQLGen(sqlType, baseTableName, null, distinct);
        }

        private void BuildSQLGen(SqlTypes sqlType, string baseTableName, SqlTable[] optionalTables = null, bool distinct = false)
        {
            SqlTypeString = GetSqlType(sqlType);
            SqlTable basetable = new SqlTable();
            basetable.Name = baseTableName.ToUpper();
            BaseTable = baseTableName.ToUpper();
            if (BaseTable.Contains(".")) basetable.DoNotBracket = true;
            Tables.Add(basetable);
            if (optionalTables != null)
            {
                Tables.AddRange(optionalTables);
            }
        }

        #region ADO Execution Methods

        public object Execute(string connectionName = "CountyDatabase")
        {
            try
            {
                if (SqlTypeString == "INSERT" || SqlTypeString == "UPDATE" || SqlString == "DELETE")
                {
                    return Adocls.ExecuteSql(this, true, connectionName);
                }
                else
                {
                    return Adocls.FetchDataTable(this, connectionName);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SqlDataReader GetDataReader(string connectionName = "CountyDatabase")
        {
            return Adocls.FetchDataReader(this, connectionName);
        }

        #endregion

        #region SQL Generation from Model Methods

        public void SelectFromModel<T>()
        {
            Type t = typeof(T);

            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var orderbyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(OrderByFieldAttribute)) as OrderByFieldAttribute;
                        AddField(propertyInfo.Name);
                        if (orderbyattribute != null)
                        {
                            if (orderbyattribute.OrderByAssending) AddOrderBy(propertyInfo.Name, BaseTable, SqlOrders.Ascending, orderbyattribute.Index);
                            else AddOrderBy(propertyInfo.Name, BaseTable, SqlOrders.Descending, orderbyattribute.Index);
                        }

                    }
                }
            }
        }

        public void InsertFromModel<T>(T model)
        {
            Type t = typeof(T);

            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var isReadOnly = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        var isAutonumberKey = Attribute.GetCustomAttribute(propertyInfo, typeof(IsAutoNumberAttribute)) as IsAutoNumberAttribute;
                        object val = propertyInfo.GetValue(model);
                        if (isReadOnly == null && isAutonumberKey == null)
                        {
                            AddField(propertyInfo.Name, BaseTable, val);
                        }
                        else if (isAutonumberKey != null)
                        {
                            this.IsAutonumberKey = true;
                        }
                    }
                }
            }
        }

        public void UpdateFromModel<T>(T model)
        {
            Type t = model.GetType();
            string tableName;

            if (BaseTable == null)
            {
                tableName = t.Name;

                if (tableName.ToUpper().EndsWith("MODEL"))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }
            }
            else
            {
                tableName = BaseTable;
            }

            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var keyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute)) as KeyAttribute;
                        var readonlyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        object val = propertyInfo.GetValue(model);
                        if (readonlyattribute == null)
                        {
                            if (keyattribute != null)
                            {
                                AddWhereParameter(tableName, propertyInfo.Name, val, SqlWhereComparison.SqlComparer.Equal);
                            }
                            else
                            {
                                AddField(propertyInfo.Name, BaseTable, val);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteFromModel<T>(T model)
        {
            Type t = typeof(T);

            foreach (PropertyInfo propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var keyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute)) as KeyAttribute;
                    var readonlyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                    object val = propertyInfo.GetValue(model);
                    if (readonlyattribute == null)
                    {
                        if (keyattribute != null)
                        {
                            AddWhereParameter(BaseTable, propertyInfo.Name, val, SqlWhereComparison.SqlComparer.Equal);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Field Methods

        public void AddSubQueryField(string subQuery, string alias)
        {
            AddField(subQuery, alias, BaseTable, null, true);
        }

        public void AddField(string fieldName)
        {
            if (fieldName.Contains(" ")) AddField(fieldName, BaseTable, null, true);
            else AddField(fieldName, BaseTable, null);
        }

        public void AddField(string fieldName, string tableName)
        {
            AddField(fieldName, tableName, null);
        }

        public void AddField(string[] fields, string tableName)
        {
            foreach (string field in fields)
            {
                AddField(field, tableName, null);
            }
        }

        public void AddField(string fieldName, string tableName, object valueAssigned)
        {
            AddField(fieldName, null, tableName, valueAssigned, false);
        }

        public void AddField(string fieldName, string tableName, bool groupby)
        {
            AddField(fieldName, null, tableName, null, false, groupby);
        }

        public void AddField(string fieldName, string alias, string tableName, object valueAssigned, bool isFunction = false, bool groupBy = false)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name == fieldName && field.Table.Name == tableName)
                {
                    return;
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name.ToUpper() == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName, alias, valueAssigned, table, isFunction, groupBy);
                    Fields.Add(newfield);
                }
            }
        }

        #endregion

        #region Table Methods

        /// <summary>
        /// Adds a joined table to the SQL with joining this table to the base table
        /// on a common field name. BaseTable.FieldA => NewTable.FieldA
        /// </summary>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        /// <param name="joinType">Join method (LEFT, RIGHT, INNER).</param>
        /// <param name="joinFieldName">Joining field name in both tables.</param>
        public void AddTable(string tableName, SqlJoins joinType, string joinFieldName, bool doNotBracket = false)
        {
            AddTable(tableName, joinType, joinFieldName, BaseTable, joinFieldName, doNotBracket);
        }

        /// <summary>
        /// Adds a joined table to the SQL with joining this table to the base table
        /// on a common field name. BaseTable.FieldA => NewTable.FieldA
        /// </summary>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        /// <param name="joinType">Join method (LEFT, RIGHT, INNER).</param>
        /// <param name="joinFieldNameA">Joining field name in the new table.</param>
        /// <param name="joiningTableName">Existing table to be joined to.</param>
        /// <param name="joinFieldNameB">Joining field name in the existing table.</param>
        public void AddTable(string tableName, SqlJoins joinType, string joinFieldNameA, string joiningTableName, string joinFieldNameB, bool doNotBracket = false)
        {
            foreach (SqlTable table in Tables)
            {
                if (table.Name == tableName)
                {
                    return;
                }
            }

            SqlTable newtable = new SqlTable();
            newtable.Name = tableName;
            newtable.JoinType = GetSqlJoin(joinType);
            newtable.JoinFieldNameA = joinFieldNameA;
            newtable.JoinFieldNameB = joinFieldNameB;
            newtable.JoiningTable = joiningTableName;
            newtable.DoNotBracket = doNotBracket;
            Tables.Add(newtable);
        }

        #endregion

        #region Where Methods

        public SqlWhere AddSubWhere(string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddSubWhere(SqlWhereAndorOptions.SqlWhereAndor.And, tableName, fieldName, fieldValue, comparator);
        }

        public SqlWhere AddSubWhere(SqlWhereAndorOptions.SqlWhereAndor andOr, string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddSubWhere(andOr, tableName, fieldName, fieldValue, null, comparator);
        }

        public SqlWhere AddSubWhere(SqlWhereAndorOptions.SqlWhereAndor andOr, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name == fieldName.ToUpper() && field.Table.Name == tableName.ToUpper())
                {
                    return AddSubWhere(andOr, field, fieldValue, fieldValue2, comparator);
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false);
                    return AddSubWhere(andOr, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }

        public SqlWhere AddSubWhere(SqlWhereAndorOptions.SqlWhereAndor andOr, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            SqlWhere newwhere = new SqlWhere(ref Tables, ref Fields);
            newwhere.Andor = SqlWhereAndorOptions.GetSqlwhereandor(andOr);
            newwhere.Field = field;
            newwhere.Value1 = fieldValue1;
            newwhere.Value2 = fieldValue2;
            newwhere.Comparator = SqlWhereComparison.GetSqlComparor(comparator);
            if (newwhere.Comparator == "<>" && fieldValue1 == null)
                newwhere.Comparator = "IS NOT";
            Wheres[Wheres.Count - 1].InnerWheres.Add(newwhere);
            return newwhere;
        }


        public void AddWhereParameter(SqlWhere whereObject)
        {
            if (whereObject.MockInitialization)
            {
                AddWhereParameter(whereObject.MockTableName, whereObject.MockFieldName, whereObject.MockValue, SqlWhereComparison.ParseComparer(whereObject.MockComparator));
            }
        }

        public SqlWhere AddWhereParameter(string fieldName, List<object> inValues)
        {
            return AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, fieldName, BaseTable, inValues);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, string fieldName, List<object> inValues)
        {
            return AddWhereParameter(andOR, fieldName, BaseTable, inValues);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, string fieldName, string tableName, List<object> inValues)
        {
            SqlField field = FindOrGenerateField(fieldName, BaseTable);
            SqlWhere newwhere = new SqlWhere(ref Tables, ref Fields);
            newwhere.Andor = SqlWhereAndorOptions.GetSqlwhereandor(andOR);
            newwhere.Field = field;

            newwhere.InList = inValues;

            newwhere.Comparator = SqlWhereComparison.GetSqlComparor(SqlWhereComparison.SqlComparer.In);

            Wheres.Add(newwhere);
            return newwhere;
        }

        public SqlWhere AddWhereParameter(string function, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, function, fieldValue, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, string function, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, function, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, string function, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            foreach (SqlTable table in Tables)
            {
                if (table.Name.ToUpper() == BaseTable.ToUpper())
                {
                    SqlField newfield = new SqlField(function.ToUpper(), null, null, table, true, false);
                    return AddWhereParameter(andOR, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }

        public SqlWhere AddWhereParameter(string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, tableName, fieldName, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, SqlField field, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(andOR, field, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name.ToUpper() == fieldName.ToUpper() && field.Table.Name.ToUpper() == tableName.ToUpper())
                {
                    return AddWhereParameter(andOR, field, fieldValue, fieldValue2, comparator);
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name.ToUpper() == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false);
                    return AddWhereParameter(andOR, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }

        public SqlWhere AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor andOR, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            SqlWhere newwhere = new SqlWhere(ref Tables, ref Fields);
            newwhere.Andor = SqlWhereAndorOptions.GetSqlwhereandor(andOR);
            newwhere.Field = field;
            newwhere.Value1 = fieldValue1;
            newwhere.Value2 = fieldValue2;

            newwhere.Comparator = SqlWhereComparison.GetSqlComparor(comparator);

            if (newwhere.Comparator == "<>" && fieldValue1 == null)
                newwhere.Comparator = "IS NOT";

            Wheres.Add(newwhere);
            return newwhere;
        }


        public SqlField FindOrGenerateField(string fieldName, string tableName)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name.ToUpper() == fieldName.ToUpper() && field.Table.Name.ToUpper() == tableName.ToUpper())
                {
                    return field;
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name.ToUpper() == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false);
                    return newfield;
                }
            }

            return null;
        }

        #endregion

        #region Order Methods

        /// <summary>
        /// Adds an ORDER BY statement by for the specified field
        /// defaulted to ascending order and for the Base Table.
        /// </summary>
        /// <param name="fieldName">The Base Table field to be used.</param>
        public SqlOrder AddOrderBy(string fieldName)
        {
            return AddOrderBy(fieldName, BaseTable, SqlOrders.Ascending);
        }

        /// <summary>
        /// Adds an ORDER BY statement by for the specified field
        /// defaulted to ascending order.
        /// </summary>
        /// <param name="fieldName">The table field to be used in the SQL.</param>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        public SqlOrder AddOrderBy(string fieldName, string tableName)
        {
            return AddOrderBy(fieldName, tableName, SqlOrders.Ascending);
        }

        public SqlOrder AddOrderBy(string fieldName, string tableName, SqlOrders direction, int index = -1)
        {
            try
            {
                if (tableName.ToUpper() == BaseTable.ToUpper() && Fields.Count == 0)
                {
                    SqlField field = new SqlField(fieldName, null, null, Tables[0], false, false);
                    SqlOrder order = new SqlOrder(field, GetSqlOrder(direction));
                    if (index >= 0) Orders.Insert(index, order);
                    else Orders.Add(order);

                    return order;
                }

                foreach (SqlOrder order in Orders)
                {
                    if (order.Field.Name == fieldName && order.Field.Table.Name == tableName)
                    {
                        return order;
                    }
                }

                foreach (SqlField field in Fields)
                {
                    if (field.Name.ToUpper() == fieldName.ToUpper() && field.Table.Name.ToUpper() == tableName.ToUpper())
                    {
                        SqlOrder order = new SqlOrder(field, GetSqlOrder(direction));
                        if (index >= 0) Orders.Insert(index, order);
                        else Orders.Add(order);

                        return order;
                    }
                }

                return null;
            }
            catch
            { return null; }
        }

        #endregion

        #region SQL Generation Methods

        public string SqlString
        {
            get
            {
                SqlVariables.Clear();
                string sql = "";

                if (SqlTypeString == "SELECT")
                {
                    sql += SqlTypeString.Trim();

                    if (Distinct)
                        sql += " DISTINCT";

                    sql += " TOP " + SelectStatementLimit + " ";
                }
                else if (SqlTypeString == "UPDATE")
                {
                    if (BaseTable.Contains("[") || BaseTable.Contains("."))
                        sql += SqlTypeString.Trim() + " " + BaseTable + " SET ";
                    else
                        sql += SqlTypeString.Trim() + " [" + BaseTable + "] SET ";
                }
                else if (SqlTypeString == "INSERT")
                {
                    if (BaseTable.Contains("[") || BaseTable.Contains("."))
                        sql += SqlTypeString.Trim() + " INTO " + BaseTable + " ";
                    else
                        sql += SqlTypeString.Trim() + " INTO [" + BaseTable + "] ";
                }
                else if (SqlTypeString == "DELETE")
                {
                    if (BaseTable.Contains("[") || BaseTable.Contains("."))
                        sql += SqlTypeString.Trim() + " FROM " + BaseTable + " ";
                    else
                        sql += SqlTypeString.Trim() + " FROM [" + BaseTable + "] ";
                }
                else if (SqlTypeString == "SPROC")
                {
                    sql += BaseTable + " ";
                }

                SqlVariables.Clear();

                switch (SqlTypeString)
                {
                    case "UPDATE":
                        sql += BuildFieldsUpdate();
                        sql += BuildJoinStatement();
                        sql += BuildWhereStatement(Wheres);
                        break;
                    case "INSERT":
                        sql += BuildFieldsInsert();
                        sql += BuildWhereStatement(Wheres);
                        if (IsAutonumberKey) sql += "; SELECT SCOPE_IDENTITY()";
                        break;
                    case "SELECT":
                        sql += BuildFieldsSelect();
                        sql += BuildJoinStatement();
                        sql += BuildWhereStatement(Wheres);
                        sql += BuildGroupByStatement();
                        sql += BuildOrderStatement();
                        break;
                    case "DELETE":
                        sql += BuildWhereStatement(Wheres);
                        break;
                    case "SPROC":
                        sql += BuildFieldsSproc();
                        break;
                }

                return sql;
            }
        }

        public string SqlDebug
        {
            get
            {
                string sql = SqlString;

                foreach (SqlParameter var in SqlVariables)
                {
                    if (var.Value != null)
                    {
                        sql = sql.Replace(var.ParameterName, WrapSqlValues(var.Value));
                    }
                    else
                    {
                        sql = sql.Replace(var.ParameterName, "null");
                    }

                }

                return sql;
            }
        }

        public string BuildFieldsSelect()
        {
            string fieldStructure = "";

            if (Fields.Count > 0)
            {
                foreach (SqlField field in Fields)
                {
                    if (fieldStructure.Length > 0)
                    {
                        fieldStructure += ", ";
                    }

                    if (field.Function || DoNotFullyQualifyFields) fieldStructure += field.Name;
                    else if (field.Table.DoNotBracket) fieldStructure += field.Table.Name+ "." + field.Name;
                    else fieldStructure += "[" + field.Table.Name + "]." + field.Name;

                    if (field.Alias != null) fieldStructure += " AS " + field.Alias;
                }
            }
            else
            {
                fieldStructure = "[" + BaseTable + "].*";
            }

            return fieldStructure;
        }

        public string BuildFieldsUpdate()
        {
            try
            {
                string fieldStructure = "";

                foreach (SqlField field in Fields)
                {
                    SqlParameter newvar = new SqlParameter();
                    string variableName = Guid.NewGuid().ToString("N");

                    if (fieldStructure.Length > 0)
                    {
                        fieldStructure += ", ";
                    }
                    fieldStructure += "[" + field.Name + "] = @" + variableName;
                    newvar.ParameterName = "@" + variableName;
                    newvar.Value = field.Value ?? DBNull.Value;
                    SqlVariables.Add(newvar);
                }

                return fieldStructure;
            }
            catch (Exception) { return ""; }
        }

        public string BuildFieldsInsert()
        {
            string fieldStructure = "";
            string valueStructure = "";

            foreach (SqlField field in Fields)
            {
                SqlParameter newvar = new SqlParameter();
                string variableName = Guid.NewGuid().ToString("N");

                if (fieldStructure.Length > 0)
                {
                    fieldStructure += ", ";
                    valueStructure += ", ";
                }
                fieldStructure += field.Name;
                valueStructure += "@" + variableName;
                newvar.ParameterName = "@" + variableName;
                newvar.Value = DBNull.Value;
                if (field.Value != null)
                {
                    if (field.Value.GetType() == typeof(bool))
                    {
                        if (Convert.ToBoolean(field.Value))
                        { field.Value = 1; }
                        else { field.Value = 0; }
                        newvar.Value = field.Value ?? DBNull.Value;
                    }
                    else { newvar.Value = field.Value ?? DBNull.Value; }
                }

                SqlVariables.Add(newvar);
            }

            return "(" + fieldStructure + ") VALUES (" + valueStructure + ")";
        }

        public string BuildFieldsSproc()
        {
            string fieldStructure = "";

            foreach (SqlField field in Fields)
            {
                SqlParameter newvar = new SqlParameter();
                string variableName = Guid.NewGuid().ToString("N");

                if (fieldStructure.Length > 0)
                {
                    fieldStructure += ", ";
                }
                fieldStructure += "@SPROC" + variableName;
                newvar.ParameterName = "@SPROC" + variableName;
                newvar.Value = field.Value;
                SqlVariables.Add(newvar);
            }

            return fieldStructure;
        }

        public string BuildJoinStatement()
        {
            string joinStructure = "";

            if (BaseTable.Contains("."))
                joinStructure += " FROM " + BaseTable;
            else
                joinStructure += " FROM [" + BaseTable + "]";

            foreach (SqlTable table in Tables)
            {
                if (table.JoinType.Length > 0)
                {
                    if (table.DoNotBracket)
                        joinStructure += " " + table.JoinType + " JOIN " + table.Name + " ON " + table.Name + ".[" + table.JoinFieldNameA + "] " + table.JoinComparator + " " + table.JoiningTable + ".[" + table.JoinFieldNameB + "]";
                    else
                        joinStructure += " " + table.JoinType + " JOIN [" + table.Name + "] ON [" + table.Name + "].[" + table.JoinFieldNameA + "] " + table.JoinComparator + " [" + table.JoiningTable + "].[" + table.JoinFieldNameB + "]";
                }
            }

            return joinStructure;
        }

        public string BuildOrderStatement()
        {
            string ordersString = "";

            foreach (SqlOrder order in Orders)
            {
                if (ordersString.Length > 0) ordersString += ", ";

                ordersString += "[" + order.Field.Table.Name + "].[" + order.Field.Name + "] " + order.Direction;
            }

            if (ordersString.Length > 0)
            {
                return " ORDER BY " + ordersString + " ";
            }
            else
            {
                return "";
            }
        }

        public string BuildGroupByStatement()
        {
            string groupByStatment = "";

            foreach (SqlField field in Fields)
            {
                if (field.GroupBy)
                {
                    if (groupByStatment.Length > 0) groupByStatment += ", ";
                    groupByStatment += "[" + field.Table.Name + "].[" + field.Name + "]";
                }
            }

            if (groupByStatment.Length > 0) return " GROUP BY " + groupByStatment; else return "";
        }

        public string BuildWhereStatement(List<SqlWhere> whereList, bool isSubWhere = false)
        {
            string subWhereAndOr = "";

            if (whereList.Count > 0)
            {
                string whereStructure = "";

                foreach (SqlWhere where in whereList)
                {
                    if (whereStructure.Length > 0)
                    {
                        whereStructure += " " + where.Andor + " ";
                    }
                    else
                    {
                        subWhereAndOr = where.Andor;
                    }

                    if (where.Field.Function || DoNotFullyQualifyFields)
                        whereStructure += where.Field.Name + " ";
                    else if (where.Field.Table.DoNotBracket)
                        whereStructure += where.Field.Table.Name + ".[" + where.Field.Name + "] ";
                    else
                        whereStructure += "[" + where.Field.Table.Name + "].[" + where.Field.Name + "] ";

                    whereStructure += where.Comparator;

                    SqlParameter newvar = null;

                    if (where.Value1 != null && where.Value1.ToString().Contains("("))
                    {
                        switch (where.Comparator)
                        {
                            case "LIKE":
                                whereStructure += " " + where.Value1;
                                break;
                            case "BETWEEN":
                                whereStructure += " " + where.Value1 + " AND " + where.Value2;
                                break;
                            case "IS NOT":
                                whereStructure += " NULL";
                                break;
                            default:
                                whereStructure += " " + where.Value1;
                                break;
                        }
                    }
                    else
                    {
                        string whereGuid = Guid.NewGuid().ToString("N");
                        newvar = new SqlParameter();

                        switch (where.Comparator)
                        {
                            case "LIKE":
                                whereStructure += " @" + whereGuid;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;
                            case "BETWEEN":
                                SqlParameter newvarbeween = new SqlParameter();
                                string whereGuidbetween = Guid.NewGuid().ToString("N");
                                whereStructure += " @" + whereGuid + " AND @" + whereGuidbetween;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                newvarbeween.ParameterName = "@" + whereGuidbetween;
                                newvarbeween.Value = where.Value2;
                                SqlVariables.Add(newvarbeween);
                                break;
                            case "IS NOT":
                                whereStructure += " NULL";
                                newvar = null;
                                break;
                            case "IN":
                                string inVars = "";
                                whereStructure += " (";
                                foreach (object inItem in where.InList)
                                {
                                    string newinwhereGuid = Guid.NewGuid().ToString("N");
                                    SqlParameter newinvar = new SqlParameter();

                                    if (inVars.Length > 0) inVars += ", ";

                                    newinvar.ParameterName = "@" + newinwhereGuid;
                                    newinvar.Value = inItem;

                                    SqlVariables.Add(newinvar);

                                    inVars += "@" + newinwhereGuid;
                                }
                                whereStructure += inVars + ")";
                                newvar = null;
                                break;
                            default:
                                whereStructure += " @" + whereGuid;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;
                        }
                    }

                    if (newvar != null)
                        SqlVariables.Add(newvar);

                    if (where.InnerWheres.Count > 0)
                    {
                        whereStructure += BuildWhereStatement(where.InnerWheres, true);
                    }
                }

                if (!isSubWhere)
                    whereStructure = " WHERE " + whereStructure;
                else
                    whereStructure = " " + subWhereAndOr + " (" + whereStructure + ")";

                return whereStructure;
            }
            else
            {
                return "";
            }
        }

        public string WrapSqlValues(object strValue)
        {
            if (IsNumeric(strValue))
            {
                if (strValue.ToString().Length > 0)
                {
                    return strValue.ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "'" + strValue.ToString().Replace("'", "''") + "'";
            }
        }

        public bool IsNumeric(object value)
        {
            return value is sbyte
            || value is byte
            || value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
            || value is float
            || value is double
            || value is decimal;
        }

        #endregion

        #region SQL Types Declarations

        public enum SqlTypes
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4,
            Sproc = 5,
        }

        readonly static Dictionary<SqlTypes, string> sqlTypeLookup = new Dictionary<SqlTypes, string>()
        {
            { SqlTypes.Select, "SELECT" },
            { SqlTypes.Update, "UPDATE" },
            { SqlTypes.Insert, "INSERT" },
            { SqlTypes.Delete, "DELETE" },
            { SqlTypes.Sproc, "SPROC" }
        };

        public string GetSqlType(SqlTypes c)
        {
            string sql = null;
            if (sqlTypeLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        #endregion

        #region SQL Joins Declarations

        public enum SqlJoins
        {
            Inner = 1,
            Outter = 2,
            Left = 3,
            Right = 4
        }

        readonly static Dictionary<SqlJoins, string> sqlJoinsLookup = new Dictionary<SqlJoins, string>()
        {
            { SqlJoins.Inner, "INNER" },
            { SqlJoins.Outter, "OUTTER" },
            { SqlJoins.Left, "LEFT" },
            { SqlJoins.Right, "RIGHT" },
        };

        public string GetSqlJoin(SqlJoins c)
        {
            string sql = null;
            if (sqlJoinsLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        #endregion

        #region SQL Order Declarations

        public enum SqlOrders
        {
            Ascending = 1,
            Descending = 2
        }

        readonly static Dictionary<SqlOrders, string> sqlOrderLookup = new Dictionary<SqlOrders, string>()
            {
                { SqlOrders.Ascending, "ASC"},
                {SqlOrders.Descending, "DESC"}
            };

        public string GetSqlOrder(SqlOrders c)
        {
            string sql = null;
            if (sqlOrderLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        public class SqlOrder
        {
            public SqlField Field;
            public string Direction = "Asc";
            public int Index = -1;

            public SqlOrder(SqlField field, string direction, int index = -1)
            {
                this.Field = field;
                this.Direction = direction;
                this.Index = index;
            }
        }

        #endregion

        #region SQL GroupBy Declarations




        #endregion
    }
}
