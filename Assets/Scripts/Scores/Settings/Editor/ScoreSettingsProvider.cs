using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Scores.Settings.Editor
{
    public class ScoreSettingsProvider : SettingsProvider
    {
    private const string SettingsPath = "Fantazee/Score";
        
    private SerializedObject serializedSettings;
        
    public ScoreSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
        : base(path, scopes, keywords)
    {
    }
        
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        return new ScoreSettingsProvider(SettingsPath, SettingsScope.Project);
    }
        
    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        serializedSettings = ScoreSettings.GetSerializedSettings();
    }
        
    public override void OnGUI(string searchContext)
    {
        EditorGUILayout.PropertyField(serializedSettings.FindProperty("scores"));
        EditorGUILayout.PropertyField(serializedSettings.FindProperty("fantazeeScore"));
            
        EditorGUILayout.Space(20);
        if (GUILayout.Button("Save"))
        {
            serializedSettings.ApplyModifiedProperties();
        }
            
        serializedSettings.ApplyModifiedProperties();
    }
    }
}