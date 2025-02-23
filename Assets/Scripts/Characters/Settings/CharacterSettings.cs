using UnityEditor;
using UnityEngine;

namespace Fantazee.Characters.Settings
{
    public class CharacterSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/CharacterSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static CharacterSettings settings;
        public static CharacterSettings Settings => settings ??= GetOrCreateSettings();
        
        #region Settings
        
        public static CharacterSettings GetOrCreateSettings()
        {
            CharacterSettings settings = Resources.Load<CharacterSettings>(ResourcePath);

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

                settings = CreateInstance<CharacterSettings>();
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