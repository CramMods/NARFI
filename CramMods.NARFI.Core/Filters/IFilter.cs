using CramMods.NARFI.FieldValueGetters;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.Filters
{
    public interface IFilter
    {
        public Dictionary<string, object?> Extensions { get; set; }
        public bool Test<T>(T record, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter;
        public IEnumerable<T> Find<T>(IEnumerable<T> records, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter;
    }
}
