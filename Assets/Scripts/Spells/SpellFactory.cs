using System;
using Fantazee.Spells.Dagger;

namespace Fantazee.Spells
{
    public static class SpellFactory
    {
        public static SpellInstance Create(SpellData data)
        {
            return data.Type switch
                   {
                       SpellType.None => (SpellInstance)null,
                       SpellType.Dagger => new DaggerInstance(data as DaggerData),
                       _ => throw new ArgumentOutOfRangeException(nameof(data.Type), data.Type, null)
                   };
        }
    }
}