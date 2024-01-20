using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageObjectHandler : MonoBehaviour
{
    [SerializeField] private ImageObjectLibrary _imgObjLib;

    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private int _virtualObjIndex = 0;

    public void Start()
    {
        UIManager.Instance.OnSwitchVirtualObject += ReplaceSpawnedObjects;
    }
    private void OnDestroy()
    {
        UIManager.Instance.OnSwitchVirtualObject -= ReplaceSpawnedObjects;
    }

    
    public void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(ARTrackedImage image in eventArgs.added)
        {
            Debug.Log("[Added]: " +  image.referenceImage.name + " | Tracking state: " + image.trackingState);


            GameObject newSpawn = Instantiate(
                _imgObjLib.GetGameObject(image.referenceImage.name, _virtualObjIndex), 
                image.transform);

            _spawnedObjects.Add(newSpawn);
        }

        /*foreach (var image in eventArgs.updated)
        {
            Debug.Log("[Updated]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }

        foreach (var image in eventArgs.removed)
        {
            Debug.Log("[Removed]: " + image.referenceImage.name + " | Tracking state: " + image.trackingState);
        }*/
    }

    public void ReplaceSpawnedObjects(object sender, int virtualObjIndex)
    {
        
        if (virtualObjIndex == this._virtualObjIndex)
        {
            Debug.LogWarning("Switch cancelled. Same virtual objects");
            return;
        }

        Debug.Log("Switching virtual objects to " + virtualObjIndex);

        this._virtualObjIndex = virtualObjIndex;

        
        List<GameObject> newSpawns = new List<GameObject>();
        foreach(GameObject oldObj in  _spawnedObjects)
        {
            newSpawns.Add( 
                Instantiate(_imgObjLib.GetGameObject(oldObj, virtualObjIndex), oldObj.transform.parent)
                );

            Destroy(oldObj);
        }

        _spawnedObjects.Clear();
        _spawnedObjects = newSpawns;
    }
}
