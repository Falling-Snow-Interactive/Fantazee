using System;
using DG.Tweening;
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
        private EventInstance healSfx;
        
        [SerializeField]
        private EventReference deathSfxRef;
        
        protected virtual void Awake()
        {
            footstepsSfx = RuntimeManager.CreateInstance(footstepsSfxRef);
            hitSfx = RuntimeManager.CreateInstance(hitSfxRef);
            if (!healSfxRef.IsNull)
            {
                healSfx = RuntimeManager.CreateInstance(healSfxRef);
            }
        }
        
        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }
        
        public void Initialize()
        {
            Debug.Log($"Player - Initialize");
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
        
        public void Damage(int damage)
        {
            Debug.Log($"Enemy: Damage {damage}");
            
            // shield first
            int rem = damage - shield.Current;
            shield.Remove(damage);

            if (rem > 0)
            {
                Health.Damage(rem);
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
        }
        
        public void Heal(int heal)
        {
            Health.Heal(heal);
            if (healSfx.isValid())
            {
                healSfx.start();
            }

            visuals.Action();
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = localRoot + hideOffset;
                return;
            }

            transform.DOLocalMove(localRoot + hideOffset, hideTime)
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