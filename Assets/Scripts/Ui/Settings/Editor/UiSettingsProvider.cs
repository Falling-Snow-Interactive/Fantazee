using fsi.prototyping.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Ui.Settings.Editor
{
    public static class UiSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fantazee/Ui", SettingsScope.Project)
                                        {
                                            label = "Ui",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject uiSettingsProp = UiSettings.GetSerializedSettings();

            ScrollView scrollView = new();
            root.Add(scrollView);
            
            // Header
            Label title = new Header(0, "Ui Settings");
            scrollView.Add(title);
            scrollView.Add(Spacer.Wide());

            // Color palettes
            Label colorsHeader = new Header(1, "Colors");
            SerializedProperty infoProp = uiSettingsProp.FindProperty("colorPalettes");
            PropertyField infoField = new PropertyField(infoProp);
            
            scrollView.Add(colorsHeader);
            scrollView.Add(infoField);
            
            scrollView.Add(Spacer.Wide());
            
            root.Bind(uiSettingsProp);
        }
    }
}