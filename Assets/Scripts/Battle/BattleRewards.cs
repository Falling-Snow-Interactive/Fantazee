using System;
using System.Collections.Generic;
using Fantahzee.Currencies;
using UnityEngine;

namespace Fantahzee.Battle
{
    [Serializable]
    public class BattleRewards
    {
        [SerializeField]
        private List<Currency> currencies;
        public List<Currency> Currencies => currencies;
    }
}