namespace CramMods.NARFI.FieldValues
{
    public static class FieldValueConverter
    {

        public static IArrayFieldValue ToArray(IEnumerable<IFieldValue?> fieldValues)
        {
            List<IFieldValue> notNull = fieldValues.Where(fv => fv != null).Select(fv => fv!).ToList();
            
            if (notNull.Count == 0) throw new Exception("No non-null values. Unable to determine inner type");
            Type innerType = notNull[0].StoredType;

            Type returnType = typeof(ArrayFieldValue<>).MakeGenericType(innerType);
            IArrayFieldValue? returnValue = (IArrayFieldValue?)Activator.CreateInstance(returnType, notNull.Select(v => v.RawData).ToList());
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }

        public static IArrayFieldValue ToArray(params IFieldValue?[] values) => ToArray(new List<IFieldValue?>(values));


        public static IGenderedFieldValue ToGendered(IFieldValue? male, IFieldValue? female)
        {
            Type? innerType;
            if (male != null) innerType = male.StoredType;
            else if (female != null) innerType = female.StoredType;
            else throw new Exception("Both values are null. Unable to determin inner type");

            Type returnType = typeof(GenderedFieldValue<>).MakeGenericType(innerType);
            IGenderedFieldValue? returnValue = (IGenderedFieldValue?)Activator.CreateInstance(returnType, male?.RawData, female?.RawData);
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }

        public static IFieldValue? ToSingle(IGenderedFieldValue? gendered, Gender gender)
        {
            if (gendered == null) throw new Exception("FieldValue cannot be null");
            Type innerType = gendered.StoredType;

            Type returnType = typeof(SingleFieldValue<>).MakeGenericType(innerType);
            ISingleFieldValue? returnValue = (ISingleFieldValue?)Activator.CreateInstance(returnType, gendered.GetRawValue(gender));
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }

        public static ISingleFieldValue ToSingle(IArrayFieldValue? array, int index)
        {
            if (array == null) throw new Exception("FieldValue cannot be null");
            Type innerType = array.StoredType;

            Type returnType = typeof(SingleFieldValue<>).MakeGenericType(innerType);
            ISingleFieldValue? returnValue = (ISingleFieldValue?)Activator.CreateInstance(returnType, array.GetRawValue(index));
            if (returnValue == null) throw new Exception("Unable to create instance");

            return returnValue;
        }
    }
}
