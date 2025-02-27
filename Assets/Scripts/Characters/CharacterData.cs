using System.Collections.Generic;
using Fantazee.Battle.Characters;
using Fantazee.Currencies;
using Fantazee.Scores;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Characters
{
    [CreateAssetMenu(menuName = "Characters/Data")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private CharacterType type;
        public CharacterType Type => type;
        
        [Header("Localization")]
        
        [SerializeField]
        private LocalizedString locName;
        public LocalizedString LocName => locName;

        [SerializeField]
        private LocalizedString locDesc;
        public LocalizedString LocDesc => locDesc;

        [Header("Battle")]

        [SerializeField]
        private int maxHealth = 500;
        public int MaxHealth => maxHealth;

        [SerializeField]
        private BattleCharacter battleCharacter;
        public BattleCharacter BattleCharacter => battleCharacter;

        [Header("Scores")]

        [SerializeField]
        private List<ScoreData> scoreData;
        public List<ScoreData> ScoreData => scoreData;
        
        [SerializeField]
        private List<SpellType> fantazeeSpells;
        public List<SpellType> FantazeeSpells => fantazeeSpells;

        [Header("Waller")]

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;
    }
}
