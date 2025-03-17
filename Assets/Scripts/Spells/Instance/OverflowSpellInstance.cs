using System;
using System.Collections;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class OverflowSpellInstance : SpellInstance
    {
        private OverflowSpellData overflowData;
        
        public OverflowSpellInstance(OverflowSpellData data) : base(data)
        {
            overflowData = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                int total = scoreResults.Value;
                int d = enemy.Damage(scoreResults.Value);
                int rem = total - d;

                BattleController.Instance.StartCoroutine(OverflowSequence(rem, onComplete));
            }
        }

        private IEnumerator OverflowSequence(int remainingDamage, Action onComplete = null)
        {
            while (remainingDamage > 0 && BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
            {
                yield return new WaitForSeconds(overflowData.SpreadTime);
                
                int o = front.Damage(remainingDamage);
                remainingDamage -= o;
                    
                if (overflowData.HitAnim.Vfx)
                {
                    Object.Instantiate(overflowData.HitAnim.Vfx,
                                       front.transform.position + overflowData.HitAnim.Offset,
                                       front.transform.rotation);
                }
            }
            
            onComplete?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy) 
                       ? enemy.transform.position + overflowData.HitAnim.Offset
                       : Vector3.zero;
        }
    }
}