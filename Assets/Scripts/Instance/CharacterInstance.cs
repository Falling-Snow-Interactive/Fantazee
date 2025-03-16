using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.SaveLoad;
using Fantazee.Scores;
using Fantazee.Scores.Scoresheets;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Instance
{
    [Serializable]
    public class CharacterInstance
    {
        private const int BaseRolls = 3;
        
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
        private List<Die> dice;
        public List<Die> Dice => dice;

        public int Rolls { get; set; }

        [SerializeReference]
        private List<RelicInstance> relics;
        public List<RelicInstance> Relics => relics;

        public CharacterInstance(CharacterData data)
        {
            this.data = data;
            
            Rolls = BaseRolls;
            
            health = new Health(data.MaxHealth);
            wallet = new Wallet(data.Wallet);
            scoresheet = new Scoresheet(data.Scores, data.Fantazee);
            dice = Die.DefaultDice(5);
            
            relics = new List<RelicInstance>();
            RelicInstance relic = RelicFactory.Create(data.Relic, this);
            AddRelic(relic);
        }

        public CharacterInstance(CharacterSave save)
        {
            data = save.Data;
            Rolls = save.Rolls;
            health = new Health(save.Health.max)
                     {
                         current = save.Health.current,
                     };
            wallet = new Wallet(save.Wallet);
            scoresheet = new Scoresheet(save.Scoresheet);
            dice = Die.DefaultDice(5);
            relics = new List<RelicInstance>();
            foreach (RelicSave rs in save.Relics)
            {
                RelicInstance r = RelicFactory.Create(rs, this);
                AddRelic(r);
            }
        }

        public void AddRelic(RelicInstance relic)
        {
            relic.Enable();
            relics.Add(relic);
        }

        public void RemoveRelic(RelicInstance relic)
        {
            relic.Disable();
            relics.Remove(relic);
        }

        public void Clear()
        {
            while (relics.Count > 0)
            {
                RemoveRelic(relics[0]);
            }
        }
    }
}
