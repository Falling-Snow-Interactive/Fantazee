using System;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace Fantazee.Battle.Characters.Enemies.Actions.Randomizer
{
    [Serializable]
    public class ActionRandomizerEntry : RandomizerEntry<EnemyActionData>, ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private int weight = 1;
        public override int Weight
        {
            get => weight;
            set => weight = value;
        }

        [SerializeField]
        private EnemyActionData action;
        public override EnemyActionData Value
        {
            get => action;
            set => action = value;
        }

        public override string ToString()
        {
            if (!action)
            {
                return "no_action";
            }
            
            string s = $"{action.name}: {weight}";
            return s;
        }

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}