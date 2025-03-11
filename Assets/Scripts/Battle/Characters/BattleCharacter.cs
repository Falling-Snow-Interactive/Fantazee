using System;
using DG.Tweening;
using Fantazee.Battle.DamageNumbers;
using Fantazee.Battle.Shields;
using Fantazee.Battle.Shields.Ui;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
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
        
        [SerializeField]
        private Vector3 localRoot;
        
        [Header("Character")]
        
        [Header("Visuals")]

        [SerializeField]
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;

        [SerializeField]
        private float size = 1f;
        public float Size => size;
        
        [Header("Health")]

        [SerializeField]
        private HealthUi healthUi;
        
        public abstract Health Health { get; }

        [SerializeField]
        private DamageNumbersController damageNumbers;
        
        [Header("Shield")]
        
        [SerializeField]
        private Shield shield;
        public Shield Shield => shield;

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

        [FormerlySerializedAs("footsteps")]
        [Header("Audio")]

        [SerializeField]
        private EventReference footstepsSfxRef;
        private EventInstance footstepsSfx;

        [SerializeField]
        private EventReference hitSfxRef;
        private EventInstance hitSfx;

        [SerializeField]
        private EventReference healSfxRef;
        
        [SerializeField]
        private EventReference deathSfxRef;
        
        protected virtual void Awake()
        {
            footstepsSfx = RuntimeManager.CreateInstance(footstepsSfxRef);
        }
        
        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }
        
        public void Initialize()
        {
            Debug.Log($"Character: {name} - Initialize");
            localRoot = transform.localPosition;
            
            healthUi.Initialize(Health);
            Spawned?.Invoke(this);

            shieldUi.Initialize(shield);
        }

        public void StartTurn()
        {
            visuals.Idle();
            shield.Clear();
        }
        
        public int Damage(int damage)
        {
            Debug.Log($"Enemy: Damage {damage}");

            int total = 0;
            
            // shield first
            int dealt = shield.Remove(damage);
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

            hitSfx.start();
            visuals.Hit(() =>
                        {
                            if (Health.IsDead)
                            {
                                RuntimeManager.PlayOneShot(deathSfxRef);
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
                     .OnPlay(() => footstepsSfx.start())
                     .OnComplete(() =>
                                 {
                                     footstepsSfx.stop(STOP_MODE.IMMEDIATE);
                                     onComplete?.Invoke();
                                 });
        }
        
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(size, 0.2f, 0));
        }
    }
}