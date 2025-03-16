using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Encounters;
using Fantazee.Environments;
using Fantazee.Instance;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class EnvironmentSave
    {
        [SerializeField]
        private EnvironmentData data;
        public EnvironmentData Data => data;

        [SerializeField]
        private int index;
        public int Index => index;

        [SerializeField]
        private int node;
        public int Node => node;

        [SerializeField]
        private List<EncounterData> encounters;
        public List<EncounterData> Encounters => encounters;
        
        public EnvironmentSave(EnvironmentInstance instance)
        {
            data = instance.Data;
            index = instance.Index;
            node = instance.Node;
            encounters = new List<EncounterData>(instance.Encounters);
        }
    }
}