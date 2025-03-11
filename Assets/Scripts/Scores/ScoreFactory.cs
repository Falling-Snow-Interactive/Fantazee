using System;
using System.Collections.Generic;
using Fantazee.Characters;
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
    }
}