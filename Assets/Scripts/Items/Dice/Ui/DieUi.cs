using System;
using DG.Tweening;
using ProjectYahtzee.Battle;
using ProjectYahtzee.Items.Dice.Information;
using ProjectYahtzee.Items.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ProjectYahtzee.Items.Dice.Ui
{
    public class DieUi : MonoBehaviour
    {
        public Die Die { get; private set; }

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
        private Vector3 lockOffset = new Vector3(0, -1, 0);

        [SerializeField]
        private float lockTime = 0.2f;
        
        [SerializeField]
        private Ease lockEase = Ease.OutBounce;
        
        [Header("Hide/Show")]

        [SerializeField]
        private Vector3 hideOffset = new Vector3(0, -3, 0);

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
            image.gameObject.SetActive(true);
            if (Die != null)
            {
                SetImage(Die.Value);
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

        public void Initialize(Die d)
        {
            ResetDice();
            rolling = false;
            Die = d;
            SetImage(Die.Value);
        }

        public void Roll(float delay = 0, Action<Die> onRollComplete = null)
        {
            ResetDice();
            
            rolling = true;
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(image.transform.DOLocalMoveY(rollHeight, throwTime).SetEase(throwEase));
            sequence.Append(image.transform.DOLocalMoveY(0, fallTime).SetEase(fallEase));

            sequence.OnComplete(() =>
                                {
                                    rolling = false;
                                    SetImage(Die.Value);
                                    onRollComplete?.Invoke(Die);
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

        public void UpdateImage()
        {
            if (DiceSettings.Settings.SideInformation.TryGetInformation(Die.Value, out SideInformation info))
            {
                image.sprite = info.Sprite;
            }
        }

        public void ToggleLock()
        {
            bool shouldLock = !BattleController.Instance.LockedDice.Contains(Die);
            if (shouldLock)
            {
                BattleController.Instance.LockedDice.Add(Die);
            }
            else
            {
                BattleController.Instance.LockedDice.Remove(Die);
            }

            Vector3 offset = shouldLock ? lockOffset : Vector3.zero;
            image.transform.DOLocalMove(offset, lockTime).SetEase(lockEase);
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                image.transform.localPosition = hideOffset;
                return;
            }

            image.transform.DOLocalMove(hideOffset, hideTime)
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

            image.transform.DOLocalMove(Vector3.zero, showTime)
                 .SetEase(showEase)
                 .SetDelay(delay)
                 .OnComplete(() => onComplete?.Invoke());
        }
    }
}