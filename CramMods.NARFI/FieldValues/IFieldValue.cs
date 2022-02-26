namespace CramMods.NARFI.FieldValues
{
    public interface IFieldValue
    {
        public object? RawData { get; }
        public Type StoredType { get; }
    }
}
