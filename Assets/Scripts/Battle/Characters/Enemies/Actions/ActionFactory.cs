using System;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Attacks;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Defends;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Heals;
using Fantazee.Battle.Characters.Enemies.Actions.Instances;
using Fantazee.Battle.Characters.Enemies.Actions.Instances.Attacks;
using Fantazee.Battle.Characters.Enemies.Actions.Instances.Defends;
using Fantazee.Battle.Characters.Enemies.Actions.Instances.Heals;

namespace Fantazee.Battle.Characters.Enemies.Actions
{
    public static class ActionFactory
    {
        public static EnemyAction Create(EnemyActionData data, BattleEnemy enemy)
        {
            return data switch
                   {
                       AttackActionData a => new AttackAction(a, enemy),
                       DefendActionData d => new DefendAction(d, enemy),
                       HealActionData h => new HealAction(h, enemy),
                       _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
                   };
        }
    }
}