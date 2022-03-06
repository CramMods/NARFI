using CramMods.NARFI.FieldValues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CramMods.NARFI.Tests.Core
{
    [TestClass]
    public class FieldValueTests
    {
        [TestMethod]
        public void TestFieldValues()
        {
            IFieldValue fv1 = new SingleFieldValue<string>("CoolStringValue");
            IFieldValue fv2 = new SingleFieldValue<int>(42);
            IFieldValue fv3 = new SingleFieldValue<TestEnum>(TestEnum.ValueA);

            IFieldValue gfv1 = new GenderedFieldValue<string>("MaleValueGo!", "FemaleValue1");

            IFieldValue afv1 = new ArrayFieldValue<string>("One", "Two", "Three", "Four", "Five");
            IFieldValue afv2 = new ArrayFieldValue<TestEnum>(TestEnum.ValueC, TestEnum.ValueA, TestEnum.ValueB);

            Assert.IsTrue(fv1.IsMatch(ComparisonOperator.EQ, "coolstringvalue"));

            Assert.IsTrue(fv2.IsMatch(ComparisonOperator.EQ, 42));
            Assert.IsTrue(fv2.IsMatch(ComparisonOperator.EQ, "42"));
            Assert.IsFalse(fv2.IsMatch(ComparisonOperator.EQ, "9"));

            Assert.IsTrue(fv3.IsMatch(ComparisonOperator.EQ, TestEnum.ValueA));
            Assert.IsFalse(fv3.IsMatch(ComparisonOperator.EQ, TestEnum.ValueD));
            Assert.IsTrue(fv3.IsMatch(ComparisonOperator.EQ, "valuea"));
            Assert.IsFalse(fv3.IsMatch(ComparisonOperator.EQ, "coolstringvalue"));

            //Assert.ThrowsException<NotImplementedException>(() => gfv1.Match(ComparisonOperator.EQ, "coolstringvalue"));

            Assert.IsTrue(afv1.IsMatch(ComparisonOperator.EQ, "three"));
            Assert.IsFalse(afv1.IsMatch(ComparisonOperator.EQ, "coolstringvalue"));
            Assert.IsTrue(afv1.IsMatch(ComparisonOperator.Contain, "hre"));

            Assert.IsTrue(afv2.IsMatch(ComparisonOperator.EQ, TestEnum.ValueB));
            Assert.IsFalse(afv2.IsMatch(ComparisonOperator.EQ, TestEnum.ValueE));
            Assert.IsTrue(afv2.IsMatch(ComparisonOperator.EQ, "valuea"));

        }
    }

    public enum TestEnum
    {
        ValueA = 0,
        ValueB,
        ValueC,
        ValueD,
        ValueE,
    }
}
