using System;
using ProjectYahtzee.Boons.TwoMod;

namespace ProjectYahtzee.Boons
{
    public static class BoonFactory
    {
        public static Boon Create(BoonType type)
        {
            return type switch
                   {
                       BoonType.None => (Boon)null,
                       BoonType.TwoMod => new TwoModBoon(),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                   };
        }
    }
}