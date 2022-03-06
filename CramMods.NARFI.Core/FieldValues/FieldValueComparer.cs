namespace CramMods.NARFI.FieldValues
{
    public static class FieldValueComparer
    {
        public static bool Compare(ComparisonOperator op, IFieldValue? fieldValue, object? value)
        {
            if (fieldValue == null) return false;
            if (value == null) return false;

            if (fieldValue.GetType().IsAssignableTo(typeof(ISingleFieldValue))) return Compare(op, (ISingleFieldValue)fieldValue, value);
            if (fieldValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue))) throw new NotImplementedException();
            if (fieldValue.GetType().IsAssignableTo(typeof(IArrayFieldValue))) return Compare(op, (IArrayFieldValue)fieldValue, value);

            throw new NotImplementedException();
        }

        public static bool Compare(ComparisonOperator op, ISingleFieldValue? fieldValue, object? value)
        {
            if (fieldValue == null) return false;
            if (value == null) return false;
            
            return Compare(op, fieldValue.RawValue, value);
        }

        public static bool Compare(ComparisonOperator op, IArrayFieldValue? fieldValue, object? value)
        {
            if (fieldValue == null) return false;
            if (value == null) return false;

            return fieldValue.RawValues.Any(v => Compare(op, v, value));
        }

        public static bool Compare(ComparisonOperator op, object? actualValue, object? compareValue)
        {
            if (actualValue == null) return false;
            if (compareValue == null) return false;

            Type actualType = actualValue.GetType();
            Type compareType = compareValue.GetType();

            if (actualType.IsAssignableTo(typeof(string)))
            {
                if (!compareType.IsAssignableTo(typeof(string))) throw new Exception("FieldValue is a string, but comparison is not");
                string actualString = (string)actualValue;
                string compareString = (string)compareValue;

                return op switch 
                {
                    ComparisonOperator.Equal => actualString.Equals(compareString, StringComparison.InvariantCultureIgnoreCase),
                    ComparisonOperator.NotEqual => !actualString.Equals(compareString, StringComparison.InvariantCultureIgnoreCase),
                    ComparisonOperator.Contain => actualString.Contains(compareString, StringComparison.InvariantCultureIgnoreCase),
                    ComparisonOperator.NotContain => !actualString.Contains(compareString, StringComparison.InvariantCultureIgnoreCase),
                    _ => throw new NotImplementedException($"Operator {op} is not implemented for string")
                };
            }

            if (actualType.IsAssignableTo(typeof(int)))
            {
                int actualInt = (int)actualValue;
                int compareInt = 0;

                if (compareType.IsAssignableTo(typeof(int))) compareInt = (int)compareValue;
                else if (compareType.IsAssignableTo(typeof(string)))
                {
                    string tempString = (string)compareValue;
                    if (!int.TryParse(tempString, out compareInt)) throw new Exception("Unable to convert compare value to int");
                }
                else throw new NotImplementedException($"Not sure how to compare int field to {compareType.Name}");

                return op switch 
                {
                    ComparisonOperator.EQ => actualInt == compareInt,
                    ComparisonOperator.NE => actualInt != compareInt,
                    ComparisonOperator.GT => actualInt > compareInt,
                    ComparisonOperator.LT => actualInt < compareInt,
                    ComparisonOperator.GE => actualInt >= compareInt,
                    ComparisonOperator.LE => actualInt <= compareInt,
                    _ => throw new NotImplementedException($"Operator {op} is not implemented for int")
                };
            }

            if (actualType.IsAssignableTo(typeof(float)))
            {
                float actualFloat = (float)actualValue;
                float compareFloat = 0.0F;

                if (compareType.IsAssignableTo(typeof(float))) compareFloat = (float)compareValue;
                else if (compareType.IsAssignableTo(typeof(string)))
                {
                    string tempString = (string)compareValue;
                    if (!float.TryParse(tempString, out compareFloat)) throw new Exception("Unable to convert compare value to float");
                }
                else throw new NotImplementedException($"Not sure how to compare float field to {compareType.Name}");

                return op switch
                {
                    ComparisonOperator.EQ => actualFloat == compareFloat,
                    ComparisonOperator.NE => actualFloat != compareFloat,
                    ComparisonOperator.GT => actualFloat > compareFloat,
                    ComparisonOperator.LT => actualFloat < compareFloat,
                    ComparisonOperator.GE => actualFloat >= compareFloat,
                    ComparisonOperator.LE => actualFloat <= compareFloat,
                    _ => throw new NotImplementedException($"Operator {op} is not implemented for float")
                };
            }

            if (actualType.IsAssignableTo(typeof(bool)))
            {
                bool actualBool = (bool)actualValue;
                bool compareBool = false;

                if (compareType.IsAssignableTo(typeof(bool))) compareBool = (bool)compareValue;
                else if (compareType.IsAssignableTo(typeof(string)))
                {
                    string tempString = (string)compareValue;
                    if (!bool.TryParse(tempString, out compareBool)) throw new Exception("Unable to convert compare value to bool");
                }
                else throw new NotImplementedException($"Not sure how to compare bool field to {compareType.Name}");

                return op switch
                {
                    ComparisonOperator.EQ => actualBool == compareBool,
                    ComparisonOperator.NE => actualBool != compareBool,
                    _ => throw new NotImplementedException($"Operator {op} is not implemented for bool")
                };
            }

            if (actualType.IsAssignableTo(typeof(Enum)))
            {
                object? compareValue2 = compareValue;

                if (!compareType.IsAssignableTo(actualType))
                {
                    if (!Enum.TryParse(actualType, (string)compareValue, true, out compareValue2)) return false;
                }

                return op switch 
                {
                    ComparisonOperator.Equal => Equals(actualValue, compareValue2),
                    ComparisonOperator.NotEqual => !Equals(actualValue, compareValue2),
                    _ => throw new NotImplementedException($"Operator {op} is not implemented for enum")
                };
            }

            throw new NotImplementedException($"Comparison between {actualType.Name} and {compareType.Name} not implemented");
        }
    }
}
