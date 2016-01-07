using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using IrisWeb.Code.Data.Models.Database;
using IrisWeb.Code.Data.Models.System;
using IrisWeb.Code.Extensions;

namespace IrisWeb
{
    class ModelBase
    {
        /// <summary>
        /// Uses the SQL Generator to dynamically create a SELECT statement based on 
        /// the properties of the model referenced and returns an array of the
        /// referenced model populated from the API.
        /// </summary>
        public static IEnumerable<T> LoadModel<T>(SqlGenerator gen = null)
        {
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();

            DatabaseModelBindings bindings = t.GetDatabaseBindings();

            if (gen == null)
            {
                gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName);
                gen.SelectFromModel<T>();
            }

            SqlDataReader dr = Adocls.FetchDataReader(gen, bindings.DatabaseName);

            if (dr != null)
            {
                while (dr.Read())
                {
                    T newo = Activator.CreateInstance<T>();

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string name = dr.GetName(i);

                        PropertyInfo pi = t.GetProperty(name);

                        if (dr[i] != System.DBNull.Value)
                        {
                            if (pi != null && pi.CanWrite)
                            {
                                object val = dr[i];
                                Type newT = val.GetType();

                                if (pi.PropertyType.UnderlyingSystemType == typeof(bool))
                                {
                                    if (newT == typeof(bool))
                                        pi.SetValue(newo, val);
                                    else if (newT == typeof(byte))
                                        pi.SetValue(newo, (byte)val == 1);
                                }
                                else if (pi.PropertyType.UnderlyingSystemType == typeof(string))
                                {
                                    // We must excape the # when it is in text
                                    pi.SetValue(newo, val.ToString().Replace("#", "\\#"));
                                }
                                else
                                {
                                    pi.SetValue(newo, val);
                                }

                            }
                        }

                    }
                    o.Add(newo);
                }
            }
            else
            {
                Console.WriteLine();
            }

            return o;
        }

        ///// <summary>
        ///// Uses the SQL Generator to dynamically create a SELECT statement based on 
        ///// the properties of the model referenced and returns an array of the
        ///// referenced model populated from the API.
        ///// </summary>
        //public static IEnumerable<T> LoadModel<T>(SQLWhere OptionalWhere = null)
        //{
        //    Type t = typeof(T);
        //    List<T> o = Activator.CreateInstance<List<T>>();
        //    string TableName = t.Name;

        //    if (TableName.EndsWith("DROPDOWN"))
        //    {
        //        TableName = TableName.Remove(TableName.Length - 8, 8);
        //    }

        //    SQLGenerator Gen = new SQLGenerator(SQLGenerator.SqlTypes.Select, TableName);

        //    if (OptionalWhere != null)
        //    {
        //        Gen.AddWhereParameter(OptionalWhere);
        //    }

        //    foreach (PropertyInfo propertyInfo in t.GetProperties())
        //    {
        //        if (propertyInfo.CanRead && propertyInfo.CanWrite)
        //        {
        //            Gen.AddField(propertyInfo.Name);
        //        }
        //    }

        //    DataTable dt = ADOCLS.FetchDataTable(Gen);

        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {
        //        T newo = Activator.CreateInstance<T>();

        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            string name = dt.Columns[i].ColumnName;

        //            PropertyInfo pi = t.GetProperty(name);

        //            if (dt.Rows[j][i] != System.DBNull.Value)
        //            {
        //                if (pi != null && pi.CanWrite)
        //                {
        //                    object val = dt.Rows[j][i];
        //                    Type newT = val.GetType();

        //                    if (pi.PropertyType.UnderlyingSystemType == typeof(bool))
        //                    {
        //                        if (newT == typeof(bool))
        //                            pi.SetValue(newo, val);
        //                        else if (newT == typeof(byte))
        //                            pi.SetValue(newo, (byte)val == 1);
        //                    }
        //                    else
        //                    {
        //                        pi.SetValue(newo, val);
        //                    }

        //                }
        //            }

        //        }
        //        o.Add(newo);
        //    }

        //    return o;
        //}

        /// <summary>
        /// Uses the SQL Generator to dynamically create a INSERT statement based on 
        /// the properties of the model referenced and returns a string result.
        /// </summary>
        /// <param name="model">The model used to generate the INSERT statement</param>
        public static string InsertModel<T>(T model, string connectionString = "CountyDatabase", SqlGenerator gen = null)
        {
            try
            {
                Type t = model.GetType();
                string tableName = t.Name;

                if (tableName.ToUpper().EndsWith("MODEL"))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }

                if (gen == null)
                {
                    gen = new SqlGenerator(SqlGenerator.SqlTypes.Insert, tableName);
                    gen.InsertFromModel(model);
                }

                return Adocls.ExecuteSql(gen, false, connectionString).ToString();
            }
            catch { return ""; }
        }

        /// <summary>
        /// Uses the SQL Generator to dynamically create a UPDATE statement based on 
        /// the properties of the model referenced and returns a string result.
        /// </summary>
        /// <param name="model">The model used to generate the UPDATE statement</param>
        public static string UpdateModel<T>(T model, string connectionString = "CountyDatabase", SqlGenerator gen = null)
        {
            try
            {
                Type t = model.GetType();

                string tableName = t.Name;

                if (tableName.ToUpper().EndsWith("MODEL"))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }

                if (gen == null)
                {
                    gen = new SqlGenerator(SqlGenerator.SqlTypes.Update, tableName);
                    gen.UpdateFromModel(model);
                }

                return Adocls.ExecuteSql(gen, false, connectionString).ToString();
            }
            catch { return ""; }
        }

        /// <summary>
        /// Uses the SQL Generator to dynamically create a DELETE statement based on 
        /// the properties of the model referenced and returns a string result.
        /// </summary>
        /// <param name="model">The model used to generate the DELETE statement</param>
        public static string DeleteModel<T>(T model, string connectionString = "CountyDatabase", SqlGenerator gen = null)
        {
            try
            {
                Type t = model.GetType();
                string tableName = t.Name;

                if (tableName.ToUpper().EndsWith("MODEL"))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }

                if (gen == null)
                {
                    gen = new SqlGenerator(SqlGenerator.SqlTypes.Delete, tableName);
                    gen.DeleteFromModel(model);                
                }
                

                return Adocls.ExecuteSql(gen, false, connectionString).ToString();
            }
            catch { return ""; }
        }
    }
}
