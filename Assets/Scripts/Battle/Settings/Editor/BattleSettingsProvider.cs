using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Battle.Settings.Editor
{
    public class BattleSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Battle";
        
        private SerializedObject serializedSettings;
        
        public BattleSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new BattleSettingsProvider(SettingsPath, SettingsScope.Project);
        }
        
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = BattleSettings.GetSerializedSettings();
        }
        
        public override void OnGUI(string searchContext)
        { 
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("scoreSfx"));
            
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("scoreTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("scoreEase"));
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                serializedSettings.ApplyModifiedProperties();
            }
            
            serializedSettings.ApplyModifiedProperties();
        }
    }
}