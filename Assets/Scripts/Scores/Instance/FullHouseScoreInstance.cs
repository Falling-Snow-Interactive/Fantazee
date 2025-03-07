using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public class FullHouseScoreInstance : ScoreInstance
    {
        [Header("Full House")]
        
        [SerializeReference]
        private FullHouseScoreData data;
        
        public FullHouseScoreInstance(FullHouseScoreData data, List<SpellInstance> spellTypes) : base(data, spellTypes)
        {
            this.data = data;
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            
            bool foundPair = false;
            bool foundThree = false;
            
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                switch (kvp.Value)
                {
                    case >=5:
                        return 20;
                    case >=3:
                        foundThree = true;
                        break;
                    case >=2:
                        foundPair = true;
                        break;
                }
            }

            if (foundPair && foundThree)
            {
                return 20;
            }

            return 0;
        }
    }
}