using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace DefaultNamespace
{
    public class InstrumentSpawner : MonoBehaviour
    {
        public event Action OnSpawned;
        
        public Orchestra Orchestra;
        public InstrumentsToImages Instruments;
        public ARRaycastManager RaycastManager;

        private List<ARRaycastHit> _hitResults = new();
        private SpawnedInstrumentsContainer _scannedInstrumentContainer;
        private int _scannedPage;

        public void ScanInstrument(string imageName)
        {
            string[] values = imageName.Split("_");
            string name = values[0];
            _scannedPage = int.Parse(values[1]);
            _scannedInstrumentContainer = Instruments.GetInstrumentByName(name);
        }

        private void Update()
        {
            if (_scannedInstrumentContainer == null)
                return;
            
            if (Input.touchCount == 0)
                return;

            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;
            
            var ray = Camera.main.ScreenPointToRay(touch.position);
            var result = RaycastManager.Raycast(ray, _hitResults, TrackableType.Planes);
            if (!result)
                return;

            var hit = _hitResults[0];
            SpawnInstrument(hit.pose.position, hit.pose.up);
        }

        private void SpawnInstrument(Vector3 position, Vector3 planeNormal)
        {
            if (_scannedInstrumentContainer == null)
                return;

            var cameraPosition = Camera.main.transform.position;
            var directionToCamera = cameraPosition - position;
            var projectedDirection = Vector3.ProjectOnPlane(directionToCamera, planeNormal);
            var rotation = Quaternion.LookRotation(projectedDirection, planeNormal);
            
            Orchestra.ClearInstruments();
            
            var instrumentsContainer = Instantiate(_scannedInstrumentContainer, position, rotation);
            instrumentsContainer.Initialize(_scannedPage);
            instrumentsContainer.Register(Orchestra);

            _scannedInstrumentContainer = null;
            OnSpawned?.Invoke();
        }
    }
}