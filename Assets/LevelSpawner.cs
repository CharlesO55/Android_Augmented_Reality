using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private ARRaycastManager m_Raycaster;
    [SerializeField] private GameObject m_LevelToSpawn;

    private bool m_HasSpawnedAlready = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            !m_HasSpawnedAlready)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            List<ARRaycastHit> hits = new();
            if (m_Raycaster.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                foreach (var hit in hits)
                {
                    if (hit.trackable.GetComponent<ARPlane>())
                    {
                        Debug.Log("Che");

                        SpawnLevel(hit.trackable.transform);

                        break;
                    }
                }
            }
        }   
    }

    private void SpawnLevel(Transform trackableTransform)
    {
        m_HasSpawnedAlready = true;

        Instantiate(this.m_LevelToSpawn, trackableTransform);

        Debug.Log("Spawned level on horizontal plane");
    }
}