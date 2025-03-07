using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class NumberScoreInstance : ScoreInstance
    {
        [Header("Number")]
        
        [SerializeReference]
        private NumberScoreData data;
        public new NumberScoreData Data => data;
        
        public NumberScoreInstance(NumberScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.data = data;
        }

        public NumberScoreInstance(NumberScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.data = data;
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);

            if (dict.TryGetValue(data.Number, out int result))
            {
                return result * data.Number;
            }

            return 0;
        }
    }
}