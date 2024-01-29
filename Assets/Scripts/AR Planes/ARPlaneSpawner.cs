using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_prefabToSpawn;


    [SerializeField] ARRaycastManager m_raycastManager;


    List<ARRaycastHit> hitsList = new();


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);


            if (!TryDestroyObject(ray))
            {
                m_raycastManager.Raycast(Input.GetTouch(0).position, hitsList, TrackableType.PlaneWithinPolygon);
                TrySpawnObject();
            }
        }
    }

    private bool TryDestroyObject(Ray ray)
    {


        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform parentAnchor = hit.collider.transform.parent;

            if (parentAnchor.GetComponent<ARAnchor>() != null)
            {
                Debug.Log("Destroy Anchor");
                Destroy(parentAnchor.gameObject);
                return true;
            }
        }


        return false;
    }

    private bool TrySpawnObject()
    {
        foreach (ARRaycastHit hit in hitsList)
        {
            if (hit.trackable is ARPlane plane)
            {
                if (plane.isActiveAndEnabled && plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    Debug.Log("Spawned Object");
                    SpawnPlaneObject(hit.pose.position);
                    return true;
                }
            }
        }

        return false;
    }

    public void SpawnPlaneObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = this.transform;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();


        if (m_prefabToSpawn != null)
        {
            GameObject obj = Instantiate(m_prefabToSpawn, newAnchor.transform);
            obj.transform.localPosition = Vector3.zero;

            obj.AddComponent<MeshCollider>();
        }
    }
}