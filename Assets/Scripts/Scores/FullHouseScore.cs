using System;
using System.Collections.Generic;
using Fantazee.Dice;

namespace Fantazee.Scores
{
    [Serializable]
    public class FullHouseScore : Score
    {
        public override int Calculate()
        {
            Dictionary<int, int> dict = DiceToDict(Dice);
            
            bool foundPair = false;
            bool foundThree = false;
            
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                switch (kvp.Value)
                {
                    case >=5:
                        return 25;
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
                return 25;
            }

            return 0;
        }

        public override List<Die> GetScoredDice()
        {
            return Calculate() > 0 ? new List<Die>(Dice) : new List<Die>();
        }
    }
}