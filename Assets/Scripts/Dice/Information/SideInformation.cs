using System;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantahzee.Items.Dice.Information
{
    [Serializable]
    public class SideInformation : Information<int>
    {
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}