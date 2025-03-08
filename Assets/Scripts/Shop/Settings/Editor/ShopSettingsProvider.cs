using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Shop.Settings.Editor
{
    public class ShopSettingsProvider : SettingsProvider
    {
        private const string SettingsPath = "Fantazee/Shop";
            
        private SerializedObject serializedSettings;
            
        public ShopSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }
            
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new ShopSettingsProvider(SettingsPath, SettingsScope.Project);
        }
            
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            serializedSettings = ShopSettings.GetSerializedSettings();
        }
            
        public override void OnGUI(string searchContext)
        {
            // Audio
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("selectTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("selectEase"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("chargeTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("chargeEase"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("chargeOffset"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("purchaseTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("purchaseEase"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("purchaseOffset"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("punchAmount"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("punchTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("returnTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("returnEase"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("fadeAmount"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("fadeTime"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("fadeEase"));
            
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("enterSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("exitSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("chargeSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("purchaseSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("swooshSfx"));
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("upgradeSfx"));
            
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