using System.Collections.Generic;
using ProjectYahtzee.Dice;
using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Battle.Scores
{
    public class FullHouseScore : Score
    {
        public override ScoreType Type => ScoreType.FullHouse;
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

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return Calculate(dice) > 0 ? new List<Die>(dice) : new List<Die>();
        }
    }
}