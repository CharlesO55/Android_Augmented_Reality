using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class MarkerEnabler : MonoBehaviour
{
    [SerializeField] UIControls _controls;

    private ARPointCloudParticleVisualizer _particleVisualizer;
    
    void Start()
    {
        _particleVisualizer = GetComponent<ARPointCloudParticleVisualizer>();
        _particleVisualizer.enabled = false;

        _controls.MarkersToggle.RegisterValueChangedCallback(ToggleMarkers);
    }

    private void OnDestroy()
    {
        _controls.MarkersToggle?.UnregisterValueChangedCallback(ToggleMarkers);
    }

    void ToggleMarkers(ChangeEvent<bool> evt)
    {
        if (this.name.Contains("Default"))
            Destroy(this.gameObject);

        _particleVisualizer.enabled = evt.newValue;
    }
}
