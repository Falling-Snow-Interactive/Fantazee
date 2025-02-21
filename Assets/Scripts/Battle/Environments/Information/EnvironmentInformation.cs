using System;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazee.Battle.Environments.Information
{
    [Serializable]
    public class EnvironmentInformation : Information<EnvironmentType>
    {
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;
    }
}