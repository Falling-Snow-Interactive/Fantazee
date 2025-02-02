using System;
using System.Collections.Generic;
using ProjectYahtzee.Gameplay.Scores.Information;
using ProjectYahtzee.Gameplay.Settings;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Scores
{
    [Serializable]
    public class Score : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private ScoreType type;
        public ScoreType Type => type;

        [SerializeField]
        private int level;
        public int Level => level;

        [SerializeField]
        private float value;
        public float Value => value;

        [SerializeField]
        private float mod;
        public float Mod => mod;

        public Score(ScoreType type, int level)
        {
            this.type = type;
            this.level = level;

            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(type, out ScoreInformation information))
            {
                value = information.BaseValue;
                mod = information.BaseMod;
            }
        }

        public int Calculate(List<Dices.Dice> dice)
        {
            return ScoreCalculator.Calculate(this, dice);
        }

        public void OnBeforeSerialize()
        {
            name = $"{type} - {level}";
        }

        public void OnAfterDeserialize() { }
    }
}