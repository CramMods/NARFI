using CramMods.NARFI.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CramMods.NARFI.Tests.Fields
{
    [TestClass]
    public class FieldsTest
    {
        [TestMethod]
        public void TestParsing()
        {
            Dictionary<string, string> testIds = new() {
                { "editorid", nameof(Field.EditorID) },
                { "edid", nameof(Field.EditorID) },
                { "formkey", nameof(Field.FormKey) },
                { "class", nameof(Field.Class) },
                { "cnam", nameof(Field.Class) },
                { "crimefaction", nameof(Field.CrimeFaction) },
                { "crif", nameof(Field.CrimeFaction) },
                { "defaultoutfit", nameof(Field.DefaultOutfit) },
                { "doft", nameof(Field.DefaultOutfit) },
                { "faction", nameof(Field.Faction) },
                { "snam", nameof(Field.Faction) },
                { "haircolor", nameof(Field.HairColor) },
                { "hclf", nameof(Field.HairColor) }
            };

            foreach (KeyValuePair<string, string> pair in testIds)
            {
                Field? field = Field.Parse(pair.Key);
                Assert.IsNotNull(field);
                Assert.AreEqual(field.Id, pair.Value);
            }
        }

        [TestMethod]
        public void TestEquality()
        {
            Field testField1 = new("HaIrCoLoR");
            Field testField2 = new("HaIrZZLoR");

            Field testFieldReal = Field.HairColor;
            Assert.IsTrue(testField1.Equals(testFieldReal));
            Assert.IsFalse(testField2.Equals(testFieldReal));
        }
    }
}