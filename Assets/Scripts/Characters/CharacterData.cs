using Fantazee.Battle.Characters;
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
        private BattleCharacter battleCharacter;
        public BattleCharacter BattleCharacter => battleCharacter;

        [Header("Spells")]

        [SerializeField]
        private SpellType onesSpell;
        public SpellType OneSpell => onesSpell;
        
        [SerializeField]
        private SpellType twosSpell;
        public SpellType TwoSpell => twosSpell;
        
        [SerializeField]
        private SpellType threesSpell;
        public SpellType ThreesSpell => threesSpell;
        
        [SerializeField]
        private SpellType foursSpell;
        public SpellType FoursSpell => foursSpell;
        
        [SerializeField]
        private SpellType fivesSpell;
        public SpellType FivesSpell => fivesSpell;
        
        [SerializeField]
        private SpellType sixesSpell;
        public SpellType SixesSpell => sixesSpell;

        [SerializeField]
        private SpellType threeOfAKindSpell;
        public SpellType ThreeOfAKindSpell => threeOfAKindSpell;
        
        [SerializeField]
        private SpellType fourOfAKindSpell;
        public SpellType FourOfAKindSpell => fourOfAKindSpell;

        [SerializeField]
        private SpellType fullHouseSpell;
        public SpellType FullHouseSpell => fullHouseSpell;

        [SerializeField]
        private SpellType smallStraightSpell;
        public SpellType SmallStraightSpell => smallStraightSpell;
        
        [SerializeField]
        private SpellType largeStraightSpell;
        public SpellType LargeStraightSpell => largeStraightSpell;

        [SerializeField]
        private SpellType chanceSpell;
        public SpellType ChanceSpell => chanceSpell;

        [SerializeField]
        private SpellType fantazeeSpell;
        public SpellType FantazeeSpell => fantazeeSpell;
    }
}
