using System.Collections.Generic;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Currencies;
using Fantazee.Relics;
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

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;

        [Header("Battle")]

        [SerializeField]
        private int maxHealth = 500;
        public int MaxHealth => maxHealth;

        [SerializeField]
        private BattlePlayer battleCharacter;
        public BattlePlayer BattleCharacter => battleCharacter;

        [Header("Scores")]

        [SerializeField]
        private List<CharacterScoreData> scoreData;
        public List<CharacterScoreData> ScoreData => scoreData;

        [SerializeField]
        private CharacterScoreData fantazeeData;
        public CharacterScoreData FantazeeData => fantazeeData;

        [Header("Wallet")]

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        [Header("Relic")]

        [SerializeField]
        private RelicType relic;
        public RelicType Relic => relic;
    }
}
