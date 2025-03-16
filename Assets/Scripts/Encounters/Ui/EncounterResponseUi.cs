using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Fantazee.Encounters.Ui
{
    public class EncounterResponseUi : MonoBehaviour
    {
        private EncounterResponse response;
        private Action<EncounterResponse> onSelected;

        [Header("Anim")]

        [Header("Select")]

        [SerializeField]
        private float selectTime = 0.2f;

        [SerializeField]
        private Vector3 selectAmount = Vector3.one * -0.1f;
        
        [SerializeField]
        private int selectVibrato = 10;

        [SerializeField]
        private float selectElasticity = 1f;
        
        [Header("References")]
        
        [SerializeField]
        private TMP_Text headerText;
        
        [SerializeField]
        private TMP_Text bodyText;
        
        public void Initialize(EncounterResponse response, Action<EncounterResponse> onSelected)
        {
            this.response = response;
            this.onSelected = onSelected;
            
            headerText.text = response.Header;
            bodyText.text = response.Body;
        }

        public void OnClick()
        {
            transform.DOPunchScale(selectAmount, selectTime, selectVibrato, selectElasticity)
                     .OnComplete(() =>
                                 {
                                     onSelected?.Invoke(response);
                                 });
        }
    }
}