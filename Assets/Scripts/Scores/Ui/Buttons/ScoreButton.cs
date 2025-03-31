using System;
using DG.Tweening;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.Buttons
{
    public class ScoreButton : SimpleButton
    {
        // Callback
        private Action<ScoreButton> onClickCallback;

        // Score
        public ScoreInstance Score { get; set; }
        private List<Tweener> colorTweens = new();

        [Header("Score Button")]

        [SerializeField]
        private SpellTooltip tooltipPrefab;

        [Header("References")]

        [SerializeField]
        private TMP_Text nameText;

        public TMP_Text NameText => nameText;

        [SerializeField]
        private List<SpellButton> spells = new();

        [SerializeField]
        private Transform tooltipContainer;

        [Header("     Input")]

        [SerializeField]
        private InputActionReference expandActionReference;
        private InputAction expandAction;

        private void Awake()
        {
            if (expandActionReference)
            {
                expandAction = expandActionReference.ToInputAction();
            }
        }

        protected virtual void OnEnable()
        {
            if (expandAction != null)
            {
                expandAction.started += OnExpandStarted; // ctx => SetTooltips(true);
                expandAction.canceled += OnExpandCanceled; // ctx => SetTooltips(false);

                expandAction.Enable();
            }
        }

        protected virtual void OnDisable()
        {
            if (expandAction != null)
            {
                expandAction.started -= OnExpandStarted;
                expandAction.canceled -= OnExpandCanceled;

                expandAction.Disable();
            }
        }

        public void Initialize(ScoreInstance score, Action<ScoreButton> onClickCallback)
        {
            Score = score;
            this.onClickCallback = onClickCallback;

            Debug.Assert(spells.Count <= score.Spells.Count);
            List<SpellType> tooltipCreated = new();
            for (int i = 0; i < spells.Count; i++)
            {
                SpellButton button = spells[i];
                SpellInstance spell = score.Spells[i];
                
                button.Initialize(spell, null);

                if (spell.Data.Type != SpellType.spell_none 
                    && !tooltipCreated.Contains(spell.Data.Type))
                {
                    SpellTooltip tooltip = Instantiate(tooltipPrefab, tooltipContainer);
                    tooltip.Initialize(spell);
                    tooltipCreated.Add(spell.Data.Type);
                }
            }

            UpdateVisuals();
            SetTooltips(false);
        }

        public void UpdateVisuals()
        {
            nameText.text = Score.Data.Name;
            if (button)
            {
                button.interactable = true;
            }

            Debug.Assert(spells.Count == Score.Spells.Count,
                         $"Spells Count ({spells.Count}) != Score Spells Count ({Score.Spells.Count}).",
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(Score.Spells[i], null);
            }
        }

        #region Request Spell

        public void RequestSpell(Action<SpellButton> onSpellSelect)
        {
            Sequence sequence = DOTween.Sequence();
            
            foreach (SpellButton spell in spells)
            {
                sequence.Insert(0, spell.Activate(s => OnSpellSelected(s, onSpellSelect)));
            }

            sequence.OnComplete(() =>
                                {
                                    EventSystem.current.SetSelectedGameObject(spells[0].gameObject);
                                });
            
            sequence.Play();
        }

        private void OnSpellSelected(SpellButton spell, Action<SpellButton> onSpellSelect)
        {
            foreach (SpellButton s in spells)
            {
                s.Deactivate();
            }

            EventSystem.current.SetSelectedGameObject(null);
            onSpellSelect?.Invoke(spell);
        }

        #endregion

        public override void OnClick()
        {
            base.OnClick();
            onClickCallback?.Invoke(this);
        }

        protected virtual void SetTooltips(bool set)
        {
            tooltipContainer.gameObject.SetActive(set && IsSelected);
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipContainer.transform as RectTransform);
        }

        public override void OnDeselect()
        {
            base.OnDeselect();
            SetTooltips(false);
        }
        
        private void OnExpandStarted(InputAction.CallbackContext ctx)
        {
            if (IsSelected)
            {
                SetTooltips(true);
            }
        }
        
        private void OnExpandCanceled(InputAction.CallbackContext ctx)
        {
            SetTooltips(false);
        }
    }
}