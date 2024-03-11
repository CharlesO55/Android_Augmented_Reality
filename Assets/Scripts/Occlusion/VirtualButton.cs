using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImage))]
public class VirtualButton : MonoBehaviour
{
    private ARTrackedImage trackedImage;
    [SerializeField] private UnityEvent onTrackedImageLimited;

    // Start is called before the first frame update
    void Start()
    {
        trackedImage = GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {
            Debug.Log("Invoking trackedImage limited listeners");
            onTrackedImageLimited.Invoke();
        }
    }
}
