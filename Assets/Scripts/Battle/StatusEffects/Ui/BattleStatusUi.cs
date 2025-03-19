using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Battle.StatusEffects.Ui
{
    public class BattleStatusUi : MonoBehaviour
    {
        [SerializeField]
        private BattleStatusEntryUi battleStatusEntryPrefab;

        [SerializeField]
        private Transform content;
        
        [SerializeField]
        private List<BattleStatusEntryUi> battleStatusEntries = new();

        public void AddStatus(BattleStatusEffect statusEffect)
        {
            BattleStatusEntryUi entry = Instantiate(battleStatusEntryPrefab, content);
            entry.Initialize(statusEffect);
            
            battleStatusEntries.Add(entry);
        }

        public void RemoveStatus(BattleStatusEffect statusEffect)
        {
            foreach (BattleStatusEntryUi battleStatusEntry in new List<BattleStatusEntryUi>(battleStatusEntries))
            {
                if (battleStatusEntry.StatusEffect.Data.Type == statusEffect.Data.Type)
                {
                    battleStatusEntries.Remove(battleStatusEntry);
                    Destroy(battleStatusEntry.gameObject);
                    break;
                }
            }
        }
    }
}