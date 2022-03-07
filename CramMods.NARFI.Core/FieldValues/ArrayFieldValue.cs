namespace CramMods.NARFI.FieldValues
{
    public class ArrayFieldValue<T> : FieldValue<T>, IArrayFieldValue
    {
        private List<T> _value
        {
            get => (List<T>)_rawData!;
            set => _rawData = value;
        }

        public IReadOnlyList<T> Values => _value;
        public IReadOnlyList<object> RawValues => _value.ConvertAll(v => (object)v!);

        public T GetValue(int index) => throw new NotImplementedException();
        public object GetRawValue(int index) => throw new NotImplementedException();

        public ArrayFieldValue(IEnumerable<T> values) : base(new List<T>(values)) {}
        public ArrayFieldValue(params T[] values) : base(new List<T>(values)) {}
        public ArrayFieldValue(object rawData) : base(rawData) {}

        public override bool IsMatch(ComparisonOperator op, object? other) => FieldValueComparer.Compare(op, this, other);
    }
}
