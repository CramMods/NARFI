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

        [TestMethod]
        public void TestRaceGroupConversion()
        {
            List<RaceGroup> raceGroups = RaceGroup.FromIdDictionary(raceIdGroups, _state.LoadOrder.PriorityOrder.Race().WinningOverrides()).ToList();
            Assert.AreEqual(raceGroups.Count, 18);
            Assert.AreEqual(raceGroups[17].Races.Count, 23);
        }
    }
}
