using System.Collections.Generic;
using Fantazee.Relics.Data;
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

        [Header("Data")]

        [SerializeField]
        private RelicData defaultRelic;
        public RelicData DefaultRelic => defaultRelic;

        [SerializeField]
        private List<RelicData> relics;
        public List<RelicData> Relics => relics;
        private Dictionary<RelicType, RelicData> relicDict = null;
        
        public bool TryGetRelic(RelicType type, out RelicData data)
        {
            if (type == RelicType.relic_default)
            {
                data = defaultRelic;
                return true;
            }
            
            relicDict ??= BuildDictionary();
            return relicDict.TryGetValue(type, out data);
        }

        private Dictionary<RelicType, RelicData> BuildDictionary()
        {
            Dictionary<RelicType, RelicData> dictionary = new();
            foreach (RelicData relic in relics)
            {
                if (relic == null)
                {
                    continue;
                }

                if (!dictionary.TryAdd(relic.Type, relic))
                {
                    Debug.LogError($"Cannot add {relic.name} to dictionary, {relic.Type} already exists.", relic);
                }
            }
            return dictionary;
        }

        public void RebuildDictionary()
        {
            relicDict = BuildDictionary();
        }
        
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