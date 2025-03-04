using System;
using Fantazee.Relics.Data;
using Fantazee.Relics.Information;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Settings;

namespace Fantazee.Relics
{
    public static class RelicFactory
    {
        public static RelicInstance Create(RelicData data)
        {
            return data switch
                   {
                       MulliganRelicData mulligan => new MulliganRelicInstance(mulligan),
                       _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
                   };
        }

        public static RelicInstance Create(RelicType type)
        {
            if (RelicSettings.Settings.Information.TryGetInformation(type, out RelicInformation info))
            {
                return Create(info.Data);
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}