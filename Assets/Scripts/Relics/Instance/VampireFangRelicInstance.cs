using Fantazee.Battle;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class VampireFangRelicInstance : RelicInstance
    {
        private readonly VampireFangRelicData data;
        
        public VampireFangRelicInstance(VampireFangRelicData data, CharacterInstance character) : base(data, character)
        {
            this.data = data;
        }

        public override void Enable()
        {
            BattleEnemy.CharacterDamaged += OnDamaged;
        }

        public override void Disable()
        {
            BattleEnemy.CharacterDamaged-= OnDamaged;
        }

        private void OnDamaged(BattleCharacter battleCharacter, int damage)
        {
            if (battleCharacter is not BattleEnemy enemy)
            {
                return;
            }
            
            int adjustedAmount = Mathf.RoundToInt(Mathf.Max(data.MinLifeStealAmount, damage * data.LifeSteal));
            Debug.Log($"Vampire Fang: Healing {adjustedAmount}");
            BattleController.Instance.Player.Heal(adjustedAmount);
        }
    }
}