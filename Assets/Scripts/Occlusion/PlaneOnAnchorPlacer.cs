using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))]
public class PlaneOnAnchorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject contentPrefab;
    [SerializeField] private Vector3 contentOffset = new Vector3(0, .05f, 0);
    private ARRaycastManager raycastManager;
    private ARAnchorManager anchorManager;
    private List<ARRaycastHit> hits;

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        hits = new List<ARRaycastHit>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction, Color.red, 1.5f);

            // Physics Raycast first to check if tapping on an existing virtual object
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("Removable"))
                {
                    Destroy(hitInfo.collider.gameObject.GetComponentInParent<ARAnchor>().gameObject);
                    return; // exit out of update
                }

            }
            // Raycast only checks against plane within polygon trackables
            if (raycastManager.Raycast(r, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Debug.Log($"Hit Count: {hits.Count}");
                foreach (ARRaycastHit hit in hits)
                {
                    // Check if plane hit is aligned horizontally (up)
                    if (hit.trackable is ARPlane plane && plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
                    {
                        AnchorContent(hit.pose.position, plane);
                        break;  // break, so only checks up until the first valid plane
                    }
                }
            }
        }
    }

    private void AnchorContent(Vector3 worldPos, ARPlane plane)
    {
        //GameObject newAnchor = new GameObject("Anchor");
        //newAnchor.transform.parent = null;
        //newAnchor.transform.position = worldPos;
        Pose p = new Pose(worldPos, Quaternion.identity);
        ARAnchor newAnchor = anchorManager.AttachAnchor(plane, p);

        GameObject content = Instantiate(contentPrefab, newAnchor.transform);
        content.transform.localPosition = contentOffset;
    }


    public void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARAnchor> args)
    {
        foreach (ARAnchor anchor in args.added)
        {
            Debug.Log("New anchor added - " + " State: " + anchor.trackingState);
        }

        foreach (ARAnchor anchor in args.updated)
        {
            Debug.Log("Anchor updated - " + " State: " + anchor.trackingState);
        }

        foreach (ARAnchor anchor in args.removed)
        {
            Debug.Log("Anchor removed");
        }
    }


}
