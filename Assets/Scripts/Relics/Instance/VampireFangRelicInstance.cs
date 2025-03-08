using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class VampireFangRelicInstance : RelicInstance
    {
        private VampireFangRelicData data;
        
        public VampireFangRelicInstance(VampireFangRelicData data, CharacterInstance character) : base(data, character)
        {
            this.data = data;
        }

        public override void Enable()
        {
            BattleController.Scored += OnScored;
        }

        public override void Disable()
        {
            BattleController.Scored -= OnScored;
        }

        private void OnScored(BattleScore battleScore)
        {
            BattleController.Instance.Player.Heal(Mathf.RoundToInt(battleScore.Calculate() * data.LifeSteal));
        }
    }
}