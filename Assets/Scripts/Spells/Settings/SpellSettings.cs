using System.Collections.Generic;
using Fantazee.Spells.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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
        private SpellData none;
        public SpellData None => none;

        [FormerlySerializedAs("spellLibrary")]
        [SerializeField]
        private List<SpellData> spells = new();
        private Dictionary<SpellType, SpellData> spellsDictionary = new();

        public bool TryGetSpell(SpellType type, out SpellData spell)
        {
            spellsDictionary ??= GetSpellsDictionary();
            return spellsDictionary.TryGetValue(type, out spell);
        }

        private Dictionary<SpellType, SpellData> GetSpellsDictionary()
        {
            foreach (SpellData spell in spells)
            {
                spellsDictionary.Add(spell.Type, spell);
            }
            return spellsDictionary;
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