using System;
using DG.Tweening;
using Fantazee.Ui.ColorPalettes;
using Fantazee.Ui.ColorPalettes.Information;
using Fantazee.Ui.Settings;
using UnityEngine.EventSystems;
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

        [Header("Colors")]

        [SerializeField]
        private ColorPaletteType colorPalette;

        [Header("Simple Button")]

        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> outlines = new();

        [SerializeField]
        protected Button button;
        public Button Button => button;

        private ColorPaletteInformation paletteInfo;
        private ColorPalette ColorPalette
        {
            get
            {
                paletteInfo ??= GetPaletteInfo();
                return paletteInfo != null ? paletteInfo.Palettes : ColorPalette.Default;
            }
        }

        private ColorPaletteInformation GetPaletteInfo()
        {
            return UiSettings.Settings.ColorPalettes.TryGetInformation(colorPalette, out var info) ? info : null;
        }

        private void OnValidate()
        {
            paletteInfo = GetPaletteInfo();
            
            foreach (Graphic bg in backgrounds)
            {
                ColorPalette.Normal.ApplyBackground(bg);
            }

            foreach (Graphic outline in outlines)
            {
                ColorPalette.Normal.ApplyOutline(outline);
            }
        }

        private void OnEnable()
        {
            UpdateColors();
        }

        #region Ui Events

        public virtual void OnClick()
        {
            ClickFlash();
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

        protected void UpdateColors()
        {
            foreach (Graphic bg in backgrounds)
            {
                ColorPalette.Normal.ApplyBackground(bg);
            }

            foreach (Graphic outline in outlines)
            {
                ColorPalette.Normal.ApplyOutline(outline);
            }

            // Select will just be the outline and take piority
            if (IsSelected)
            {
                foreach (Graphic ol in outlines)
                {
                    ColorPalette.Selected.ApplyOutline(ol);
                }
            }

            if (IsDisabled)
            {
                foreach (Graphic bg in backgrounds)
                {
                    ColorPalette.Disabled.ApplyBackground(bg);
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

        public void ClickFlash()
        {
            Sequence sequence = DOTween.Sequence();
            
            foreach (Graphic bg in backgrounds)
            {
                var s = ColorPalette.Clicked.InOutBackground(bg);
                sequence.Insert(0, s);
            }

            foreach (Graphic outline in outlines)
            {
                var s = ColorPalette.Clicked.InOutOutline(outline);
                sequence.Insert(0, s);
            }

            sequence.OnComplete(UpdateColors);
            
            sequence.Play();
        }

        #endregion
    }
}