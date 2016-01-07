using System;

namespace IrisWeb.Code.Data.Attributes
{
    public enum AggregateMode
    {
        None,
        Sum,
        Average,
        Min,
        Max
    }

    public sealed class IrisGridColumnAttribute : Attribute
    {
        public static readonly IrisGridColumnAttribute Default = new IrisGridColumnAttribute();

        public int Width { get; set; }
        public string Format { get; set; }
        public AggregateMode Aggregate { get; set; }

        public IrisGridColumnAttribute()
        {
            Width = 120;
            Aggregate = AggregateMode.None;
        }
    }
}
