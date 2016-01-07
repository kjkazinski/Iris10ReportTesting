namespace IrisWeb.Code.Data.Filters
{
    public interface IDataFilter
    {
        void Apply(SqlGenerator sqlGen);
    }
}
