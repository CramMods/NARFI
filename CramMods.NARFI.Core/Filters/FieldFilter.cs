using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.Filters
{
    public class FieldFilter<TVAL> : IFieldFilter
    {
        protected FieldPath _fieldPath;
        public FieldPath Path { get => _fieldPath; set => _fieldPath = value; }

        protected ComparisonOperator _operator;
        public ComparisonOperator Operator { get => _operator; set => _operator = value; }

        private TVAL? _value;
        public TVAL? Value { get => _value; set => _value = value; }
        public object? RawValue { get => _value; set => _value = (TVAL?)value; }

        protected Dictionary<string, object?> _exensions = new();
        public Dictionary<string, object?> Extensions { get => _exensions; set => _exensions = value; }

        public FieldFilter(FieldPath path, ComparisonOperator op, TVAL? value) => (_fieldPath, _operator, _value) = (path, op, value);


        public bool Test<TREC>(TREC record, IFieldValueGetter fieldValueGetter) where TREC : IMajorRecordGetter
        {
            IFieldValue? fieldValue = fieldValueGetter.GetFieldValue(record, _fieldPath.Clone());
            return fieldValue?.IsMatch(_operator, _value) ?? false;
        }

        public IEnumerable<TREC> Find<TREC>(IEnumerable<TREC> records, IFieldValueGetter fieldValueGetter) where TREC : IMajorRecordGetter
        {
            return records
                .ToList()
                .FindAll(r => Test(r, fieldValueGetter));
        }
    }
}
