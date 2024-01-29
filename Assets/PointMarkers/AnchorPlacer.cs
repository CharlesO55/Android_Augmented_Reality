using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacer : MonoBehaviour
{
    //ARAnchorManager _anchorManager;

    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] float ForwardOffset = 20f;

    [SerializeField] GameObject[] _anchorPrefabs = new GameObject[3];

    void Start()
    {
        //_anchorManager = GetComponent<ARAnchorManager>();

        this.SwapAnchorPrefab(0);

        UIControls.Instance.DeleteAnchorsBtn.RegisterCallback<ClickEvent>(DestroyAllAnchors);
        UIControls.Instance.AnchorSelector.RegisterCallback<ChangeEvent<string>>(ChangeSelectedAnchorPrefab);
    }

    void OnDestroy()
    {
        UIControls.Instance.DeleteAnchorsBtn.UnregisterCallback<ClickEvent>(DestroyAllAnchors);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position );


            if (Physics.Raycast(ray, out RaycastHit hitObj, Mathf.Infinity))
            {
                Debug.Log("Removed anchor" + hitObj.collider);

                Destroy(hitObj.collider.transform.parent.gameObject);
            }
            else
            {
                //NO HIT
                Debug.Log("Added anchor");

                float distanceFromCam = 0.5f + ForwardOffset * UIControls.Instance.CamDistanceMultiplier;
                Vector3 spawnPos = ray.GetPoint(distanceFromCam);
                this.AnchorObject(spawnPos);
            }
        }
    }

    public void AnchorObject(Vector3 worldPos)
    {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = this.transform;
        newAnchor.transform.position = worldPos;
        newAnchor.AddComponent<ARAnchor>();


        if (_prefabToSpawn != null)
        {
            GameObject obj = Instantiate(_prefabToSpawn, newAnchor.transform);
            obj.transform.localPosition = Vector3.zero;

            obj.AddComponent<MeshCollider>();
        }
    }

    public void SwapAnchorPrefab(int _objIndex)
    {
        this._prefabToSpawn = this._anchorPrefabs[Mathf.Clamp(_objIndex, 0, _anchorPrefabs.Length -1 )];
    }


    void DestroyAllAnchors(ClickEvent evt)
    {
        Debug.Log("Deleting all anchors...");
        foreach (Transform anchor in this.transform)
        {
            Destroy(anchor.gameObject);
        }
    }


    void ChangeSelectedAnchorPrefab(ChangeEvent<string> evt)
    {
        Debug.Log("Selected: " + evt.newValue);

        switch (evt.newValue.Last())
        {
            case 'A':
                this._prefabToSpawn = this._anchorPrefabs[0];
                break;
            case 'B':
                this._prefabToSpawn = this._anchorPrefabs[1];
                break;
            case 'C':
                this._prefabToSpawn = this._anchorPrefabs[2];
                break;
            default:
                Debug.LogError("SELECTOR NO MATCH: " + evt.newValue);
                break;
        }
    }
}
