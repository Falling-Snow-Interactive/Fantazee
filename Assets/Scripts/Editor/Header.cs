using UnityEngine.UIElements;

namespace Fantazee;

public class Header : Label
{
    private  const float DefaultFontSize = 24f;
    
    public Header(string text) : base(text)
    {
        style.fontSize = DefaultFontSize;
    }
}