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
        private Sprite attack;

        [SerializeField]
        private Sprite hit;

        [SerializeField]
        private Sprite death;

        [Header("Animations")]

        [SerializeField]
        private TweenAnim idleTween;

        [SerializeField]
        private PunchTweenAnim attackPunch;
        
        [FormerlySerializedAs("hitAnim")]
        [SerializeField]
        private PunchTweenAnim hitPunch;

        [SerializeField]
        private TweenAnim deathAnim;
        
        [Header("References")]
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Idle();
        }

        private void ResetTransform()
        {
            DOTween.Complete(spriteRenderer.transform);
            spriteRenderer.transform.localScale = Vector3.one;
            spriteRenderer.transform.localPosition = Vector3.zero;
            spriteRenderer.transform.localRotation = Quaternion.identity;
        }

        public void Idle()
        {
            ResetTransform();
            spriteRenderer.sprite = idle;
            idleTween.Play(spriteRenderer.transform);
        }

        public void Attack(Action onComplete = null)
        {
            ResetTransform();
            spriteRenderer.sprite = attack;
            attackPunch.Play(spriteRenderer.transform, () =>
                                                       {
                                                           Idle();
                                                           onComplete?.Invoke();
                                                       });
        }
        
        public void Hit(Action onComplete = null)
        {
            ResetTransform();
            spriteRenderer.sprite = hit;
            hitPunch.Play(spriteRenderer.transform, () =>
                                                    {
                                                        Idle();
                                                        onComplete?.Invoke();
                                                    });
        }

        public void Death(Action onComplete = null)
        {
            ResetTransform();
            spriteRenderer.sprite = death;
            deathAnim.Play(spriteRenderer.transform, onComplete);
        }
    }
}