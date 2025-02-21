using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Spells.Settings
{
    public class SpellSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/SpellSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static SpellSettings settings;
        public static SpellSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Library")]

        [SerializeField]
        private List<SpellData> spellLibrary = new();

        public bool TryGetSpell(SpellType type, out SpellData spell)
        {
            foreach (SpellData spellData in spellLibrary)
            {
                if (spellData.Type == type)
                {
                    spell = spellData;
                    return true;
                }
            }
            
            spell = null;
            return false;
        }
        
        #region Settings
        
        public static SpellSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<SpellSettings>(ResourcePath);

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

                settings = CreateInstance<SpellSettings>();
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