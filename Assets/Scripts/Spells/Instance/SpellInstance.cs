using System;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class SpellInstance
    {
        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        public SpellInstance(SpellData data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            return data.Name;
        }
    }
}