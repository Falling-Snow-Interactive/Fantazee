using System;
using System.Collections.Generic;
using Fantazee.StatusEffects;
using UnityEngine;

namespace Fantazee.Battle.StatusEffects
{
    [Serializable]
    public class BattleStatusData
    {
        [SerializeField]
        private StatusEffectData data;
        public StatusEffectData Data => data;
        
        [Range(0f, 1f)]
        [SerializeField]
        private float chance = 0.5f;
        public float Chance => chance;

        [SerializeField]
        private int turns = 2;
        public int Turns => turns;
        
        public Dictionary<string, string> GetDescArgs()
        {
            float percent = chance * 100f;

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
    }
}