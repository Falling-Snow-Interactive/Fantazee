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
                    return Mathf.RoundToInt((CalculateSmallStraight(score.Type, dice) + score.Value) * score.Mod);
                case ScoreType.LargeStraight:
                    return Mathf.RoundToInt((CalculateLargeStraight(dice) + score.Value) * score.Mod);
                case ScoreType.Yahtzee:
                    return Mathf.RoundToInt((CalculateYahtzee(score.Type, dice) + score.Value) * score.Mod);
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

        private static int CalculateChance(List<Dices.Dice> dice)
        {
            int diceTotal = 0;
            foreach (var d in dice)
            {
                diceTotal += d.Value;
            }

            return diceTotal;
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