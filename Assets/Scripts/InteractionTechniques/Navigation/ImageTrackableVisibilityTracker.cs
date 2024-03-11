using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackableVisibilityTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePanel;

    public void ShowMessagePanel(bool val)
    {
        messagePanel.SetActive(val);
    }

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var image in args.added)
        {
            if (ARNavigationManager.Instance != null)
            {
                ARNavigationManager.Instance.IsBeaconVisible = true;
            }
        }

        foreach (var image in args.updated)
        {
            // Handle updated event
            Debug.Log("Updated Image: " + image.referenceImage.name + " | Tracking State: " + image.trackingState);
            
            if (ARNavigationManager.Instance != null && ARNavigationManager.Instance.HasLevelPlaced) 
            {
                if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                    ARNavigationManager.Instance.IsBeaconVisible = false;
                else if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                    ARNavigationManager.Instance.IsBeaconVisible = true;

                messagePanel.SetActive(!ARNavigationManager.Instance.IsBeaconVisible);
            }
        }
    }
}
