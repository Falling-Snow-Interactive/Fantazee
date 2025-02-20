using Fantazee.Boons.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Boons.Settings
{
    public class BoonSettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/BoonSettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static BoonSettings _settings;
        public static BoonSettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Information")]

        [SerializeField]
        private BoonInformationGroup information;
        public BoonInformationGroup Information => information;

        #region Settings

        public static BoonSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<BoonSettings>(RESOURCE_PATH);

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

                settings = CreateInstance<BoonSettings>();
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
