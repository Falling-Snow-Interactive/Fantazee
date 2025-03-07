using System;
using System.Collections.Generic;
using Fantazee.Scores.Data;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using Fantazee.Spells.Instance;

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
                       ScoreType.SmallRun => new StraightScore(spells, 3),
                       ScoreType.LargeRun => new StraightScore(spells, 4),
                       ScoreType.Chance => new ChanceScore(spells),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                   };
        }

        public static Score Create(PlayerScoreData data)
        {
            return Create(data.Score, data.Spells);
        }

        public static ScoreInstance CreateInstance(ScoreData data, List<SpellInstance> spells)
        {
            return data.Type switch
                   {
                       ScoreType.Ones => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.Twos => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.Threes => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.Fours => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.Fives => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.Sixes => new NumberScoreInstance(data as NumberScoreData, spells),
                       ScoreType.ThreeOfAKind => new KindScoreInstance(data as KindScoreData, spells),
                       ScoreType.FourOfAKind => new KindScoreInstance(data as KindScoreData, spells),
                       ScoreType.FullHouse => new FullHouseScoreInstance(data as FullHouseScoreData, spells),
                       ScoreType.SmallRun => new RunScoreInstance(data as RunScoreData, spells),
                       ScoreType.LargeRun => new RunScoreInstance(data as RunScoreData, spells),
                       ScoreType.Chance => new ChanceScoreInstance(data as ChanceScoreData, spells),
                       ScoreType.Fantazee => new FantazeeScoreInstance(data as FantazeeScoreData, spells),
                       // ScoreType.Pair => new PairScore(data as RunScoreData, spells),
                       // ScoreType.TwoPair => expr,
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}