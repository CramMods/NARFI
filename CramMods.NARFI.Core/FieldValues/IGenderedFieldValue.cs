namespace CramMods.NARFI.FieldValues
{
    public interface IGenderedFieldValue : IFieldValue
    {
        public Dictionary<Gender, object?> RawValues { get; }
        public object? RawMaleValue { get; }
        public object? RawFemaleValue { get; }
        public object? GetRawValue(Gender gender);
    }
}
