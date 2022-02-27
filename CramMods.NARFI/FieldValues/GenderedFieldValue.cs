namespace CramMods.NARFI.FieldValues
{
    public class GenderedFieldValue<T> : FieldValue, IGenderedFieldValue
    {
        private struct GenderedData
        {
            public T? MaleValue;
            public T? FemaleValue;
        }

        private GenderedData _data
        {
            get => (GenderedData)_rawData!;
            set => _rawData = value;
        }

        public T? MaleValue => _data.MaleValue;
        public object? RawMaleValue => MaleValue;

        public T? FemaleValue => _data.FemaleValue;
        public object? RawFemaleValue => FemaleValue;

        public object? GetRawValue(Gender gender) => (gender == Gender.Male) ? MaleValue : FemaleValue;

        public GenderedFieldValue() : this(default, default) { }
        public GenderedFieldValue(T? maleValue, T? femaleValue) : base(typeof(T))
        {
            _data = new()
            {
                MaleValue = maleValue,
                FemaleValue = femaleValue
            };
        }
    }
}
