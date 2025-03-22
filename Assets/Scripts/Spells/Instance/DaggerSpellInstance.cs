using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class DaggerSpellInstance : SpellInstance
    {
        private DaggerSpellData data;

        private Vector3? cachedPosition;
        
        public DaggerSpellInstance(DaggerSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                cachedPosition = enemy.transform.position;
                enemy.Damage(scoreResults.Value);
            }
            
            onComplete?.Invoke();
        }
        
        protected override void OnCast()
        {
            BattleController.Instance.Player.Visuals.Attack();
            base.OnCast();
        }

        protected override Vector3 GetHitPos()
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                return enemy.transform.position + data.HitAnim.Offset;
            }
            
            return cachedPosition ?? Vector3.zero;
        }
    }
}