using System;

namespace Fantazee.Ui.ColorPalettes
{

    [Serializable]
    public class ColorPalette
    {
        #region Constants

        #region Default

        private static Color NormalBgColor => new(81f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color NormalOutlineColor => new(127f / 255f, 71f / 255f, 9f / 255f, 255f / 255f);

        private static Color SelectBgColor => new(81f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color SelectOutlineColor => new(203f / 255f, 123f / 255f, 9f / 255f, 255f / 255f);

        private static Color DisableBgColor => new(41f / 255f, 21f / 255f, 13 / 255f, 255f / 255f);
        private static Color DisableOutlineColor => new(127f / 255f, 71f / 255f, 9f / 255f, 255f / 255f);

        private static Color ClickBgColor => new(139 / 255f, 83 / 255f, 16 / 255f, 255f / 255f);
        private static Color ClickOutlineColor => new(127f / 255f, 71f / 255f, 9f / 255f, 255f / 255f);

        #endregion

        #endregion

        [SerializeField]
        private ButtonColorProperty normalColors;
        public ButtonColorProperty NormalColors => normalColors;

        [SerializeField]
        private ButtonColorProperty selectedColors;
        public ButtonColorProperty SelectedColors => selectedColors;

        [SerializeField]
        private ButtonColorProperty disabledColors;
        public ButtonColorProperty DisabledColors => disabledColors;

        [SerializeField]
        private ButtonColorProperty clickedColors;
        public ButtonColorProperty ClickedColors => clickedColors;

        public ColorPalette()
        {
            normalColors = new(NormalBgColor, NormalOutlineColor);
            selectedColors = new(SelectBgColor, SelectOutlineColor);
            disabledColors = new(DisableBgColor, DisableOutlineColor);
            clickedColors = new(ClickBgColor, ClickOutlineColor);
        }

        public void ResetColors()
        {
            normalColors = new(NormalBgColor, NormalOutlineColor);
            selectedColors = new(SelectBgColor, SelectOutlineColor);
            disabledColors = new(DisableBgColor, DisableOutlineColor);
            clickedColors = new(ClickBgColor, ClickOutlineColor);
        }

        public static ColorPalette Default
        {
            get
            {
                ColorPalette palette = new ColorPalette();
                palette.ResetColors();
                return palette;
            }
        }
    }
}