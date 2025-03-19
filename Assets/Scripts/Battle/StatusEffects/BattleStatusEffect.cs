using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects;
using UnityEngine;

namespace Fantazee.Battle.StatusEffects
{
    [Serializable]
    public abstract class BattleStatusEffect : ITurnEndCallbackReceiver
    {
        public event Action Changed;
        
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private StatusEffectData data;
        public StatusEffectData Data => data;

        [SerializeField]
        private bool active;
        public bool Active => active;
        
        [SerializeField]
        private BattleCharacter character;
        public BattleCharacter Character => character;

        [SerializeField]
        private int turnsRemaining;
        public int TurnsRemaining => turnsRemaining;

        protected BattleStatusEffect(StatusEffectData data, int turns, BattleCharacter character)
        {
            this.data = data;
            
            turnsRemaining = turns;
            this.character = character;
        }

        public void Add(int amount)
        {
            turnsRemaining += amount;
            Changed?.Invoke();
        }
        
        public virtual void Activate()
        {
            active = true;
            character.RegisterTurnEndReceiver(this);
        }

        public virtual void Deactivate()
        {
            active = false;
            character.UnregisterTurnEndReceiver(this);
        }

        public void OnTurnEnd(Action onComplete)
        {
            turnsRemaining--;
            Changed?.Invoke();

            if (turnsRemaining == 0)
            {
                character.RemoveStatusEffect(this);
            }
            
            onComplete?.Invoke();
        }
        
        public override string ToString()
        {
            return $"{data.name} - {turnsRemaining}";
        }
    }
}