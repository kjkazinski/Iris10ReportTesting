using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;

using IrisWeb.Code.Data.Filters;
using IrisWeb.Code.Data.Models.System;
using IrisWeb.Code.Extensions;

namespace IrisWeb.Code.Data.Services
{
    public class DatabaseServiceBase<T> : IDisposable
    {
        protected DatabaseModelBindings modelDataBindings;

        public DatabaseServiceBase()
        {
            modelDataBindings = typeof(T).GetDatabaseBindings();
        }
        ~DatabaseServiceBase()
        {
            Dispose(false);
        }

        public IEnumerable<T> Read() { return ReadForFilters(null); }
        public IEnumerable<T> Read(string key) { return ReadForFilters(new ModelKeyFilter(modelDataBindings.TableName, modelDataBindings.KeyFieldName, key)); }
        public IEnumerable<T> ReadForFilters(IDataFilter filters)
        {
            SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Select, modelDataBindings.TableName);
            sqlgen.SelectFromModel<T>();
            sqlgen.DoNotFullyQualifyFields = true;

            if (filters != null)
                filters.Apply(sqlgen);

            return ModelBase.LoadModel<T>(sqlgen);
        }

        public T Create(T model)
        {
            SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Insert, modelDataBindings.TableName);

            Type t = typeof(T);
            PropertyInfo keyProp = t.GetProperty(modelDataBindings.KeyFieldName, BindingFlags.Public | BindingFlags.Instance);

            // Set a unique key for this model
            string newKey = Adocls.GetUniqueKey();
            keyProp.SetValue(model, newKey);

            sqlgen.InsertFromModel<T>(model);
            sqlgen.DoNotFullyQualifyFields = true;

            ModelBase.InsertModel<T>(model, modelDataBindings.DatabaseName, sqlgen);

            return model;
        }

        public void Update(T model)
        {
            SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Update, modelDataBindings.TableName);
            sqlgen.DoNotFullyQualifyFields = true;
            sqlgen.UpdateFromModel<T>(model);

            ModelBase.UpdateModel<T>(model, modelDataBindings.DatabaseName, sqlgen);
        }

        public void Destroy(T model)
        {
            SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Delete, modelDataBindings.TableName);
            sqlgen.DoNotFullyQualifyFields = true;
            sqlgen.DeleteFromModel<T>(model);

            ModelBase.DeleteModel<T>(model, modelDataBindings.DatabaseName, sqlgen);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing) { }
    }
}
