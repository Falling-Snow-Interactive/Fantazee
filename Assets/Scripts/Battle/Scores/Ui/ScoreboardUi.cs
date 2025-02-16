using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ProjectYahtzee.Battle.Dice.Ui;
using ProjectYahtzee.Battle.Scores.Bonus.Ui;
using UnityEngine;

namespace ProjectYahtzee.Battle.Scores.Ui
{
    public class ScoreboardUi : MonoBehaviour
    {
        public readonly Dictionary<ScoreType, ScoreEntry> scoreEntries = new Dictionary<ScoreType, ScoreEntry>();
        
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

        public void Initialize()
        {
            List<Score> scores = BattleController.Instance.ScoreTracker.Scores;
            for (int i = 0; i < scores.Count; i++)
            {
                Score card = scores[i];
                ScoreEntry entry = entries[i];
                entry.Initialize(card);
                scoreEntries.Add(entry.Score.Type, entry);
            }

            bonusScoreUi.Initialize(BattleController.Instance.ScoreTracker.BonusScore);
        }

        public void SetScore(ScoreType type, List<Dice.Die> diceList, int score)
        {
            ScoreEntry entry = scoreEntries[type];
            entry.SetDice(diceList);
            entry.SetScore(score);
        }
        
        public void PlayScoreSequence(ScoreEntry entry, List<DieUi> dice, Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

           const  float delay = 0.3f;

            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                int iCached = i;
                float delayTime = delay * i;

                Vector3 destination = entry.DiceImages[i].transform.position;
                TweenerCore<Vector3, Vector3, VectorOptions> move = d.Image
                                                                     .transform
                                                                     .DOMove(destination, scoreTime)
                                                                     .SetEase(scoreEase)
                                                                     .SetDelay(delayTime)
                                                                     .OnComplete(() =>
                                                                                  {
                                                                                      entry.SetDice(iCached, d.Die.Value);
                                                                                      d.gameObject.SetActive(false);
                                                                                  });
                TweenerCore<Vector3, Vector3, VectorOptions> scale = d.Image
                                                                      .transform
                                                                      .DOScale(Vector3.one * 0.1f, scoreTime)
                                                                      .SetEase(scoreEase)
                                                                      .SetDelay(delayTime);
                
                sequence.Insert(0, move);
                sequence.Insert(0, scale);
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
    }
}