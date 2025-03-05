using Fantazee.Relics.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Relics.Settings
{
    public class RelicSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/RelicSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static RelicSettings settings;
        public static RelicSettings Settings => settings ??= GetOrCreateSettings();

        [SerializeField]
        private RelicInformationGroup information;
        public RelicInformationGroup Information => information;
        
        #region Settings

        public static RelicSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<RelicSettings>(ResourcePath);

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

                settings = CreateInstance<RelicSettings>();
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