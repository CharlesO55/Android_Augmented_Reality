using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ARPlaneSpawner : MonoBehaviour
{
    private GameObject m_prefabToSpawn;
    private GameObject m_FurnitureToMove;
    [SerializeField] private List<GameObject> m_Furnitures;

    
    
    [SerializeField] ARRaycastManager m_raycastManager;


    [SerializeField] private PlaneControls m_Controls;
    List<ARRaycastHit> m_HitsList = new();


    void Start()
    {
        this.m_FurnitureToMove = null;


        this.m_prefabToSpawn = m_Furnitures[0];
        m_Controls.m_Dropdown.RegisterValueChangedCallback<string>(UpdateFurnitureChoice);
    }

    void UpdateFurnitureChoice(ChangeEvent<string> e)
    {
        switch (e.newValue)
        {
            case "Pizza":
                //https://skfb.ly/6DGT6
                this.m_prefabToSpawn = m_Furnitures[0];
                break;
            case "Table":
                //https://skfb.ly/Y6yA
                this.m_prefabToSpawn = m_Furnitures[1];
                break;
            case "Pillow":
                //https://skfb.ly/6yo8o
                this.m_prefabToSpawn = m_Furnitures[2];
                break;
        }

        Debug.Log("Selected " + m_prefabToSpawn.name);
    }

    void Update()
    {
        if (this.m_FurnitureToMove)
        {
            // GET AN ARPLANE POS AT THE SCREEN CENTER
            if (this.GetARPlanePos(new Vector2(Screen.width / 2, Screen.height / 2), out Vector3 movePos))
            {
                this.m_FurnitureToMove.transform.position = movePos;
            }

            // ROTATE SELECTED OBJECT WHEN SCROLLING
            if (Input.mouseScrollDelta.magnitude != 0)
            {
                this.m_FurnitureToMove.transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse ScrollWheel") * 90);
            }
        }



        if (Input.GetMouseButtonDown(0))
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 inputPos = Input.mousePosition;
            //Vector2 targetPos = Input.GetTouch(0).position;


            // STOP MOVING FURNITURE ON TAP
            if (this.m_FurnitureToMove)
            {
                this.m_FurnitureToMove = null;
            }
            // ELSE CONTINUE PLACING FURNITURE WHEN NOT MOVING
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(inputPos);

                // SELECT THE FURNITURE WHEN HIT BY RACYAST
                if (RaycastForAnchor(ray, out GameObject anchorParent))
                {
                    this.m_FurnitureToMove = anchorParent;

                    //Debug.Log("Destroy Anchor");
                    //Destroy(anchorParent);
                }
                // TRY SPAWN NEW FURNITURE WHEN NOTHING WAS HIT
                else
                {
                    if (this.GetARPlanePos(inputPos, out Vector3 spawnPos))
                    {
                        SpawnPlaneObject(spawnPos);
                    }
                    else
                        Debug.LogWarning("Select a horizontal place to spawn objects");
                }
            }
            
        }
    }


    private bool RaycastForAnchor(Ray ray, out GameObject anchorParent)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform parentAnchor = hit.collider.transform.parent;

            if (parentAnchor.GetComponent<ARAnchor>() != null)
            {
                anchorParent = parentAnchor.gameObject;
                return true;
            }
        }

        anchorParent = null;
        return false;
    }

    private bool GetARPlanePos(Vector2 inputPos, out Vector3 hitPos)
    {
        m_raycastManager.Raycast(inputPos, m_HitsList, TrackableType.PlaneWithinPolygon);


        foreach (ARRaycastHit hit in m_HitsList)
        {
            if (hit.trackable is ARPlane plane)
            {
                if (plane.isActiveAndEnabled && plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    //Debug.Log("Spawned Object");
                    //SpawnPlaneObject(hit.pose.position);

                    hitPos = hit.pose.position;
                    return true;
                }
            }
        }

        hitPos = Vector3.zero;
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

            Debug.Log("Spawned " + m_prefabToSpawn.name);
        }
    }



    private bool CheckEventRaycast(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}