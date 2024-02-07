using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelToSpawn;

    private bool m_HasSpawnedAlready = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            !m_HasSpawnedAlready)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (RaycastSystem.INSTANCE.ARRaycast(Input.mousePosition, out Vector3 hitPos))
            {
                SpawnAnchored(hitPos);
            }
        }
    }

    private void SpawnAnchored(Vector3 worldPos)
    {
        GameObject newAnchor = new("Level Anchor", typeof(ARAnchor));
        newAnchor.transform.SetParent(this.transform);
        newAnchor.transform.position = worldPos;


        GameObject newLevel = GameObject.Instantiate(m_LevelToSpawn, newAnchor.transform);
        newLevel.transform.localPosition = Vector3.zero;


        this.m_HasSpawnedAlready = true;
    }
}