using System;
using System.Collections.Generic;
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
            return data switch
                   {
                       MulliganRelicData mulligan => new MulliganRelicInstance(mulligan, character),
                       ExplosiveRelicData explosive => new ExplosiveRelicInstance(explosive, character),
                       LuckyRelicData lucky => new LuckyRelicInstance(lucky, character),
                       NoVacancyRelicData noVacancy => new NoVacancyRelicInstance(noVacancy, character),
                       VampireFangRelicData vampireFang => new VampireFangRelicInstance(vampireFang, character),
                       _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
                   };
        }

        public static RelicInstance Create(RelicType type, CharacterInstance character)
        {
            if (RelicSettings.Settings.TryGetRelic(type, out RelicData data))
            {
                return Create(data, character);
            }
            Debug.LogError($"Relic type {type} not found in settings.");
            throw new ArgumentOutOfRangeException(nameof(type), $"Relic type {type} not found in settings.");
        }
    }
}