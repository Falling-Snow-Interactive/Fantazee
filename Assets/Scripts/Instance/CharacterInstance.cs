using System;
using Fantazee.Characters;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.Scores.Scoresheets;
using Fsi.Gameplay.Healths;

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
        
        // [Header("Dice")]
        //
        // [SerializeField]
        // private List<Die> dice;
        // public List<Die> Dice => dice;

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
            // dice = Die.DefaultDice(5);
            
            relics = new List<RelicInstance>();
            RelicInstance relic = RelicFactory.Create(data.Relic, this);
            AddRelic(relic);
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

        public override string ToString()
        {
            return data.ToString();
        }
    }
}
