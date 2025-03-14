using System.Collections.Generic;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Characters.Settings;
using Fantazee.Currencies;
using Fantazee.Relics;
using Fantazee.Spells;
using FMODUnity;
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

        [Header("Health")]

        [SerializeField]
        private int maxHealth = 500;
        public int MaxHealth => maxHealth;

        [Header("Scoresheet")]

        [SerializeField]
        private List<CharacterScoreData> scores;
        public List<CharacterScoreData> Scores => scores;

        [SerializeField]
        private CharacterScoreData fantazee;
        public CharacterScoreData Fantazee => fantazee;

        [Header("Wallet")]

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        [Header("Relic")]

        [SerializeField]
        private RelicType relic;
        public RelicType Relic => relic;
        
        [Header("Audio")]
        
        [SerializeField]
        private EventReference enterSfx;
        public EventReference EnterSfx => enterSfx;
        
        [SerializeField]
        private EventReference exitSfx;
        public EventReference ExitSfx => exitSfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
        
        [SerializeField]
        private EventReference deathSfx;
        public EventReference DeathSfx => deathSfx;

        public static CharacterData Default => CharacterSettings.Settings.DefaultCharacter;
    }
}
