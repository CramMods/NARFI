namespace CramMods.NARFI.FieldValues
{
    public static class FieldValueConverter
    {
        public static IArrayFieldValue? ToArray(IEnumerable<IFieldValue?> fieldValues)
        {
            List<IFieldValue> notNull = fieldValues
                .Where(fv => fv != null)
                .Select(fv => fv!)
                .ToList();

            if (notNull.Count == 0) return null;

            Type innerType = notNull[0].StoredType;
            Type returnType = typeof(ArrayFieldValue<>).MakeGenericType(innerType);

            IArrayFieldValue? returnValue = (IArrayFieldValue?)Activator.CreateInstance(returnType, notNull.Select(fv => fv.RawData).ToList());
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }
        public static IArrayFieldValue? ToArray(params IFieldValue?[] fieldValues) => ToArray(fieldValues.ToList());

        public static ISingleFieldValue? ToSingle(IFieldValue? arrayFieldValue, int index)
        {
            if (arrayFieldValue == null) return null;
            if (!arrayFieldValue.GetType().IsAssignableTo(typeof(IArrayFieldValue))) throw new Exception("Not an ArrayFieldValue");

            object? rawData = ((IArrayFieldValue)arrayFieldValue).GetRawValue(index);

            Type returnType = typeof(SingleFieldValue<>).MakeGenericType(arrayFieldValue.StoredType);
            ISingleFieldValue? returnValue = (ISingleFieldValue?)Activator.CreateInstance(returnType, rawData);
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }

    }
}
