using CramMods.NARFI.Fields;
using CramMods.NARFI.FieldValues;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramMods.NARFI.ValueGetters
{
    public partial class ValueGetter
    {
        public IFieldValue? GetRaceFieldValue(IRaceGetter race, Field field, FieldPath remainingPath)
        {
            return field switch
            {
                var _ when field.Equals(Field.Playable) => new SingleFieldValue<bool>(race.Flags.HasFlag(Race.Flag.Playable)),
                var _ when field.Equals(Field.Swims) => new SingleFieldValue<bool>(race.Flags.HasFlag(Race.Flag.Swims)),
                var _ when field.Equals(Field.Walks) => new SingleFieldValue<bool>(race.Flags.HasFlag(Race.Flag.Walks)),
                var _ when field.Equals(Field.Flies) => new SingleFieldValue<bool>(race.Flags.HasFlag(Race.Flag.Flies)),

                var _ when field.Equals(Field.Description) => ((race.Description == null) || (race.Description.String == null)) ? null : new SingleFieldValue<string>(race.Description.String),
                var _ when field.Equals(Field.HairColor) => (race.DefaultHairColors == null) ? null : FieldValueConverter.ToGendered(GetFieldValue(race.DefaultHairColors.Male.Resolve(_linkCache), remainingPath.Clone()), GetFieldValue(race.DefaultHairColors.Female.Resolve(_linkCache), remainingPath.Clone())),
                var _ when field.Equals(Field.Name) => ((race.Name == null) || (race.Name.String == null)) ? null : new SingleFieldValue<string>(race.Name.String),
                var _ when field.Equals(Field.Keyword) => (race.Keywords == null) ? null : FieldValueConverter.ToArray(race.Keywords.Select(k => GetFieldValue(k.Resolve(_linkCache), remainingPath.Clone()))),
                var _ when field.Equals(Field.Voice) => FieldValueConverter.ToGendered(GetFieldValue(race.Voices.Male.Resolve(_linkCache), remainingPath.Clone()), GetFieldValue(race.Voices.Female.Resolve(_linkCache), remainingPath.Clone())),
                var _ when field.Equals(Field.WornArmor) => race.Skin.IsNull ? null : GetFieldValue(race.Skin.Resolve(_linkCache), remainingPath),

                _ => throw new NotImplementedException($"Race.{field}"),
            };
        }
    }
}
