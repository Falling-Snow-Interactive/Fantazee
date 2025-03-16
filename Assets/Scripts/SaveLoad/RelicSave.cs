using System;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class RelicSave
    {
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;
        
        public RelicSave(RelicInstance instance)
        {
            data = instance.Data;
        }
    }
}