using Fantazee.Scores.Data;
using Fantazee.Ui;
using UnityEditor;

namespace Fantazee.Scores.Settings
{
    public class ScoreSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/ScoreSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static ScoreSettings _settings;
        public static ScoreSettings Settings => _settings ??= GetOrCreateSettings();

        [SerializeField]
        private FantazeeScoreData fantazeeScore;
        public FantazeeScoreData FantazeeScore => fantazeeScore;
        
        [SerializeField]
        private List<ScoreData> scores = new();
        private Dictionary<ScoreType, ScoreData> scoreDict;
        
        [SerializeField]
        private BackgroundColorPalette buttonColorPalette;
        public BackgroundColorPalette ButtonColorPalette => buttonColorPalette;

        private Dictionary<ScoreType, ScoreData> BuildScoreDict()
        {
            Dictionary<ScoreType, ScoreData> dict = new();
            foreach (ScoreData score in scores)
            {
                dict.Add(score.Type, score);
            }

            return dict;
        }

        public bool TryGetScore(ScoreType type, out ScoreData score)
        {
            scoreDict ??= BuildScoreDict();
            return scoreDict.TryGetValue(type, out score);
        }

        public void ResetColors()
        {
            buttonColorPalette.ResetColors();
        }

        #region Settings

        public static ScoreSettings GetOrCreateSettings()
        {
            var settings = Resources.Load<ScoreSettings>(ResourcePath);

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

                settings = CreateInstance<ScoreSettings>();
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