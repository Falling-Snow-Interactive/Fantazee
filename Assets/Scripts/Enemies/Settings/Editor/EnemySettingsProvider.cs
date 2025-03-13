
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Enemies.Settings.Editor
{
    public class EnemySettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Enemy";
            
        private SerializedObject serializedSettings;
            
        public EnemySettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
            
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new EnemySettingsProvider(SettingsPath, SettingsScope.Project);
        }
            
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = EnemySettings.GetSerializedSettings();
        }
            
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("enemies"));
            
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