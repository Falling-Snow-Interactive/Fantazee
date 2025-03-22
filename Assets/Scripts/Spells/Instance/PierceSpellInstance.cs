using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class PierceSpellInstance : SpellInstance
    {
        private PierceSpellData data;

        public PierceSpellInstance(PierceSpellData data) : base(data)
        {
            this.data = data;
        }
        
        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.Enemies.Count > 0)
            {
                if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
                {
                    int index = BattleController.Instance.Enemies.IndexOf(front);
                    index++;
                    if (BattleController.Instance.Enemies.Count > index)
                    {
                        BattleEnemy second = BattleController.Instance.Enemies[index];
                        front.Damage(Mathf.Max(Mathf.RoundToInt(scoreResults.Value * data.FirstEnemyMod), 1));
                        second.Damage(Mathf.Max(Mathf.RoundToInt(scoreResults.Value * data.SecondEnemyMod), 1));
                    }
                    else
                    {
                        front.Damage(scoreResults.Value);
                    }
                }
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
            return BattleController.Instance.Enemies.Count switch
                   {
                       1 => BattleController.Instance.Enemies[0].transform.position + data.HitAnim.Offset,
                       > 1 => BattleController.Instance.Enemies[1].transform.position + data.HitAnim.Offset,
                       _ => Vector3.zero
                   };
        }
    }
}