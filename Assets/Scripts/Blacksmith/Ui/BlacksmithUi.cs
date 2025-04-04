using System.Collections;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Dice.Randomizer;
using Fantazee.Instance;
using Fantazee.Items.Dice.Randomizer;
using UnityEngine;

namespace Fantazee.Blacksmith.Ui
{
    public class BlacksmithUi : MonoBehaviour
    {
        [SerializeField]
        private BlacksmithController blacksmithController;
        
        [SerializeField]
        private BlacksmithSideGroupUi blacksmithSideGroupUi;

        [SerializeField]
        private List<BlacksmithDieUi> diceButtons = new();

        private BlacksmithDieUi selected;

        private bool canUpgrade = true;
        
        public void Initialize()
        {
            Debug.Log("Blacksmith - BlacksmithUi initialize");
            
            int i = 1;
            foreach (BlacksmithDieUi d in diceButtons)
            {
                d.SetImage(i);
                i++;
            }

            OnDieSelect(0);
            canUpgrade = true;
        }

        public void OnDieSelect(int i)
        {
            if (!canUpgrade)
            {
                return;
            }
            
            // Die die = GameInstance.Current.Character.Dice[i];
            blacksmithSideGroupUi.Initialize(this);
            // blacksmithSideGroupUi.SetDie(die);
            SetSelected(i);
        }

        private void SetSelected(int i)
        {
            if (!canUpgrade)
            {
                return;
            }
            
            selected?.Deselect();
            selected = diceButtons[i];
            selected?.Select();
        }

        public void UpgradeValue(BlacksmithSideUi entry, int side, int change)
        {
            if (!canUpgrade)
            {
                return;
            }
            
            DieRandomizer r = entry.Die.DieRandomizer;
            foreach (DieRandomizerEntry s in r.Entries)
            {
                if (s.Value == side)
                {
                    s.Value += change;
                    canUpgrade = false;
                    StartCoroutine(DelayedUpdate(entry, s.Value, s.Weight));
                    Debug.Log($"Blacksmith - Value: {s.Value - change} -> {s.Value}");
                    return;
                }
            }
        }

        public void UpgradeWeight(BlacksmithSideUi entry, int side, int change)
        {
            if (!canUpgrade)
            {
                return;
            }
            
            DieRandomizer r = entry.Die.DieRandomizer;
            foreach (DieRandomizerEntry s in r.Entries)
            {
                if (s.Value == side)
                {
                    s.Weight += change;
                    canUpgrade = false;
                    StartCoroutine(DelayedUpdate(entry, s.Value, s.Weight));
                    Debug.Log($"Blacksmith - {side} Weight: {s.Weight - change} -> {s.Weight}");
                    return;
                }
            }
        }
        
        private IEnumerator DelayedUpdate(BlacksmithSideUi entry, int value, float weight)
        {
            yield return new WaitForSeconds(0.5f);
            entry.SetImage(value);
            entry.SetWeight(weight);
            yield return new WaitForSeconds(0.5f);
            ExitBlacksmith();
        }

        private void ExitBlacksmith()
        {
            blacksmithController.ExitBlacksmith();
        }
    }
}