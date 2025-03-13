using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Maps.Settings.Editor
{
    // public class MapSettingsProvider : SettingsProvider
    // {
    //     private const string SettingsPath = "Fantazee/Map";
    //     
    //     private SerializedObject serializedSettings;
    //     
    //     public MapSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
    //         : base(path, scopes, keywords)
    //     {
    //     }
    //     
    //     [SettingsProvider]
    //     public static SettingsProvider CreateMyCustomSettingsProvider()
    //     {
    //         return new MapSettingsProvider(SettingsPath, SettingsScope.Project);
    //     }
    //     
    //     public override void OnActivate(string searchContext, VisualElement rootElement)
    //     {
    //         serializedSettings = MapSettings.GetSerializedSettings();
    //     }
    //     
    //     public override void OnGUI(string searchContext)
    //     {
    //         EditorGUILayout.PropertyField(serializedSettings.FindProperty("nodeInformation"));
    //         
    //         EditorGUILayout.Space(20);
    //         if (GUILayout.Button("Save"))
    //         {
    //             serializedSettings.ApplyModifiedProperties();
    //         }
    //         
    //         serializedSettings.ApplyModifiedProperties();
    //     }
    // }

    public static class MapSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider("Fantazee/Map", SettingsScope.Project)
                                        {
                                            label = "Map",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject mapSettingsProp = MapSettings.GetSerializedSettings();
            
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            Label title = new ("Map Settings");
            root.Add(title);
            
            SerializedProperty nodeInfoProp = mapSettingsProp.FindProperty("nodeInformation");
            PropertyField nodeInfoField = new PropertyField(nodeInfoProp);
            
            root.Add(nodeInfoField);
            
            root.Bind(mapSettingsProp);
        }
    }
}