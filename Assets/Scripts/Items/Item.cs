using System;
using Fsi.Roguelite.Upgradable;

namespace ProjectYahtzee.Items
{
    [Serializable]
    public abstract class Item : IUpgradable
    {
        public abstract void Upgrade();
    }
}