using System;
using UnityEngine;

namespace Fantazee.Battle.Shields
{
    [Serializable]
    public class Shield
    {
        public event Action Changed;
        
        [SerializeField]
        private int current;
        public int Current => current;

        public void Add(int amount)
        {
            current += amount;
            Changed?.Invoke();
        }

        public int Remove(int amount)
        {
            int d = Mathf.Clamp(current - amount, 0, current);
            current = d;
            Changed?.Invoke();
            return d;
        }

        public void Clear()
        {
            current = 0;
            Changed?.Invoke();
        }
    }
}