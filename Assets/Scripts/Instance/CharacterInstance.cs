using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.Scores;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.Serialization;

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
        private Scoresheet scoresheet;
        public Scoresheet Scoresheet => scoresheet;
        
        [Header("Dice")]
        
        [SerializeField]
        private List<Die> dice; // TODO - For some reason this is getting cleared when the game exits - KD
        public List<Die> Dice => dice;

        [SerializeField]
        private int baseRolls = 3;

        public int Rolls { get; set; }

        [SerializeReference]
        private List<RelicInstance> relics;
        public List<RelicInstance> Relics => relics;

        public CharacterInstance(CharacterData data)
        {
            this.data = data;
            
            Rolls = baseRolls;
            
            health = new Health(data.MaxHealth);
            wallet = new Wallet(data.Wallet);
            scoresheet = new Scoresheet(data.Scores, data.Fantazee);
            dice = Die.DefaultDice(6);
            
            relics = new List<RelicInstance>();
            RelicInstance relic = RelicFactory.Create(data.Relic, this);
            relics.Add(relic);
        }

        public void AddRelic(RelicInstance relic)
        {
            relics.Add(relic);
        }

        public void Clear()
        {
            foreach (RelicInstance relic in relics)
            {
                relic.Clear();
            }
        }
    }
}
