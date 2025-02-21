using System;
using UnityEngine;

namespace Fantazee.Spells
{
    [Serializable]
    public class SpellInstance
    {
        [SerializeField]
        private SpellData data;
        public SpellData Data => data;

        public SpellInstance(SpellData data)
        {
            this.data = data;
        }
    }
}
