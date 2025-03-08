using System.Collections.Generic;
using Fantazee.Instance;
using UnityEngine;

namespace Fantazee.Battle.Help.Ui
{
    public class ScoreHelpUi : MonoBehaviour
    {
        [SerializeField]
        private List<HelpScoreEntry> entries = new List<HelpScoreEntry>();

        [SerializeField]
        private HelpScoreEntry fantazeeEntry;

        private void Start()
        {
            for (int i = 0; i < entries.Count; i++)
            {
                HelpScoreEntry entry = entries[i];

                if (i < GameInstance.Current.Character.Scoresheet.Scores.Count)
                {
                    entry.gameObject.SetActive(true);
                    entry.Initialize(GameInstance.Current.Character.Scoresheet.Scores[i]);
                }
                else
                {
                    entry.gameObject.SetActive(false);
                }
            }

            fantazeeEntry.gameObject.SetActive(true);
            fantazeeEntry.Initialize(GameInstance.Current.Character.Scoresheet.Fantazee);
        }
    }
}