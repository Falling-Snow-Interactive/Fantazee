using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Spacer = fsi.prototyping.Spacers.Spacer;

namespace Fantazee
{
    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            GameController gameController = (GameController)target;
            VisualElement root = new();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            root.Add(new Spacer());

            // Button resetScoresButton = new()
            //                 {
            //                     text = "Reset Scores"
            //                 };
            // resetScoresButton.clicked += () =>
            //                   {
            //                       gameController.GameInstance.ResetScore();
            //                       serializedObject.ApplyModifiedProperties();
            //                   };
            //
            // root.Add(resetScoresButton);
            
            return root;
        }
    }
}
