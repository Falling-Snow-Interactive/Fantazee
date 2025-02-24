using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Characters.Settings
{
    public class CharacterSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Character";
        
        private SerializedObject serializedSettings;
        
        public CharacterSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new CharacterSettingsProvider(SettingsPath, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = CharacterSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("defaultCharacter"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("characters"));
            
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