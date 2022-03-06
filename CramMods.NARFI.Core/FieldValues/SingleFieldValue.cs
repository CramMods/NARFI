namespace CramMods.NARFI.FieldValues
{
    public class SingleFieldValue<T> : FieldValue<T>, ISingleFieldValue
    {
        private T? _value { get => (T?)_rawData; set => _rawData = value; }

        public T? Value => _value;
        public object? RawValue => _value;

        public SingleFieldValue(T? value) : base(value) { }
        public SingleFieldValue(object rawData) : base(rawData) {}

        public override bool IsMatch(ComparisonOperator op, object? other) => FieldValueComparer.Compare(op, this, other);
    }
}
