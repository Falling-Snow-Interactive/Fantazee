using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Encounters.Settings
{
    public class EncounterSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/EncounterSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static EncounterSettings _settings;
        public static EncounterSettings Settings => _settings ??= GetOrCreateSettings();

        [SerializeField]
        private List<EncounterData> encounters = new();
        public List<EncounterData> Encounters => encounters;
        
        private Dictionary<EncounterType, EncounterData> encountersDict = new();

        public bool TryGetEncounter(EncounterType type, out EncounterData data)
        {
            encountersDict ??= BuildDictionary();
            return encountersDict.TryGetValue(type, out data);
        }

        private Dictionary<EncounterType, EncounterData> BuildDictionary()
        {
            Dictionary<EncounterType, EncounterData> dictionary = new();
            foreach (EncounterData encounter in encounters)
            {
                if (encounter == null)
                {
                    continue;
                }
                dictionary.Add(encounter.Type, encounter);
            }
            return dictionary;
        }

        public void RebuildDictionary()
        {
            encountersDict = BuildDictionary();
        }
        
        #region Settings
            
        public static EncounterSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<EncounterSettings>(ResourcePath);

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

                settings = CreateInstance<EncounterSettings>();
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