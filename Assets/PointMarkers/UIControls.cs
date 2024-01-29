using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class UIControls : MonoBehaviour
{
    public static UIControls Instance = null;

    VisualElement _root;



    public float CamDistanceMultiplier {  get; private set; }
    public Toggle MarkersToggle { get; private set; }
    public Button DeleteAnchorsBtn { get; private set; }
    public DropdownField AnchorSelector { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        this.MarkersToggle = this._root.Q<Toggle>("ToggleMarkers");
        this.DeleteAnchorsBtn = this._root.Q<Button>("DeleteAnchorsBtn");
        this.AnchorSelector = this._root.Q<DropdownField>("AnchorSelector");

        this.AnchorSelector.value = "OBJECT A";

        this._root.Q<Slider>().RegisterValueChangedCallback(evt =>
        {
            this.CamDistanceMultiplier = evt.newValue;
        });

        this._root.Q<Image>("SettingsIconBtn").RegisterCallback<ClickEvent>(this.ToggleControlsVisible);
    }

    void OnDestroy()
    {
        this._root.Q<Slider>().UnregisterValueChangedCallback(evt =>
        {
            this.CamDistanceMultiplier = evt.newValue;
        });

        this._root.Q<Image>("SettingsIconBtn").UnregisterCallback<ClickEvent>(this.ToggleControlsVisible);
    }

    void ToggleControlsVisible(ClickEvent evt)
    {
        VisualElement controls = this._root.Q<VisualElement>("ControlsContainer");

        if (controls.style.display == DisplayStyle.Flex)
        {
            controls.style.display = DisplayStyle.None;
        }
        else
        {
            controls.style.display = DisplayStyle.Flex;
        }
    }
    
}