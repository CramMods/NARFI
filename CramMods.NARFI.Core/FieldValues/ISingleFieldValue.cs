namespace CramMods.NARFI.FieldValues
{
    public interface ISingleFieldValue : IFieldValue
    {
        public object? RawValue { get; }
    }

}
