namespace CramMods.NARFI.FieldValues
{
    public abstract class FieldValue<T> : IFieldValue
    {
        protected Type _storedType = typeof(T);        
        public Type StoredType => _storedType;

        protected object? _rawData;
        public object? RawData => _rawData;

        public FieldValue(object? rawData) => (_storedType, _rawData) = (typeof(T), rawData);

        public abstract bool IsMatch(ComparisonOperator op, object? other);
    }
}
