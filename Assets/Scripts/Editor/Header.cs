using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fantazee;

public class Header : Label
{
    private const float H0Size = 24f;
    private const float H1Size = 20f;
    private const float H2Size = 16f;
    
    public Header(string text) : base(text)
    {
        style.fontSize = H0Size;
    }

    public Header(int size, string text) : base(text)
    {
        Debug.Assert(size is 0 or 1 or 2);
        style.fontSize = size switch
                         {
                             0 => H0Size,
                             1 => H1Size,
                             2 => H2Size,
                             _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
                         };
    }
}