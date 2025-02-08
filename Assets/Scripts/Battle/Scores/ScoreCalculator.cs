using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectYahtzee.Battle.Scores
{
    public static class ScoreCalculator
    {
        public static int Calculate(Score score, List<Dices.Dice> dice)
        {
            Dictionary<int, int> diceByValue = new Dictionary<int, int>();
            foreach (Dices.Dice d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] = diceByValue[d.Value] + 1;
                }
            }
            switch (score.Type)
            {
                case ScoreType.None:
                    return 0;
                case ScoreType.Ones:
                    return Mathf.RoundToInt((CalculateNumber(1, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Twos:
                    return Mathf.RoundToInt((CalculateNumber(2, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Threes:
                    return Mathf.RoundToInt((CalculateNumber(3, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Fours:
                    return Mathf.RoundToInt((CalculateNumber(4, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Fives:
                    return Mathf.RoundToInt((CalculateNumber(5, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Sixes:
                    return Mathf.RoundToInt((CalculateNumber(6, diceByValue) + score.Value) * score.Mod);
                case ScoreType.ThreeOfAKind:
                    return Mathf.RoundToInt((CalculateThreeOfAKind(diceByValue) + score.Value) * score.Mod);
                case ScoreType.FourOfAKind:
                    return Mathf.RoundToInt((CalculateFourOfAKind(diceByValue) + score.Value) * score.Mod);
                case ScoreType.FullHouse:
                    return Mathf.RoundToInt((CalculateFullHouse(diceByValue) + score.Value) * score.Mod);
                case ScoreType.SmallStraight:
                    return Mathf.RoundToInt((CalculateStraight(4, diceByValue) + score.Value) * score.Mod);
                case ScoreType.LargeStraight:
                    return Mathf.RoundToInt((CalculateStraight(5, diceByValue) + score.Value) * score.Mod);
                case ScoreType.Yahtzee:
                    return Mathf.RoundToInt((CalculateYahtzee(diceByValue) + score.Value) * score.Mod);
                case ScoreType.Chance:
                    return Mathf.RoundToInt((CalculateChance(dice) + score.Value) * score.Mod);
                default:
                    throw new ArgumentOutOfRangeException(nameof(score.Type), score.Type, null);
            }
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

        private static int CalculateYahtzee(Dictionary<int, int> dice)
        {
            foreach (var kvp in dice)
            {
                if (kvp.Value >= 5)
                {
                    return kvp.Key * kvp.Value;
                }
            }

            return 0;
        }

        private static int CalculateChance(List<Dices.Dice> dice)
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