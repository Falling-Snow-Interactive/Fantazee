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
        
        [SerializeField]
        private PunchTweenAnim hitAnim;
        
        [SerializeField]
        private PunchTweenAnim deathAnim;
        
        [Header("References")]
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Idle();
        }

        public void Idle()
        {
            spriteRenderer.sprite = idle;
            idleTween.Play(spriteRenderer.transform);
        }

        public void Attack(Action onComplete = null)
        {
            spriteRenderer.sprite = attack;
            attackPunch.Play(spriteRenderer.transform, () =>
                                                       {
                                                           Idle();
                                                           onComplete?.Invoke();
                                                       });
        }
        
        public void Hit()
        {
            spriteRenderer.sprite = hit;
            hitAnim.Play(spriteRenderer.transform, Idle);
        }

        public void Death()
        {
            spriteRenderer.sprite = death;
        }
    }
}