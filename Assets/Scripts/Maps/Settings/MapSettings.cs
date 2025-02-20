using Fantazee.Maps.Nodes.Information;
using Fantazee.Maps.Nodes;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Maps.Settings
{
    public class MapSettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/MapSettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static MapSettings _settings;
        public static MapSettings Settings => _settings ??= GetOrCreateSettings();
        
        [Header("Information")]
        
        [SerializeField]
        private NodeInformationGroup nodeInformation;
        public NodeInformationGroup NodeInformation => nodeInformation;
        
        #region Settings
        
        public static MapSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<MapSettings>(RESOURCE_PATH);

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

                settings = CreateInstance<MapSettings>();
                AssetDatabase.CreateAsset(settings, FULL_PATH);
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