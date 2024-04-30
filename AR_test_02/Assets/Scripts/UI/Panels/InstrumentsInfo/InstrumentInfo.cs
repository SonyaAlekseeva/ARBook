using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels.InstrumentsInfo
{
    public class InstrumentInfo : MonoBehaviour
    {
        public event Action<bool, InstrumentInfo> OnInfoClicked;

        [HideInInspector] public Instrument Instrument;
        
        public RectTransform RectTransform;
        
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _toggleInfoButton;
        [SerializeField] private GameObject _factsPanel;
        [SerializeField] private FactsScroller _factsScroller;
        
        private bool _factsActive;
        
        private void Awake()
        {
            _toggleInfoButton.onClick.AddListener(InfoClicked);
        }

        public void SetUpForInstrument(Instrument instrument)
        {
            Instrument = instrument;
            _factsScroller.SetUpForInstrument(instrument);
            _nameText.text = instrument.Data.Name;
        }

        private void InfoClicked()
        {
            OnInfoClicked?.Invoke(!_factsActive, this);
        }

        public void SetFactsState(bool state)
        {
            _factsActive = state;
            _factsPanel.SetActive(_factsActive);
        }
    }
}