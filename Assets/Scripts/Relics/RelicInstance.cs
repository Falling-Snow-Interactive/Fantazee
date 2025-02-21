using System;
using UnityEngine;

namespace Fantazee.Relics
{
    [Serializable]
    public class RelicInstance
    {
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;

        public RelicInstance(RelicData data)
        {
            this.data = data;
        }
    }
}