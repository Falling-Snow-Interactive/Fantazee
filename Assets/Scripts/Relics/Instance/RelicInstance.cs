using System;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    [Serializable]
    public abstract class RelicInstance : ISerializationCallbackReceiver
    {
        public event Action Activated;

        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;

        protected CharacterInstance character;
        public CharacterInstance Character => character;

        public RelicInstance(RelicData data, CharacterInstance character)
        {
            this.data = data;
            this.character = character;
            Debug.Log($"Relic: {data.name} Initialized");
        }

        public void Activate()
        {
            Activated?.Invoke();
        }
        
        public abstract void Enable();
        
        public abstract void Disable();
        
        public override string ToString()
        {
            string s = $"{data.Type}";
            return s;
        }

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}