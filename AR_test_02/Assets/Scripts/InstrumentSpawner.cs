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

        public float MaxYPercent;
        
        public Orchestra Orchestra;
        public InstrumentsToImages Instruments;
        public ARRaycastManager RaycastManager;

        private List<ARRaycastHit> _hitResults = new();
        private InstrumentDataContainer _scannedInstrumentContainer;

        public void ScanInstrument(string imageName)
        {
            Debug.Log($"Scanned instrument: {imageName}");
            if (string.IsNullOrWhiteSpace(imageName))
            {
                Debug.LogError("Scanned empty instrument name!");
                return;
            }
            
            _scannedInstrumentContainer = Instruments.GetInstrumentByName(imageName);
            if (_scannedInstrumentContainer == null)
                Debug.LogError("Cannot find instruments for this image!");
        }

        private void Update()
        {
            if (Input.touchCount == 0)
                return;

            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;

            float maxY = Screen.height * MaxYPercent;
            Debug.Log($"Touch at Y: {touch.position.y}, max: {maxY}, touch percent: {touch.position.y / Screen.height}");
            if (touch.position.y > maxY)
                return;
            
            if (_scannedInstrumentContainer == null)
                return;
            
            var ray = Camera.main.ScreenPointToRay(touch.position);
            var result = RaycastManager.Raycast(ray, _hitResults, TrackableType.Planes);
            if (!result)
                return;

            var hit = _hitResults[0];
            SpawnInstrument(hit.pose.position, hit.pose.up);
        }

        public void SpawnInstrument(Vector3 position, Vector3 planeNormal)
        {
            if (_scannedInstrumentContainer == null)
            {
                Debug.Log("Cannot spawn instrument, cause it's null!");
                return;
            }

            var cameraPosition = Camera.main.transform.position;
            var directionToCamera = cameraPosition - position;
            var projectedDirection = Vector3.ProjectOnPlane(directionToCamera, planeNormal);
            var rotation = Quaternion.LookRotation(projectedDirection, planeNormal);
            
            Orchestra.ClearInstruments();
            
            Debug.Log($"Spawning instrument container {_scannedInstrumentContainer.Target.name}");
            var instrumentsContainer = Instantiate(_scannedInstrumentContainer.Target, position, rotation);
            instrumentsContainer.Initialize(_scannedInstrumentContainer);
            instrumentsContainer.Register(Orchestra);

            _scannedInstrumentContainer = null;
            OnSpawned?.Invoke();
        }
    }
}