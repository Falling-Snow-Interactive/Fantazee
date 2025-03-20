using System;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Intentions;

namespace Fantazee.Battle.Characters.Enemies.Actions
{
    public abstract class EnemyAction
    {
        public abstract ActionType Type { get; }
        public abstract Intention Intention { get; }
        
        public BattleEnemy Source { get; set; }
        public BattleCharacter Target { get; set; }

        private EnemyActionData data;

        public EnemyAction(EnemyActionData data, BattleEnemy source, BattleCharacter target)
        {
            Source = source;
            Target = target;
            
            this.data = data;
        }

        public abstract void Perform(Action onComplete);
    }
}