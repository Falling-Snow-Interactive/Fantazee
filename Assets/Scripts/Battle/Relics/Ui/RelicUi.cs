using System.Collections.Generic;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Relics.Ui
{
    public class RelicUi : MonoBehaviour
    {
        private List<RelicEntryUi> entries = new List<RelicEntryUi>();
        
        [FormerlySerializedAs("relicPrefab")]
        [Header("Prefabs")]
        
        [SerializeField] 
        private RelicEntryUi relicEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform content;

        public void Initialize(List<RelicInstance> relics)
        {
            foreach (RelicInstance relic in relics)
            {
                RelicEntryUi r = Instantiate(relicEntryPrefab, content);
                r.Initialize(relic);
                entries.Add(r);
            }
        }
    }
}