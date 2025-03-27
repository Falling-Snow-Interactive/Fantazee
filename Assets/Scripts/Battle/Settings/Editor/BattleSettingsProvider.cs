using fsi.prototyping.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Battle.Settings.Editor
{
    public static class BattleSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fantazee/Battle", SettingsScope.Project)
                                        {
                                            label = "Battle",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject battleSettingsProp = BattleSettings.GetSerializedSettings();

            ScrollView scrollView = new()
                                    {
                                        style =
                                        {
                                            marginTop = 5,
                                            marginBottom = 5,
                                            marginLeft = 5,
                                            marginRight = 5,
                                        }
                                    };
            root.Add(scrollView);
            
            // Header
            Label title = new Header("Battle Settings");
            scrollView.Add(title);
            
            scrollView.Add(Spacer.Wide());
            
            // Intentions
            scrollView.Add(new Header(1, "Intentions"));
            
            SerializedProperty intentionsProp = battleSettingsProp.FindProperty("intentions");
            PropertyField intentionsField = new(intentionsProp);
            scrollView.Add(intentionsField);
            
            scrollView.Add(Spacer.Wide());
            
            // Ui
            // Animations
            scrollView.Add(new Header(1, "Ui Animation"));
            
            // Scores
            scrollView.Add(new Header(2, "Scores"));

            SerializedProperty scoreTimeProp = battleSettingsProp.FindProperty("scoreTime");
            PropertyField scoreTimeField = new(scoreTimeProp);
            scrollView.Add(scoreTimeField);
            
            SerializedProperty scoreEaseProp = battleSettingsProp.FindProperty("scoreEase");
            PropertyField scoreEaseField = new(scoreEaseProp);
            scrollView.Add(scoreEaseField);
            
            scrollView.Add(Spacer.Wide());
            
            // Audio
            scrollView.Add(new Header(1, "Audio"));
            
            SerializedProperty sfxProp = battleSettingsProp.FindProperty("scoreSfx");
            PropertyField sfxField = new(sfxProp)
                                     {
                                         style =
                                         {
                                             // marginBottom = 5,
                                             // marginTop = 5,
                                             marginLeft = 2,
                                             // marginRight = 5,
                                         }
                                     };
            scrollView.Add(sfxField);
            
            scrollView.Add(Spacer.Wide());
            
            // Bind everything togethers
            root.Bind(battleSettingsProp);
        }
    }
}