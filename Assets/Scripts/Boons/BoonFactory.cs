using System;
using Fantahzee.Boons.ExplosiveDice;
using Fantahzee.Boons.GiveTake;
using Fantahzee.Boons.PlusTwo;
using Fantahzee.Boons.TwoTwos;

namespace Fantahzee.Boons
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