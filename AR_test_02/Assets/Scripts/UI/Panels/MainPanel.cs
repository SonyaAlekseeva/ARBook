using System.Collections.Generic;
using DefaultNamespace.UI.Panels.InstrumentsInfo;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class MainPanel : MonoBehaviour
    {
        [Header("Main")]
        public Orchestra Orchestra;
        
        [Header("Scan")]
        public Button ScanButton;
        public ScanPanel ScanPanel;
        
        [Header("Onboarding")]
        public Button OnboardingButton;
        public OnboardingPanel OnboardingPanel;
        
        [Header("Dictionary")]
        public Button DictionaryButton;
        public DictionaryPanel DictionaryPanel;

        [Header("Instrument info")]
        public RectTransform PanelTransform;
        public InstrumentInfo InfoButtonPrefab;
        
        private List<InstrumentInfo> _infoButtons;

        private void Start()
        {
            _infoButtons ??= new List<InstrumentInfo>();
            
            ScanButton.onClick.AddListener(() => OpenPanel(ScanPanel.gameObject));
            OnboardingButton.onClick.AddListener(() =>
            {
                OnboardingPanel.ReturnToMainPanel = true;
                OpenPanel(OnboardingPanel.gameObject);
            });
            DictionaryButton.onClick.AddListener(() => DictionaryPanel.gameObject.SetActive(true));
        }

        public void CreateInfoButtonForInstrument(Instrument instrument)
        {
            Debug.Log($"Creating info button for instrument {instrument.Name}");
            var button = Instantiate(InfoButtonPrefab);
            button.transform.position = instrument.transform.position + Vector3.up * instrument.Data.InfoButtonHeight;
            button.SetUpForInstrument(instrument);
            button.SetFactsState(false);
            button.SetPanel(PanelTransform);
            button.OnInfoClicked += OnInstrumentInfoClicked;
            
            _infoButtons.Add(button);
        }

        private void OnInstrumentInfoClicked(bool requestedState, InstrumentInfo info)
        {
            foreach (var infoButton in _infoButtons)
            {
                infoButton.SetFactsState(infoButton == info && !infoButton.FactsActive);
            }
        }

        public void ClearInfoButtons()
        {
            if (_infoButtons == null)
            {
                _infoButtons = new List<InstrumentInfo>();
                return;
            }
            
            foreach (var infoButton in _infoButtons)
            {
                if (infoButton == null)
                    continue;
                
                Destroy(infoButton.gameObject);
            }
            
            _infoButtons.Clear();
        }

        private void OpenPanel(GameObject panel)
        {
            Orchestra.Stop();
            gameObject.SetActive(false);
            panel.SetActive(true);

            foreach (var infoButton in _infoButtons)
            {
                infoButton.SetFactsState(false);
            }
        }
    }
}