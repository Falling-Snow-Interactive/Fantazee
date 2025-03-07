using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Spells.Settings
{
    public class SpellSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Spells";
        
        private SerializedObject serializedSettings;
        
        public SpellSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new SpellSettingsProvider(SettingsPath, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = SpellSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("none"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("spells"));
            // EditorGUILayout.PropertyField(serializedSettings.FindProperty("prop"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}