using System;
using System.Collections.Generic;
using Fantazee.Battle.BattleSpells;
using Fantazee.Dice;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using UnityEngine;

namespace Fantazee.Battle.Score
{
    [Serializable]
    public class BattleScore
    {
        public event Action Changed;
        public event Action DieAdded;
        public event Action DiceCleared;
        
        [SerializeReference]
        private List<BattleSpell> spells;
        public List<BattleSpell> Spells => spells;

        [SerializeReference]
        private Scores.Score score;
        public Scores.Score Score => score;
        
        [SerializeField]
        private List<Die> dice = new();
        public List<Die> Dice => dice;

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

        public void Cast(Damage damage, Action onComplete)
        {
            spells[0].Cast(damage, () =>
                                   {
                                       spells[1].Cast(damage, onComplete);
                                   });
        }

        public int Calculate()
        {
            return score.Calculate(dice);
        }
        
        public bool CanScore()
        {
            return dice.Count == 0;
        }

        public void AddDie(Die die)
        {
            dice.Add(die);
            string s = "";
            for (int i = 0; i < dice.Count; i++)
            {
                Die d = dice[i];
                s += $"{d.Value}";
                if (i != dice.Count - 1)
                {
                    s += " - ";
                }
            }

            Debug.Log($"Score: {score.Type} - Die: {die.Value}\n{s}");
            DieAdded?.Invoke();
            Changed?.Invoke();
        }

        public void ClearDice()
        {
            dice.Clear();
            DiceCleared?.Invoke();
            Changed?.Invoke();
        }
    }
}