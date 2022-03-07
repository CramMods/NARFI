using CramMods.NARFI.FieldValueGetters;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.Filters
{
    public class GroupFilter : IFilter
    {
        private Dictionary<string, object?> _extensions = new();
        public Dictionary<string, object?> Extensions { get => _extensions; set => _extensions = value; }

        private GroupFilterOperator _operator;
        public GroupFilterOperator Operator { get => _operator; set => _operator = value; }

        private List<IFilter> _filters = new();
        public List<IFilter> Filters { get => _filters; set => _filters = value; }

        public GroupFilter(GroupFilterOperator op, IEnumerable<IFilter> filters) => (_operator, _filters) = (op, filters.ToList());
        public GroupFilter(GroupFilterOperator op, params IFilter[] filters) : this(op, filters.ToList()) { }

        public bool Test<T>(T record, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter
        {
            return _operator switch 
            {
                GroupFilterOperator.OR => _filters.Any(f => f.Test(record, fieldValueGetter)),
                GroupFilterOperator.AND => _filters.All(f => f.Test(record, fieldValueGetter)),
                _ => throw new NotImplementedException(),
            };
        }

        public IEnumerable<T> Find<T>(IEnumerable<T> records, IFieldValueGetter fieldValueGetter) where T : IMajorRecordGetter
        {
            return records
                .ToList()
                .FindAll(r => Test(r, fieldValueGetter));
        }
    }
}
