using CramMods.NARFI.FieldValues;
using CramMods.NARFI.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using System.Linq;

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

            IEnumerable<INpcGetter> matching1 = filter1.Find(_npcs, _narfi);
            Assert.AreEqual(matching1.Count(), 1);
            Assert.AreEqual(matching1.First().EditorID, "Delphine");

            IFilter filter2 = new GroupFilter(GroupFilterOperator.AND,
                new FieldFilter<string>("editorid", ComparisonOperator.Contains, "phi"),
                new FieldFilter<string>("editorid", ComparisonOperator.Contains, "delp"));

            Assert.IsTrue(filter2.Test(_npcs[2], _narfi));
            Assert.IsFalse(filter1.Test(_npcs[3], _narfi));

            IEnumerable<INpcGetter> matching2 = filter2.Find(_npcs, _narfi);
            Assert.AreEqual(matching2.Count(), 1);
            Assert.AreEqual(matching2.First().EditorID, "Delphine");

        }

        [TestMethod]
        public void TestFilterUtils()
        {
            IFilter filter1 = new FieldFilter<string>("some.path", ComparisonOperator.EQ, "somestring1");
            IFilter filter2 = new FieldFilter<string>("other.path", ComparisonOperator.EQ, "somestring2");

            IFilter? merged1 = FilterUtils.Merge(filter1, null);
            Assert.AreEqual(merged1!.ToString(), filter1.ToString());

            IFilter? merged2 = FilterUtils.Merge(null, filter2);
            Assert.AreEqual(merged2!.ToString(), filter2.ToString());

            IFilter? merged3 = FilterUtils.Merge(filter1, filter2);
            Assert.IsInstanceOfType(merged3!, typeof(GroupFilter));
            Assert.AreEqual(((GroupFilter)merged3!).Filters[0].ToString(), filter1.ToString());
            Assert.AreEqual(((GroupFilter)merged3!).Filters[1].ToString(), filter2.ToString());

            IFilter filter3 = new FieldFilter<int>("numerical.field.path", ComparisonOperator.LT, 42);
            IFilter groupFilter1 = new GroupFilter(GroupFilterOperator.AND, filter1, filter2);

            IFilter? merged4 = FilterUtils.Merge(filter3, groupFilter1);
            Assert.AreEqual(((GroupFilter)merged4!).Filters.Count, 3);
        }
    }
}
