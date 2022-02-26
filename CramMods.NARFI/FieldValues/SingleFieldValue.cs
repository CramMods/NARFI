namespace CramMods.NARFI.FieldValues
{
    public class SingleFieldValue<T> : FieldValue, ISingleFieldValue
    {
        private T? _value
        {
            get => (T?)_rawData;
            set => _rawData = value;
        }

        public T? Value => _value;
        public object? RawValue => _rawData;

        public SingleFieldValue() : this(default) { }
        public SingleFieldValue(T? value) : base(typeof(T)) => _value = value;
    }
}
