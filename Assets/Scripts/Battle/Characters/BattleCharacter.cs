using System;
using DG.Tweening;
using Fantazee.Battle.DamageNumbers;
using Fantazee.Battle.Shields;
using Fantazee.Battle.Shields.Ui;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Serialization;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Battle.Characters
{
    public abstract class BattleCharacter : MonoBehaviour, IDamageable, IHealable
    {
        public event Action Damaged;

        public static event Action<BattleCharacter> Spawned;
        public static event Action<BattleCharacter> Despawned;
        
        private Vector3 localRoot;
        
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
        
        [Header("Animations")]

        [Header("Hide/Show")]

        [SerializeField]
        private Vector3 hideOffset = Vector3.zero;

        [SerializeField]
        private float hideTime = 0.5f;

        [SerializeField]
        private Ease hideEase = Ease.Linear;

        [SerializeField]
        private float showTime = 0.5f;
        
        [SerializeField]
        private Ease showEase = Ease.Linear;
        
        // Audio
        protected abstract EventReference DeathSfxRef { get; }
        protected abstract EventReference EnterSfxRef { get; }
        
        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }

        protected void Initialize()
        {
            Debug.Log($"Character: {name} - Initialize");
            localRoot = transform.localPosition;
            
            healthUi.Initialize(Health);
            Spawned?.Invoke(this);

            Shield = new Shield();
            shieldUi.Initialize(Shield);
        }

        protected void SpawnVisuals(GameplayCharacterVisuals prefab)
        {
            visuals = Instantiate(prefab, transform);
        }

        public void StartTurn()
        {
            visuals.Idle();
            Shield.Clear();
        }
        
        public int Damage(int damage)
        {
            Debug.Log($"Enemy: Damage {damage}");

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
                                RuntimeManager.PlayOneShot(DeathSfxRef);
                                visuals.Death(() =>
                                              {
                                                  Destroy(gameObject);
                                              });
                            }
                        });

            return total;
        }
        
        public void Heal(int heal)
        {
            int healed = Health.Heal(heal);
            damageNumbers.AddHealing(healed);
            visuals.Action();
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = hideOffset;
                return;
            }

            transform.DOLocalMove(hideOffset, hideTime)
                     .SetEase(hideEase)
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = localRoot;
                return;
            }
            
            transform.DOLocalMove(localRoot, showTime)
                     .SetEase(showEase)                     
                     .SetDelay(delay)
                     .OnPlay(() => RuntimeManager.PlayOneShot(EnterSfxRef))
                     .OnComplete(() =>
                                 {
                                     onComplete?.Invoke();
                                 });
        }
    }
}