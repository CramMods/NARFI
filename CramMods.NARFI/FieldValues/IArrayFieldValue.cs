namespace CramMods.NARFI.FieldValues
{
    public interface IArrayFieldValue : IFieldValue
    {
        public IReadOnlyList<object> RawValues { get; }
        public object GetRawValue(int index);
    }
}
