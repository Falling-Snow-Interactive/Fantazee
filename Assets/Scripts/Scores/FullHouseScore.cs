using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    [Serializable]
    public class FullHouseScore : Score
    {
        public FullHouseScore(List<SpellType> spells) : base(ScoreType.FullHouse, spells)
        {
            
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            
            bool foundPair = false;
            bool foundThree = false;
            
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                switch (kvp.Value)
                {
                    case >=5:
                        return 20;
                    case >=3:
                        foundThree = true;
                        break;
                    case >=2:
                        foundPair = true;
                        break;
                }
            }

            if (foundPair && foundThree)
            {
                return 20;
            }

            return 0;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return Calculate(dice) > 0 ? new List<Die>(dice) : new List<Die>();
        }
    }
}