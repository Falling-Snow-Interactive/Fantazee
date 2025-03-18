using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.DamageNumbers;
using Fantazee.Battle.Shields;
using Fantazee.Battle.Shields.Ui;
using Fantazee.Battle.StatusEffects;
using Fantazee.Battle.StatusEffects.Ui;
using Fantazee.Scores;
using Fantazee.StatusEffects;
using Fantazee.StatusEffects.Settings;
using FMODUnity;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using UnityEngine;

namespace Fantazee.Battle.Characters
{
    public abstract class BattleCharacter : MonoBehaviour, IHealable
    {
        public static event Action<BattleCharacter, int> CharacterDamaged;
        
        public event Action Damaged;

        public static event Action<BattleCharacter> Spawned;
        public static event Action<BattleCharacter> Despawned;

        public event Action TurnStarted;
        public event Action TurnEnded;
        
        // Callbacks
        private Action onTurnCompleteCallback;
        
        // Visuals
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;
        
        [Header("Health")]

        [SerializeField]
        private HealthUi healthUi;
        
        public abstract Health Health { get; }
        public Shield Shield { get; private set; }

        [SerializeField]
        private DamageNumbersController damageNumbers;

        [SerializeField]
        private ShieldUi shieldUi;
        public ShieldUi ShieldUi => shieldUi;
        
        [SerializeField]
        private BattleStatusUi statusEffectUi;
        private List<BattleStatusEffect> statusEffects = new();
        
        // Audio
        protected abstract EventReference DeathSfxRef { get; }
        protected abstract EventReference EnterSfxRef { get; }
        
        // Callback registers
        private List<ITurnStartCallbackReceiver> turnStartReceivers = new();
        private List<ITurnEndCallbackReceiver> turnEndReceivers = new();
        private List<ITakingDamageCallback> takingDamageReceivers = new();
        protected List<IScoreCallbackReceiver> scoreReceivers = new();
        
        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }

        protected void Initialize()
        {
            Debug.Log($"Character: {name} - Initialize");
            healthUi.Initialize(Health);
            Spawned?.Invoke(this);

            Shield = new Shield();
            shieldUi.Initialize(Shield);
        }

        protected void SpawnVisuals(GameplayCharacterVisuals prefab)
        {
            visuals = Instantiate(prefab, transform);
        }

        public void StartTurn(Action onTurnComplete)
        {
            onTurnCompleteCallback = onTurnComplete;
            
            visuals.Idle();
            Shield.Clear();

            StartCoroutine(CallTurnStartReceivers(() =>
                                                  {
                                                      TurnStarted?.Invoke();
                                                      CharacterStartTurn();
                                                  }));
        }

        public virtual void EndTurn()
        {
            StartCoroutine(CallTurnEndReceivers(() =>
                                                {
                                                    TurnEnded?.Invoke();
                                                    onTurnCompleteCallback?.Invoke();
                                                }));
        }

        protected abstract void CharacterStartTurn();
        
        public int Damage(int damage)
        {
            damage = CallTakingDamageReceivers(damage); 
            int total = 0;
           // shield first
           int dealt = Shield.Remove(damage);
           total += dealt;
           if (dealt > 0)
           {
               damageNumbers.AddShield(dealt);
           }

           int rem = damage - dealt;
           if (rem > 0)
           {
               int damaged = Health.Damage(rem);
               total += damaged;
               damageNumbers.AddDamage(damaged);
           }

           visuals.Hit(() =>
           {
               if (Health.IsDead)
               {
                   RuntimeManager
                       .PlayOneShot(DeathSfxRef);
                   visuals.Death(() =>
                   {
                       Destroy(gameObject);
                   });
               }
           });

           Debug.Log($"Enemy: Damage {damage}");
           CharacterDamaged?.Invoke(this, total);
           return total;
        }
        
        public void Heal(int heal)
        {
            int healed = Health.Heal(heal);
            damageNumbers.AddHealing(healed);
            visuals.Action();
        }

        public void AddStatusEffect(StatusEffectType type, int turns)
        {
            Debug.Log($"BattleCharacter: Status Effect {type} - {turns} turns");

            bool found = false;
            foreach (BattleStatusEffect statusEffect in statusEffects)
            {
                if (statusEffect.Data.Type == type)
                {
                    statusEffect.Add(turns);
                    found = true;
                }
            }
            
            if (!found && StatusEffectSettings.Settings.TryGetStatus(type, out StatusEffectData data))
            {
                BattleStatusEffect statusEffect = BattleStatusFactory.CreateInstance(data, turns, this);
                statusEffect.Activate();
                statusEffectUi.AddStatus(statusEffect);
                statusEffects.Add(statusEffect);
            }
        }

        public void RemoveStatusEffect(BattleStatusEffect statusEffect)
        {
            statusEffect.Deactivate();
            statusEffectUi.RemoveStatus(statusEffect);
            statusEffects.Remove(statusEffect);
        }
        
        // Callback Registers
        #region Callback Registers

        #region Turn Start
        public void RegisterTurnStartReceiver(ITurnStartCallbackReceiver callbackReceiver)
        {
            turnStartReceivers.Add(callbackReceiver);
        }

        public void UnregisterTurnStartReceiver(ITurnStartCallbackReceiver callbackReceiver)
        {
            turnStartReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallTurnStartReceivers(Action onComplete)
        {
            foreach (ITurnStartCallbackReceiver receiver in turnStartReceivers)
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnTurnStart(() =>
                                     {
                                         if (Health.IsDead)
                                         {
                                             EndTurn();
                                         }
                                         else
                                         {
                                             ready = true;
                                         }
                                     });
                yield return new WaitUntil(() => ready);
            }

            onComplete?.Invoke();
        }
        #endregion

        #region Turn End
        public void RegisterTurnEndReceiver(ITurnEndCallbackReceiver callbackReceiver)
        {
            turnEndReceivers.Add(callbackReceiver);
        }

        public void UnregisterTurnEndReceiver(ITurnEndCallbackReceiver callbackReceiver)
        {
            turnEndReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallTurnEndReceivers(Action onComplete)
        {
            foreach (ITurnEndCallbackReceiver receiver in new List<ITurnEndCallbackReceiver>(turnEndReceivers))
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnTurnEnd(() => ready = true);
                yield return new WaitUntil(() => ready);
            }

            yield return new WaitForEndOfFrame();
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Take Damage
        public void RegisterTakingDamageReceiver(ITakingDamageCallback callbackReceiver)
        {
            takingDamageReceivers.Add(callbackReceiver);
        }

        public void UnregisterTakingDamageReceiver(ITakingDamageCallback callbackReceiver)
        {
            takingDamageReceivers.Remove(callbackReceiver);
        }

        private int CallTakingDamageReceivers(int damage)
        {
            int d = damage;
            foreach (ITakingDamageCallback receiver in new List<ITakingDamageCallback>(takingDamageReceivers))
            {
                if (receiver == null)
                {
                    continue;
                }

                d = receiver.OnTakingDamage(damage);
            }

            return d;
        }
        
        #endregion

        #region Score Callback Receivers

        public void RegisterScoreReceiver(IScoreCallbackReceiver callbackReceiver)
        {
            scoreReceivers.Add(callbackReceiver);
        }

        public void UnregisterScoreReceiver(IScoreCallbackReceiver callbackReceiver)
        {
            scoreReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallScoreReceivers(ScoreResults scoreResults, Action<ScoreResults> onComplete)
        {
            foreach (IScoreCallbackReceiver receiver in scoreReceivers)
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnScore(scoreResults, sr =>
                                               {
                                                   scoreResults = sr;
                                                   ready = true;
                                               });
                yield return new WaitUntil(() => ready);
            }
            onComplete?.Invoke(scoreResults);
        }

        #endregion
        
        #endregion
    }
}