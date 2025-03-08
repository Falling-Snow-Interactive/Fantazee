using DG.Tweening;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Settings
{
    public class ShopSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/ShopSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static ShopSettings _settings;
        public static ShopSettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Animation")]
        
        [SerializeField]
        private float selectTime = 0.6f;
        public float SelectTime => selectTime;

        [SerializeField]
        private Ease selectEase = Ease.InCubic;
        public Ease SelectEase => selectEase;
        
        [Space(10)]
        
        [SerializeField]
        private float chargeTime = 0.7f;
        public float ChargeTime => chargeTime;
        
        [SerializeField]
        private Ease chargeEase = Ease.InCubic;
        public Ease ChargeEase => chargeEase;

        [SerializeField]
        private float chargeOffset = 200f;
        public float ChargeOffset => chargeOffset;
        
        [Space(10)]

        [SerializeField]
        private float purchaseTime = 0.5f;
        public float PurchaseTime => purchaseTime;
        
        [SerializeField]
        private Ease purchaseEase = Ease.InCubic;
        public Ease PurchaseEase => purchaseEase;

        [SerializeField]
        private float purchaseOffset = -150f;
        public float PurchaseOffset => purchaseOffset;
        
        [Space(10)]

        [SerializeField]
        private float punchAmount = 250f;
        public float PunchAmount => punchAmount;

        [SerializeField]
        private float punchTime = 0.25f;
        public float PunchTime => punchTime;
        
        [Space(10)]

        [SerializeField]
        private float returnTime = 1f;
        public float ReturnTime => returnTime;
        
        [SerializeField]
        private Ease returnEase = Ease.InCubic;
        public Ease ReturnEase => returnEase;
        
        [Space(10)]

        [SerializeField]
        protected float fadeAmount = 0.6f;
        public float FadeAmount => fadeAmount;
        
        [SerializeField]
        protected float fadeTime = 0.5f;
        public float FadeTime => fadeTime;
        
        [SerializeField]
        protected Ease fadeEase = Ease.InOutCubic;
        public Ease FadeEase => fadeEase;
        
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