using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using UnityEngine;

namespace Fantazee.Battle
{
    [Serializable]
    public class BattleRewards
    {
        [SerializeField]
        private List<Currency> currencies;
        public List<Currency> Currencies => currencies;
    }
}