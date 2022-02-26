namespace CramMods.NARFI.FieldValues
{
    public abstract class FieldValue : IFieldValue
    {
        protected Type _storedType;
        public Type StoredType => _storedType;

        protected object? _rawData;
        public object? RawData => _rawData;

        public FieldValue(Type storedType) => _storedType = storedType;
    }
}
