using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    [Serializable]
    public class KindScore : Score
    {
        public KindScore(SpellType spell, int kind) : base(GetType(kind), spell)
        {
        }

        public static ScoreType GetType(int kind)
        {
            return kind switch
                   {
                       3 => ScoreType.ThreeOfAKind,
                       4 => ScoreType.FourOfAKind,
                       _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
                   };
        }
        
        private int GetMatches()
        {
            switch (Type)
            {
                case ScoreType.ThreeOfAKind:
                    return 3;
                case ScoreType.FourOfAKind:
                    return 4;
                case ScoreType.Fantazee:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            bool isValid = false;
            int total = 0;

            foreach (KeyValuePair<int, int> kvp in dict)
            {
                total += kvp.Key * kvp.Value;
                if (kvp.Value >= GetMatches())
                {
                    isValid = true;
                }
            }

            if (isValid)
            {
                return Type == ScoreType.Fantazee ? 50 : total;
            }

            return 0;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return Calculate(dice) > 0 ? new List<Die>(dice) : new List<Die>();
        }
    }
}