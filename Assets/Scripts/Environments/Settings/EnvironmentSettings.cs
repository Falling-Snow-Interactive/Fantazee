using Fantazee.Battle.Environments.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Environments.Settings
{
    public class EnvironmentSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/EnvironmentSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static EnvironmentSettings settings;
        public static EnvironmentSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Audio")]

        [SerializeField]
        private EnvironmentInformationGroup information;
        public EnvironmentInformationGroup Information => information;
            
        #region Settings
            
        public static EnvironmentSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<EnvironmentSettings>(ResourcePath);

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

                settings = CreateInstance<EnvironmentSettings>();
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