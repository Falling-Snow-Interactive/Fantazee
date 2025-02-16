using System;
using ProjectYahtzee.Boons.ExplosiveDice;
using ProjectYahtzee.Boons.GiveTake;
using ProjectYahtzee.Boons.PlusTwo;
using ProjectYahtzee.Boons.TwoTwos;

namespace ProjectYahtzee.Boons
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