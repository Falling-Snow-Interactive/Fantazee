using UnityEngine;

namespace Fantazee.Battle.DamageNumbers
{
    public class DamageNumbersController : MonoBehaviour
    {
        [SerializeField] 
        private DamageNumber damageNumber;

        [SerializeField]
        private DamageNumber healingNumber;

        [SerializeField]
        private DamageNumber shieldNumber;

        [SerializeField]
        private Transform content;
        
        public void AddDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }
            DamageNumber dn = Instantiate(damageNumber, content);
            dn.SetValue(damage);
        }

        public void AddHealing(int healing)
        {
            if (healing <= 0)
            {
                return;
            }
            DamageNumber dn = Instantiate(healingNumber, content);
            dn.SetValue(healing);
        }

        public void AddShield(int shield)
        {
            if (shield <= 0)
            {
                return;
            }
            DamageNumber dn = Instantiate(shieldNumber, content);
            dn.SetValue(shield);
        }
    }
}