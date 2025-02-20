using System;
using System.Collections.Generic;
using Fantazhee.Currencies;
using UnityEngine;

namespace Fantazhee.Battle
{
    [Serializable]
    public class BattleRewards
    {
        [SerializeField]
        private List<Currency> currencies;
        public List<Currency> Currencies => currencies;
    }
}