using System;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    public static class ScoreFactory
    {
        public static Score Create(ScoreType type, SpellType spell)
        {
            return type switch
                   {
                       ScoreType.Ones => new NumberScore(spell, 1),
                       ScoreType.Twos => new NumberScore(spell, 2),
                       ScoreType.Threes => new NumberScore(spell, 3),
                       ScoreType.Fours => new NumberScore(spell, 4),
                       ScoreType.Fives => new NumberScore(spell, 5),
                       ScoreType.Sixes => new NumberScore(spell, 6),
                       ScoreType.ThreeOfAKind => new KindScore(spell, 3),
                       ScoreType.FourOfAKind => new KindScore(spell, 4),
                       ScoreType.FullHouse => new FullHouseScore(spell),
                       ScoreType.SmallStraight => new StraightScore(spell, 3),
                       ScoreType.LargeStraight => new StraightScore(spell, 4),
                       ScoreType.Chance => new ChanceScore(spell),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                   };
        }

        public static Score Create(ScoreData data)
        {
            return Create(data.Score, data.Spell);
        }
    }
}