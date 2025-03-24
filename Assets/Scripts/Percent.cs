using System;

namespace Fantazee
{
    [Serializable]
    public class Percent
    {
        [Range(0, 100)]
        [SerializeField]
        private int chance;

        public bool Roll()
        {
            return Random.Range(0, 100) < chance;
        }

        public override string ToString()
        {
            return chance.ToString();
        }
    }
}
