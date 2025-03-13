using Fantazee.Encounters.Settings;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Npcs.Settings.Editor
{
    public static class NpcSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider("Fantazee/NPCs", SettingsScope.Project)
                                        {
                                            label = "NPCs",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject npcSettingsProp = NpcSettings.GetSerializedSettings();
            
            GUIStyle titleStyle = new(EditorStyles.boldLabel);
            Label title = new ("NPC Settings");
            root.Add(title);
            
            SerializedProperty npcsProp = npcSettingsProp.FindProperty("npcs");
            PropertyField npcsField = new PropertyField(npcsProp);
            npcsField.RegisterValueChangeCallback(evt =>
                                                  {
                                                      if (npcSettingsProp.targetObject is NpcSettings npcSettings)
                                                      {
                                                          npcSettings.RebuildDictionary();
                                                      }
                                                      npcSettingsProp.ApplyModifiedProperties();
                                                  });
            
            
            root.Add(npcsField);
            
            root.Bind(npcSettingsProp);
        }
    }
}