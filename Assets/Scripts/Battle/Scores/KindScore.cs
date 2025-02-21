using System;
using System.Collections.Generic;
using Fantazee.Dice;
using UnityEngine;

namespace Fantazee.Battle.Scores
{
    [Serializable]
    public class KindScore : Score
    {
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
        
        public override int Calculate()
        {
            Dictionary<int, int> dict = DiceToDict(Dice);
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

        public override List<Die> GetScoredDice()
        {
            return Calculate() > 0 ? new List<Die>(Dice) : new List<Die>();
        }
    }
}