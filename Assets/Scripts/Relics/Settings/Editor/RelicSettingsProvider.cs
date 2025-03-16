using fsi.prototyping.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Relics.Settings.Editor
{
    public static class RelicSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fantazee/Relics", SettingsScope.Project)
                                        {
                                            label = "Relics",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject relicSettingsProp = RelicSettings.GetSerializedSettings();
            
            Label title = new ("Relic Settings");
            root.Add(title);
            root.Add(new Spacer());
            
            SerializedProperty defaultProp = relicSettingsProp.FindProperty("defaultRelic");
            PropertyField defaultField = new(defaultProp);
            root.Add(defaultField);
            
            SerializedProperty relicsProp = relicSettingsProp.FindProperty("relics");
            PropertyField relicsField = new(relicsProp);
            relicsField.RegisterValueChangeCallback(evt =>
                                                        {
                                                            if (relicSettingsProp.targetObject is RelicSettings relicSettings)
                                                            {
                                                                relicSettings.RebuildDictionary();
                                                            }
                                                            relicSettingsProp.ApplyModifiedProperties();
                                                        });
            
            
            root.Add(relicsField);
            
            root.Bind(relicSettingsProp);
        }
    }
}