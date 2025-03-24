using System;
using System.Collections;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Intentions;
using Fantazee.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Battle.Characters.Enemies.Actions.Instances
{
    public class SummonAction : EnemyAction
    {
        public override ActionType Type => ActionType.action_03_summon;
        
        private readonly Intention intention;
        public override Intention Intention => intention;

        private readonly SummonActionData data;
        
        public SummonAction(SummonActionData data, BattleEnemy source) : base(data, source)
        {
            this.data = data;
            intention = new Intention(IntentionType.intention_03_special, data.Summons.Random());
        }
        
        public override void Perform(Action onComplete)
        {
            Source.StartCoroutine(SummonSequence(onComplete));
        }

        protected override Vector3 GetHitPos()
        {
            return Source.transform.position;
        }
        
        private IEnumerator SummonSequence(Action onComplete = null)
        {
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                Source.Visuals.Action();
                PlayCastFx();
            }
            
            DoSummon();
            if (data.HitAnim.HasHit)
            {
                PlayHitFx();
            }

            yield return new WaitForSeconds(0.75f);
            onComplete?.Invoke();
        }

        private void DoSummon()
        {
            int spawns = data.Summons.Random();
            for (int i = 0; i < spawns; i++)
            {
                EnemyData enemyData = data.SummonPool[Random.Range(0, data.SummonPool.Count)];
                BattleEnemy enemy = BattleController.Instance.SpawnEnemy(enemyData);
                enemy.Show(null);
            }
        }
    }
}