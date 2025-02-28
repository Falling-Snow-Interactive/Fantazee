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
            // info groups
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("sideInformation"));
            
            // Animation
            // squish
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishAmount"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishEase"));
            
            // sfx
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("rollSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("squishSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("showSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("hideSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("lockSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("unlockSfx"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}