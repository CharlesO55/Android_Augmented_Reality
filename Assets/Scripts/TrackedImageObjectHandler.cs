using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;

    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var image in eventArgs.added)
        {
            Debug.Log("[Added]: " +  image.referenceImage.name + " | Tracking state: " + image.trackingState);

            TestSpawn(image.transform.position);
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


    private void TestSpawn(Vector3 loc)
    {
        //Instantiate(_spawnObject, loc, Quaternion.identity);
    }
}
