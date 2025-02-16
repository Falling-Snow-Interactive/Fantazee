using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectYahtzee.Battle.Dice.Settings.Editor
{
    public class DiceSettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Project Yahtzee/Dice";
        
        private SerializedObject serializedSettings;
        
        public DiceSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new DiceSettingsProvider(SETTINGS_PATH, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = DiceSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("sideInformation"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}