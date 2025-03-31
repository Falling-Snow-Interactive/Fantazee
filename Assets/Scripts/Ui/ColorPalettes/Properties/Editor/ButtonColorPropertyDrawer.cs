using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Ui.ColorPalettes.Properties.Editor
{
    [CustomPropertyDrawer(typeof(ButtonColorProperties))]
    public class ButtonColorPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new()
                                 {
                                     style = { flexDirection = FlexDirection.Row }
                                 };

            Label header = new Label("Button");
            
            SerializedProperty bgProp = property.FindPropertyRelative("background");
            SerializedProperty outlineProp = property.FindPropertyRelative("outline");

            PropertyField bgField = new(bgProp)
                                    {
                                        label = "",
                                        style =
                                        {
                                            flexGrow = 1,
                                            flexShrink = 1,
                                        }
                                    };
            PropertyField outlineField = new(outlineProp)
                                         {
                                             label = "",
                                             style =
                                             {
                                                 flexGrow = 1,
                                                 flexShrink = 1,
                                             }
                                         };

            root.Add(header);
            root.Add(bgField);
            root.Add(outlineField);

            return root;
        }
    }
}