using fsi.prototyping.Spacers;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Scores.Settings.Editor;

public static class ScoreSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        SettingsProvider provider = new("Fantazee/Score", SettingsScope.Project)
                                    {
                                        label = "Score",
                                        activateHandler = OnActivate,
                                    };

        return provider;
    }

    private static void OnActivate(string searchContext, VisualElement root)
    {
        SerializedObject scoreSettingsProp = ScoreSettings.GetSerializedSettings();

        ScrollView scrollView = new();
        root.Add(scrollView);

        Label title = new("Score Settings")
                      {
                          style =
                          {
                              fontSize = 24, // EditorStyles.boldLabel.fontSize,
                          }
                      };
        scrollView.Add(title);
        scrollView.Add(new Spacer());

        SerializedProperty fantazeeScoreProp = scoreSettingsProp.FindProperty("fantazeeScore");
        SerializedProperty scoresProp = scoreSettingsProp.FindProperty("scores");
        SerializedProperty bgPaletteProp = scoreSettingsProp.FindProperty("buttonColorPalette");

        PropertyField fantazeeField = new(fantazeeScoreProp);
        PropertyField scoreField = new(scoresProp);
        PropertyField bgPaletteField = new(bgPaletteProp);

        scrollView.Add(fantazeeField);
        scrollView.Add(scoreField);
        scrollView.Add(new Spacer());
        scrollView.Add(bgPaletteField);

        VisualElement space = new()
                              {
                                  style =
                                  {
                                      flexShrink = 0,
                                      flexGrow = 0,
                                      minHeight = 10f,
                                      height = 10f,
                                  }
                              };
        scrollView.Add(space);

        Button resetColors = new()
                             {
                                 text = "Reset Colours",
                             };
        resetColors.clicked += () => { ScoreSettings.Settings.ResetColors(); };

        scrollView.Add(resetColors);

        scrollView.Add(new Spacer());

        root.Bind(scoreSettingsProp);
    }
}