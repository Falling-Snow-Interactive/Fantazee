using System;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class SpellSave
    {
        [SerializeField]
        private SpellData data;
        public SpellData Data => data;
        
        public SpellSave(SpellInstance instance)
        {
            data = instance.Data;
        }
    }
}