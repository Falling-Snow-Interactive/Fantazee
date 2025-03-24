using System;
using System.Collections;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Heals;
using Fantazee.Battle.Characters.Intentions;

namespace Fantazee.Battle.Characters.Enemies.Actions.Instances.Heals
{
    public class HealAction : EnemyAction
    {
        public override ActionType Type => ActionType.action_02_heal;
        public override Intention Intention { get; }

        private readonly HealActionData data;

        public HealAction(HealActionData data, BattleEnemy source) : base(data, source)
        {
            Intention = new Intention(IntentionType.intention_02_healing, data.Amount.Random());

            this.data = data;
        }
        
        public override void Perform(Action onComplete)
        {
            Source.StartCoroutine(HealSequence(onComplete));
        }

        private void DoHeal()
        {
            switch (data.Target)
            {
                case HealActionData.TargetType.Self:
                    Source.Heal(Intention.Amount);
                    break;
                case HealActionData.TargetType.RandomEnemy:
                    HealRandom();
                    break;
                case HealActionData.TargetType.AllEnemies:
                    HealAll();
                    break;
                case HealActionData.TargetType.FrontEnemy:
                    HealFront();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HealRandom()
        {
            BattleEnemy enemy = BattleController.Instance.Enemies[Random.Range(0, BattleController.Instance.Enemies.Count)];
            enemy.Heal(Intention.Amount);
        }

        private void HealAll()
        {
            foreach (BattleEnemy e in BattleController.Instance.Enemies)
            {
                e.Heal(Intention.Amount);
            }
        }

        private void HealFront()
        {
            BattleController.Instance.Enemies[0].Heal(Intention.Amount);
        }
        
        private IEnumerator HealSequence(Action onComplete = null)
        {
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                Source.Visuals.Action();
                PlayCastFx();
            }
            
            DoHeal();
            if (data.HitAnim.HasHit)
            {
                PlayHitFx();
            }

            onComplete?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return Source.transform.position;
        }
    }
}