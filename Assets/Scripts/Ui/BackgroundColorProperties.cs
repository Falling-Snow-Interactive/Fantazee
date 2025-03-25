using System;
using DG.Tweening;
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
        ApplyBackground(backgroundRef);
        ApplyOutline(backgroundRef);
    }

    public void ApplyBackground(BackgroundRef backgroundRef)
    {
        if (backgroundRef.Background)
        {
            backgroundRef.Background.color = background;
        }
    }

    public void ApplyOutline(BackgroundRef backgroundRef)
    {
        if (backgroundRef.Outline)
        {
            backgroundRef.Outline.color = outline;
        }
    }

    public Sequence InOut(BackgroundRef backgroundRef)
    {
        Sequence sequence = DOTween.Sequence();
        
        if (backgroundRef.Background)
        {
            Color start = backgroundRef.Background.color;
            Color end = background;
            Tween s0 = backgroundRef.Background.DOColor(end, 0.05f);
            Tween s1 = backgroundRef.Background.DOColor(start, 0.05f);

            sequence.Insert(0, s0);
            sequence.Insert(0.1f, s1);
        }

        if (backgroundRef.Outline)
        {
            Color start = backgroundRef.Outline.color;
            Color end = outline;
            Tween s0 = backgroundRef.Outline.DOColor(end, 0.05f);
            Tween s1 = backgroundRef.Outline.DOColor(start, 0.05f);

            sequence.Insert(0, s0);
            sequence.Insert(0.1f, s1);
        }

        sequence.Play();
        
        return sequence;
    }
}