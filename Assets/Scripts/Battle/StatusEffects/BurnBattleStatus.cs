using System;
using System.Collections;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Battle.StatusEffects
{
    public class BurnBattleStatus : BattleStatusEffect, ITurnStartCallbackReceiver
    {
        private readonly BurnStatusData data;

        private readonly EventInstance? burnSfx;
        
        public BurnBattleStatus(BurnStatusData data, int turns, BattleCharacter character) 
            : base(data, turns, character)
        {
            this.data = data;
            if (!data.BurnSfx.IsNull)
            {
                burnSfx = RuntimeManager.CreateInstance(data.BurnSfx);
            }
        }

        public override void Activate()
        {
            base.Activate();
            Character.RegisterTurnStartReceiver(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Character.UnregisterTurnStartReceiver(this);
        }

        public void OnTurnStart(Action onComplete)
        {
            BattleController.Instance.StartCoroutine(BurnSequence(onComplete));
        }

        private IEnumerator BurnSequence(Action onComplete)
        {
            Character.Damage(TurnsRemaining);
            if (data.BurnEffectPrefab)
            {
                GameObject.Instantiate(data.BurnEffectPrefab, Character.transform);
            }

            burnSfx?.start();
            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
    }
}