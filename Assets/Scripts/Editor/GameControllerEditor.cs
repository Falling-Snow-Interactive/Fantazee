using Fsi.Prototyping.Editor.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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
            
            Button resetDiceButton = new()
                            {
                                text = "Reset Dice"
                            };
            resetDiceButton.clicked += () =>
                              {
                                  gameController.GameInstance.ResetDice();
                                  serializedObject.ApplyModifiedProperties();
                              };
            
            root.Add(resetDiceButton);

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
