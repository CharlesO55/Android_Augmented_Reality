using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneControls : MonoBehaviour
{
    private VisualElement m_root;

    private Toggle m_planeToggle;


    [SerializeField] private bool m_IsPlanesVisible;
    [SerializeField] ARPlaneManager m_planeManager;


    public DropdownField m_Dropdown { private set; get; }

    void Awake()
    {
        this.m_root = GetComponent<UIDocument>().rootVisualElement;
        this.m_planeToggle = m_root.Q<Toggle>("PlaneVisibility");


        m_IsPlanesVisible = true;
        m_planeToggle.RegisterValueChangedCallback<bool>(TogglePlaneVisibility);
        m_planeToggle.value = true;

        this.m_Dropdown = m_root.Q<DropdownField>("FurnitureChoice");
    }

    void LateUpdate()
    {
        foreach (var plane in m_planeManager.trackables)
        {
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = m_IsPlanesVisible;
        }
    }


    void TogglePlaneVisibility(ChangeEvent<bool> evt)
    {/*
        m_planeManager.SetTrackablesActive(evt.newValue);
        m_planeManager.enabled = evt.newValue;*/

        /*foreach (ARPlaneMeshVisualizer visualzer in m_XROrigin.gameObject.GetComponentsInChildren<ARPlaneMeshVisualizer>())
        {
            visualzer.enabled = evt.newValue;
        }*/

        m_IsPlanesVisible = evt.newValue;
        foreach (var plane in m_planeManager.trackables)
        {
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = m_IsPlanesVisible;
        }
    }


}
