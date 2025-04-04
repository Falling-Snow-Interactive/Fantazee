using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    public class ChainLightningSpellInstance : SpellInstance
    {
        private ChainLightningSpellData lightningData;
        
        public ChainLightningSpellInstance(ChainLightningSpellData lightningData) : base(lightningData)
        {
            this.lightningData = lightningData;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
            {
                List<BattleEnemy> rand = new(BattleController.Instance.Enemies);
                rand.Remove(front);

                if (rand.Count > 0)
                {
                    int d0 = Mathf.Max(Mathf.RoundToInt(scoreResults.Value * lightningData.FirstEnemyMod), 1);
                    int d1 = Mathf.Max(Mathf.RoundToInt(scoreResults.Value * lightningData.SecondEnemyMod), 1);
                    
                    front.Damage(d0);
                    rand[Random.Range(0, rand.Count)].Damage(d1);
                }
                else
                {
                    front.Damage(scoreResults.Value);
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
            return BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy) 
                       ? enemy.transform.position + lightningData.HitAnim.Offset
                       : Vector3.zero;
        }
    }
}