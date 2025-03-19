using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.StatusEffects.Settings.Editor
{
    public static class StatusEffectSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fantazee/Status Effects", SettingsScope.Project)
                                        {
                                            label = "Status Effects",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject statusSettingsProp = StatusEffectSettings.GetSerializedSettings();
            
            Label title = new ("Status Effect Settings");
            root.Add(title);
            
            SerializedProperty statusProp = statusSettingsProp.FindProperty("statusEffects");
            PropertyField statusField = new(statusProp);
            statusField.RegisterValueChangeCallback(evt =>
                                                    {
                                                        if (statusSettingsProp.targetObject is StatusEffectSettings statusSettings)
                                                        {
                                                            statusSettings.RebuildDictionary();
                                                        }
                                                        statusSettingsProp.ApplyModifiedProperties();
                                                    });
            
            
            root.Add(statusField);
            
            root.Bind(statusSettingsProp);
        }
    }
}