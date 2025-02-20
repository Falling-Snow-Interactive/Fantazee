using Fantazee.Currencies.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Currencies.Settings
{
    public class CurrencySettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/CurrencySettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static CurrencySettings _settings;
        public static CurrencySettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Information")]

        [SerializeField]
        private CurrencyInformationGroup currencyInformation;
        public CurrencyInformationGroup CurrencyInformation => currencyInformation;
            
        #region Settings
            
        public static CurrencySettings GetOrCreateSettings()
        {
            var settings = Resources.Load<CurrencySettings>(RESOURCE_PATH);

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

                settings = CreateInstance<CurrencySettings>();
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