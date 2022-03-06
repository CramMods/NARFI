using CramMods.NARFI.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CramMods.NARFI.Tests.Core
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void TestEquality()
        {
            Field one = new("One", "uno", "wun");
            Field test1 = new("one");
            Field test2 = new("UNO");
            Field test3 = new("two");

            Assert.AreEqual(one, test1);
            Assert.AreEqual(one, test2);
            Assert.AreNotEqual(one, test3);

            Assert.IsTrue(one.Equals("Wun"));
        }

        [TestMethod]
        public void TestFieldPath()
        {
            Field f1 = new("One");
            Field f2 = new("Two");
            Field f3 = new("Three");

            FieldPath a = new(f1, f2, f3);
            FieldPath b = new("One.Two.Three");

            Assert.AreEqual(a.ToString(), b.ToString());

            FieldPath c = a.Clone();
            Assert.AreEqual(a.ToString(), c.ToString());

            Field fx = a.Dequeue();
            Assert.AreEqual(a.ToString(), "Two.Three");

            FieldPath d = a.AddToFront(fx);
            Assert.AreEqual(d.ToString(), b.ToString());
        }
    }
}
