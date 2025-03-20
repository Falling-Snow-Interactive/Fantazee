using Fantazee.Battle.Characters.Intentions.Information;
using Fantazee.Battle.Settings;
using UnityEngine;

namespace Fantazee.Battle.Characters.Intentions
{
    public class Intention
    {
        public IntentionType Type { get; private set; }
        public int Amount { get; private set; }
        
        private readonly IntentionInformation information;
        public IntentionInformation Information => information;

        public Intention(IntentionType type, int amount)
        {
            Type = type;
            Amount = amount;

            if (!BattleSettings.Settings.Intentions.TryGetInformation(type, out information))
            {
                Debug.LogError($"No intention information found for type {type}");
            }
        }
    }
}