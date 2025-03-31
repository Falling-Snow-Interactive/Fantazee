using System;
using Fantazee.Ui.ColorPalettes.Properties;
using UnityEngine.Serialization;

namespace Fantazee.Ui.ColorPalettes
{

    [Serializable]
    public class ColorPalette
    {
        #region Constants

        #region Default

        private static Color NormalBgColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color NormalOutlineColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);

        private static Color SelectBgColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color SelectOutlineColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);

        private static Color DisableBgColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color DisableOutlineColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);

        private static Color ClickBgColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);
        private static Color ClickOutlineColor => new(80f / 255f, 45f / 255f, 13f / 255f, 255f / 255f);

        #endregion

        #endregion

        [FormerlySerializedAs("normalColors")]
        [SerializeField]
        private ColorPaletteProperties normal;
        public ColorPaletteProperties Normal => normal;

        [FormerlySerializedAs("selectedColors")]
        [SerializeField]
        private ColorPaletteProperties selected;
        public ColorPaletteProperties Selected => selected;

        [FormerlySerializedAs("disabledColors")]
        [SerializeField]
        private ColorPaletteProperties disabled;
        public ColorPaletteProperties Disabled => disabled;

        [FormerlySerializedAs("clickedColors")]
        [SerializeField]
        private ColorPaletteProperties clicked;
        public ColorPaletteProperties Clicked => clicked;

        public ColorPalette()
        {
            normal = new(NormalBgColor, NormalOutlineColor);
            selected = new(SelectBgColor, SelectOutlineColor);
            disabled = new(DisableBgColor, DisableOutlineColor);
            clicked = new(ClickBgColor, ClickOutlineColor);
        }

        public void ResetColors()
        {
            normal = new(NormalBgColor, NormalOutlineColor);
            selected = new(SelectBgColor, SelectOutlineColor);
            disabled = new(DisableBgColor, DisableOutlineColor);
            clicked = new(ClickBgColor, ClickOutlineColor);
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