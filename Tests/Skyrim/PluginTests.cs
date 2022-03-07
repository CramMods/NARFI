using CramMods.NARFI;
using CramMods.NARFI.FieldValues;
using CramMods.NARFI.Filters;
using CramMods.NARFI.Skyrim;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CramMods.NARFI.Tests.Skyrim
{
    [TestClass]
    public class PluginTests
    {
        private IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> _state = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);
        private List<string> _npcNames = new() { "Narfi", "Gerdur", "LucanValerius", "Delphine", "Alvor" };

        private NARFI _narfi;
        private List<INpcGetter> _npcs;

        public PluginTests()
        {
            _narfi = new(_state);
            _npcs = _state.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => _npcNames.Contains(n.EditorID!)).ToList();
            _narfi.RegisterPlugin(new SkyrimPlugin());
        }

        [TestMethod]
        public void TestFilters()
        {
            IFieldFilter filter1 = new FieldFilter<string>("Race.EditorId", ComparisonOperator.Contains, "bret");
            List<INpcGetter> matching1 = filter1.Find(_npcs, _narfi).ToList();
            Assert.AreEqual(matching1.Count, 1);
            Assert.AreEqual(matching1[0].EditorID, "Delphine");

            IFieldFilter filter2 = new FieldFilter<string>("race", ComparisonOperator.EQ, "BretonRace");
            List<INpcGetter> matching2 = filter2.Find(_npcs, _narfi).ToList();
            Assert.AreEqual(matching2.Count, 1);
            Assert.AreEqual(matching2[0].EditorID, "Delphine");

            IFieldFilter filter3 = new FieldFilter<string>("gender", ComparisonOperator.EQ, "female");
            Assert.IsTrue(filter3.Test(_npcs[3], _narfi));
            Assert.IsFalse(filter3.Test(_npcs[4], _narfi));
        }

        [TestMethod]
        public void TestGetter()
        {
            IFieldValue? fv1 = _narfi.GetFieldValue(_npcs[0], "race");
            Assert.IsNotNull(fv1);
            Assert.AreEqual(fv1.RawData, "NordRace");

            IFieldValue? fv2 = _narfi.GetFieldValue(_npcs[0], NpcFields.Gender);
            Assert.IsNotNull(fv2);
            Assert.AreEqual(fv2.RawData, Gender.Male);

            IFieldValue? fv3 = _narfi.GetFieldValue(_npcs[3], "race");
            Assert.IsNotNull(fv3);
            Assert.AreEqual(fv3.RawData, "BretonRace");

            IFieldValue? fv4 = _narfi.GetFieldValue(_npcs[4], NpcFields.Class);
            Assert.IsNotNull(fv4);
            Assert.AreEqual(fv4.RawData, "VendorBlacksmith");

            IFieldValue? fv5 = _narfi.GetFieldValue(_npcs[3], NpcFields.WornArmor);
            Assert.IsNotNull(fv5);
            Assert.AreEqual(fv5.RawData, "SkinNaked");

            IFieldValue? fv6 = _narfi.GetFieldValue(_npcs[2], "haircolor.formkey");
            Assert.IsNotNull(fv6);
            Assert.AreEqual(fv6.RawData, "0A0434:Skyrim.esm");
        }
    }
}