using System;
using Fantazee.Spells.Animations;
using Fantazee.Spells.Data.Animations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData
{
    [Serializable]
    public abstract class EnemyActionData : ScriptableObject
    {
        public abstract ActionType Type { get; }

        [Header("Action")]

        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no_loc" : locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description => locDescription.IsEmpty ? "no_loc" : locDescription.GetLocalizedString();

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
        
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;
        
        [Header("Animations")]
        
        [SerializeField]
        private CastAnimProp castAnim;
        public CastAnimProp CastAnim => castAnim;
        
        [SerializeField]
        private HitAnimProp hitAnim;
        public HitAnimProp HitAnim => hitAnim;
    }
}