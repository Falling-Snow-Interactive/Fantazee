using System;
using fsi.settings.Informations;
using UnityEngine;

namespace ProjectYahtzee.Battle.Dice.Information
{
    [Serializable]
    public class SideInformation : Information<int>
    {
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}