using System;
using UnityEngine;

namespace Environments
{
    public class Environment : MonoBehaviour
    {
        public static event Action<Environment> Ready;

        public void Start()
        {
            Ready?.Invoke(this);
        }
    }
}
