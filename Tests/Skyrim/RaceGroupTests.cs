using CramMods.NARFI.FieldValues;
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
    public class RaceGroupTests
    {
        private IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> _state = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);
        private Dictionary<string, string[]> raceIdGroups = new()
        {
            { "Argonian", new[] { "ArgonianRace", "ArgonianRaceVampire" } },
            { "Breton", new[] { "BretonRace", "BretonRaceVampire" } },
            { "DarkElf", new[] { "DarkElfRace", "DarkElfRaceVampire" } },
            { "Elder", new[] { "ElderRace", "ElderRaceVampire" } },
            { "HighElf", new[] { "HighElfRace", "HighElfRaceVampire" } },
            { "Imperial", new[] { "ImperialRace", "ImperialRaceVampire" } },
            { "Khajiit", new[] { "KhajiitRace", "KhajiitRaceVampire" } },
            { "Nord", new[] { "NordRace", "NordRaceVampire" } },
            { "Orc", new[] { "OrcRace", "OrcRaceVampire" } },
            { "Redguard", new[] { "RedguardRace", "RedguardRaceVampire" } },
            { "SnowElf", new[] { "SnowElfRace", "SnowElfRaceVampire" } },
            { "WoodElf", new[] { "WoodElfRace", "WoodElfRaceVampire" } },

            { "Human", new[] { "Breton", "Elder", "Imperial", "Nord", "Redguard" } },
            { "Elf", new[] { "DarkElf", "HighElf", "SnowElf", "WoodElf" } },
            { "Humanoid", new[] { "Human", "Elf", "Orc" } },
            { "Beast", new[] { "Argonian", "Khajiit" } },
            { "All", new[] { "Humanoid", "Beast" } },

            { "CheckDups", new[] { "Human", "Human", "Humanoid", "All" } },
        };

        private List<RaceGroup> _raceGroups;

        private SkyrimPlugin _skyrimPlugin;
        private NARFI _narfi;
        private INpcGetter _testNpc;

        public RaceGroupTests()
        {
            _raceGroups = RaceGroup.FromIdDictionary(raceIdGroups, _state.LoadOrder.PriorityOrder.Race().WinningOverrides()).ToList();

            _skyrimPlugin = new SkyrimPlugin();
            _skyrimPlugin.SetRaceGroups(_raceGroups);

            _narfi = new(_state);
            _narfi.RegisterPlugin(_skyrimPlugin);

            _testNpc = _state.LoadOrder.PriorityOrder.Npc().WinningOverrides().First(n => n.EditorID == "Narfi");
        }


        [TestMethod]
        public void TestRaceGroups()
        {
            Assert.AreEqual(_raceGroups.Count, 18);
            Assert.AreEqual(_raceGroups[17].Races.Count, 23);

            IFieldValue? fv1 = _narfi.GetFieldValue(_testNpc, "racegroup");
            Assert.IsNotNull(fv1);
            Assert.AreEqual(((IArrayFieldValue)fv1).RawValues.Count, 5);

            throw new Exception();
        }
    }
}
