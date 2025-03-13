using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Enemies.Settings
{
    public class EnemySettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/EnemySettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static EnemySettings _settings;
        public static EnemySettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Data")]

        [SerializeField]
        private List<EnemyData> enemies = new();
        public List<EnemyData> Enemies => enemies;
            
        #region Settings
            
        public static EnemySettings GetOrCreateSettings()
        {
            var settings = Resources.Load<EnemySettings>(ResourcePath);

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

                settings = CreateInstance<EnemySettings>();
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