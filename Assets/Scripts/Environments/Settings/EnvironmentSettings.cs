using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Environments.Settings
{
    public class EnvironmentSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/EnvironmentSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static EnvironmentSettings _settings;
        public static EnvironmentSettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Data")]

        [SerializeField]
        private List<EnvironmentData> data = new List<EnvironmentData>();
        public List<EnvironmentData> Data => data;

        private Dictionary<EnvironmentType, EnvironmentData> dataByType;

        public bool TryGetEnvironment(EnvironmentType environmentType, 
                                      out EnvironmentData environmentData)
        {
            dataByType ??= BuildDict();
            return dataByType.TryGetValue(environmentType, out environmentData);
        }

        private Dictionary<EnvironmentType, EnvironmentData> BuildDict()
        {
            Dictionary<EnvironmentType, EnvironmentData> dict = new Dictionary<EnvironmentType, EnvironmentData>();
            foreach (EnvironmentData d in data)
            {
                dict.TryAdd(d.Type, d);
            }
            return dict;
        }
            
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