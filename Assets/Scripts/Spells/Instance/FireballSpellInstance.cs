using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class FireballSpellInstance : SpellInstance
    {
        private FireballSpellData data;
        
        public FireballSpellInstance(FireballSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            int d = Mathf.Max(Mathf.RoundToInt(scoreResults.Value * data.DamageMod));
            foreach (BattleEnemy enemy in BattleController.Instance.Enemies)
            {
                enemy.Damage(d);

                if (data.StatusEffect.Percent.Roll())
                {
                    enemy.AddStatusEffect(data.StatusEffect.Data, data.StatusEffect.Turns);
                }
            }
            
            onComplete?.Invoke();
        }
        
        protected override void OnCast()
        {
            BattleController.Instance.Player.Visuals.Action();
            base.OnCast();
        }

        protected override Vector3 GetHitPos()
        {
            Vector3 hitPos = Vector3.zero;
            foreach (BattleEnemy e in BattleController.Instance.Enemies)
            {
                hitPos += e.transform.position + data.HitAnim.Offset;
            }
            return hitPos / BattleController.Instance.Enemies.Count;
        }
    }
}