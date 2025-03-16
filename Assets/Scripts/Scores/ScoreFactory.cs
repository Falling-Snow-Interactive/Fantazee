using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.SaveLoad;
using Fantazee.Scores.Data;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;

namespace Fantazee.Scores
{
    public static class ScoreFactory
    {
        public static ScoreInstance CreateInstance(ScoreData data, List<SpellData> spells)
        {
            return data switch
                   {
                       NumberScoreData d => new NumberScoreInstance(d, spells),
                       KindScoreData d => new KindScoreInstance(d, spells),
                       FullHouseScoreData d => new FullHouseScoreInstance(d, spells),
                       RunScoreData d => new RunScoreInstance(d, spells),
                       ChanceScoreData d => new ChanceScoreInstance(d, spells),
                       FantazeeScoreData d => new FantazeeScoreInstance(d, spells),
                       EvenOddScoreData d => new EvenOddScoreInstance(d, spells),
                       TwoPairScoreData d => new TwoPairScoreInstance(d, spells),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }

        public static ScoreInstance CreateInstance(ScoreData data, List<SpellInstance> spells)
        {
            return data switch
                   {
                       NumberScoreData d => new NumberScoreInstance(d, spells),
                       KindScoreData d => new KindScoreInstance(d, spells),
                       FullHouseScoreData d => new FullHouseScoreInstance(d, spells),
                       RunScoreData d => new RunScoreInstance(d, spells),
                       ChanceScoreData d => new ChanceScoreInstance(d, spells),
                       FantazeeScoreData d => new FantazeeScoreInstance(d, spells),
                       EvenOddScoreData d => new EvenOddScoreInstance(d, spells),
                       TwoPairScoreData d => new TwoPairScoreInstance(d, spells),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }

        public static ScoreInstance CreateInstance(CharacterScoreData data)
        {
            return CreateInstance(data.Score, data.Spells);
        }

        public static ScoreInstance CreateInstance(ScoreSave save)
        {
            return save.Data.Type switch
                   {
                       ScoreType.Fantazee => new FantazeeScoreInstance(save),
                       ScoreType.Ones => new NumberScoreInstance(save),
                       ScoreType.Twos => new NumberScoreInstance(save),
                       ScoreType.Threes => new NumberScoreInstance(save),
                       ScoreType.Fours => new NumberScoreInstance(save),
                       ScoreType.Fives => new NumberScoreInstance(save),
                       ScoreType.Sixes => new NumberScoreInstance(save),
                       ScoreType.TwoOfAKind => new KindScoreInstance(save),
                       ScoreType.ThreeOfAKind => new KindScoreInstance(save),
                       ScoreType.FourOfAKind => new KindScoreInstance(save),
                       ScoreType.FiveOfAKind => new KindScoreInstance(save),
                       ScoreType.TwoPair => new TwoPairScoreInstance(save),
                       ScoreType.FullHouse => new FullHouseScoreInstance(save),
                       ScoreType.SmallRun => new RunScoreInstance(save),
                       ScoreType.LargeRun => new RunScoreInstance(save),
                       ScoreType.FullRun => new RunScoreInstance(save),
                       ScoreType.Evens => new EvenOddScoreInstance(save),
                       ScoreType.Odds => new EvenOddScoreInstance(save),
                       ScoreType.Chance => new ChanceScoreInstance(save),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}