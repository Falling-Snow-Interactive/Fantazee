using FMODUnity;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Shop.Settings
{
    public class ShopSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/ShopSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static ShopSettings settings;
        public static ShopSettings Settings => settings ??= GetOrCreateSettings();

        [Header("Audio")]

        [SerializeField]
        private EventReference enterSfx;
        public EventReference EnterSfx => enterSfx;
        
        [SerializeField]
        private EventReference exitSfx;
        public EventReference ExitSfx => exitSfx;

        [SerializeField]
        private EventReference chargeSfx;
        public EventReference ChargeSfx => chargeSfx;

        [SerializeField]
        private EventReference purchaseSfx;
        public EventReference PurchaseSfx => purchaseSfx;

        [SerializeField]
        private EventReference swooshSfx;
        public EventReference SwooshSfx => swooshSfx;
        
        [SerializeField]
        private EventReference upgradeSfx;
        public EventReference UpgradeSfx => upgradeSfx;
            
        #region Settings
            
        public static ShopSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<ShopSettings>(ResourcePath);

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

                settings = CreateInstance<ShopSettings>();
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