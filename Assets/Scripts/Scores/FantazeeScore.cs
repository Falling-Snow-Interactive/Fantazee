using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Scores
{
    public class FantazeeScore : Score
    {
        public FantazeeScore(List<SpellType> spells) : base(ScoreType.Fantazee, spells)
        {
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            bool isValid = false;
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                if (kvp.Value >= 5)
                {
                    isValid = true;
                }
            }

            return isValid ? 50 : 0;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return Calculate(dice) > 0 ? new List<Die>(dice) : new List<Die>();
        }
    }
}