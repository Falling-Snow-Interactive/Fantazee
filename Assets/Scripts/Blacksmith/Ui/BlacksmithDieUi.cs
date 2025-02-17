using DG.Tweening;
using ProjectYahtzee.Items.Dice.Information;
using ProjectYahtzee.Items.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.Blacksmith.Ui
{
    public class BlacksmithDieUi : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private float scale = 1.25f;

        [SerializeField]
        private float time = 0.25f;
        
        public void SetImage(int value)
        {
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                image.sprite = info.Sprite;
            }
        }

        public void Select()
        {
            image.transform.DOScale(Vector3.one * scale, time).SetEase(Ease.OutBounce);
        }

        public void Deselect()
        {
            image.transform.DOScale(Vector3.one, time).SetEase(Ease.InBounce);
        }
    }
}