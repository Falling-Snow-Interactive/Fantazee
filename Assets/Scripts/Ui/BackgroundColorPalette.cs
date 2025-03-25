using System;

namespace Fantazee.Ui;

[Serializable]
public class BackgroundColorPalette
{
    #region Constants
    private static Color NormalBgColor => new(81f/255f, 45f/255f,13f/255f,255f/255f);
    private static Color NormalOutlineColor => new(127f/255f, 71f/255f,9f/255f,255f/255f);

    private static Color SelectBgColor => new(81f/255f, 45f/255f,13f/255f,255f/255f);
    private static Color SelectOutlineColor => new(203f/255f, 123f/255f,9f/255f,255f/255f);
        
    private static Color DisableBgColor => new(41f/255f, 21f/255f,13/255f,255f/255f);
    private static Color DisableOutlineColor => new(127f/255f, 71f/255f,9f/255f,255f/255f);
        
    private static Color ClickBgColor => new(139/255f, 83/255f,16/255f,255f/255f);
    private static Color ClickOutlineColor => new(127f/255f, 71f/255f,9f/255f,255f/255f);
    #endregion
    
    [SerializeField]
    private BackgroundColorProperties normalColors = new(NormalBgColor, NormalOutlineColor);
    public BackgroundColorProperties NormalColors => normalColors;
        
    [SerializeField]
    private BackgroundColorProperties selectedColors = new(SelectBgColor, SelectOutlineColor);
    public BackgroundColorProperties SelectedColors => selectedColors;
        
    [SerializeField]
    private BackgroundColorProperties disabledColors = new(DisableBgColor, DisableOutlineColor);
    public BackgroundColorProperties DisabledColors => disabledColors;

    [SerializeField]
    private BackgroundColorProperties clickedColors = new(ClickBgColor, ClickOutlineColor);
    public BackgroundColorProperties ClickedColors => clickedColors;
    
    public void ResetColors()
    {
        normalColors = new BackgroundColorProperties(NormalBgColor, NormalOutlineColor);
        selectedColors = new BackgroundColorProperties(SelectBgColor, SelectOutlineColor);
        disabledColors = new BackgroundColorProperties(DisableBgColor, DisableOutlineColor);
        clickedColors = new BackgroundColorProperties(ClickBgColor, ClickOutlineColor);
    }
}