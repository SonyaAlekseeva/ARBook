using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace.UI.Panels
{
    public class ScanPanel : MonoBehaviour
    {
        [Header("Onboarding")]
        public Button OnboardingButton;
        public OnboardingPanel OnboardingPanel;
        
        [Header("Dictionary")]
        public Button DictionaryButton;
        public DictionaryPanel DictionaryPanel;

        [Header("Main")]
        public IntermediateScanPanel IntermediateScanPanel;
        public GameObject MainPanel;
        public GameObject ScanView;
        public GameObject PlaceView;
        public ARTrackedImageManager ImageManager;
        public InstrumentSpawner Spawner;
        public PlaneMarkerUpdater PlaneMarkerUpdater;

        private void OnEnable()
        {
            ScanView.SetActive(true);
            PlaceView.SetActive(false);
            
            IntermediateScanPanel.OnCompleted += OnIntermediateScanCompleted;
            IntermediateScanPanel.gameObject.SetActive(false);

            ImageManager.enabled = true;
            ImageManager.trackedImagesChanged += OnTrackedChanged;

            Spawner.enabled = false;
            Spawner.OnSpawned += OnInstrumentSpawned;
            
            PlaneMarkerUpdater.SetMarkerState(false);
            
            OnboardingButton.onClick.AddListener(() => OpenPanel(OnboardingPanel.gameObject));
            DictionaryButton.onClick.AddListener(() => DictionaryPanel.gameObject.SetActive(true));
        }

        private void OnDisable()
        {
            ImageManager.enabled = false;
            ImageManager.trackedImagesChanged -= OnTrackedChanged;
            
            Spawner.enabled = false;
            Spawner.OnSpawned -= OnInstrumentSpawned;
            
            PlaneMarkerUpdater.SetMarkerState(false);
        }

        public void TestScan(string imageName)
        {
            IntermediateScanPanel.gameObject.SetActive(true);
            ScanView.SetActive(false);
            Spawner.ScanInstrument(imageName);
        }

        private void OnIntermediateScanCompleted()
        {
            PlaceView.SetActive(true);
            Spawner.enabled = true;
            PlaneMarkerUpdater.SetMarkerState(true);
        }

        private void OnTrackedChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            if (eventArgs.added == null || eventArgs.added.Count == 0)
                return;
            
            IntermediateScanPanel.gameObject.SetActive(true);
            ScanView.SetActive(false);
            
            foreach (var trackedImage in eventArgs.added)
            {
                Debug.Log($"Tracked changed, name: {trackedImage.name}, reference name: {trackedImage.referenceImage.name}");
                Spawner.ScanInstrument(trackedImage.referenceImage.name);
            }
        }

        private void OnInstrumentSpawned()
        {
            gameObject.SetActive(false);
            MainPanel.SetActive(true);
        }

        private void OpenPanel(GameObject panel)
        {
            gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}