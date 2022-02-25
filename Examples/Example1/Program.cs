using CramMods.NARFI.RaceGroups;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;

IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> state = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);

Console.WriteLine($"SkyrimSE Data Folder: \"{state.DataFolderPath}\"");
Console.WriteLine($"There are {state.LoadOrder.Count} mods in the load order");
Console.WriteLine();

Dictionary<string, string[]> myRaceGroups = new() {
    { "All", new[] { "Humanoid", "Beast" }},

    { "Argonian", new[] { "ArgonianRace", "ArgonianRaceVampire" }},
    { "Breton", new[] { "BretonRace", "BretonRaceVampire" }},
    { "DarkElf", new[] { "DarkElfRace", "DarkElfRaceVampire" }},
    { "Elder", new[] { "ElderRace", "ElderRaceVampire" }},
    { "HighElf", new[] { "HighElfRace", "HighElfRaceVampire" }},
    { "Imperial", new[] { "ImperialRace", "ImperialRaceVampire" }},
    { "Khajiit", new[] { "KhajiitRace", "KhajiitRaceVampire" }},
    { "Nord", new[] { "NordRace", "NordRaceVampire" }},
    { "Orc", new[] { "OrcRace", "OrcRaceVampire" }},
    { "Redguard", new[] { "RedguardRace", "RedguardRaceVampire" }},
    { "SnowElf", new[] { "SnowElfRace", "SnowElfRaceVampire" }},
    { "WoodElf", new[] { "WoodElfRace", "WoodElfRaceVampire" }},

    { "Human", new[] { "Breton", "Elder", "Imperial", "Nord", "Redguard" }},
    { "Elf", new[] { "DarkElf", "HighElf", "SnowElf", "WoodElf" }},
    { "Humanoid", new[] { "Human", "Elf", "Orc" }},
    { "Beast", new[] { "Argonian", "Khajiit" }},

};

IRaceGroupCollection raceGroups = RaceGroupCollection.FromDictionary(myRaceGroups, state);

INpcGetter testNpc = state.LoadOrder.PriorityOrder.Npc().WinningOverrides()
    .Where(npc => npc.EditorID == "Narfi")
    .First();

List<IRaceGroup> testNpcGroups = raceGroups.Find(testNpc.Race.Resolve(state.LinkCache)).ToList();


Console.WriteLine();
Console.WriteLine("Press any key to exit");
Console.ReadKey();