using System;
using System.Collections;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Defends;
using Fantazee.Battle.Characters.Intentions;
using UnityEngine;

namespace Fantazee.Battle.Characters.Enemies.Actions.Instances.Defends
{
    public class DefendAction : EnemyAction
    {
        public override ActionType Type => ActionType.action_02_heal;
        public override Intention Intention { get; }
        
        private readonly DefendActionData data;
        
        public DefendAction(DefendActionData data, BattleEnemy source) : base(data, source)
        {
            Intention = new Intention(IntentionType.intention_01_defend, data.Amount.Random());
            
            this.data = data;
        }
        
        public override void Perform(Action onComplete)
        {
            Source.StartCoroutine(DefendSequence(onComplete));
        }

        private void DoDefend()
        {
            switch (data.Target)
            {
                case DefendActionData.TargetType.Self:
                    Source.Shield.Add(Intention.Amount);
                    break;
                case DefendActionData.TargetType.RandomEnemy:
                    DefendRandom();
                    break;
                case DefendActionData.TargetType.AllEnemies:
                    DefendAll();
                    break;
                case DefendActionData.TargetType.FrontEnemy:
                    DefendFront();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            BattleController.Instance.Player.Damage(Intention.Amount); // TODO do this properly.
        }

        private void DefendRandom()
        {
            BattleEnemy enemy = BattleController.Instance.Enemies[UnityEngine.Random.Range(0, BattleController.Instance.Enemies.Count)];
            enemy.Shield.Add(Intention.Amount);
        }

        private void DefendAll()
        {
            foreach (BattleEnemy e in BattleController.Instance.Enemies)
            {
                e.Shield.Add(Intention.Amount);
            }
        }

        private void DefendFront()
        {
            BattleController.Instance.Enemies[0].Shield.Add(Intention.Amount);
        }
        
        private IEnumerator DefendSequence(Action onComplete = null)
        {
            bool ready;
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                ready = false;
                BattleController.Instance.StartCoroutine(CastSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }
            
            DoDefend();
            if (data.HitAnim.HasHit)
            {
                HitSequence();
            }

            onComplete?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return Source.transform.position;
        }
    }
}