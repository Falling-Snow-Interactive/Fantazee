using fsi.prototyping.Spacers;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fantazee.Scores.Ui.Buttons.Editor;

// [CustomEditor(typeof(ScoreButton), true)]
public class ScoreButtonEditor : UnityEditor.Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        ScoreButton scoreButton = target as ScoreButton;
        
        VisualElement root = new();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);
        
        root.Add(new Spacer());
        
        return root;
    }
}