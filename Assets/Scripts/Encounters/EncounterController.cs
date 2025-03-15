using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Currencies;
using Fantazee.Encounters.Ui;
using Fantazee.Encounters.Ui.Rewards;
using Fantazee.Instance;
using Fantazee.Npcs;
using Fantazee.Npcs.Settings;
using Fantazee.Relics;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Ui;
using Fantazee.Scores.Scoresheets.Ui;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using FMODUnity;
using Fsi.Gameplay;
using Fsi.Gameplay.Healths;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Encounters
{
    public class EncounterController : MbSingleton<EncounterController>
    {
        private EncounterData encounter;

        [Header("Responses")]

        [SerializeField]
        private EncounterResponseUi responsePrefab;
        
        [SerializeField]
        private Transform responsesContainer;
        
        private readonly List<EncounterResponseUi> responses = new();

        [Header("Rewards")]

        [SerializeField]
        private CurrencyEncounterReward currencyEncounterReward;
        
        [SerializeField]
        private HealthEncounterReward healthEncounterReward;
        
        [SerializeField]
        private RelicEntryUi relicEntryUi;
        
        [SerializeField]
        private SpellButton spellButtonPrefab;
        
        [SerializeField]
        private Transform rewardsContainer;
        
        [SerializeField]
        private Transform rewardsParent;

        [Header("Hit Flash")]

        [SerializeField]
        private float hitTime = 0.2f;

        [SerializeField]
        private Ease hitEase = Ease.Linear;
        
        [SerializeField]
        private Color hitColor = Color.red;

        [SerializeField]
        private Color hitFinishColor = Color.clear;
        
        [Header("Scoresheet")]
        
        [SerializeField]
        private ScoresheetUpgradeScreen scoresheetUpgradeScreen;

        [Header("References")]

        [SerializeField]
        private GameObject root;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private TMP_Text headerText;
        
        [SerializeField]
        private TMP_Text bodyText;

        [SerializeField]
        private Button continueButton;

        [SerializeField]
        private Image hitFlash;

        [SerializeField]
        private Image npcImage;

        [SerializeField]
        private Image fadeImage;
        
        // Some rewards need a selection, so lets save them.
        private List<SpellInstance> spellsToReward = new();
        
        protected override void Awake()
        {
            base.Awake();

            responsesContainer.gameObject.SetActive(true);
            rewardsParent.gameObject.SetActive(false);
            
            scoresheetUpgradeScreen.Hide(true);

            var color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;

            fadeImage.raycastTarget = false;
        }

        private void Start()
        {
            EnvironmentInstance env = GameInstance.Current.Environment;
            encounter = env.GetEncounter();
            backgroundImage.sprite = env.Data.GetBackground();
            headerText.text = encounter.Title;
            bodyText.text = encounter.Body;

            if (NpcSettings.Settings.TryGetNpc(encounter.Npc, out NpcData npc))
            {
                npcImage.sprite = npc.Sprite;
                Sequence s = npc.EnterAnimation.Apply(npcImage);
                s.Play();
            }

            foreach (EncounterResponse response in encounter.Responses)
            {
                EncounterResponseUi responseUi = Instantiate(responsePrefab, responsesContainer);
                responseUi.Initialize(response, OnResponse);

                responses.Add(responseUi);
            }
            
            GameController.Instance.EncounterReady();
        }
        
        private void HitEffect()
        {
            RuntimeManager.PlayOneShot(GameInstance.Current.Character.Data.HitSfx);
            hitFlash.color = hitColor;
            hitFlash.DOColor(hitFinishColor, hitTime)
                    .SetEase(hitEase);
        }

        private void OnResponse(EncounterResponse response)
        {
            foreach (EncounterResponseUi responseUi in responses)
            {
                Destroy(responseUi.gameObject);
            }

            responses.Clear();
            bodyText.text = response.Result;
            responsesContainer.gameObject.SetActive(false);
            rewardsParent.gameObject.SetActive(true);

            // Cost
            if (response.Cost.Health.max > 0)
            {
                Health health = GameInstance.Current.Character.Health;
                health.max -= response.Cost.Health.max;
                health.current = Mathf.Clamp(health.current, 0, health.max);
                health.Damage(0);
                HitEffect();
            }

            if (response.Cost.Health.current > 0)
            {
                GameInstance.Current.Character.Health.Damage(response.Cost.Health.current);
                HitEffect();
            }

            foreach (Currency currency in response.Cost.Wallet.Currencies)
            {
                GameInstance.Current.Character.Wallet.Remove(currency);
            }
            
            // Rewards
            if (response.Rewards.Health.max > 0)
            {
                GameInstance.Current.Character.Health.max += response.Rewards.Health.max;
                HealthEncounterReward healthReward = Instantiate(healthEncounterReward, rewardsContainer);
                healthReward.Initialize(response.Rewards.Health.max);
            }
            
            if (response.Rewards.Health.current > 0)
            {
                GameInstance.Current.Character.Health.current += response.Rewards.Health.current;
                HealthEncounterReward healthReward = Instantiate(healthEncounterReward, rewardsContainer);
                healthReward.Initialize(response.Rewards.Health.max);
            }

            if (response.Rewards.Wallet.Currencies.Count > 0)
            {
                GameInstance.Current.Character.Wallet.Add(response.Rewards.Wallet);
                foreach (Currency currency in response.Rewards.Wallet.Currencies)
                {
                    CurrencyEncounterReward currencyReward = Instantiate(currencyEncounterReward, rewardsContainer);
                    currencyReward.Initialize(currency);
                }
            }
            
            foreach (RelicData relic in response.Rewards.Relics)
            {
                RelicInstance relicInstance = RelicFactory.Create(relic, GameInstance.Current.Character);
                RelicEntryUi relicReward = Instantiate(relicEntryUi, rewardsContainer);
                relicReward.Initialize(relicInstance);
                GameInstance.Current.Character.AddRelic(relicInstance);
            }

            foreach (SpellData spell in response.Rewards.Spells)
            {
                SpellInstance spellInstance = SpellFactory.CreateInstance(spell);
                SpellButton spellButtonReward = Instantiate(spellButtonPrefab, rewardsContainer);
                spellButtonReward.Initialize(spellInstance, null);

                spellsToReward.Add(spellInstance);
            }
        }

        public void OnContinue()
        {
            continueButton.transform.DOPunchScale(Vector3.one * -0.1f, 0.2f)
                          .OnComplete(() =>
                                      {
                                          if (spellsToReward.Count > 0)
                                          {
                                              SetupUpgrades(spellsToReward);
                                          }
                                          else
                                          {
                                              LeaveEncounter();
                                          }
                                      });
        }

        private void LeaveEncounter()
        {
            Debug.Log("Encounter: Leaving");
            GameController.Instance.LoadMap();
        }

        private void SetupUpgrades(List<SpellInstance> spells)
        {
            fadeImage.raycastTarget = true;
            fadeImage.DOFade(0.6f, 0.2f).SetEase(Ease.InOutSine);
            Queue<SpellInstance> spellQueue = new(spells);
            UpgradeSpell(spellQueue);
        }

        private void UpgradeSpell(Queue<SpellInstance> spellQueue)
        {
            SpellInstance spell = spellQueue.Dequeue();
            root.SetActive(false);
            scoresheetUpgradeScreen.Show();
            scoresheetUpgradeScreen.StartSpellUpgrade(spell, () =>
                                                      {
                                                          if (spellQueue.Count > 0)
                                                          {
                                                              UpgradeSpell(spellQueue);
                                                          }
                                                          else
                                                          {
                                                              LeaveEncounter();
                                                          }
                                                      });
        }
    }
}
