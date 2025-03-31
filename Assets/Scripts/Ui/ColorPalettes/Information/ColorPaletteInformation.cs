using System;
using fsi.settings.Informations;

namespace Fantazee.Ui.ColorPalettes.Information
{
    [Serializable]
    public class ColorPaletteInformation : Information<ColorPaletteType>
    {
        [SerializeField]
        private ColorPalette palette = new();
        public ColorPalette Palette => palette;
    }
}