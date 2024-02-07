using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnObject;
    [SerializeField] private float yOffset = 0;

    private TrackingState m_LastState;
    private bool m_HasSpawned;
    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var image in eventArgs.added)
        {
            Debug.Log("[Added]: " +  image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }

        foreach (var image in eventArgs.updated)
        {
            Debug.Log("[Updated]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);

            //SPAWN THE BEACON
            if (AgentManager.INSTANCE && !m_HasSpawned)
            {
                Debug.Log("Spawned Beacon");
                m_HasSpawned = true;

                SpawnOnImage(image.transform);
                AgentManager.INSTANCE.MoveAllAgents(image.transform.position);
            }


            //MOVE PLAYERS ONLY WHEN VISIBLE
            if (AgentManager.INSTANCE &&
                image.trackingState == TrackingState.Tracking)
            {
                AgentManager.INSTANCE.MoveAllAgents(image.transform.position);
            }

            m_LastState = image.trackingState;
        }

        foreach (var image in eventArgs.removed)
        {
            Debug.Log("[Removed]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }
    }


    private void SpawnOnImage(Transform transform)
    {
        GameObject newSpawn = Instantiate(m_SpawnObject);

        newSpawn.transform.SetParent(transform, true);
        
        newSpawn.transform.localPosition = Vector3.up * yOffset;
    }
}
