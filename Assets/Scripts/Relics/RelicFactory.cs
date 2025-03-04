using System;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Relics.Information;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Settings;
using UnityEngine;

namespace Fantazee.Relics
{
    public static class RelicFactory
    {
        public static RelicInstance Create(RelicData data, CharacterInstance character)
        {
            Debug.Log($"Relic: Creating {data.name}");
            return data switch
                   {
                       MulliganRelicData mulligan => new MulliganRelicInstance(mulligan, character),
                       ExplosiveRelicData explosive => new ExplosiveRelicInstance(explosive, character),
                       LuckyRelicData lucky => new LuckyRelicInstance(lucky, character),
                       _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
                   };
        }

        public static RelicInstance Create(RelicType type, CharacterInstance character)
        {
            if (RelicSettings.Settings.Information.TryGetInformation(type, out RelicInformation info))
            {
                return Create(info.Data, character);
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}