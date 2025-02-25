using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Dice;
using Fantazee.Scores;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Instance
{
    [Serializable]
    public class CharacterInstance
    {
        [SerializeField]
        private CharacterData data;
        public CharacterData Data => data;
        
        [SerializeField]
        private Health health;
        public Health Health => health;
        
        [Header("Scores")]
        
        [SerializeField]
        private ScoreTracker scoreTracker;
        public ScoreTracker ScoreTracker => scoreTracker;
        
        [Header("Dice")]
        
        [SerializeField]
        private List<Die> dice; // TODO - For some reason this is getting cleared when the game exits - KD
        public List<Die> Dice => dice;
        
        [SerializeField]
        private int rolls = 3;
        public int Rolls => rolls;

        public CharacterInstance(CharacterData data)
        {
            this.data = data;
            
            health = new Health(data.MaxHealth);
            scoreTracker = new ScoreTracker(data);
            dice = Die.DefaultDice(6);
        }
    }
}
