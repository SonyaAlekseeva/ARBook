using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace.UI.Panels
{
    public class ScanPanel : MonoBehaviour
    {
        public GameObject MainPanel;
        public GameObject ScanView;
        public GameObject PlaceView;
        public ARTrackedImageManager ImageManager;
        public InstrumentSpawner Spawner;

        private void OnEnable()
        {
            ScanView.SetActive(true);
            PlaceView.SetActive(false);

            ImageManager.enabled = true;
            ImageManager.trackedImagesChanged += OnTrackedChanged;

            Spawner.enabled = true;
            Spawner.OnSpawned += OnInstrumentSpawned;
        }

        private void OnDisable()
        {
            ImageManager.enabled = false;
            ImageManager.trackedImagesChanged -= OnTrackedChanged;
            
            Spawner.enabled = false;
            Spawner.OnSpawned -= OnInstrumentSpawned;
        }

        private void OnTrackedChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            if (eventArgs.added == null || eventArgs.added.Count == 0)
                return;
            
            ScanView.SetActive(false);
            PlaceView.SetActive(true);
            
            foreach (var trackedImage in eventArgs.added)
            {
                Spawner.ScanInstrument(trackedImage.name);
            }
        }

        private void OnInstrumentSpawned()
        {
            gameObject.SetActive(false);
            MainPanel.SetActive(true);
        }
    }
}