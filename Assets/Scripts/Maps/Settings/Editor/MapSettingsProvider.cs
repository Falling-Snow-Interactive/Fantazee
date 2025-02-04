using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectYahtzee.Maps.Settings.Editor
{
    public class MapSettingsProvider : SettingsProvider
    {
        private const string SETTINGS_PATH = "Project Yahtzee/Map";
        
        private SerializedObject serializedSettings;
        
        public MapSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new MapSettingsProvider(SETTINGS_PATH, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = MapSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("mapProperties"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeShape"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeConnection"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeSpacing"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("capNodeOffset"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("connectionThickness"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeRadius"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeOutlineThickness"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeInformation"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}