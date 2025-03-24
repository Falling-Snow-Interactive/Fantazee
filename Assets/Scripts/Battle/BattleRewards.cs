using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Instance;
using UnityEngine;

namespace Fantazee.Battle
{
    [Serializable]
    public class BattleRewards
    {
        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        public BattleRewards()
        {
            wallet = new Wallet();
        }
        
        public void Add(BattleRewards other)
        {
            wallet.Add(other.Wallet);
        }

        public void Grant()
        {
            Debug.Log($"Battle: Rewards Granted\n{wallet}");
            GameInstance.Current.Character.Wallet.Add(wallet);
        }

        public override string ToString()
        {
            string s = "Wallet: \n";
            s += wallet.ToString();
            return s;
        }
    }
}