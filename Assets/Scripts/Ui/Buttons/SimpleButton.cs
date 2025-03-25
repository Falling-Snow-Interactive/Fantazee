using Fantazee.Scores.Settings;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.Ui.Buttons;

public class SimpleButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
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

    public virtual void OnClick()
    {
        Debug.Log($"OnClick: {gameObject.name}");
        foreach (BackgroundRef bg in backgroundRefs)
        {
            ColorPalette.ClickedColors.InOut(bg);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log($"OnSelect: {gameObject.name}");
        isSelected = true;
        UpdateColors();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log($"OnDeselect: {gameObject.name}");
        isSelected = false;
        UpdateColors();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"OnPointerEnter: {gameObject.name}");
        EventSystem.current.SetSelectedGameObject(gameObject);
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
        
        button.interactable = set;
        isDisabled = !set;

        if (back)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        UpdateColors();
    }

    #endregion
}