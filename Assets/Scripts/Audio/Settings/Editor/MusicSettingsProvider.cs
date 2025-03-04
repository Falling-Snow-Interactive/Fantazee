using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Audio.Settings.Editor
{
    public class MusicSettingsProvider : SettingsProvider
    {
    private const string SettingsPath = "Fantazee/Music";
        
    private SerializedObject serializedSettings;
        
    public MusicSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
        : base(path, scopes, keywords)
    {
    }
        
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        return new MusicSettingsProvider(SettingsPath, SettingsScope.Project);
    }
        
    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        serializedSettings = MusicSettings.GetSerializedSettings();
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