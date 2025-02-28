using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Items.Dice.Settings.Editor
{
    public class DiceSettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Fantazee/Dice";
        
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
            
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishAmount"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishEase"));
            
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("dieRollRef"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("dieSquishRef"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}