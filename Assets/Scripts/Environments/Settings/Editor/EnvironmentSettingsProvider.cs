using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Environments.Settings.Editor
{
    public class EnvironmentSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Environment";
            
        private SerializedObject serializedSettings;
            
        public EnvironmentSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
            
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new EnvironmentSettingsProvider(SettingsPath, SettingsScope.Project);
        }
            
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = EnvironmentSettings.GetSerializedSettings();
        }
            
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("information"));
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