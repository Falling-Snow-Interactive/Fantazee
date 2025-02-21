using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Environments.Information;
using Fantazee.Battle.Scores.Bonus.Ui;
using Fantazee.Battle.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Scores.Ui
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

        public void Initialize()
        {
            List<Score> scores = GameController.Instance.GameInstance.ScoreTracker.GetScoreList();
            for (int i = 0; i < scores.Count; i++)
            {
                Score card = scores[i];
                ScoreEntry entry = entries[i];
                entry.Initialize(card);
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