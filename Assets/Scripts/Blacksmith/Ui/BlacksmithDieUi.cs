using DG.Tweening;
using Fantazhee.Items.Dice.Information;
using Fantazhee.Items.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazhee.Blacksmith.Ui
{
    public class BlacksmithDieUi : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private float scale = 1.25f;

        [SerializeField]
        private float time = 0.25f;

        [SerializeField]
        private Ease ease = Ease.Linear;
        
        public void SetImage(int value)
        {
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                image.sprite = info.Sprite;
            }
        }

        public void Select()
        {
            image.transform.DOScale(Vector3.one * scale, time).SetEase(ease);
        }

        public void Deselect()
        {
            image.transform.DOScale(Vector3.one, time).SetEase(ease);
        }
    }
}