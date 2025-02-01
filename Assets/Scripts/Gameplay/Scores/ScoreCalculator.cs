using System;
using System.Collections.Generic;

namespace ProjectYahtzee.Gameplay.Scores
{
    public static class ScoreCalculator
    {
        public static int Calculate(ScoreType type, List<Dices.Dice> dice)
        {
            Dictionary<int, int> diceByValue = new Dictionary<int, int>();
            foreach (Dices.Dice d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] = diceByValue[d.Value] + 1;
                }
            }
            switch (type)
            {
                case ScoreType.None:
                    return 0;
                case ScoreType.Ones:
                    return diceByValue[1] * 1;
                case ScoreType.Twos:
                    return diceByValue[2] * 2;
                case ScoreType.Threes:
                    return diceByValue[3] * 3;
                case ScoreType.Fours:
                    return diceByValue[4] * 4;
                case ScoreType.Fives:
                    return diceByValue[5] * 5;
                case ScoreType.Sixes:
                    return diceByValue[6] * 6;
                case ScoreType.ThreeOfAKind:
                    return CalculateThreeOfAKind(diceByValue);
                case ScoreType.FourOfAKind:
                    return CalculateFourOfAKind(diceByValue);
                case ScoreType.FullHouse:
                    return CalculateFullHouse(diceByValue);
                case ScoreType.SmallStraight:
                    return CalculateSmallStraight(type, dice);
                case ScoreType.LargeStraight:
                    return CalculateLargeStraight(dice);
                case ScoreType.Yahtzee:
                    return CalculateYahtzee(type, dice);
                case ScoreType.Chance:
                    return CalculateChance(type, dice);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static int CalculateThreeOfAKind(Dictionary<int, int> diceByValue)
        {
            int bestCount = 2;
            int bestValue = 0;
            foreach (KeyValuePair<int, int> kvp in diceByValue)
            {
                if (kvp.Value > bestCount)
                {
                    bestCount = kvp.Value;
                    bestValue = kvp.Key;
                }
            }
            
            return bestCount * bestValue;
        }

        private static int CalculateFourOfAKind(Dictionary<int, int> diceByValue)
        {
            int bestCount = 3;
            int bestValue = 0;
            foreach (KeyValuePair<int, int> kvp in diceByValue)
            {
                if (kvp.Value > bestCount)
                {
                    bestCount = kvp.Value;
                    bestValue = kvp.Key;
                }
            }
            
            return bestCount * bestValue;
        }

        private static int CalculateFullHouse(Dictionary<int, int> diceByValue)
        {
            bool foundPair = false;
            bool foundThree = false;
            
            foreach (KeyValuePair<int, int> kvp in diceByValue)
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

        private static int CalculateSmallStraight(ScoreType type, List<Dices.Dice> dice)
        {
            throw new NotImplementedException();
        }

        private static int CalculateLargeStraight(List<Dices.Dice> dice)
        {
            throw new NotImplementedException();
        }

        private static int CalculateYahtzee(ScoreType type, List<Dices.Dice> dice)
        {
            return CalculateCopies(5, dice);
        }

        private static int CalculateChance(ScoreType type, List<Dices.Dice> dice)
        {
            throw new NotImplementedException();
        }
        
        private static int CalculateValue(int value, List<Dices.Dice> dice)
        {
            int score = 0;
            foreach (var d in dice)
            {
                if (value == d.Value)
                {
                    score += d.Value;
                }
            }

            return score;
        }

        private static int CalculateCopies(int min, List<Dices.Dice> dice)
        {
            int count = 0;
            int score = 0;
            for (int x = 0; x < dice.Count; x++)
            {
                Dices.Dice d1 = dice[x];
                int curr = 1;
                int currScore = d1.Value;
                for (int y = x + 1; y < dice.Count; y++)
                {
                    Dices.Dice d2 = dice[y];
                    if (d1.Value == d2.Value)
                    {
                        curr++;
                        currScore += d1.Value;
                    }
                }
                
                if (curr > count)
                {
                    count = curr;
                    score = currScore;
                }
            }

            return count >= min ? score : 0;
        }
    }
}