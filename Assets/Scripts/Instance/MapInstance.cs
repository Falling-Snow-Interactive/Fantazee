using System;
using Fantazee.Battle.Environments;
using UnityEngine;

namespace Fantazee.Instance
{
    [Serializable]
    public class MapInstance
    {
        [SerializeField]
        private int index;
        public int Index
        {
            get => index;
            set => index = value;
        }
        
        [SerializeField]
        private int node;
        public int Node
        {
            get => node;
            set => node = value;
        }
        
        [SerializeField]
        private EnvironmentType environment;
        public EnvironmentType Environment => environment;

        public bool ReadyToAdvance { get; set; }

        public MapInstance()
        {
            index = 0;
            node = 0;
            environment = EnvironmentType.Woods;
            ReadyToAdvance = false;
        }

        public void SetReadyToAdvance()
        {
            ReadyToAdvance = true;
        }

        public void Advance()
        {
            Debug.Log($"Map: Advance: \nIndex {index} -> {index + 1}");
            index++;
            node = 0;
            ReadyToAdvance = false;
        }
    }
}