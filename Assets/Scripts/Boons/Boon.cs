using System;
using ProjectYahtzee.Battle.Boons.Ui;
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
        
        // Gameplay
        public BoonsEntryUi entryUi;

        protected Boon()
        {
            name = Type.ToString();
            if (!BoonSettings.Settings.Information.TryGetInformation(Type, out information))
            {
                Debug.LogError($"Boon {Type} not found");
            }
        }
        
        public abstract string GetBonusText();
        
        public void SetBoonsEntryUi(BoonsEntryUi entry)
        {
            entryUi = entry;
        }
    }
}