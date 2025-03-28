using System;
using Fantazee.Scores.Settings;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Ui.Buttons
{

    public class SimpleButton : MonoBehaviour, 
                                ISelectHandler, 
                                IPointerEnterHandler, 
                                IDeselectHandler
    {
        public event Action<SimpleButton> Selected;
        public event Action<SimpleButton> Deselected;

        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        [Header("Simple Button")]

        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> outlines = new();

        [SerializeField]
        protected Button button;

        protected virtual BackgroundColorPalette ColorPalette => ScoreSettings.Settings.ButtonColorPalette;

        private void OnValidate()
        {
            foreach (Graphic bg in backgrounds)
            {
                ColorPalette.NormalColors.ApplyBackground(bg);
            }
        }

        #region Ui Events
        
        public void OnPointerClick(PointerEventData _)
        {
            OnClick();
        }

        public virtual void OnClick()
        {
            foreach (Graphic bg in backgrounds)
            {
                ColorPalette.ClickedColors.InOutBackground(bg);
            }
        }

        public virtual void OnSelect()
        {
            IsSelected = true;
            UpdateColors();
            Selected?.Invoke(this);
        }

        public void OnSelect(BaseEventData _)
        {
            OnSelect();
        }

        public virtual void OnDeselect()
        {
            IsSelected = false;
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
            OnPointerExit();
        }

        private void UpdateColors()
        {
            foreach (Graphic bg in backgrounds)
            {
                ColorPalette.NormalColors.ApplyBackground(bg);
            }

            foreach (Graphic outline in outlines)
            {
                ColorPalette.NormalColors.ApplyOutline(outline);
            }

            // Select will just be the outline and take piority
            if (IsSelected)
            {
                foreach (Graphic ol in outlines)
                {
                    ColorPalette.SelectedColors.ApplyOutline(ol);
                }
            }

            if (IsDisabled)
            {
                foreach (Graphic bg in backgrounds)
                {
                    ColorPalette.DisabledColors.ApplyBackground(bg);
                }
            }
        }

        public void SetInteractable(bool set)
        {
            bool back = IsSelected;

            if (button)
            {
                button.interactable = set;
            }
            IsDisabled = !set;

            if (back)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }

            UpdateColors();
        }

        #endregion
    }
}