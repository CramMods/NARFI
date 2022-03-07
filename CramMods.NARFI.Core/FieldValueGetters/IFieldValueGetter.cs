using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;

namespace CramMods.NARFI.FieldValueGetters
{
    public interface IFieldValueGetter
    {
        public void SetMasterGetter(IFieldValueGetter master);
        public void SetLinkCache(ILinkCache linkCache);

        public bool CanGetFieldValue(IMajorRecordGetter record, Field field);
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, Field field, FieldPath remainingPath);
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, FieldPath path);
    }
}
