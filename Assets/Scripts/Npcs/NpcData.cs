using DG.Tweening;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Npcs
{
    [CreateAssetMenu(fileName = "Npc_", menuName = "NPC/Data")]
    public class NpcData : ScriptableObject
    {
        [SerializeField]
        private NpcType type;
        public NpcType Type => type;
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description => locDescription.GetLocalizedString();

        [Header("Visuals")]

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;

        [Header("Animations")]

        [SerializeField]
        private NpcAnimationProperty enterAnimation;
        public NpcAnimationProperty EnterAnimation => enterAnimation;
        
        [SerializeField]
        private NpcAnimationProperty exitAnimation;
        public NpcAnimationProperty ExitAnimation => exitAnimation;

        [Header("Audio")]

        [SerializeField]
        private EventReference enterSfx;
        public EventReference EnterSfx => enterSfx;
    }
}