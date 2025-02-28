using DG.Tweening;
using Fantazee.Items.Dice.Information;
using FMODUnity;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Items.Dice.Settings
{
    public class DiceSettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/DiceSettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static DiceSettings _settings;
        public static DiceSettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Sides")]

        [SerializeField]
        private SideInformationGroup sideInformation;
        public SideInformationGroup SideInformation => sideInformation;
        
        [Header("Animations")]
        
        [SerializeField]
        private Vector3 squishAmount = new Vector3(0.2f, -0.2f, 0);
        public Vector3 SquishAmount => squishAmount;

        [SerializeField]
        private float squishTime = 0.25f;
        public float SquishTime => squishTime;
        
        [SerializeField]
        private Ease squishEase = Ease.Linear;
        public Ease SquishEase => squishEase;
        
        [Header("Audio")]
        
        [SerializeField]
        private EventReference dieRollRef;
        public EventReference DieRollRef => dieRollRef;

        [SerializeField]
        private EventReference dieSquishRef;
        public EventReference DieSquishRef => dieSquishRef;
        
        #region Settings
        
        public static DiceSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<DiceSettings>(RESOURCE_PATH);

            #if UNITY_EDITOR
            if (!settings)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                settings = CreateInstance<DiceSettings>();
                AssetDatabase.CreateAsset(settings, FULL_PATH);
                AssetDatabase.SaveAssets();
            }
            #endif

            return settings;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif
        
        #endregion
    }
}