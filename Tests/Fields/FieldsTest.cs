using CramMods.NARFI.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
                { "editorid", Field.EditorID.Id },
                { "edid", Field.EditorID.Id },
                { "formkey", Field.FormKey.Id },
                { "class", Field.Class.Id },
                { "cnam", Field.Class.Id },
                { "crimefaction", Field.CrimeFaction.Id },
                { "crif", Field.CrimeFaction.Id },
                { "defaultoutfit", Field.DefaultOutfit.Id },
                { "doft", Field.DefaultOutfit.Id },
                { "faction", Field.Faction.Id },
                { "snam", Field.Faction.Id },
                { "haircolor", Field.HairColor.Id },
                { "hclf", Field.HairColor.Id },
                { "skill:archery", Field.Skill_Archery.Id },
                { "skill:marksman", Field.Skill_Archery.Id },
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

        [TestMethod]
        public void TestFieldPath()
        {
            string[] input1a = new[] { "gonna", "fail", "bad" };
            string[] input1b = new[] { "race", "headpart", "formkey" };

            Assert.ThrowsException<Exception>(() => new FieldPath(input1a));
            Assert.IsNotNull(new FieldPath(input1b));

            string input2a = "gonna.fail.bad";
            string input2b = "race.headpart.formkey";
            string input2c = "skill:smithing";

            Assert.ThrowsException<Exception>(() => new FieldPath(input2a));
            Assert.IsNotNull(new FieldPath(input2b));
            Assert.IsNotNull(new FieldPath(input2c));

            FieldPath fieldPath1 = new("RNAM.EDID");
            Assert.AreEqual(fieldPath1[0], Field.Race);

            FieldPath fieldPath2 = fieldPath1.Clone();
            Assert.AreEqual(fieldPath1.ToString(), fieldPath2.ToString());

            Assert.AreEqual(fieldPath2.Dequeue(), Field.Race);
        }
    }
}