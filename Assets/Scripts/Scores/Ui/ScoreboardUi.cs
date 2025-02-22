using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Environments.Information;
using Fantazee.Battle.Settings;
using Fantazee.Scores.Bonus.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui
{
    public class ScoreboardUi : MonoBehaviour
    {
        [Header("Animation")]
        
        [SerializeField]
        private float scoreTime = 0.6f;
        
        [SerializeField]
        private Ease scoreEase = Ease.InCirc;

        [Header("Prefabs")]

        [SerializeField]
        private ScoreEntry scoreEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform entryContainer;
        
        [SerializeField]
        private List<ScoreEntry> entries = new List<ScoreEntry>();

        [SerializeField]
        private BonusScoreUi bonusScoreUi;
        public BonusScoreUi BonusScoreUi => bonusScoreUi;

        [SerializeField]
        private Image background;

        public void Initialize(ScoreTracker scoreTracker)
        {
            List<Score> scores = scoreTracker.GetScoreList();
            for (int i = 0; i < scores.Count; i++)
            {
                Score score = scores[i];
                ScoreEntry entry = entries[i];
                entry.Initialize(score);
            }

            // bonusScoreUi.Initialize(BattleController.Instance.ScoreTracker.BonusScore);

            if (BattleSettings.Settings.EnvironmentInformation
                              .TryGetInformation(GameController.Instance.GameInstance.Environment, 
                                                 out EnvironmentInformation info))
            {
                background.color = info.Color;
            }
        }
    }
}