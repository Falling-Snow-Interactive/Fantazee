using System;
using DG.Tweening;
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
        }
        
        private void OnEnable()
        {
            foreach (DieUi d in dice)
            {
                d.Selected += OnDieSelected;
            }
            
            nextDieAction.performed += NextDie;
            prevDieAction.performed += PrevDie;
            toggleDieAction.performed += ToggleDie;
            
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
            
            nextDieAction.performed -= NextDie;
            prevDieAction.performed -= PrevDie;
            toggleDieAction.performed -= ToggleDie;
            
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
            Sequence sequence = DOTween.Sequence();
            float delay = 0;
            foreach (DieUi d in dice)
            {
                if (!player.LockedDice.Contains(d.Die))
                {
                    Sequence s = d.Roll(0, die => onDieRollComplete?.Invoke(die));
                    sequence.Insert(delay, s);
                    delay += 0.2f;
                }
            }

            sequence.OnComplete(() => onDiceRollsComplete?.Invoke());
            sequence.Play();
        }

        public void HideDice(Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                Tweener tweener = d.Hide();
                sequence.Insert(i * 0.2f, tweener);
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }

        public void ShowDice(Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();
            
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                Tweener tweener = d.Show();
                sequence.Insert(i * 0.2f, tweener);
            }
            
            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
        
        public void ResetDice()
        {
            foreach (DieUi d in BattleUi.Instance.DiceControl.Dice)
            {
                d.ResetDice();
            }
        }

        private void SelectDie(int index)
        {
            dice[selectedIndex].OnDeselect();
            selectedIndex = index;
            dice[selectedIndex].OnSelect();
        }

        private void NextDie(InputAction.CallbackContext callbackContext)
        {
            dice[selectedIndex].OnDeselect();
            
            selectedIndex++;
            selectedIndex %= dice.Count;
            
            dice[selectedIndex].OnSelect();
        }

        private void PrevDie(InputAction.CallbackContext callbackContext)
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

        private void ToggleDie(InputAction.CallbackContext callbackContext)
        {
            Debug.Log($"Toggle: {selectedIndex}");
            dice[selectedIndex].ToggleLock();
        }
    }
}