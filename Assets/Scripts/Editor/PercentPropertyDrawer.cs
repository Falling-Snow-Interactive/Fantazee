using UnityEditor;
using UnityEngine.UIElements;

namespace Fantazee;

[CustomPropertyDrawer(typeof(Percent))]
public class PercentPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        SerializedProperty chanceProp = property.FindPropertyRelative("chance");
        
        VisualElement root = new()
                             {
                                 style =
                                 {
                                     flexDirection = FlexDirection.Row,
                                 }
                             };

        SliderInt slider = new()
                           {
                               label = "Chance",
                               value = chanceProp.intValue,
                               showInputField = false,
                               lowValue = 0,
                               highValue = 100,
                               bindingPath = "chance",
                               style =
                               {
                                   flexGrow = 1,
                                   flexShrink = 1,
                               }
                           };

        VisualElement space = new()
                              {
                                  style =
                                  {
                                      minWidth = 5,
                                      flexGrow = 0,
                                      flexShrink = 0,
                                  }
                              };

        Label label = new($"{chanceProp.intValue}%")
                      {
                          style =
                          {
                              flexGrow = 0,
                              flexShrink = 1,
                          }
                      };
        
        slider.RegisterValueChangedCallback(evt =>
                                            {
                                                label.text = $"{chanceProp.intValue}%";
                                            });

        root.Add(slider);
        root.Add(space);
        root.Add(label);
        
        return root;
    }
}