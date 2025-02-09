using System;

namespace ProjectYahtzee.Battle.Scores
{
    public static class ScoreFactory
    {
        public static Score Create(ScoreType type)
        {
            return type switch
                   {
                       ScoreType.Ones => new NumberScore(1),
                       ScoreType.Twos => new NumberScore(2),
                       ScoreType.Threes => new NumberScore(3),
                       ScoreType.Fours => new NumberScore(4),
                       ScoreType.Fives => new NumberScore(5),
                       ScoreType.Sixes => new NumberScore(6),
                       ScoreType.ThreeOfAKind => new KindScore(3),
                       ScoreType.FourOfAKind => new KindScore(4),
                       ScoreType.FullHouse => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                       ScoreType.SmallStraight=> throw new ArgumentOutOfRangeException(nameof(type), type, null),
                       ScoreType.LargeStraight => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                       ScoreType.Yahtzee => new KindScore(5), 
                       ScoreType.Chance => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                   };
        }
    }
}