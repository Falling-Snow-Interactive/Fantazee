using System.Collections.Generic;
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

        [Header("Characters")]
        
        [SerializeField]
        private List<CharacterData> characters;

        public bool TryGetCharacter(CharacterType type, out CharacterData data)
        {
            foreach (CharacterData character in characters)
            {
                if (character.Type == type)
                {
                    data = character;
                    return true;
                }
            }
            
            data = null;
            return false;
        }
        
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