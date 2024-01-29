using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class PlaneControls : MonoBehaviour
{
    private VisualElement m_root;

    private Toggle m_planeToggle;

    [SerializeField] ARPlaneManager m_planeManager;

    void Start()
    {
        this.m_root = GetComponent<UIDocument>().rootVisualElement;
        this.m_planeToggle = m_root.Q<Toggle>("PlaneVisibility");

        m_planeToggle.RegisterValueChangedCallback<bool>(TogglePlaneVisibility);
        m_planeToggle.value = true;
    }

    void TogglePlaneVisibility(ChangeEvent<bool> evt)
    {
        m_planeManager.SetTrackablesActive(evt.newValue);
        m_planeManager.enabled = evt.newValue;
    }


}
