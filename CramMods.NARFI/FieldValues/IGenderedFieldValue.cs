﻿namespace CramMods.NARFI.FieldValues
{
    public interface IGenderedFieldValue : IFieldValue
    {
        public object? RawMaleValue { get; }
        public object? RawFemaleValue { get; }

        public object? GetRawValue(Gender gender);
    }
}
