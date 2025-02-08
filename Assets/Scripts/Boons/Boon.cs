using System;
using ProjectYahtzee.Boons.Information;
using ProjectYahtzee.Boons.Settings;
using UnityEngine;

namespace ProjectYahtzee.Boons
{
    [Serializable]
    public abstract class Boon
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public abstract BoonType Type { get; }

        private BoonInformation information;
        public BoonInformation Information => information;

        protected Boon()
        {
            name = Type.ToString();
            if (!BoonSettings.Settings.Information.TryGetInformation(Type, out information))
            {
                Debug.LogError($"Boon {Type} not found");
            }
        }

        public virtual float GetValue() => 0;
        public virtual float GetModifier() => 0;
    }
}