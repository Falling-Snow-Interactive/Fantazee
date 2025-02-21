using System;
using System.Collections.Generic;
using Fantazee.Dice;

namespace Fantazee.Scores
{
    public static class ScoreCalculator
    {
        public static int Calculate(ScoreType type, List<Die> dice)
        {
            Dictionary<int, int> diceByValue = new Dictionary<int, int>();
            foreach (Die d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] += 1;
                }
            }

            return type switch
                   {
                       ScoreType.None => 0,
                       ScoreType.Ones => CalculateNumber(1, diceByValue),
                       ScoreType.Twos => CalculateNumber(2, diceByValue),
                       ScoreType.Threes => CalculateNumber(3, diceByValue),
                       ScoreType.Fours => CalculateNumber(4, diceByValue),
                       ScoreType.Fives => CalculateNumber(5, diceByValue),
                       ScoreType.Sixes => CalculateNumber(6, diceByValue),
                       ScoreType.ThreeOfAKind => CalculateThreeOfAKind(diceByValue),
                       ScoreType.FourOfAKind => CalculateFourOfAKind(diceByValue),
                       ScoreType.FullHouse => CalculateFullHouse(diceByValue),
                       ScoreType.SmallStraight => CalculateStraight(4, diceByValue),
                       ScoreType.LargeStraight => CalculateStraight(5, diceByValue),
                       ScoreType.Fantazee => CalculateFantazee(diceByValue),
                       ScoreType.Chance => CalculateChance(dice),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                   };
        }

        private static int CalculateNumber(int value, Dictionary<int, int> diceByValue)
        {
            if (diceByValue.TryGetValue(value, out int result))
            {
                return result * value;
            }

            return 0;
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

        private static int CalculateStraight(int length, Dictionary<int, int> dice)
        {
            // Small straight must start with 1 or 2
            int startRange = 6 - length;
            int start = -1;
            for (int i = 1; i <= startRange; i++)
            {
                if (dice.TryGetValue(i, out _))
                {
                    start = i;
                    break;
                }
            }

            if (start == -1)
            {
                return 0;
            }

            int total = 0;
            for (int i = start; i <= start + length; i++)
            {
                if (!dice.TryGetValue(i, out _))
                {
                    return 0;
                }

                total += i;
            }
            
            return total;
        }

        private static int CalculateFantazee(Dictionary<int, int> dice)
        {
            foreach (KeyValuePair<int, int> kvp in dice)
            {
                if (kvp.Value >= 5)
                {
                    return kvp.Key * kvp.Value;
                }
            }

            return 0;
        }

        private static int CalculateChance(List<Die> dice)
        {
            int diceTotal = 0;
            foreach (var d in dice)
            {
                diceTotal += d.Value;
            }

            return diceTotal;
        }
    }
}