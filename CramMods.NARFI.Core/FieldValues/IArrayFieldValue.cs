namespace CramMods.NARFI.FieldValues
{
    public interface IArrayFieldValue : IFieldValue
    {
        public IEnumerable<object> RawValues { get; }
        public object GetRawValue(int index);
    }
}
