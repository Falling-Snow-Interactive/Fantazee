using FMODUnity;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Battle.Settings
{
    public class BattleSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/BattleSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static BattleSettings settings;
        public static BattleSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Scores")]

        [SerializeField]
        private EventReference scoreSfx;
        public EventReference ScoreSfx => scoreSfx;
        
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