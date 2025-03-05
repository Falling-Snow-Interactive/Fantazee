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
            DamageNumber dn = Instantiate(damageNumber, content);
            dn.SetValue(damage);
        }

        public void AddHealing(int healing)
        {
            DamageNumber dn = Instantiate(healingNumber, content);
            dn.SetValue(healing);
        }

        public void AddShield(int shield)
        {
            DamageNumber dn = Instantiate(shieldNumber, content);
            dn.SetValue(shield);
        }
    }
}