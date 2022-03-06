using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.FieldValueGetters
{
    public interface IFieldValueGetter
    {
        public bool CanGet(IMajorRecordGetter record, Field field);
        public IFieldValue? Get(IMajorRecordGetter record, Field field, FieldPath? remainingPath = null);
        public IFieldValue? Get(IMajorRecordGetter record, FieldPath path);
    }
}
