using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Items.Dice.Randomizer;
using UnityEngine;

namespace Fantazee.Blacksmith.Ui
{
    public class BlacksmithSideGroupUi : MonoBehaviour
    {
        private Die die;

        [SerializeField]
        private BlacksmithUi blacksmithUi;
        
        [SerializeField]
        private List<BlacksmithSideUi> entries = new();
        public List<BlacksmithSideUi> Entries => entries;

        public void Initialize(BlacksmithUi blacksmithUi)
        {
            this.blacksmithUi = blacksmithUi;
        }
        
        public void SetDie(Die die)
        {
            this.die = die;
            for (int i = 0; i < die.DieRandomizer.Entries.Count; i++)
            {
                DieRandomizerEntry side = die.DieRandomizer.Entries[i];
                BlacksmithSideUi entry = entries[i];
                entry.Initialize(die, side, this);
            }
        }

        public void UpgradeDieValue(BlacksmithSideUi entry, int side, int change)
        {
            blacksmithUi.UpgradeValue(entry, side, change);
        }

        public void UpgradeDieWeight(BlacksmithSideUi entry, int side, int change)
        {
            blacksmithUi.UpgradeWeight(entry, side, change);
        }
    }
}
