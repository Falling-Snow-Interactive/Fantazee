using fsi.settings.Informations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Information
{
    [CustomPropertyDrawer(typeof(InformationGroup<,>), true)]
    public class InformationGroupPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();
            
            SerializedProperty infoProp = property.FindPropertyRelative("information");
            PropertyField infoField = new(infoProp);
            root.Add(infoField);
            
            return root; // base.CreatePropertyGUI(property);
        }
    }
}