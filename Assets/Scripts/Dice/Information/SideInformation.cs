using System;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazhee.Items.Dice.Information
{
    [Serializable]
    public class SideInformation : Information<int>
    {
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}