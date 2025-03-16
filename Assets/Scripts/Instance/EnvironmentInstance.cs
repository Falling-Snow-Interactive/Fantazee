using System;
using System.Collections.Generic;
using Fantazee.Encounters;
using Fantazee.Environments;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Instance
{
    [Serializable]
    public class EnvironmentInstance
    {
        public bool ReadyToAdvance { get; set; }

        [SerializeField]
        private EnvironmentData data;
        public EnvironmentData Data => data;
        
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
        private List<EncounterData> encounters;
        public List<EncounterData> Encounters => encounters;

        public EnvironmentInstance(EnvironmentData data)
        {
            index = 0;
            node = 0;
            ReadyToAdvance = false;
            
            this.data = data;
            encounters = new List<EncounterData>(data.Encounters);
        }

        public void SetReadyToAdvance()
        {
            ReadyToAdvance = true;
        }

        public void Advance()
        {
            Debug.Log($"Environment: Advance: \nIndex {index} -> {index + 1}");
            index++;
            node = 0;
            ReadyToAdvance = false;
        }
        
        public EncounterData GetEncounter()
        {
            if (encounters == null || encounters.Count == 0)
            {
                Debug.LogWarning("Environment: No encounters remaining. Resetting list.");
                encounters = new List<EncounterData>(data.Encounters);
            }
            
            EncounterData e = encounters[Random.Range(0, encounters.Count)];
            encounters.Remove(e);
            return e;
        }
    }
}