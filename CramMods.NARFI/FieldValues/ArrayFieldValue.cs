namespace CramMods.NARFI.FieldValues
{
    public class ArrayFieldValue<T> : FieldValue, IArrayFieldValue
    {
        private List<T> _values
        {
            get => (List<T>)_rawData!;
            set => _rawData = value;
        }

        public IReadOnlyList<T> Values => _values.AsReadOnly();
        public IReadOnlyList<object> RawValues => Values.Select(v => (object)v!).ToList();

        public T GetValue(int index) => _values[index];
        public object GetRawValue(int index) => _values[index]!;

        public ArrayFieldValue() : this(new List<T>()) { }
        public ArrayFieldValue(IEnumerable<object?> rawValues) : this(rawValues.Select(raw => (T?)raw)) { }
        public ArrayFieldValue(IEnumerable<T?> values) : base(typeof(T)) => _values = new(values.Where(v => v != null).Select(v => v!));
    }
}
