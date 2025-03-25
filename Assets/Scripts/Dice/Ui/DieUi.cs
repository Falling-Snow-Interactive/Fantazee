using System;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Dice.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Ui;
using Fantazee.Ui.Buttons;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;

namespace Fantazee.Dice.Ui
{
    public class DieUi : SimpleButton
    {
        public Die Die { get; private set; }

        [SerializeField]
        private BackgroundColorPalette colorPalette;
        protected override BackgroundColorPalette ColorPalette => colorPalette;

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
        private Vector3 lockRot = new Vector3(0, 0, 25);
        
        [SerializeField]
        private Vector3 lockScale = new Vector3(0.8f, 0.8f, 0.8f);

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
        
        // Audio
        private EventInstance rollSfx;
        private EventInstance squishSfx;
        private EventInstance showSfx;
        private EventInstance hideSfx;
        private EventInstance lockSfx;
        private EventInstance unlockSfx;
        
        [Header("References")]
        
        [SerializeField]
        private Image image;
        public Image Image => image;

        [SerializeField]
        private Transform root;

        private void Awake()
        {
            rollSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.RollSfx);
            squishSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.SquishSfx);
            showSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.ShowSfx);
            hideSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.HideSfx);
            lockSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.LockSfx);
            unlockSfx = RuntimeManager.CreateInstance(DiceSettings.Settings.UnlockSfx);
        }
        
        private void Start()
        {
            timer = 1 / fps;
        }

        public void ResetDice()
        {
            ToggleOff();
            root.gameObject.SetActive(true);
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
            if (rolling)
            {
                return;
            }
            
            ResetDice();
            rolling = true;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(root.DOLocalMoveY(rollHeight, throwTime)
                                .SetEase(throwEase)
                                .OnStart(() => rollSfx.start()));
            sequence.Append(root.DOLocalMoveY(0, fallTime)
                                .SetEase(fallEase));
            
            sequence.OnComplete(() =>
                                {
                                    rolling = false;
                                    SetImage(Die.Value); 
                                    onRollComplete?.Invoke(Die);
                                });
            sequence.SetDelay(delay);
            sequence.Play();
        }

        public void SetImage(int value)
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
            bool shouldLock = !BattleController.Instance.Player.LockedDice.Contains(Die);
            if (shouldLock)
            {
                BattleController.Instance.Player.LockedDice.Add(Die);
                lockSfx.start();
            }
            else
            {
                BattleController.Instance.Player.LockedDice.Remove(Die);
                unlockSfx.start();
            }

            Vector3 rot = shouldLock ? lockRot : Vector3.zero;
            Vector3 scale = shouldLock ? lockScale : Vector3.one;
            
            root.DOLocalRotate(rot, lockTime).SetEase(lockEase);
            root.DOScale(scale, lockTime).SetEase(lockEase);
        }

        public void ToggleOff()
        {
            BattleController.Instance.Player.LockedDice.Remove(Die);
            
            root.DOLocalRotate(Vector3.zero, lockTime).SetEase(lockEase);
            root.DOScale(Vector3.one, lockTime).SetEase(lockEase);
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                root.localPosition = hideOffset;
                return;
            }

            hideSfx.start();
            root.DOLocalMove(hideOffset, hideTime)
                 .SetEase(hideEase)
                 .SetDelay(delay)
                 .OnComplete(() => onComplete?.Invoke());
        }
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                root.localPosition = Vector3.zero;
                return;
            }

            showSfx.start();
            root.DOLocalMove(Vector3.zero, showTime)
                 .SetEase(showEase)
                 .SetDelay(delay)
                 .OnComplete(() => onComplete?.Invoke());
        }

        public void Squish()
        {
            root.transform.localScale = Vector3.one;
            squishSfx.start();
            root.transform.DOPunchScale(DiceSettings.Settings.SquishAmount, 
                                        DiceSettings.Settings.SquishTime, 
                                         10, 
                                         1f)
                 .SetEase(DiceSettings.Settings.SquishEase);
        }
    }
}