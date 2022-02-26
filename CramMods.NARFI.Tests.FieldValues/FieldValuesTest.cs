using CramMods.NARFI.FieldValues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CramMods.NARFI.Tests.FieldValues
{
    [TestClass]
    public class FieldValuesTest
    {
        private enum TestEnum
        {
            Item1,
            Item2,
            Item3,
        }

        [TestMethod]
        public void TestSingleFieldValue()
        {
            string testString = "Test:S;tr,in/g1.23!";
            IFieldValue stringValue = new SingleFieldValue<string>(testString);
            Assert.IsTrue(stringValue.StoredType == typeof(string));
            Assert.IsTrue(stringValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsTrue(stringValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(stringValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(stringValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((ISingleFieldValue)stringValue).RawValue!, testString);

            int testInt = 9814;
            IFieldValue intValue = new SingleFieldValue<int>(testInt);
            Assert.IsTrue(intValue.StoredType == typeof(int));
            Assert.IsTrue(intValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsTrue(intValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(intValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(intValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((ISingleFieldValue)intValue).RawValue!, testInt);

            float testFloat = 40.12F;
            IFieldValue floatValue = new SingleFieldValue<float>(testFloat);
            Assert.IsTrue(floatValue.StoredType == typeof(float));
            Assert.IsTrue(floatValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsTrue(floatValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(floatValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(floatValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((ISingleFieldValue)floatValue).RawValue!, testFloat);

            bool testBool = true;
            IFieldValue boolValue = new SingleFieldValue<bool>(testBool);
            Assert.IsTrue(boolValue.StoredType == typeof(bool));
            Assert.IsTrue(boolValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsTrue(boolValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(boolValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(boolValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((ISingleFieldValue)boolValue).RawValue!, testBool);

            TestEnum testEnum = TestEnum.Item2;
            IFieldValue enumValue = new SingleFieldValue<TestEnum>(testEnum);
            Assert.IsTrue(enumValue.StoredType == typeof(TestEnum));
            Assert.IsTrue(enumValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsTrue(enumValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(enumValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(enumValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((ISingleFieldValue)enumValue).RawValue!, testEnum);
        }

        [TestMethod]
        public void TestArrayFieldValue()
        {
            string[] testStringArray = new[] { "TestStringA", "TestStringB", "TestStringC", "TestStringD", "TestStringE" };
            IFieldValue stringArrayValue = new ArrayFieldValue<string>(testStringArray);
            Assert.IsTrue(stringArrayValue.StoredType == typeof(string));
            Assert.IsTrue(stringArrayValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsFalse(stringArrayValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsTrue(stringArrayValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(stringArrayValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));

            IReadOnlyList<string> stored1 = (IReadOnlyList<string>)((IArrayFieldValue)stringArrayValue).RawValues.Select(v => (string)v).ToList();
            for (int i = 0; i < stored1.Count; i++)
            {
                Assert.IsTrue(stored1[i] == testStringArray[i]);
            }

            List<string> testStringList = new() { "TestString1", "TestString2", "TestString3", "TestString4", "TestString5" };
            IFieldValue stringListValue = new ArrayFieldValue<string>(testStringList);
            Assert.IsTrue(stringListValue.StoredType == typeof(string));
            Assert.IsTrue(stringListValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsFalse(stringListValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsTrue(stringListValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(stringListValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));

            IReadOnlyList<string> stored2 = (IReadOnlyList<string>)((IArrayFieldValue)stringListValue).RawValues.Select(v => (string)v).ToList();
            for (int i = 0; i < stored2.Count; i++)
            {
                Assert.IsTrue(stored2[i] == testStringList[i]);
            }
        }

        [TestMethod]
        public void TestGenderedFieldValue()
        {
            string testMale = "This is a string for the male value";
            string testFemale = "This is the other string, for the female value";
            IFieldValue stringValue = new GenderedFieldValue<string>(testMale, testFemale);
            Assert.IsTrue(stringValue.StoredType == typeof(string));
            Assert.IsTrue(stringValue.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsFalse(stringValue.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsFalse(stringValue.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsTrue(stringValue.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.AreEqual(((IGenderedFieldValue)stringValue).RawMaleValue!, testMale);
            Assert.AreEqual(((IGenderedFieldValue)stringValue).RawFemaleValue!, testFemale);
        }

        [TestMethod]
        public void TestConvertToArrayFieldValue()
        {
            string string1 = "ValueOne";
            IFieldValue value1 = new SingleFieldValue<string>(string1);

            string string2 = "ValueTwo";
            IFieldValue value2 = new SingleFieldValue<string>(string2);

            IFieldValue value3 = new SingleFieldValue<string>(null);
            IFieldValue value4 = new SingleFieldValue<string>(null);
            IFieldValue value5 = new SingleFieldValue<string>(null);

            IReadOnlyList<string> expected = new List<string>() { (string)value1.RawData!, (string)value2.RawData! };

            IFieldValue arrayValue1 = FieldValueConverter.ToArray(value1, value2, value3, value4, value5);
            Assert.IsTrue(arrayValue1.StoredType == typeof(string));
            Assert.IsTrue(arrayValue1.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsFalse(arrayValue1.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsTrue(arrayValue1.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(arrayValue1.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.IsTrue(arrayValue1.GetType().IsAssignableTo(typeof(ArrayFieldValue<string>)));

            for (int i = 0; i < ((IArrayFieldValue)arrayValue1).RawValues.Count; i++)
            {
                Assert.AreEqual(((IArrayFieldValue)arrayValue1).RawValues[i], expected[i]);
            }

            IFieldValue arrayValue2 = FieldValueConverter.ToArray(new List<IFieldValue?>() { value1, null, value2, null, value3, value4, value5 });
            Assert.IsTrue(arrayValue2.StoredType == typeof(string));
            Assert.IsTrue(arrayValue2.GetType().IsAssignableTo(typeof(IFieldValue)));
            Assert.IsFalse(arrayValue2.GetType().IsAssignableTo(typeof(ISingleFieldValue)));
            Assert.IsTrue(arrayValue2.GetType().IsAssignableTo(typeof(IArrayFieldValue)));
            Assert.IsFalse(arrayValue2.GetType().IsAssignableTo(typeof(IGenderedFieldValue)));
            Assert.IsTrue(arrayValue2.GetType().IsAssignableTo(typeof(ArrayFieldValue<string>)));

            for (int i = 0; i < ((IArrayFieldValue)arrayValue2).RawValues.Count; i++)
            {
                Assert.AreEqual(((IArrayFieldValue)arrayValue2).RawValues[i], expected[i]);
            }
        }
    }
}