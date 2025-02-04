using ProjectYahtzee.Maps.Nodes;
using ProjectYahtzee.Maps.Nodes.Information;
using UnityEditor;
using UnityEngine;

namespace ProjectYahtzee.Maps.Settings
{
    public class MapSettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/MapSettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static MapSettings _settings;
        public static MapSettings Settings => _settings ??= GetOrCreateSettings();

        [SerializeField]
        private MapProperties mapProperties;
        public MapProperties MapProperties => mapProperties;

        [SerializeField]
        private NodeShape nodeShape;
        public NodeShape NodeShape => nodeShape;
        
        [SerializeField]
        private NodeConnection nodeConnection;
        public NodeConnection NodeConnection => nodeConnection;

        [SerializeField]
        private Vector2 nodeSpacing;
        public Vector2 NodeSpacing => nodeSpacing;

        [SerializeField]
        private float capNodeOffset;
        public float CapNodeOffset => capNodeOffset;

        [SerializeField]
        private float connectionThickness;
        public float ConnectionThickness => connectionThickness;

        [SerializeField]
        private float nodeRadius = 1;
        public float NodeRadius => nodeRadius;
        
        [SerializeField]
        private float nodeOutlineThickness;
        public float NodeOutlineThickness => nodeOutlineThickness;
        
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