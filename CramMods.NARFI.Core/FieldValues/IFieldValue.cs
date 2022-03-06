namespace CramMods.NARFI.FieldValues
{
    public interface IFieldValue
    {
        public Type StoredType { get; }
        public object? RawData { get; }

        public bool IsMatch(ComparisonOperator op, object? other);
    }
}
