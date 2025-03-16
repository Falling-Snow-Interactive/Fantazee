using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Encounters.Settings.Editor
{
    public static class EncounterSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider("Fantazee/Encounters", SettingsScope.Project)
                                        {
                                            label = "Encounters",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject encounterSettingsProp = EncounterSettings.GetSerializedSettings();
            
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            Label title = new ("Encounter Settings");
            root.Add(title);
            
            SerializedProperty encountersProp = encounterSettingsProp.FindProperty("encounters");
            PropertyField encountersField = new PropertyField(encountersProp);
            encountersField.RegisterValueChangeCallback(evt =>
                                                        {
                                                            if (encounterSettingsProp.targetObject is EncounterSettings encounterSettings)
                                                            {
                                                                encounterSettings.RebuildDictionary();
                                                            }
                                                            encounterSettingsProp.ApplyModifiedProperties();
                                                        });
            
            
            root.Add(encountersField);
            
            root.Bind(encounterSettingsProp);
        }
    }
}