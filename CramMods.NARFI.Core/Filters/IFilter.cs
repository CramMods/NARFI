using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.Filters
{
    public interface IFilter
    {
        public FieldPath Path { get; set; }
        public ComparisonOperator Operator { get; set; }
        public object? RawValue { get; set; }
        public Dictionary<string, object?> Extensions { get; set; }

        public bool Test<T>(T record, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter;
        public IEnumerable<T> Find<T>(IEnumerable<T> records, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter;
    }
}
