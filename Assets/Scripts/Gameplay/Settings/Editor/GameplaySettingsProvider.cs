using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectYahtzee.Gameplay.Settings.Editor
{
    public class GameplaySettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Project Yahtzee/Gameplay";
        
        private SerializedObject serializedSettings;
        
        public GameplaySettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new GameplaySettingsProvider(SETTINGS_PATH, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = GameplaySettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("scoreInformation"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}