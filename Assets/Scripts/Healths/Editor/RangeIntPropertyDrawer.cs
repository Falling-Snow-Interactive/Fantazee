using Fsi.Gameplay;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Healths
{
    [CustomPropertyDrawer(typeof(RangeInt))]
    public class RangeIntPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         flexDirection = FlexDirection.Row,
                                         flexGrow = 1,
                                         flexShrink = 1,
                                     }
                                 };
            
            SerializedProperty minProperty = property.FindPropertyRelative("min");
            SerializedProperty maxProperty = property.FindPropertyRelative("max");
            
            PropertyField minField = new(minProperty)
                                     {
                                         style =
                                         {
                                             flexGrow = 1,
                                             flexShrink = 1,
                                         },
                                         label = property.displayName,
                                     };
            PropertyField maxField = new(maxProperty)
                                     {
                                         style =
                                         {
                                             flexGrow = 1,
                                             flexShrink = 1
                                         },
                                         label = "",
                                     };

            VisualElement space = new()
                                  {
                                      style =
                                      {
                                          maxWidth = 5f,
                                          flexGrow = 1,
                                      }
                                  };
            
            root.Add(minField);
            root.Add(space);
            root.Add(maxField);
            
            return root;
        }
    }
}
