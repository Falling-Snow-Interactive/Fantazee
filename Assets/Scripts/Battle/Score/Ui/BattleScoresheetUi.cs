using System;
using Fantazee.Ui.Buttons;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoresheetUi : MonoBehaviour
    {
        private Action<BattleScoreButton> onScoreSelected;
        
        [SerializeField]
        private List<BattleScoreButton> scoreButtons = new();
        public List<BattleScoreButton> ScoreButtons => scoreButtons;

        [SerializeField]
        private BattleScoreButton fantazeeBattleButton;

        [Header("Input")]
        [SerializeField]
        private InputActionReference moveActionRef;
        private InputAction moveAction;

        // Allow selection to leave but bring it back for gamepads;

        private readonly HashSet<int> buttonIds = new();
        
        private GameObject lastSelected;

        private void Awake()
        {
            moveAction = moveActionRef.ToInputAction();
        }

        private void OnEnable()
        {
            foreach (BattleScoreButton scoreButton in scoreButtons)
            {
                scoreButton.Selected += OnSelected;
            }
            
            fantazeeBattleButton.Selected += OnSelected;

            moveAction.performed += OnMoveAction;
            
            moveAction.Enable();
        }

        private void OnDisable()
        {
            foreach (BattleScoreButton scoreButton in scoreButtons)
            {
                scoreButton.Selected -= OnSelected;
            }
            fantazeeBattleButton.Selected -= OnSelected;

            moveAction.performed -= OnMoveAction;
            
            moveAction.Disable();
        }

        private void OnSelected(SimpleButton button)
        {
            lastSelected = button.gameObject;
        }

        public void Initialize(List<BattleScore> scores, 
                               BattleScore fantazeeScore, 
                               Action<BattleScoreButton> onScoreSelected)
        {
            this.onScoreSelected = onScoreSelected;
            
            Debug.Assert(scores.Count == scoreButtons.Count);
            for (int i = 0; i < scores.Count; i++)
            {
                BattleScore battleScore = scores[i];
                BattleScoreButton battleButton = scoreButtons[i];
                battleButton.Initialize(battleScore, OnScoreEntryClicked);
                
                buttonIds.Add(battleButton.gameObject.GetInstanceID());
            }
            
            fantazeeBattleButton.Initialize(fantazeeScore, OnScoreEntryClicked);
            buttonIds.Add(fantazeeBattleButton.gameObject.GetInstanceID());
        }
        
        private void OnScoreEntryClicked(BattleScoreButton battleButton)
        {
            onScoreSelected?.Invoke(battleButton);
        }
        
        private void OnMoveAction(InputAction.CallbackContext ctx)
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected)
            {
                int id = EventSystem.current.currentSelectedGameObject.gameObject.GetInstanceID();
                if (!buttonIds.Contains(id))
                {
                    EventSystem.current.SetSelectedGameObject(lastSelected);
                }
            }
        }
    }
}