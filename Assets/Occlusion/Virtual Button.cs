using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImage))]
public class VirtualButton : MonoBehaviour
{
    private ARTrackedImage m_TrackedImage;


    [SerializeField] private UnityEvent onTrackedImageLimited;



    void Start()
    {
        m_TrackedImage = GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TrackedImage.trackingState == TrackingState.Limited)
        {
            Debug.Log("Limited tracking state");
            this.onTrackedImageLimited?.Invoke();
        }
    }
}
