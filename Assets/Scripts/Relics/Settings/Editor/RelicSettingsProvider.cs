using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Relics.Settings.Editor
{
    public class RelicSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Relic";

        private SerializedObject serializedSettings;

        public RelicSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new RelicSettingsProvider(SettingsPath, SettingsScope.Project);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = RelicSettings.GetSerializedSettings();
        }

        public override void OnGUI(string searchContext)
        {
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