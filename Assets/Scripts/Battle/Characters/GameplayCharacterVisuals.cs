using UnityEngine;

namespace Fantazee.Battle.Characters
{
    public class GameplayCharacterVisuals : MonoBehaviour
    {
        [Header("Params")]

        [SerializeField]
        private string idleParam = "idle";
        
        [SerializeField]
        private string actionParam = "action";
        
        [SerializeField]
        private string attackParam = "attack";
        
        [SerializeField]
        private string hitParam = "hit";
        
        [SerializeField]
        private string deathParam = "death";
        
        [SerializeField]
        private string victoryParam = "victory";
        
        [Header("References")]
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        [SerializeField]
        private Animator animator;
        
        public void Action()
        {
            animator.SetTrigger(actionParam);
        }

        public void Attack()
        {
            animator.SetTrigger(attackParam);
        }
        
        public void Hit()
        {
            animator.SetTrigger(hitParam);
        }

        public void Death()
        {
            animator.SetTrigger(deathParam);
        }

        public void Victory()
        {
            animator.SetTrigger(victoryParam);
        }
    }
}