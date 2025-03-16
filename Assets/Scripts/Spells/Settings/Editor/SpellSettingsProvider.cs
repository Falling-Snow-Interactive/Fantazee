using System.Collections.Generic;
using Fantazee.Encounters.Settings;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Spells.Settings
{
    public static class SpellSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fantazee/Spells", SettingsScope.Project)
                                        {
                                            label = "Spells",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            // Build the Ui
            SerializedObject encounterSettingsProp = SpellSettings.GetSerializedSettings();
            
            Label title = new ("Spell Settings");
            root.Add(title);
            
            SerializedProperty noneProp = encounterSettingsProp.FindProperty("none");
            PropertyField noneField = new(noneProp);
            root.Add(noneField);
            
            SerializedProperty spellsProp = encounterSettingsProp.FindProperty("spells");
            PropertyField spellsField = new(spellsProp);
            spellsField.RegisterValueChangeCallback(evt =>
                                                        {
                                                            if (encounterSettingsProp.targetObject is SpellSettings spellSettings)
                                                            {
                                                                spellSettings.RebuildDictionary();
                                                            }
                                                            encounterSettingsProp.ApplyModifiedProperties();
                                                        });
            
            
            root.Add(spellsField);
            
            root.Bind(encounterSettingsProp);
            
            // Update the dictionary
            if (encounterSettingsProp.targetObject is SpellSettings spellSettings)
            {
                spellSettings.RebuildDictionary();
            }
        }
    }
}