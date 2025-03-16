using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.SaveLoad;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using Fantazee.Spells.Settings;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public abstract class ScoreInstance : ISerializationCallbackReceiver
    {
        public virtual int MaxSpells => 2;

        [HideInInspector]
        [SerializeField]
        private string name;
        
        [Header("Score")]
        
        [SerializeField]
        private ScoreData data;
        public ScoreData Data => data;

        [SerializeReference]
        private List<SpellInstance> spells;
        public List<SpellInstance> Spells => spells;

        protected ScoreInstance(ScoreData data, List<SpellData> spells)
        {
            this.data = data;

            this.spells = new List<SpellInstance>();
            foreach (SpellData spell in spells)
            {
                SpellInstance spellInstance = SpellFactory.CreateInstance(spell);
                this.spells.Add(spellInstance);
            }
            
            while (this.spells.Count < MaxSpells)
            {
                SpellInstance none = SpellFactory.CreateInstance(SpellSettings.Settings.None);
                this.spells.Add(none);
            }
        }

        protected ScoreInstance(ScoreData data, List<SpellInstance> spells)
        {
            this.data = data; 
            this.spells = spells;
            if (spells.Count < 2 && SpellSettings.Settings.TryGetSpell(SpellType.spell_none, out SpellData d))
            {
                while (spells.Count < 2)
                {
                    SpellInstance none = SpellFactory.CreateInstance(d);
                    this.spells.Add(none);
                }
            }
        }

        protected ScoreInstance(ScoreSave save)
        {
            data = save.Data;
            spells = new List<SpellInstance>();
            foreach (SpellSave ss in save.Spells)
            {
                SpellInstance spell = SpellFactory.CreateInstance(ss);
                spells.Add(spell);
            }
        }

        public abstract int Calculate(List<Die> dice);
        
        protected Dictionary<int, int> DiceToDict(List<Die> dice)
        {
            Dictionary<int, int> diceByValue = new();
            foreach (Die d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] += 1;
                }
            }
            
            return diceByValue;
        }

        public override string ToString()
        {
            string s = $"{data.Name} : ";
            foreach (SpellInstance spell in Spells)
            {
                s += spell.ToString();
            }

            return s;
        }

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}
