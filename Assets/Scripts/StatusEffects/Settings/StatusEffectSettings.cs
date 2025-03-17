using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fantazee.StatusEffects.Settings
{
    public class StatusEffectSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/ScoreSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static StatusEffectSettings _settings;
        public static StatusEffectSettings Settings => _settings ??= GetOrCreateSettings();
        
        [SerializeField]
        private List<StatusEffectData> statusEffects = new();
        private Dictionary<StatusEffectType, StatusEffectData> statusDict;

        private Dictionary<StatusEffectType, StatusEffectData> BuildStatusDict()
        {
            Dictionary<StatusEffectType, StatusEffectData> dict = new();
            foreach (StatusEffectData status in statusEffects)
            {
                dict.Add(status.Type, status);
            }

            return dict;
        }

        public void RebuildDictionary()
        {
            statusDict = BuildStatusDict();
        }

        public bool TryGetStatus(StatusEffectType type, out StatusEffectData status)
        {
            statusDict ??= BuildStatusDict();
            return statusDict.TryGetValue(type, out status);
        }

        #region Settings

        public static StatusEffectSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<StatusEffectSettings>(ResourcePath);

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

                settings = CreateInstance<StatusEffectSettings>();
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