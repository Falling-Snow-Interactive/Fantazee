using System;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics
{
    [Serializable]
    public class RelicInstance
    {
        public event Action Activated;
        
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;

        public RelicInstance(RelicData data)
        {
            this.data = data;
        }

        public void Activate()
        {
            Activated?.Invoke();
        }
    }
}