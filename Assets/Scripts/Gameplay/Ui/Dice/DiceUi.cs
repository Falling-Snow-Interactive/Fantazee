using System;
using DG.Tweening;
using ProjectYahtzee.Dice.Information;
using ProjectYahtzee.Dice.Settings;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ProjectYahtzee.Gameplay.Ui.Dice
{
    public class DiceUi : MonoBehaviour
    {
        [Range(1,6)]
        [SerializeField]
        private int value;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
                {
                    if (image)
                    {
                        image.sprite = info.Sprite;
                    }
                }
            }
        }
        
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
        private bool locked;

        public bool Locked => locked;

        [SerializeField]
        private float lockOffset = -1;

        [SerializeField]
        private float lockTime = 0.2f;
        
        [SerializeField]
        private Ease lockEase = Ease.OutBounce;
        
        [Header("References")]
        
        [SerializeField]
        private Image image;
        
        //Gameplay
        private Vector2 rootPosition;

        private void OnValidate()
        {
            Value = value;
        }

        private void Awake()
        {
            rootPosition = transform.position;
        }
        
        private void Start()
        {
            timer = 1 / fps;
        }
        
        private void FixedUpdate()
        {
            if (rolling)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 1 / fps;
                    Value = Random.Range(1, 6);
                }
            }
        }

        public void Roll(float delay = 0)
        {
            rolling = true;
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOMoveY(rootPosition.y + rollHeight, throwTime).SetEase(throwEase));
            sequence.Append(transform.DOMoveY(rootPosition.y, fallTime).SetEase(fallEase));

            sequence.OnComplete(() => rolling = false);
            sequence.SetDelay(delay);
            sequence.Play();
        }

        public void ToggleLock()
        {
            locked = !locked;

            float pos = locked ? rootPosition.y + lockOffset : rootPosition.y;
            transform.DOMoveY(pos, lockTime).SetEase(lockEase);
        }
    }
}