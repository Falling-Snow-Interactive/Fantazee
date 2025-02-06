using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ProjectYahtzee.Dice.Information;
using ProjectYahtzee.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ProjectYahtzee.Battle.Ui.Dices
{
    public class DiceUi : MonoBehaviour
    {
        private Battle.Dices.Dice dice;
        public Battle.Dices.Dice Dice => dice;
        
        [Header("Rolling")]
        
        public bool rolling = false;

        [SerializeField]
        private float fps = 25;

        [SerializeField]
        private float timer = 0;

        [SerializeField]
        private float rollHeight = 10;

        [SerializeField]
        private float throwTime = 1f;

        [SerializeField]
        private Ease throwEase = Ease.OutQuad;

        [SerializeField]
        private float fallTime = 1f;
        
        [SerializeField]
        private Ease fallEase = Ease.OutBounce;

        [Header("Lock")]

        [SerializeField]
        private float lockOffset = -1;

        [SerializeField]
        private float lockTime = 0.2f;
        
        [SerializeField]
        private Ease lockEase = Ease.OutBounce;
        
        [Header("Hide/Show")]

        [SerializeField]
        private float hideOffset = 3f;

        [SerializeField]
        private float hideTime = 0.5f;

        [SerializeField]
        private Ease hideEase = Ease.Linear;

        [SerializeField]
        private float showTime = 0.5f;
        
        [SerializeField]
        private Ease showEase = Ease.Linear;
        
        [Header("References")]
        
        [SerializeField]
        private Image image;
        public Image Image => image;
        
        private void Start()
        {
            timer = 1 / fps;
        }

        public void ResetDice()
        {
            image.transform.localPosition = Vector3.zero;
            image.transform.localScale = Vector3.one;
            if (dice != null)
            {
                SetImage(dice.Value);
            }
        }
        
        private void FixedUpdate()
        {
            if (rolling)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 1 / fps;
                    int v = Random.Range(1, 6);
                    SetImage(v);
                }
            }
        }

        public void Initialize(Battle.Dices.Dice d)
        {
            ResetDice();
            rolling = false;
            dice = d;
            SetImage(dice.Value);
        }

        public void Roll(float delay = 0)
        {
            ResetDice();
            
            rolling = true;
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(image.transform.DOLocalMoveY(rollHeight, throwTime).SetEase(throwEase));
            sequence.Append(image.transform.DOLocalMoveY(0, fallTime).SetEase(fallEase));

            sequence.OnComplete(() =>
                                {
                                    rolling = false;
                                    SetImage(dice.Value);
                                });
            sequence.SetDelay(delay);
            sequence.Play();
        }

        private void SetImage(int value)
        {
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                image.sprite = info.Sprite;
            }
        }

        public void ToggleLock()
        {
            dice.ToggleLock();
            float offset = dice.Locked ? lockOffset : 0;
            image.transform.DOLocalMoveY(offset, lockTime).SetEase(lockEase);
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                Vector3 vector3 = transform.localPosition;
                vector3.y = hideOffset;
                transform.localPosition = vector3;
                return;
            }

            transform.DOLocalMoveX(hideOffset, hideTime)
                     .SetEase(hideEase)
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                image.transform.localPosition = Vector3.zero;
                return;
            }

            transform.DOLocalMoveY(0, showTime)
                     .SetEase(showEase)                     
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
    }
}