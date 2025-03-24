using fsi.prototyping.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee.Battle.Editor
{
    [CustomEditor(typeof(BattleController), true)]
    public class BattleControllerEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            
            if (target is BattleController controller)
            {
                if (Application.isPlaying && controller.Rewards != null)
                {
                    root.Add(new Spacer());
                    root.Add(DrawRewards(controller));
                }
            }

            return root;
        }

        private VisualElement DrawRewards(BattleController controller)
        {
            VisualElement root = new();
            
            Label header = new("Rewards");
            root.Add(header);

            Label rewards = new(controller.Rewards.ToString());
            root.Add(rewards);

            return root;
        }
    }
}
