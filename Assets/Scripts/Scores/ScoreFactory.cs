using System;
using System.Collections.Generic;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    public static class ScoreFactory
    {
        public static Score Create(ScoreType type, List<SpellType> spells)
        {
            return type switch
                   {
                       ScoreType.Ones => new NumberScore(spells, 1),
                       ScoreType.Twos => new NumberScore(spells, 2),
                       ScoreType.Threes => new NumberScore(spells, 3),
                       ScoreType.Fours => new NumberScore(spells, 4),
                       ScoreType.Fives => new NumberScore(spells, 5),
                       ScoreType.Sixes => new NumberScore(spells, 6),
                       ScoreType.ThreeOfAKind => new KindScore(spells, 3),
                       ScoreType.FourOfAKind => new KindScore(spells, 4),
                       ScoreType.FullHouse => new FullHouseScore(spells),
                       ScoreType.SmallStraight => new StraightScore(spells, 3),
                       ScoreType.LargeStraight => new StraightScore(spells, 4),
                       ScoreType.Chance => new ChanceScore(spells),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                   };
        }

        public static Score Create(ScoreData data)
        {
            return Create(data.Score, data.Spells);
        }
    }
}