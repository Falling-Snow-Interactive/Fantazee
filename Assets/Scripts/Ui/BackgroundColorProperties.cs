using System;
using UnityEngine.UI;

namespace Fantazee.Ui;

[Serializable]
public class BackgroundColorProperties
{
    [SerializeField]
    private Color background;
    public Color Background => background;

    [SerializeField]
    private Color outline;
    public Color Outline => outline;

    public BackgroundColorProperties(Color background, Color outline)
    {
        this.background = background;
        this.outline = outline;
    }

    public void Apply(BackgroundRef backgroundRef)
    {
        backgroundRef.Background.color = background;
        backgroundRef.Outline.color = outline;
    }
}