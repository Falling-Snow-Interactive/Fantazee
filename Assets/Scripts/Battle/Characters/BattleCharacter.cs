using System;
using Fantazee.Battle.DamageNumbers;
using Fantazee.Battle.Shields;
using Fantazee.Battle.Shields.Ui;
using FMODUnity;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using UnityEngine;

namespace Fantazee.Battle.Characters
{
    public abstract class BattleCharacter : MonoBehaviour, IDamageable, IHealable
    {
        public static event Action<BattleCharacter, int> EnemyDamaged;
        
        public event Action Damaged;

        public static event Action<BattleCharacter> Spawned;
        public static event Action<BattleCharacter> Despawned;
        
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

            EnemyDamaged?.Invoke(this, total);
            return total;
        }
        
        public void Heal(int heal)
        {
            int healed = Health.Heal(heal);
            damageNumbers.AddHealing(healed);
            visuals.Action();
        }
    }
}