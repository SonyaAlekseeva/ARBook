using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace
{
    public class ImageRecognizer : MonoBehaviour
    {
        [SerializeField] private ARTrackedImageManager _trackedImageManager;

        private void OnEnable()
        {
            _trackedImageManager.trackedImagesChanged += OnTrackedChanged;
        }

        private void OnDisable()
        {
            _trackedImageManager.trackedImagesChanged -= OnTrackedChanged;
        }

        private void OnTrackedChanged(ARTrackedImagesChangedEventArgs image)
        {
            
        }
    }
}