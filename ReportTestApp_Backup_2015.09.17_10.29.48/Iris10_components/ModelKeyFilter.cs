namespace IrisWeb.Code.Data.Filters
{
    class ModelKeyFilter : IDataFilter
    {
        private string modelTableName;
        private string keyFieldName;
        private string keyValue;

        public ModelKeyFilter(string tableName, string fieldName, string value)
        {
            modelTableName = tableName;
            keyFieldName = fieldName;
            keyValue = value;
        }

        public void Apply(SqlGenerator sqlGen)
        {
            sqlGen.AddWhereParameter(modelTableName, keyFieldName, keyValue, SqlWhereComparison.SqlComparer.Equal);
        }
    }
}
