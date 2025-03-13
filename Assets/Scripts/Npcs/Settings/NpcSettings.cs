using System.Collections.Generic;
using Fantazee.Npcs.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Npcs.Settings
{
    public class NpcSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/NpcSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static NpcSettings _settings;
        public static NpcSettings Settings => _settings ??= GetOrCreateSettings();
        
        [SerializeField]
        private List<NpcInformation> npcs = new();
        public List<NpcInformation> Npcs => npcs;
        
        private Dictionary<NpcType, NpcInformation> npcDict = new();

        public bool TryGetEncounter(NpcType type, out NpcInformation data)
        {
            npcDict ??= BuildDictionary();
            return npcDict.TryGetValue(type, out data);
        }

        private Dictionary<NpcType, NpcInformation> BuildDictionary()
        {
            Dictionary<NpcType, NpcInformation> dictionary = new();
            foreach (NpcInformation npc in npcs)
            {
                if (npc == null)
                {
                    continue;
                }
                dictionary.Add(npc.Type, npc);
            }
            return dictionary;
        }

        public void RebuildDictionary()
        {
            npcDict = BuildDictionary();
        }
            
        #region Settings
            
        public static NpcSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<NpcSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!settings)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                settings = CreateInstance<NpcSettings>();
                AssetDatabase.CreateAsset(settings, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return settings;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif
            
        #endregion
    }
}