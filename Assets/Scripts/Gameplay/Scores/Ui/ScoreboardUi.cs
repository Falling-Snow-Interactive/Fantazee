using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ProjectYahtzee.Gameplay.Ui.Dices;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Scores.Ui
{
    public class ScoreboardUi : MonoBehaviour
    {
        private readonly Dictionary<ScoreType, ScoreEntry> scoreEntries = new Dictionary<ScoreType, ScoreEntry>();
        
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

        private void Start()
        {
            // foreach (var entry in entries)
            // {
            //     scoreEntries.Add(entry.Type, entry);
            // }
            for (int i = 0; i < GameController.Instance.GameInstance.ScoreCards.Count; i++)
            {
                ScoreCard card = GameController.Instance.GameInstance.ScoreCards[i];
                ScoreEntry entry = entries[i];
                entry.Initialize(card.Type);
                scoreEntries.Add(entry.Type, entry);
            }
        }

        public void SetScore(ScoreType type, List<Dices.Dice> diceList)
        {
            ScoreEntry entry = scoreEntries[type];
            entry.SetDice(diceList);
            entry.SetScore(GameplayController.Instance.Score.GetScore(type));
        }
        
        public void PlayScoreSequence(ScoreEntry entry, List<DiceUi> dice, Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

           const  float delay = 0.3f;

            for (int i = 0; i < dice.Count; i++)
            {
                DiceUi d = dice[i];
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
                                                                                      entry.SetDice(iCached, d.Dice.Value);
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