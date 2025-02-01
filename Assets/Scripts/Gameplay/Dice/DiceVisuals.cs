using System;
using ProjectYahtzee.Dice.Information;
using ProjectYahtzee.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.Gameplay.Dice
{
    public class DiceVisuals : MonoBehaviour
    {
        [Range(1, 6)]
        [SerializeField]
        private int value = 1;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
                {
                    if (spriteRenderer)
                    {
                        spriteRenderer.sprite = info.Sprite;
                    }

                    if (image)
                    {
                        image.sprite = info.Sprite;
                    }
                }
            }
        }
        
        [Header("References")]
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Image image;

        private void OnValidate()
        {
            Value = value;
        }
    }
}
