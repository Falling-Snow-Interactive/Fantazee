using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Currencies;
using Fantazee.Instance;
using Fantazee.Relics.Instance;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class CharacterSave
    {
        [SerializeField]
        private CharacterData data;
        public CharacterData Data => data;

        [SerializeField]
        private Health health;
        public Health Health => health;

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        [SerializeField]
        private ScoresheetSave scoresheet;
        public ScoresheetSave Scoresheet => scoresheet;

        [SerializeField]
        private int rolls;
        public int Rolls => rolls;

        [SerializeField]
        private List<RelicSave> relics;
        public List<RelicSave> Relics => relics;
        
        public CharacterSave(CharacterInstance instance)
        {
            data = instance.Data;
            health = instance.Health;
            wallet = new Wallet(instance.Wallet);
            scoresheet = new ScoresheetSave(instance.Scoresheet);
            rolls = instance.Rolls;
            relics = new List<RelicSave>();
            foreach (RelicInstance relic in instance.Relics)
            {
                RelicSave rs = new RelicSave(relic);
                relics.Add(rs);
            }
        }
    }
}