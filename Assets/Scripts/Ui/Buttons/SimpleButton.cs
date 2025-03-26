using System;
using Fantazee.Scores.Settings;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.Ui.Buttons
{

    public class SimpleButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerClickHandler, IDeselectHandler
    {
        public event Action<SimpleButton> Selected;
        public event Action<SimpleButton> Deselected;

        private bool isSelected;
        private bool isDisabled;

        [Header("Simple Button")]

        [SerializeField]
        private List<BackgroundRef> backgroundRefs = new();

        [SerializeField]
        protected Button button;

        protected virtual BackgroundColorPalette ColorPalette => ScoreSettings.Settings.ButtonColorPalette;

        private void OnValidate()
        {
            foreach (BackgroundRef bg in backgroundRefs)
            {
                ColorPalette.NormalColors.Apply(bg);
            }
        }

        #region Ui Events
        
        public void OnPointerClick(PointerEventData _)
        {
            OnClick();
        }

        public virtual void OnClick()
        {
            foreach (BackgroundRef bg in backgroundRefs)
            {
                ColorPalette.ClickedColors.InOut(bg);
            }
        }

        public virtual void OnSelect()
        {
            isSelected = true;
            UpdateColors();
            Selected?.Invoke(this);
        }

        public void OnSelect(BaseEventData _)
        {
            OnSelect();
        }

        public virtual void OnDeselect()
        {
            isSelected = false;
            UpdateColors();
            Deselected?.Invoke(this);
        }

        public void OnDeselect(BaseEventData _)
        {
            OnDeselect();
        }

        public virtual void OnPointerEnter()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnPointerEnter(PointerEventData _)
        {
            OnPointerEnter();
        }

        public virtual void OnPointerExit()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        
        public void OnPointerExit(PointerEventData _)
        {
            OnPointerEnter();
        }

        private void UpdateColors()
        {
            foreach (BackgroundRef bg in backgroundRefs)
            {
                ColorPalette.NormalColors.Apply(bg);
            }

            // Select will just be the outline and take piority
            if (isSelected)
            {
                foreach (BackgroundRef bg in backgroundRefs)
                {
                    ColorPalette.SelectedColors.ApplyOutline(bg);
                }
            }

            if (isDisabled)
            {
                foreach (BackgroundRef bg in backgroundRefs)
                {
                    ColorPalette.DisabledColors.ApplyBackground(bg);
                }
            }
        }

        public void SetInteractable(bool set)
        {
            bool back = isSelected;

            if (button)
            {
                button.interactable = set;
            }
            isDisabled = !set;

            if (back)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }

            UpdateColors();
        }

        #endregion
    }
}