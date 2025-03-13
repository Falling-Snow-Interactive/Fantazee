using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Currencies;
using Fantazee.Encounters.Ui;
using Fantazee.Encounters.Ui.Rewards;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Instance;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Ui;
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

        [SerializeField]
        private EncounterData data;

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
        
        [Header("References")]

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

        protected override void Awake()
        {
            base.Awake();

            responsesContainer.gameObject.SetActive(true);
            rewardsParent.gameObject.SetActive(false);
        }

        private void Start()
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(GameInstance.Current.Map.Environment, 
                                                               out EnvironmentData env))
            {
                encounter = env.GetEncounter();
                backgroundImage.sprite = env.GetBackground();
                headerText.text = encounter.Title;
                bodyText.text = encounter.Body;
            }

            foreach (EncounterResponse response in data.Responses)
            {
                EncounterResponseUi responseUi = Instantiate(responsePrefab, responsesContainer);
                responseUi.Initialize(response, OnResponse);

                responses.Add(responseUi);
            }
            
            GameController.Instance.EncounterReady();
        }

        private void OnResponse(EncounterResponse response)
        {
            foreach (EncounterResponseUi responseUi in responses)
            {
                Destroy(responseUi.gameObject);
            }

            responses.Clear();
            bodyText.text = response.Result.Body;
            responsesContainer.gameObject.SetActive(false);
            rewardsParent.gameObject.SetActive(true);

            // Cost
            if (response.Health.max > 0)
            {
                Health health = GameInstance.Current.Character.Health;
                health.max -= response.Health.max;
                health.current = Mathf.Clamp(health.current, 0, health.max);
                health.Damage(0);
                
                hitFlash.color = hitColor;
                hitFlash.DOColor(hitFinishColor, hitTime)
                        .SetEase(hitEase);
            }

            if (response.Health.current > 0)
            {
                GameInstance.Current.Character.Health.Damage(response.Health.current);
            }

            foreach (Currency currency in response.Wallet.Currencies)
            {
                GameInstance.Current.Character.Wallet.Remove(currency);
            }
            
            // Rewards
            if (response.Result.Health.max > 0)
            {
                GameInstance.Current.Character.Health.max += response.Result.Health.max;
                HealthEncounterReward healthReward = Instantiate(healthEncounterReward, rewardsContainer);
                healthReward.Initialize(response.Result.Health.max);
            }
            
            if (response.Result.Health.current > 0)
            {
                GameInstance.Current.Character.Health.current += response.Result.Health.current;
                HealthEncounterReward healthReward = Instantiate(healthEncounterReward, rewardsContainer);
                healthReward.Initialize(response.Result.Health.max);
            }

            if (response.Result.Wallet.Currencies.Count > 0)
            {
                GameInstance.Current.Character.Wallet.Add(response.Result.Wallet);
                foreach (Currency currency in response.Wallet.Currencies)
                {
                    CurrencyEncounterReward currencyReward = Instantiate(currencyEncounterReward, rewardsContainer);
                    currencyReward.Initialize(currency);
                }
            }

            foreach (RelicType relic in response.Result.Relics)
            {
                RelicInstance relicInstance = RelicFactory.Create(relic, GameInstance.Current.Character);
                RelicEntryUi relicReward = Instantiate(relicEntryUi, rewardsContainer);
                relicReward.Initialize(relicInstance);
            }
        }

        public void OnLeave()
        {
            continueButton.transform.DOPunchScale(Vector3.one * -0.1f, 0.2f)
                          .OnComplete(() =>
                                      {
                                          Debug.Log("OnLeave");
                                          GameController.Instance.LoadMap();
                                      });
        }
    }
}
