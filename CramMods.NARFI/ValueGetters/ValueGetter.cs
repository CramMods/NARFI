using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace CramMods.NARFI.ValueGetters
{
    public partial class ValueGetter
    {
        private IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> _state;
        private ILinkCache<ISkyrimMod, ISkyrimModGetter> _linkCache => _state.LinkCache;

        public ValueGetter(IGameEnvironmentState<ISkyrimMod, ISkyrimModGetter> gameState) => _state = gameState;

        public IFieldValue? GetFieldValue(IMajorRecordGetter record, Field field, FieldPath remainingPath)
        {
            if (field == Field.EditorID) return (record.EditorID == null) ? null : new SingleFieldValue<string>(record.EditorID);
            if (field == Field.FormKey) return new SingleFieldValue<string>(record.FormKey.ToString());

            string recordTypeName = record.Type.Name;
            return recordTypeName switch
            {
                "INpc" => GetNpcFieldValue((INpcGetter)record, field, remainingPath),
                "IRace" => GetRaceFieldValue((IRaceGetter)record, field, remainingPath),
                _ => throw new NotImplementedException($"Unsupported record type: \"{recordTypeName}\"")
            };

        }
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, FieldPath path) => GetFieldValue(record, path.Dequeue(), path);
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, string path) => GetFieldValue(record, new FieldPath(path));

    }
}
