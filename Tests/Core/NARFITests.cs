using CramMods.NARFI.FieldValues;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CramMods.NARFI.Tests.Core
{
    [TestClass]
    public class NARFITests
    {
        private IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> _state = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);
        private List<string> _npcNames = new() { "Narfi", "Gerdur", "Delphine", "Alvor" };

        private NARFI _narfi;
        private List<INpcGetter> _npcs;

        public NARFITests()
        {
            _narfi = new(_state);
            _npcs = _state.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => _npcNames.Contains(n.EditorID!)).ToList();
        }

        [TestMethod]
        public void TestNARFI()
        {
            Assert.IsTrue(_narfi.CanGetFieldValue(_npcs[0], new("editorid")));

            IFieldValue? fv1 = _narfi.GetFieldValue(_npcs[0], "editorid.test");

            Assert.IsNotNull(fv1);
            Assert.IsInstanceOfType(fv1, typeof(IFieldValue));
            Assert.IsInstanceOfType(fv1, typeof(ISingleFieldValue));
            Assert.IsInstanceOfType(fv1, typeof(SingleFieldValue<string>));
            Assert.IsNotInstanceOfType(fv1, typeof(IArrayFieldValue));

            Assert.IsTrue(fv1.IsMatch(ComparisonOperator.EQ, "Narfi"));
            Assert.IsTrue(fv1.IsMatch(ComparisonOperator.NE, "OtherString"));
            Assert.IsTrue(fv1.IsMatch(ComparisonOperator.Contains, "nar"));

            IFieldValue? fv2 = _narfi.GetFieldValue(_npcs[3], "FormKey");
            Assert.IsNotNull(fv2);
            Assert.IsTrue(fv2.IsMatch(ComparisonOperator.EQ, "013475:Skyrim.esm"));
        }
    }
}
