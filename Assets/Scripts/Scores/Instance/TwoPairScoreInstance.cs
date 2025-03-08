using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class TwoPairScoreInstance : ScoreInstance
    {
        [SerializeReference]
        private TwoPairScoreData data;
        public new TwoPairScoreData Data => data;
        
        public TwoPairScoreInstance(TwoPairScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.data = data;
        }

        public TwoPairScoreInstance(TwoPairScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.data = data;
        }

        public override int Calculate(List<Die> dice)
        {
            throw new System.NotImplementedException();
        }
    }
}