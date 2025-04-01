using System;
using UnityEngine.UI;

namespace Fantazee.Ui;

[Serializable]
public class BackgroundRef
{
    [SerializeField]
    private Graphic background;
    public Graphic Background => background;
    
    [SerializeField]
    private Graphic outline;
    public Graphic Outline => outline;
}