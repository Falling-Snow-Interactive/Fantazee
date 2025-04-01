using DG.Tweening;
using Fantazee.Battle.Characters.Intentions.Information;
using FMODUnity;
using UnityEditor;

namespace Fantazee.Battle.Settings
{
    public class BattleSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/BattleSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static BattleSettings _settings;
        public static BattleSettings Settings => _settings ??= GetOrCreateSettings();

        // [Header("Intentions")]

        [SerializeField]
        private IntentionInformationGroup intentions;
        public IntentionInformationGroup Intentions => intentions;
        
        [Header("Scores")]

        [SerializeField]
        private EventReference scoreSfx;
        public EventReference ScoreSfx => scoreSfx;
        
        // [Header("Animations")]
        
        // [Header("Score Sequence")]
        
        [SerializeField]
        private float scoreTime = 0.2f;
        public float ScoreTime => scoreTime;
        
        [SerializeField]
        private Ease scoreEase = Ease.Linear;
        public Ease ScoreEase => scoreEase;
        
        #region Settings
        
        public static BattleSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<BattleSettings>(ResourcePath);

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

                settings = CreateInstance<BattleSettings>();
                AssetDatabase.CreateAsset(settings, FullPath);
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