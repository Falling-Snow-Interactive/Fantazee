using System.Collections.Generic;
using ProjectYahtzee.Gameplay.Ui.Dice;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Score.Ui
{
    public class ScoreboardUi : MonoBehaviour
    {
        [SerializeField]
        private List<ScoreEntry> entries = new List<ScoreEntry>();
        
        private readonly Dictionary<ScoreType, ScoreEntry> scoreEntries = new Dictionary<ScoreType, ScoreEntry>();

        private void Awake()
        {
            foreach (ScoreEntry entry in entries)
            {
                scoreEntries.Add(entry.Type, entry);
            }
        }

        public void SetScore(ScoreType type, List<DiceUi> diceList)
        {
            ScoreEntry entry = scoreEntries[type];
            entry.SetDice(diceList);
        }
    }
}