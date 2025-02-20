using System;
using Fantazee.Boons.ExplosiveDice;
using Fantazee.Boons.GiveTake;
using Fantazee.Boons.PlusTwo;
using Fantazee.Boons.TwoTwos;

namespace Fantazee.Boons
{
    public static class BoonFactory
    {
        public static Boon Create(BoonType type)
        {
            return type switch
                   {
                       BoonType.None => (Boon)null,
                       BoonType.PlusTwo => new PlusTwoBoon(),
                       BoonType.GiveTake => new GiveTakeBoon(),
                       BoonType.ExplosiveDice => new ExplosiveDiceBoon(6),
                       BoonType.TwoTwos => new TwoTwosBoon(),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                   };
        }
    }
}