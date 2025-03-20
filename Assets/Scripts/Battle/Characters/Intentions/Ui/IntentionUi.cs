using System.Collections.Generic;
using Fantazee.Battle.Characters.Enemies;
using UnityEngine;

namespace Fantazee.Battle.Characters.Intentions.Ui
{
    public class IntentionUi : MonoBehaviour
    {
        [SerializeField]
        private BattleEnemy battleEnemy; 
        
        [SerializeField]
        private IntentionEntryUi entryPrefab;
        
        [SerializeField]
        private Transform entryContainer;

        private readonly List<IntentionEntryUi> entries = new();

        private void OnEnable()
        {
            battleEnemy.IntentionsUpdated += OnIntentionsUpdated;
        }

        private void OnDisable()
        {
            battleEnemy.IntentionsUpdated -= OnIntentionsUpdated;
        }

        private void OnIntentionsUpdated()
        {
            ClearIntentions();
            foreach (Intention intention in battleEnemy.Intentions)
            {
                AddIntention(intention);
            }
        }

        private void AddIntention(Intention intention)
        {
            IntentionEntryUi entry = Instantiate(entryPrefab, entryContainer);
            entry.Initialize(intention);
            entries.Add(entry);
        }
        
        private void ClearIntentions()
        {
            foreach (IntentionEntryUi entry in entries)
            {
                if (entry != null)
                {
                    Destroy(entry.gameObject);
                }
            }
            entries.Clear();
        }
    }
}
