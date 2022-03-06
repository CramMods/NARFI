namespace CramMods.NARFI.FieldValues
{
    public class GenderedFieldValue<T> : FieldValue<T>, IGenderedFieldValue
    {
        private Dictionary<Gender, T?> _value 
        { 
            get => (Dictionary<Gender, T?>)_rawData!;
            set => _rawData = value; 
        }

        public Dictionary<Gender, T?> Values => _value;
        public Dictionary<Gender, object?> RawValues => _value.ToDictionary(i => i.Key, i => (object?)i.Value);

        public T? MaleValue => _value[Gender.Male];
        public object? RawMaleValue => _value[Gender.Male];

        public T? FemaleValue => _value[Gender.Female];
        public object? RawFemaleValue => _value[Gender.Female];

        public T? GetValue(Gender gender) => _value[gender];
        public object? GetRawValue(Gender gender) => _value[gender];

        public GenderedFieldValue(T? maleValue, T? femaleValue) : base(new Dictionary<Gender, T?>() { { Gender.Male, maleValue }, { Gender.Female, femaleValue } }) {}
        public GenderedFieldValue(object rawData) : base(rawData) { }

        public override bool IsMatch(ComparisonOperator op, object? other) => FieldValueComparer.Compare(op, this, other);

    }
}
