using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneMarkerUpdater : MonoBehaviour
{
    [Header("Put your PlaneMarker here")]
    [SerializeField] private GameObject PlaneMarker;

    [SerializeField] private ARRaycastManager ARRaycastManagerScript;

    private bool _enabled;

    private void Start()
    {
        PlaneMarker.SetActive(false);
    }

    private void Update()
    {
        if (!_enabled)
            return;
        
        ShowMarker();
    }

    public void SetMarkerState(bool state)
    {
        _enabled = state;
        
        if (!state)
            PlaneMarker.SetActive(false);
    }

    private void ShowMarker() 
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count <= 0) return;
        
        PlaneMarker.transform.position = hits[0].pose.position;
        PlaneMarker.SetActive(true);
    }
}
