using System;
using Fantazee.Battle.Characters.Player;
using Fantazee.Dice;
using Fantazee.Dice.Ui;
using Fantazee.Ui.Buttons;
using UnityEngine.InputSystem;

namespace Fantazee.Battle.Ui
{
    public class DiceControlUi : MonoBehaviour
    {
        [SerializeField]
        private List<DieUi> dice = new();
        public List<DieUi> Dice => dice;

        private int selectedIndex = 0;

        [Header("Input")]

        [SerializeField]
        private InputActionReference nextDieActionRef;
        private InputAction nextDieAction;

        [SerializeField]
        private InputActionReference prevDieActionRef;
        private InputAction prevDieAction;

        [SerializeField]
        private InputActionReference toggleDieActionRef;
        private InputAction toggleDieAction;

        private void Awake()
        {
            nextDieAction = nextDieActionRef.ToInputAction();
            prevDieAction = prevDieActionRef.ToInputAction();
            toggleDieAction = toggleDieActionRef.ToInputAction();

            nextDieAction.performed += ctx => NextDie();
            prevDieAction.performed += ctx => PrevDie();
            toggleDieAction.performed += ctx => ToggleDie();
        }
        
        private void OnEnable()
        {
            foreach (DieUi d in dice)
            {
                d.Selected += OnDieSelected;
            }
            
            nextDieAction.Enable();
            prevDieAction.Enable();
            toggleDieAction.Enable();
        }

        private void OnDisable()
        {
            foreach (DieUi d in dice)
            {
                d.Selected -= OnDieSelected;
            }
            
            nextDieAction.Disable();
            prevDieAction.Disable();
            toggleDieAction.Disable();
        }

        private void Start()
        {
            SelectDie(selectedIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onDieRollComplete">Called for each die.</param>
        /// <param name="onDiceRollsComplete">Called when all dice have finished rolling.</param>
        public void Roll(BattlePlayer player, Action<Die> onDieRollComplete, Action onDiceRollsComplete)
        {
            int i = 0;
            foreach (DieUi d in dice)
            {
                if (!player.LockedDice.Contains(d.Die))
                {
                    d.Roll(i * 0.2f, die =>
                                     {
                                         i--;
                                         onDieRollComplete?.Invoke(die);
                                         if (i == 0)
                                         {
                                             onDiceRollsComplete?.Invoke();
                                         }
                                     });
                    i++;
                }
            }
        }

        public void HideDice(Action onComplete = null)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                if (i == dice.Count - 1)
                {
                    d.Hide(onComplete, i * 0.2f, false);
                }
                else
                {
                    d.Hide(null, i * 0.2f, false);
                }
            }
        }

        public void ResetDice()
        {
            foreach (DieUi d in BattleUi.Instance.DiceControl.Dice)
            {
                d.ResetDice();
            }
        }

        public void ShowDice(Action onComplete = null)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                d.Show(i == dice.Count - 1 ? onComplete : null, i * 0.2f, false);
            }
        }

        private void SelectDie(int index)
        {
            dice[selectedIndex].OnDeselect();
            selectedIndex = index;
            dice[selectedIndex].OnSelect();
        }

        private void NextDie()
        {
            dice[selectedIndex].OnDeselect();
            
            selectedIndex++;
            selectedIndex %= dice.Count;
            
            dice[selectedIndex].OnSelect();
        }

        private void PrevDie()
        {
            dice[selectedIndex].OnDeselect();
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex += dice.Count;
            }
            dice[selectedIndex].OnSelect();
        }

        private void OnDieSelected(SimpleButton simpleButton)
        {
            if (simpleButton is DieUi dieUi)
            {
                int index = dice.IndexOf(dieUi);
                if (selectedIndex != index)
                {
                    DieUi d = dice[selectedIndex];
                    d.OnDeselect();
                    selectedIndex = index;
                    d = dice[selectedIndex];
                    d.OnSelect();
                }
            }
        }

        private void ToggleDie()
        {
            dice[selectedIndex].ToggleLock();
        }
    }
}