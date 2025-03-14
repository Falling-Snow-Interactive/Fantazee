using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Score.Ui;
using Fantazee.Dice;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Score
{
    [Serializable]
    public class BattleScore
    {
        public event Action DieAdded;
        public event Action ScoreReset;
        
        public event Action<SpellInstance> SpellCastStart;
        public event Action<SpellInstance> SpellCastEnd;

        [SerializeReference]
        private ScoreInstance score;
        public ScoreInstance Score => score;
        
        [SerializeField]
        private List<Die> dice = new();
        public List<Die> Dice => dice;

        [FormerlySerializedAs("entryUi")]
        [SerializeReference]
        private BattleScoreButton buttonUi;

        public BattleScore(ScoreInstance score)
        {
            this.score = score;
        }

        public void SetEntry(BattleScoreButton buttonUi)
        {
            this.buttonUi = buttonUi;
        }

        public void Cast(Damage damage, Action onComplete)
        {
            BattleController.Instance.StartCoroutine(CastSequence(damage, onComplete));
        }

        private IEnumerator CastSequence(Damage damage, Action onComplete)
        {
            for (int i = 0; i < score.Spells.Count; i++)
            {
                bool ready = false;
                SpellInstance spell = score.Spells[i];
                if (spell.Data.Type != SpellType.spell_none && BattleController.Instance.EnemiesRemaining() > 0)
                {
                    buttonUi.Spells[i].transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
                
                    spell.Cast(damage, () =>
                                       {
                                           SpellCastEnd?.Invoke(spell);
                                           ready = true;
                                       });
                    SpellCastStart?.Invoke(spell);
                    yield return new WaitUntil(() => ready);
                }
            }
            
            onComplete?.Invoke();
        }

        public int Calculate()
        {
            int results = score.Calculate(dice);
            return results;
        }

        public int Calculate(List<Die> dice)
        {
            int results = score.Calculate(dice);
            return results;
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