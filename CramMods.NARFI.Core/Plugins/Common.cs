using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.Plugins
{
    public class Common : Plugin
    {
        public override IReadOnlyList<Field> Fields => _fields.AsReadOnly();
        public override IReadOnlyList<IFieldValueGetter> Getters => _getters.AsReadOnly();

        private static List<Field> _fields = new()
        {
            CommonFields.EditorId,
            CommonFields.FormKey
        };

        private static List<IFieldValueGetter> _getters = new()
        {
            new CommonFieldGetter()
        };

        public Common() : base("Common") {}


        private class CommonFieldGetter : IFieldValueGetter
        {
            public bool CanGet(IMajorRecordGetter record, Field field)
            {
                bool recordOK = record.GetType().IsAssignableTo(typeof(IMajorRecordGetter));
                bool pathOK = _fields.Any(f => f.Equals(field));
                return recordOK && pathOK;
            }

            public IFieldValue? Get(IMajorRecordGetter record, Field field, FieldPath? remainingPath)
            {
                if (field.Equals(CommonFields.EditorId)) return record.EditorID == null ? null : new SingleFieldValue<string>(record.EditorID);
                if (field.Equals(CommonFields.FormKey)) return new SingleFieldValue<string>(record.FormKey.ToString());
                throw new NotImplementedException($"Unknown field {field}");
            }
            public IFieldValue? Get(IMajorRecordGetter record, FieldPath path) => Get(record, path.Dequeue(), path);
        }

        private static class CommonFields
        {
            public static Field EditorId = new Field("EditorId", "EDID");
            public static Field FormKey = new Field("FormKey");
        }
    }
}
