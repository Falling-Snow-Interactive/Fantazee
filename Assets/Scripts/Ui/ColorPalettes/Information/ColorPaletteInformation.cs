using System;
using fsi.settings.Informations;
using UnityEngine.Serialization;

namespace Fantazee.Ui.ColorPalettes.Information
{
    [Serializable]
    public class ColorPaletteInformation : Information<ColorPaletteType>
    {
        [FormerlySerializedAs("palette")]
        [SerializeField]
        private ColorPalette palettes = new();
        public ColorPalette Palettes => palettes;
    }
}