using CramMods.NARFI.RaceGroups;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.Tests.RaceGroups
{
    [TestClass]
    public class RaceGroupsTest
    {
        public IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> State = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);

        public static Dictionary<string, string[]> TestDictionary1 = new()
        {
            { "Argonian",   new[] { "ArgonianRace", "ArgonianRaceVampire" } },
            { "Breton",     new[] { "BretonRace", "BretonRaceVampire" } },
            { "DarkElf",    new[] { "DarkElfRace", "DarkElfRaceVampire" } },
            { "HighElf",    new[] { "HighElfRace", "HighElfRaceVampire" } },
            { "Imperial",   new[] { "ImperialRace", "ImperialRaceVampire" } },
            { "Khajiit",    new[] { "KhajiitRace", "KhajiitRaceVampire" } },
            { "Nord",       new[] { "NordRace", "NordRaceVampire" } },
            { "Orc",        new[] { "OrcRace", "OrcRaceVampire" } },
            { "Redguard",   new[] { "RedguardRace", "RedguardRaceVampire" } },
            { "SnowElf",    new[] { "SnowElfRace", "SnowElfRaceVampire" } },
            { "WoodElf",    new[] { "WoodElfRace", "WoodElfRaceVampire" } },

            { "Human",      new[] { "Breton", "Imperial", "Nord", "Redguard" } },
            { "Elf",        new[] { "DarkElf", "HighElf", "SnowElf", "WoodElf" } },
            { "Beast",      new[] { "Argonian", "Khajiit" } },
            { "Humanoid",   new[] { "Human", "Elf", "Orc" } },
            { "All",        new[] { "Humanoid", "Beast" } }
        };

        public static Dictionary<string, IEnumerable<string>> TestDictionary2 = new()
        {
            { "Argonian",   new List<string>() { "ArgonianRace", "ArgonianRaceVampire" } },
            { "Breton",     new List<string>() { "BretonRace", "BretonRaceVampire" } },
            { "DarkElf",    new List<string>() { "DarkElfRace", "DarkElfRaceVampire" } },
            { "HighElf",    new List<string>() { "HighElfRace", "HighElfRaceVampire" } },
            { "Imperial",   new List<string>() { "ImperialRace", "ImperialRaceVampire" } },
            { "Khajiit",    new List<string>() { "KhajiitRace", "KhajiitRaceVampire" } },
            { "Nord",       new List<string>() { "NordRace", "NordRaceVampire" } },
            { "Orc",        new List<string>() { "OrcRace", "OrcRaceVampire" } },
            { "Redguard",   new List<string>() { "RedguardRace", "RedguardRaceVampire" } },
            { "SnowElf",    new List<string>() { "SnowElfRace", "SnowElfRaceVampire" } },
            { "WoodElf",    new List<string>() { "WoodElfRace", "WoodElfRaceVampire" } },

            { "Human",      new List<string>() { "Breton", "Imperial", "Nord", "Redguard" } },
            { "Elf",        new List<string>() { "DarkElf", "HighElf", "SnowElf", "WoodElf" } },
            { "Beast",      new List<string>() { "Argonian", "Khajiit" } },
            { "Humanoid",   new List<string>() { "Human", "Elf", "Orc" } },
            { "All",        new List<string>() { "Humanoid", "Beast" } }
        };

        public static Dictionary<string, List<FormKey>> TruthFormKeys = new() {
            { 
                "Argonian", new() {
                    new FormKey( new ModKey("Skyrim", ModType.Master), 0x013740 ),
                    new FormKey( new ModKey("Skyrim", ModType.Master), 0x08883A )
                }
            },
            {
                "Breton",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013741),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883C)
                }
            },
            {
                "DarkElf",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013742),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883D)
                }
            },
            {
                "HighElf",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013743),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088840)
                }
            },
            {
                "Imperial",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013744),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088844)
                }
            },
            {
                "Khajiit",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013745),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088845)
                }
            },
            {
                "Nord",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013746),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088794)
                }
            },
            {
                "Orc",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013747),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x0A82B9)
                }
            },
            {
                "Redguard",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013748),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088846)
                }
            },
            {
                "SnowElf",
                new()
                {
                    new FormKey(new ModKey("Dawnguard", ModType.Master), 0x00377D)
                }
            },
            {
                "WoodElf",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013749),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088884)
                }
            },
            {
                "Human",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013741),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883C),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013744),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088844),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013746),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088794),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013748),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088846)
                }
            },
            {
                "Elf",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013742),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013743),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088840),
                    new FormKey(new ModKey("Dawnguard", ModType.Master), 0x00377D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013749),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088884)
                }
            },
            {
                "Beast",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013740),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883A),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013745),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088845)
                }
            },
            {
                "Humanoid",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013741),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883C),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013742),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013743),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088840),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013744),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088844),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013746),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088794),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013747),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x0A82B9),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013748),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088846),
                    new FormKey(new ModKey("Dawnguard", ModType.Master), 0x00377D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013749),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088884)
                }
            },
            {
                "All",
                new()
                {
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013740),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883A),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013741),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883C),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013742),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x08883D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013743),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088840),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013744),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088844),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013745),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088845),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013746),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088794),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013747),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x0A82B9),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013748),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088846),
                    new FormKey(new ModKey("Dawnguard", ModType.Master), 0x00377D),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x013749),
                    new FormKey(new ModKey("Skyrim", ModType.Master), 0x088884)
                }
            },

        };

        [TestMethod]
        public void ConvertDictionary1()
        {
            IRaceGroupCollection resultGroups = RaceGroupCollection.FromDictionary(TestDictionary1, State);
            
            Assert.AreEqual(TruthFormKeys.Count, resultGroups.Count(), "Number of RaceGroups is incorrect");

            for (int resultIndex = 0; resultIndex < resultGroups.Count(); resultIndex++)
            {
                IRaceGroup result = resultGroups.RaceGroups[resultIndex];
                IList<FormKey> truthKeys = TruthFormKeys.Values.ToArray()[resultIndex];

                for (int keyIndex = 0; keyIndex < result.Races.Count; keyIndex++)
                {
                    FormKey raceKey = result.Races.ToArray()[keyIndex].FormKey;
                    FormKey truthKey = truthKeys[keyIndex];

                    Assert.AreEqual(raceKey, truthKey, "FormKeys don't match");
                }
            }
        }

        [TestMethod]
        public void ConvertDictionary2()
        {
            IRaceGroupCollection resultGroups = RaceGroupCollection.FromDictionary(TestDictionary2, State);

            Assert.AreEqual(TruthFormKeys.Count, resultGroups.Count(), "Number of RaceGroups is incorrect");

            for (int resultIndex = 0; resultIndex < resultGroups.Count(); resultIndex++)
            {
                IRaceGroup result = resultGroups.RaceGroups[resultIndex];
                IList<FormKey> truthKeys = TruthFormKeys.Values.ToArray()[resultIndex];

                for (int keyIndex = 0; keyIndex < result.Races.Count; keyIndex++)
                {
                    FormKey raceKey = result.Races.ToArray()[keyIndex].FormKey;
                    FormKey truthKey = truthKeys[keyIndex];

                    Assert.AreEqual(raceKey, truthKey, "FormKeys don't match");
                }
            }
        }

        [TestMethod]
        public void GetNpcRaceGroups()
        {
            List<string> expectedGroupNames = new() { "Nord", "Human", "Humanoid", "All" };

            INpcGetter testNpc = State.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => n.EditorID == "Narfi").First();
            IRaceGroupCollection raceGroups = RaceGroupCollection.FromDictionary(TestDictionary1, State);
            IList<IRaceGroup> npcGroups = raceGroups.Find(testNpc.Race.Resolve(State.LinkCache));

            Assert.IsTrue(npcGroups.All(rg => expectedGroupNames.Contains(rg.Name)), "Found an invalid result");
            Assert.IsTrue(expectedGroupNames.All(gn => npcGroups.Any(rg => rg.Name == gn)), "Missing a result");
        }
    }
}