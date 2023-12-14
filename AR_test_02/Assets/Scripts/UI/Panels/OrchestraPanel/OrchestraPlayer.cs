using System.Collections.Generic;
using UI.Panels;
using UI.Panels.OrchestraPanel;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class OrchestraPlayer : MonoBehaviour
    {
        public Orchestra Orchestra;
        
        [Header("Play Control")]
        public Button[] PlayButtons;
        public Button[] PauseButtons;
        public OrchestraSlider[] PlaySliders;
        
        [Header("Instruments View")]
        public Button ShowInstrumentsViewButton;
        public Button HideInstrumentsViewButton;
        public GameObject MinimalisticView;
        public GameObject ExpandedView;

        [Header("Instruments List")]
        public Transform InstrumentsParent;
        public InstrumentButton InstrumentButtonPrefab;

        private List<InstrumentButton> _instrumentButtons = new();

        private void Awake()
        {
            foreach (var playButton in PlayButtons)
            {
                playButton.onClick.AddListener(Play);
            }
            foreach (var pauseButton in PauseButtons)
            {
                pauseButton.onClick.AddListener(Pause);
            }
            foreach (var playSlider in PlaySliders)
            {
                playSlider.OnValueChanged += SetPlayTime;
            }
            
            ShowInstrumentsViewButton.onClick.AddListener(ShowInstrumentsView);
            HideInstrumentsViewButton.onClick.AddListener(HideInstrumentsView);
        }

        private void OnEnable()
        {
            UpdateInstruments();
        }

        private void Update()
        {
            foreach (var playSlider in PlaySliders)
            {
                playSlider.SetTime(Orchestra.PlayTimer, Orchestra.PlayTimeSeconds);
            }
        }

        private void SetPlayTime(float t)
        {
            Orchestra.SetPlayTime(t);
        }

        private void Play()
        {
            Orchestra.Play();
        }

        private void Pause()
        {
            Orchestra.Stop();
        }
        
        private void ShowInstrumentsView()
        {
            MinimalisticView.SetActive(false);
            ExpandedView.SetActive(true);
        }

        private void HideInstrumentsView()
        {
            MinimalisticView.SetActive(true);
            ExpandedView.SetActive(false);
        }

        private void UpdateInstruments()
        {
            Clear();
            foreach (var instrument in Orchestra.Instruments)
            {
                var button = Instantiate(InstrumentButtonPrefab, InstrumentsParent);
                button.Initialize(instrument);
                button.OnInstrumentToggled += ToggleInstrument;
                _instrumentButtons.Add(button);
            }
        }

        private void ToggleInstrument(Instrument instrument)
        {
            if (instrument.IsMuted)
                Orchestra.EnableInstrument(instrument);
            else
                Orchestra.DisableInstrument(instrument);
        }

        private void Clear()
        {
            foreach (var instrument in _instrumentButtons)
            {
                Destroy(instrument.gameObject);
            }
            _instrumentButtons.Clear();
        }
    }
}