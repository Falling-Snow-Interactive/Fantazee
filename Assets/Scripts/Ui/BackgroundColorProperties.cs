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

    public void ApplyBackground(Graphic bg)
    {
        bg.color = background;
    }

    public void ApplyOutline(Graphic ol)
    {
        ol.color = outline;
    }

    public Sequence InOutBackground(Graphic bg)
    {
        Sequence sequence = DOTween.Sequence();
        
        Color start = bg.color;
        Color end = background;
        Tween s0 = bg.DOColor(end, 0.05f);
        Tween s1 = bg.DOColor(start, 0.05f);

        sequence.Insert(0, s0);
        sequence.Insert(0.1f, s1);

        sequence.Play();
        
        return sequence;
    }

    public Sequence InOutOutline(Graphic ol)
    {
        Sequence sequence = DOTween.Sequence();
        
        Color start = ol.color;
        Color end = outline;
        Tween s0 = ol.DOColor(end, 0.05f);
        Tween s1 = ol.DOColor(start, 0.05f);

        sequence.Insert(0, s0);
        sequence.Insert(0.1f, s1);

        sequence.Play();
        
        return sequence;
    }
}