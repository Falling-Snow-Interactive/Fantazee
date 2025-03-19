using System;
using DG.Tweening;
using Fantazee.Battle.Characters.Animation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Characters
{
    public class GameplayCharacterVisuals : MonoBehaviour
    {
        [Header("Frames")]

        [SerializeField]
        private Sprite idle;

        [SerializeField]
        private Sprite action;

        [SerializeField]
        private Sprite attack;

        [SerializeField]
        private Sprite hit;

        [SerializeField]
        private Sprite death;

        [SerializeField]
        private Sprite victory;

        [Header("Animations")]

        [SerializeField]
        private TweenAnim idleTween;

        [SerializeField]
        private PunchTweenAnim actionPunch;

        [SerializeField]
        private PunchTweenAnim attackPunch;
        
        [FormerlySerializedAs("hitAnim")]
        [SerializeField]
        private PunchTweenAnim hitPunch;

        [SerializeField]
        private TweenAnim deathAnim;

        [SerializeField]
        private TweenAnim victoryAnim;

        [SerializeField]
        private float resetTime = 0.2f;
        
        [Header("References")]
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Idle();
        }

        public void ResetTransform(Action onComplete = null, bool force = false)
        {
            DOTween.Kill(spriteRenderer.transform);

            if (force)
            {
                spriteRenderer.transform.localScale = Vector3.one;
                spriteRenderer.transform.localPosition = Vector3.zero;
                spriteRenderer.transform.localRotation = Quaternion.identity;
                onComplete?.Invoke();
                return;
            }

            Sequence reset = DOTween.Sequence();
            
            Tween resetScale = spriteRenderer.transform.DOScale(Vector3.one, resetTime);
            Tweener resetPos = spriteRenderer.transform.DOLocalMove(Vector3.zero, resetTime);
            Tweener resetRot = spriteRenderer.transform.DOLocalRotate(Vector3.zero, resetTime);
            
            reset.Insert(0, resetScale);
            reset.Insert(0, resetPos);
            reset.Insert(0, resetRot);
            
            reset.OnComplete(() => onComplete?.Invoke());

            reset.Play();
        }

        public void Idle()
        {
            ResetTransform(() =>
                           {
                               spriteRenderer.sprite = idle;
                               idleTween.Play(spriteRenderer.transform);
                           });
            
        }
        
        public void Action(Action onComplete = null)
        {
            ResetTransform(() =>
                                  {
                                      spriteRenderer.sprite = action;
                                      actionPunch.Play(spriteRenderer.transform, () =>
                                                       {
                                                           Idle();
                                                           onComplete?.Invoke();
                                                       });
                                  });
            
        }

        public void Attack(Action onComplete = null)
        {
            ResetTransform(() =>
                                  {
                                      spriteRenderer.sprite = attack;
                                      attackPunch.Play(spriteRenderer.transform, () =>
                                                       {
                                                           Idle();
                                                           onComplete?.Invoke();
                                                       });
                                  });
        }
        
        public void Hit(Action onComplete = null)
        {
            ResetTransform(() =>
                           {
                               spriteRenderer.sprite = hit;
                               hitPunch.Play(spriteRenderer.transform, () =>
                                                                       {
                                                                           Idle();
                                                                           onComplete?.Invoke();
                                                                       });
                           });
        }

        public void Death(Action onComplete = null)
        {
            ResetTransform(() =>
                           {
                               spriteRenderer.sprite = death;
                               deathAnim.Play(spriteRenderer.transform, onComplete);
                           });
        }

        public void Victory(Action onComplete = null)
        {
            ResetTransform(() =>
                           {
                               spriteRenderer.sprite = victory;
                               victoryAnim.Play(spriteRenderer.transform, onComplete);
                           });
        }
    }
}