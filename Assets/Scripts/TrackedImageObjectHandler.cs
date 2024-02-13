using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnObject;
    private GameObject m_BeaconRef;
    [SerializeField] private float yOffset = 0;

    //private TrackingState m_LastState;
    [SerializeField] private Canvas m_WarningAlert;
    
    private List<ARTrackedImage> m_ValidImages;

    void Start()
    {
        m_BeaconRef = null;
        m_ValidImages = new();
        m_WarningAlert.enabled = false;
    }
    
    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var image in eventArgs.added)
        {
            Debug.Log("[Added]: " +  image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }


        CheckImages(eventArgs);

        foreach (var image in eventArgs.removed)
        {
            Debug.Log("[Removed]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }
    }


    private void CheckImages(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        bool HasLevelSpawned = AgentManager.INSTANCE != null;
        m_ValidImages.Clear();


        foreach (var image in eventArgs.updated)
        {
            Debug.Log("[Updated]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);

            //PLACE THE BEACON WHEN GAME HAS SPAWNED LEVEL WITH MANAGER
            if (HasLevelSpawned)
            {
                if(m_BeaconRef == null)
                    SpawnOnImage(image.transform);

                if (image.trackingState == TrackingState.Tracking)
                {
                    AgentManager.INSTANCE.MoveAllAgents(image.transform.position);

                    //m_LastState = image.trackingState;
                    m_BeaconRef.SetActive(true);
                    m_BeaconRef.transform.SetParent(image.transform, false);
                    
                    m_ValidImages.Add(image);
                    m_WarningAlert.enabled = false;

                    break;
                }
            }
        }


        if (HasLevelSpawned && m_ValidImages.Count == 0)
        {
            m_WarningAlert.enabled = true;

            m_BeaconRef.SetActive(false);
            AgentManager.INSTANCE.StopAllAgents();
        }
    }

    private void SpawnOnImage(Transform transform)
    {
        m_BeaconRef = Instantiate(m_SpawnObject);

        m_BeaconRef.transform.SetParent(transform, true);

        m_BeaconRef.transform.localPosition = Vector3.up * yOffset;
    }
}
