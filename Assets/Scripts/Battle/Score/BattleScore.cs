using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.BattleSpells;
using Fantazee.Battle.Score.Ui;
using Fantazee.Dice;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using UnityEngine;
#pragma warning disable CS0067 // Event is never used

namespace Fantazee.Battle.Score
{
    [Serializable]
    public class BattleScore
    {
        // ReSharper disable once EventNeverSubscribedTo.Global
        public event Action Changed;
        public event Action DieAdded;
        public event Action ScoreReset;
        
        [SerializeReference]
        private List<BattleSpell> spells;
        public List<BattleSpell> Spells => spells;

        [SerializeReference]
        private Scores.Score score;
        public Scores.Score Score => score;
        
        [SerializeField]
        private List<Die> dice = new();
        public List<Die> Dice => dice;

        [SerializeReference]
        private BattleScoreEntry entryUi;

        public BattleScore(Scores.Score score)
        {
            this.score = score;

            spells = new List<BattleSpell>();
            foreach(SpellType spell in score.Spells)
            {
                if (SpellSettings.Settings.TryGetSpell(spell, out SpellData data))
                {
                    BattleSpell battleSpell = BattleSpellFactory.Create(data);
                    spells.Add(battleSpell);
                }
            }
        }

        public void SetEntry(BattleScoreEntry entryUi)
        {
            this.entryUi = entryUi;
        }

        public void Cast(Damage damage, Action onComplete)
        {
            BattleController.Instance.StartCoroutine(CastSequence(damage, onComplete));
        }

        private IEnumerator CastSequence(Damage damage, Action onComplete)
        {
            for (int i = 0; i < spells.Count; i++)
            {
                bool ready = false;
                BattleSpell spell = spells[i];
                if (!spell.IsNone && BattleController.Instance.EnemiesRemaining() > 0)
                {
                    entryUi.Spells[i].transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
                
                    spell.Cast(damage, () => { ready = true; });
                    yield return new WaitUntil(() => ready);
                }
            }
            
            onComplete?.Invoke();
        }

        public int Calculate()
        {
            int results = score.Calculate(dice);
            string s = $"BattleScore: {results}\n";
            foreach (var d in dice)
            {
                s += $"{d}";
                if (d == dice[^1])
                {
                    s += " - ";
                }
            }
            Debug.Log($"BattleScore: {results} - {dice[0]}");
            return score.Calculate(dice);
        }
        
        public bool CanScore()
        {
            return dice.Count == 0;
        }

        public void AddDie(Die die)
        {
            dice.Add(die);
            DieAdded?.Invoke();
        }

        public void ResetScore()
        {
            dice.Clear();
            ScoreReset?.Invoke();
        }
    }
}