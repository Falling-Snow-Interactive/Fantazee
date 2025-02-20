using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Scores.Information;
using UnityEditor;
using UnityEngine;

namespace Fantazee.Battle.Settings
{
    public class GameplaySettings : ScriptableObject
    {
        private const string RESOURCE_PATH = "Settings/GameplaySettings";
        private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";

        private static GameplaySettings _settings;
        public static GameplaySettings Settings => _settings ??= GetOrCreateSettings();

        [Header("Scores")]

        [SerializeField]
        private int bonusScore = 200;
        public int BonusScore => bonusScore;
        
        [Header("Team Information")]
        
        [SerializeField]
        private ScoreInformationGroup scoreInformation;
        public ScoreInformationGroup ScoreInformation => scoreInformation;
        
        [Header("Animations")]
        
        [SerializeField]
        private Vector3 squishAmount = new Vector3(0.2f, -0.2f, 0);
        public Vector3 SquishAmount => squishAmount;

        [SerializeField]
        private float squishTime = 0.25f;
        public float SquishTime => squishTime;
        
        [SerializeField]
        private Ease squishEase = Ease.Linear;
        public Ease SquishEase => squishEase;
        
        #region Settings
        
        public static GameplaySettings GetOrCreateSettings()
        {
            var settings = Resources.Load<GameplaySettings>(RESOURCE_PATH);

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

                settings = CreateInstance<GameplaySettings>();
                AssetDatabase.CreateAsset(settings, FULL_PATH);
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