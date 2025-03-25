using System;
using UnityEngine.UI;

namespace Fantazee.Ui;

[Serializable]
public class BackgroundRef
{
    [SerializeField]
    private Image background;
    public Image Background => background;
    
    [SerializeField]
    private Image outline;
    public Image Outline => outline;
    
    public bool IsValid => background != null && outline != null;
}