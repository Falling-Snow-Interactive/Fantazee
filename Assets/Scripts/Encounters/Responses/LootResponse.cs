using Fantazee.Battle;
using UnityEngine;

namespace Fantazee.Encounters.Responses
{
    [CreateAssetMenu(fileName = "EncounterResponse", menuName = "Encounters/Responses/Loot")]
    public class LootResponse : EncounterResponse
    {
        [SerializeField]
        private BattleRewards rewards;
        public BattleRewards Rewards => rewards;
    }
}