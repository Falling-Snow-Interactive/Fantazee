using System;
using UnityEngine;

namespace Fantazee.Spells
{
    [Serializable]
    public class SpellInstance : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private SpellData data;
        public SpellData Data => data;

        public SpellInstance(SpellData data)
        {
            this.data = data;
        }


        public void OnBeforeSerialize()
        {
            name = data.Type.ToString();
        }

        public void OnAfterDeserialize() { }
    }
}
