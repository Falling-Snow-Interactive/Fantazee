using System;
using System.Collections.Generic;
using Fantazee.Battle.Ui;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public class ScoreTracker : ISerializationCallbackReceiver
    {
        [SerializeField]
        private NumberScore ones;
        public NumberScore Ones => ones;

        [SerializeField]
        private NumberScore twos;
        public NumberScore Twos => twos;
        
        [SerializeField]
        private NumberScore threes;
        public NumberScore Threes => threes;
        
        [SerializeField]
        private NumberScore fours;
        public NumberScore Fours => fours;
        
        [SerializeField]
        private NumberScore fives;
        public NumberScore Fives => fives;
        
        [SerializeField]
        private NumberScore sixes;
        public NumberScore Sixes => sixes;

        [SerializeField]
        private KindScore threeOfAKind;
        public KindScore ThreeOfAKind => threeOfAKind;
        
        [SerializeField]
        private KindScore fourOfAKind;
        public KindScore FourOfAKind => fourOfAKind;

        [SerializeField]
        private FullHouseScore fullHouse;
        public FullHouseScore FullHouse => fullHouse;

        [SerializeField]
        private StraightScore smallStraight;
        public StraightScore SmallStraight => smallStraight;
        
        [SerializeField]
        private StraightScore largeStraight;
        public StraightScore LargeStraight => largeStraight;
        
        [SerializeField]
        private ChanceScore chance;
        public ChanceScore Chance => chance;

        [SerializeField]
        private KindScore fantazee;
        public KindScore Fantazee => fantazee;

        // [SerializeField]
        // private BonusScore bonusScore;
        // public BonusScore BonusScore => bonusScore;

        public void Initialize()
        {
            Debug.Log($"Score Tracker - Initialize");
            Clear();
        }

        public List<Score> GetScoreList()
        {
            List<Score> scores = new()
                                 {
                                     Ones,
                                     Twos,
                                     Threes,
                                     Fours,
                                     Fives,
                                     Sixes,
                                     ThreeOfAKind,
                                     FourOfAKind,
                                     FullHouse,
                                     SmallStraight,
                                     LargeStraight,
                                     Chance,
                                     Fantazee,
                                 };

            return scores;
        }

        public void Clear()
        {
            ones.ClearDice();
            twos.ClearDice();
            threes.ClearDice();
            fours.ClearDice();
            fives.ClearDice();
            sixes.ClearDice();
            threeOfAKind.ClearDice();
            fourOfAKind.ClearDice();
            fullHouse.ClearDice();
            smallStraight.ClearDice();
            largeStraight.ClearDice();
            chance.ClearDice();
            fantazee.ClearDice();
        }

        public void OnBeforeSerialize()
        {
            ones.Type = ScoreType.Ones;
            twos.Type = ScoreType.Twos;
            threes.Type = ScoreType.Threes;
            fours.Type = ScoreType.Fours;
            fives.Type = ScoreType.Fives;
            sixes.Type = ScoreType.Sixes;
            threeOfAKind.Type = ScoreType.ThreeOfAKind;
            fourOfAKind.Type = ScoreType.FourOfAKind;
            fullHouse.Type = ScoreType.FullHouse;
            smallStraight.Type = ScoreType.SmallStraight;
            largeStraight.Type = ScoreType.LargeStraight;
            chance.Type = ScoreType.Chance;
            fantazee.Type = ScoreType.Fantazee;
        }

        public void OnAfterDeserialize() { }
    }
}