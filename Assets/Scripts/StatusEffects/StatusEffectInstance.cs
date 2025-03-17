using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using UnityEngine;

namespace Fantazee.StatusEffects
{
    [Serializable]
    public abstract class StatusEffectInstance : ITurnEndCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private StatusEffectData data;

        [SerializeField]
        private bool active;
        public bool Active => active;
        
        [SerializeField]
        private BattleCharacter character;
        public BattleCharacter Character => character;

        [SerializeField]
        private int turnsRemaining;
        public int TurnsRemaining => turnsRemaining;

        protected StatusEffectInstance(StatusEffectData data, int turns, BattleCharacter character)
        {
            this.data = data;
            
            turnsRemaining = turns;
            this.character = character;
        }

        public virtual void Activate()
        {
            active = true;

            character.RegisterTurnEndReceiver(this);
        }

        public virtual void Deactivate()
        {
            active = false;
        }

        public void OnTurnEnd(Action onComplete)
        {
            turnsRemaining--;
            onComplete?.Invoke();
        }
        
        public override string ToString()
        {
            return $"{data.name} - {turnsRemaining}";
        }
    }
}