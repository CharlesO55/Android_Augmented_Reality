using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastSystem : MonoBehaviour
{
    public static RaycastSystem INSTANCE;

    [SerializeField] private ARRaycastManager m_ARRaycaster;

    private List<ARRaycastHit> m_ArHits = new();



    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool ARRaycast(Vector2 inputPos, out Vector3 hitPos, TrackableType type = TrackableType.PlaneWithinPolygon, PlaneAlignment align = PlaneAlignment.HorizontalUp)
    {
        if (m_ARRaycaster.Raycast(inputPos, m_ArHits, type))
        {
            foreach (var hit in m_ArHits)
            {
                if (hit.trackable is ARPlane plane)
                {
                    if (plane.isActiveAndEnabled && plane.alignment == align)
                    {
                        hitPos = plane.pose.position;
                        return true;
                    }
                }
            }
        }

        hitPos = Vector3.zero;
        return false;
    }
}
