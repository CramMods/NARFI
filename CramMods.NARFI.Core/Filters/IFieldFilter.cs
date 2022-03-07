using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;

namespace CramMods.NARFI.Filters
{
    public interface IFieldFilter : IFilter
    {
        public FieldPath Path { get; set; }
        public ComparisonOperator Operator { get; set; }
        public object? RawValue { get; set; }
    }
}
