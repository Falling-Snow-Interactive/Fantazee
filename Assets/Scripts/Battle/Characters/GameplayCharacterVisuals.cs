using System;
using UnityEngine;

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
        private CharacterTweenAnimation idleAnimation;
        
        [SerializeField]
        private CharacterTweenAnimation attackAnimation;
        
        [SerializeField]
        private CharacterTweenAnimation hitAnimation;
        
        [SerializeField]
        private CharacterTweenAnimation deathAnimation;
        
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
            idleAnimation.Play(transform);
        }

        public void Attack(Action onComplete = null)
        {
            spriteRenderer.sprite = attack;
            attackAnimation.Play(transform, () =>
                                            {
                                                Idle();
                                                onComplete?.Invoke();
                                            });
        }
        
        public void Hit()
        {
            spriteRenderer.sprite = hit;
            hitAnimation.Play(transform, Idle);
        }

        public void Death()
        {
            spriteRenderer.sprite = death;
        }
    }
}