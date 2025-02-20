using System;
using Fantazhee.Boons.ExplosiveDice;
using Fantazhee.Boons.GiveTake;
using Fantazhee.Boons.PlusTwo;
using Fantazhee.Boons.TwoTwos;

namespace Fantazhee.Boons
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