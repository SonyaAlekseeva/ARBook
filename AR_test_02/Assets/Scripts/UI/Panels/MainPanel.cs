using System.Collections.Generic;
using DefaultNamespace.UI.Panels.InstrumentsInfo;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Panels
{
    public class MainPanel : MonoBehaviour
    {
        [Header("Scan")]
        public Button ScanButton;
        public ScanPanel ScanPanel;
        
        [Header("Onboarding")]
        public Button OnboardingButton;
        public OnboardingPanel OnboardingPanel;
        
        [Header("Onboarding")]
        public Button DictionaryButton;
        public DictionaryPanel DictionaryPanel;

        [Header("Insrument info")]
        public InstrumentInfo InfoButtonPrefab;
        public Transform InfoButtonsParent;
        public Camera MainCamera;
        
        private List<InstrumentInfo> _infoButtons;

        private void Start()
        {
            _infoButtons ??= new List<InstrumentInfo>();
            
            ScanButton.onClick.AddListener(() => OpenPanel(ScanPanel.gameObject));
            OnboardingButton.onClick.AddListener(() => OpenPanel(OnboardingPanel.gameObject));
            DictionaryButton.onClick.AddListener(() => OpenPanel(DictionaryPanel.gameObject));
        }

        private void Update()
        {
            foreach (var infoButton in _infoButtons)
            {
                var position = infoButton.Instrument.transform.position;
                float height = infoButton.Instrument.Data.InfoButtonHeight;

                var infoButtonPosition = position + Vector3.up * height;
                var screenPosition = MainCamera.WorldToScreenPoint(infoButtonPosition);

                infoButton.transform.position = screenPosition;
            }
        }

        public void CreateInfoButtonForInstrument(Instrument instrument)
        {
            Debug.Log("Creating info button for instrument");
            // var button = Instantiate(InfoButtonPrefab, InfoButtonsParent);
            // button.SetUpForInstrument(instrument);
            // button.OnInfoClicked += OnInstrumentInfoClicked;
            //
            // _infoButtons.Add(button);
        }

        private void OnInstrumentInfoClicked(bool requestedState, InstrumentInfo info)
        {
            foreach (var infoButton in _infoButtons)
            {
                infoButton.SetFactsState(false);
            }
            
            info.SetFactsState(true);
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
            gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}