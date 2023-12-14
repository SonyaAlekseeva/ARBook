using System;
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

        private void Start()
        {
            ScanButton.onClick.AddListener(() => OpenPanel(ScanPanel.gameObject));
            OnboardingButton.onClick.AddListener(() => OpenPanel(OnboardingPanel.gameObject));
            DictionaryButton.onClick.AddListener(() => OpenPanel(DictionaryPanel.gameObject));
        }

        private void OpenPanel(GameObject panel)
        {
            gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}