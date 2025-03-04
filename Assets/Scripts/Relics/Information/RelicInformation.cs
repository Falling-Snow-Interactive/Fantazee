using System;
using Fantazee.Relics.Data;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazee.Relics.Information
{
    [Serializable]
    public class RelicInformation: Information<RelicType>
    {
        [SerializeField]
        private RelicData data;
        public RelicData Data => data;
    }
}