using CramMods.NARFI.FieldValues;
using CramMods.NARFI.ValueGetters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.Tests.ValueGetters
{
    [TestClass]
    public class ValueGettersTest
    {
        [TestMethod]
        public void TestValueGetter1()
        {
            IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> gameState = GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE);
            ValueGetter getter = new(gameState);

            INpcGetter testNpc1 = gameState.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => n.EditorID == "Narfi").First();

            IFieldValue? fieldValue1a = getter.GetFieldValue(testNpc1, "Voice.FormKey");
            Assert.IsNotNull(fieldValue1a);
            Assert.AreEqual(fieldValue1a.StoredType, typeof(string));
            Assert.AreEqual(((ISingleFieldValue?)fieldValue1a)!.RawData, "013AD4:Skyrim.esm");

            IFieldValue? fieldValue1b = getter.GetFieldValue(testNpc1, "Race.Description");
            Assert.IsNotNull(fieldValue1b);
            Assert.AreEqual(fieldValue1b.StoredType, typeof(string));
            Assert.AreEqual(((ISingleFieldValue?)fieldValue1b)!.RawData, "Citizens of Skyrim, they are a tall and fair-haired people.  Strong and hardy, Nords are famous for their resistance to cold and their talent as warriors. They can use a Battlecry to make opponents flee.");

            INpcGetter testNpc2 = gameState.LoadOrder.PriorityOrder.Npc().WinningOverrides().Where(n => n.EditorID == "EncWisp").First();
            IFieldValue? fieldValue2a = getter.GetFieldValue(testNpc2, "Voice.FormKey");
            Assert.IsNotNull(fieldValue2a);
            Assert.AreEqual(fieldValue2a.StoredType, typeof(string));
            Assert.AreEqual(((ISingleFieldValue?)fieldValue2a)!.RawData, "01F6A5:Skyrim.esm");
        }
    }
}