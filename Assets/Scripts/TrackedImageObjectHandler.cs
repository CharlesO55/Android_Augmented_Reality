using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_SpawnObject;
    [SerializeField] private float yOffset = 0;

    
    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var image in eventArgs.added)
        {
            Debug.Log("[Added]: " +  image.referenceImage.name + " | Tracking state: " + image.trackingState);

            TestSpawn(image.transform);
            AgentManager.INSTANCE.m_IsUpdating = true;
        }

        foreach (var image in eventArgs.updated)
        {
            Debug.Log("[Updated]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);

        }

        foreach (var image in eventArgs.removed)
        {
            Debug.Log("[Removed]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }
    }


    private void TestSpawn(Transform transform)
    {
        GameObject newSpawn = Instantiate(m_SpawnObject);

        newSpawn.transform.SetParent(transform, true);
        newSpawn.transform.position = Vector3.up * yOffset;
    }
}
