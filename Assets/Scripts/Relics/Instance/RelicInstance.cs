using System;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    [Serializable]
    public class RelicInstance
    {
        public event Action Activated;
        
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;

        private CharacterInstance character;
        public CharacterInstance Character => character;

        public RelicInstance(RelicData data, CharacterInstance character)
        {
            this.data = data;
            Debug.Log($"Relic: Created {data.name}");
        }

        public void Activate()
        {
            Activated?.Invoke();
        }
    }
}