using ProjectYahtzee.Boons;
using UnityEngine;

namespace ProjectYahtzee.Battle.Boons.Ui
{
    public class BoonsUi : MonoBehaviour
    {
        [Header("Prefabs")]

        [SerializeField]
        private BoonsEntryUi boonEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform content;

        public void AddBoon(Boon boon)
        {
            BoonsEntryUi entry = Instantiate(boonEntryPrefab, content);
            entry.Initialize(boon);
            boon.SetBoonsEntryUi(entry);
        }
    }
}
