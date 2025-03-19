using System.Collections;
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

        private bool isHolding;

        private int damageHold;
        private int healingHold;
        private int shieldHold;
        
        public void AddDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            damageHold += damage;
            if (!isHolding)
            {
                StartCoroutine(NumberHold());
            }
        }

        public void AddHealing(int healing)
        {
            if (healing <= 0)
            {
                return;
            }
            healingHold += healing;
            
            if (!isHolding)
            {
                StartCoroutine(NumberHold());
            }
        }

        public void AddShield(int shield)
        {
            if (shield <= 0)
            {
                return;
            }
            shieldHold += shield;
            
            if (!isHolding)
            {
                StartCoroutine(NumberHold());
            }
        }

        private IEnumerator NumberHold()
        {
            isHolding = true;
            yield return new WaitForEndOfFrame();
            isHolding = false;

            if (damageHold > 0)
            {
                DamageNumber dn = Instantiate(damageNumber, content);
                dn.SetValue(damageHold);
                damageHold = 0;
            }
            
            if (healingHold > 0)
            {
                DamageNumber hn = Instantiate(healingNumber, content);
                hn.SetValue(healingHold);
                healingHold = 0;
            }
            
            if (shieldHold > 0)
            {
                DamageNumber sn = Instantiate(shieldNumber, content);
                sn.SetValue(shieldHold);
                shieldHold = 0;
            }
        }
    }
}