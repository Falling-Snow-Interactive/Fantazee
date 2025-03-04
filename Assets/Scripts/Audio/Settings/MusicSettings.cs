using Fantazee.Audio.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Audio.Settings
{
    public class MusicSettings : ScriptableObject
    {
    private const string ResourcePath = "Settings/MusicSettings";
    private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

    private static MusicSettings settings;
    public static MusicSettings Settings => settings ??= GetOrCreateSettings();

    [SerializeField]
    private MusicInformationGroup information;
    public MusicInformationGroup Information => information;
    
    #region Settings
        
    public static MusicSettings GetOrCreateSettings()
    {
        var settings = Resources.Load<MusicSettings>(ResourcePath);

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

            settings = CreateInstance<MusicSettings>();
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