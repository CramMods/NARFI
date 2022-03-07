using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValueGetters;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramMods.NARFI.Skyrim
{
    public class RaceFieldValueGetter : IFieldValueGetter
    {
        private IFieldValueGetter? _master;
        public void SetMasterGetter(IFieldValueGetter master) => _master = master;

        private ILinkCache<ISkyrimMod, ISkyrimModGetter>? _linkCache;
        public void SetLinkCache(ILinkCache linkCache) => _linkCache = (linkCache as ILinkCache<ISkyrimMod, ISkyrimModGetter>)!;

        public bool CanGetFieldValue(IMajorRecordGetter record, Field field)
        {
            if (!record.GetType().IsAssignableTo(typeof(IRaceGetter))) return false;
            return RaceFields.All().Any(f => f.Equals(field));
        }

        public IFieldValue? GetFieldValue(IRaceGetter race, Field field, FieldPath remainingPath)
        {
            if (field.Equals(RaceFields.Description)) return new SingleFieldValue<string>(race.Description?.String) ?? null;

            if (field.Equals(RaceFields.HairColor)) return (race.DefaultHairColors == null)
                    ? null
                    : FieldValueConverter.ToGendered(
                        _master!.GetFieldValue(race.DefaultHairColors.Male.Resolve(_linkCache!), remainingPath.Clone()),
                        _master!.GetFieldValue(race.DefaultHairColors.Female.Resolve(_linkCache!), remainingPath.Clone()));

            if (field.Equals(race.Keywords)) return (race.Keywords == null) ? null : FieldValueConverter.ToArray(race.Keywords.Select(k => _master!.GetFieldValue(k.Resolve(_linkCache!), remainingPath.Clone())));
            if (field.Equals(RaceFields.Name)) return new SingleFieldValue<string>(race.Name?.String) ?? null;

            if (field.Equals(RaceFields.Voice)) return FieldValueConverter.ToGendered(
                _master!.GetFieldValue(race.Voices.Male.Resolve(_linkCache!), remainingPath.Clone()), 
                _master!.GetFieldValue(race.Voices.Female.Resolve(_linkCache!), remainingPath.Clone()));

            if (field.Equals(RaceFields.WornArmor)) return race.Skin.IsNull ? null : _master!.GetFieldValue(race.Skin.Resolve(_linkCache!), remainingPath);


            throw new NotImplementedException();
        }

        public IFieldValue? GetFieldValue(IMajorRecordGetter record, Field field, FieldPath remainingPath) => GetFieldValue((IRaceGetter)record, field, remainingPath);
        public IFieldValue? GetFieldValue(IMajorRecordGetter record, FieldPath path) => GetFieldValue(record, path.Dequeue(), path);

    }
}
