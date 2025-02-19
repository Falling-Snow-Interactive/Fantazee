using System;
using System.Collections.Generic;
using ProjectYahtzee.Currencies;
using UnityEngine;

namespace ProjectYahtzee.Battle
{
    [Serializable]
    public class BattleRewards
    {
        [SerializeField]
        private List<Currency> currencies;
        public List<Currency> Currencies => currencies;
    }
}