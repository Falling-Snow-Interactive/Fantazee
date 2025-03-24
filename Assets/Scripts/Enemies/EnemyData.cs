using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Enemies.Actions.Randomizer;
using Fantazee.Randomizers;
using FMODUnity;
using UnityEngine.Localization;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private EnemyType type;
        public EnemyType Type => type;

        [Header("Enemy")]

        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no loc" : locName.GetLocalizedString();

        [SerializeField]
        private LocalizedString locDesc;
        public string Description => locDesc.IsEmpty ? "no loc" : locDesc.GetLocalizedString();
        
        [Header("Visuals")]

        [SerializeField]
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;

        [SerializeField]
        private Vector2 size = Vector2.one;
        public Vector2 Size => size;
        
        [Header("Health")]
        
        [SerializeField]
        private RangeInt health;
        public RangeInt Health => health;

        [SerializeField]
        private Vector3 statusBarPosition = new Vector3(-30, 45, 0f);
        public Vector3 StatusBarPosition => statusBarPosition;
        
        [Header("Actions")]
        
        [SerializeField]
        private IntRandomizer actionsPerTurn = new();
        public IntRandomizer ActionsPerTurn => actionsPerTurn;
        
        [SerializeField]
        private List<ActionRandomizerEntry> actionRandomizer;
        public List<ActionRandomizerEntry> ActionRandomizer => actionRandomizer;
        
        [Header("Rewards")]
        
        [SerializeField]
        private BattleRewards battleRewards;
        public BattleRewards BattleRewards => battleRewards;
        
        [Header("Audio")]

        [SerializeField]
        private EventReference enterSfx;
        public EventReference EnterSfx => enterSfx;
        
        [SerializeField]
        private EventReference deathSfx;
        public EventReference DeathSfx => deathSfx;
        
        [Header("Animation")]
        
        [Header("Hide/Show")]

        [SerializeField]
        private Vector3 hideOffset = Vector3.zero;
        public Vector3 HideOffset => hideOffset;

        [SerializeField]
        private float showTime = 0.5f;
        public float ShowTime => showTime;
        
        [SerializeField]
        private Ease showEase = Ease.Linear;
        public Ease ShowEase => showEase;
    }
}