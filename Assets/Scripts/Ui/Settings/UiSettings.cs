using Fantazee.Ui.ColorPalettes.Information;
using UnityEditor;

namespace Fantazee.Ui.Settings
{
    public class UiSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/UiSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static UiSettings _settings;
        public static UiSettings Settings => _settings ??= GetOrCreateSettings();
        
        // Palettes

        [SerializeField]
        private ColorPaletteInformationGroup colorPalettes = new();
        public ColorPaletteInformationGroup ColorPalettes => colorPalettes;

        #region Settings

        private static UiSettings GetOrCreateSettings()
        {
            UiSettings settings = Resources.Load<UiSettings>(ResourcePath);

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

                settings = CreateInstance<UiSettings>();
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