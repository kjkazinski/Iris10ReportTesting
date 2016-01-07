using System;

namespace IrisWeb.Code.Data.Attributes
{
    public sealed class ModelDataBindingsAttribute : Attribute
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string KeyFieldName { get; set; }
    }
}
