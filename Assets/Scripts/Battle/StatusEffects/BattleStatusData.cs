using System;
using Fantazee.StatusEffects;

namespace Fantazee.Battle.StatusEffects
{
    [Serializable]
    public class BattleStatusData
    {
        [SerializeField]
        private StatusEffectData data;
        public StatusEffectData Data => data;
        
        [Range(0, 100)]
        [SerializeField]
        private float percent = 35;
        public float Percent => percent;

        [SerializeField]
        private int turns = 2;
        public int Turns => turns;
        
        public Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new()
                                              {
                                                  { "StatusName", data.Name },
                                                  { "StatusChance", $"{percent}" },
                                                  { "StatusTurns", $"{turns}" },
                                              };

            foreach (KeyValuePair<string, string> d in Data.GetDescArgs())
            {
                args.Add(d.Key, d.Value);
            }

            return args;
        }

        public bool Roll()
        {
            return Random.Range(0, 100) <= Percent;
        }
    }
}