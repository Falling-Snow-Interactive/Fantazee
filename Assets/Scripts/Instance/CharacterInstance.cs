using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Relics;
using Fantazee.Relics.Data;
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
        
        [Header("Wallet")]
        
        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;
        
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

        [SerializeReference]
        private List<RelicInstance> relics;
        public List<RelicInstance> Relics => relics;

        public CharacterInstance(CharacterData data)
        {
            this.data = data;
            
            health = new Health(data.MaxHealth);
            wallet = new Wallet(data.Wallet);
            scoreTracker = new ScoreTracker(data);
            dice = Die.DefaultDice(6);
            
            relics = new List<RelicInstance>();
            foreach (RelicData r in data.Relics)
            {
                RelicInstance relic = RelicFactory.Create(r);
                relics.Add(relic);
            }
        }
    }
}
