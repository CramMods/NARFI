using CramMods.NARFI.FieldValues;
using CramMods.NARFI.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramMods.NARFI.Tests.Core
{
    [TestClass]
    public class FilterTests
    {
        private IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> _state = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);
        private List<string> _npcNames = new() { "Narfi", "Gerdur", "Delphine", "Alvor" };

        private NARFI _narfi;
        private List<INpcGetter> _npcs;

        public FilterTests()
        {
            _narfi = new(_state);
            _npcs = _state.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => _npcNames.Contains(n.EditorID!)).ToList();
        }

        [TestMethod]
        public void TestFilter()
        {
            IFieldFilter filter1 = new FieldFilter<string>("editorid", ComparisonOperator.Contains, "phi");

            Assert.IsTrue(filter1.Test(_npcs[2], _narfi));
            Assert.IsFalse(filter1.Test(_npcs[3], _narfi));

            IEnumerable<INpcGetter> matching = filter1.Find(_npcs, _narfi);
            Assert.AreEqual(matching.Count(), 1);
            Assert.AreEqual(matching.First().EditorID, "Delphine");

        }
    }
}
