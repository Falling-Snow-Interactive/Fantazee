using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using UnityEngine;

namespace Fantazee.Battle.Help.Ui
{
    public class ScoreHelpUi : MonoBehaviour
    {
        [SerializeField]
        private HelpScoreEntry helpEntryPrefab;
        
        private readonly List<HelpScoreEntry> entries = new();

        [SerializeField]
        private Transform content;

        private void Start()
        {
            foreach (ScoreInstance score in GameInstance.Current.Character.Scoresheet.Scores)
            {
                HelpScoreEntry entry = Instantiate(helpEntryPrefab, content);
                entry.Initialize(score);
            }
        }
    }
}