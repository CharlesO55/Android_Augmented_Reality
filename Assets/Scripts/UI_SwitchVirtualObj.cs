using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] UIDocument _uiButtons;
    public EventHandler<int> OnSwitchVirtualObject;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        VisualElement virtObjRoot = _uiButtons.rootVisualElement;
        Button btnA = virtObjRoot.Q<Button>("BtnVirtualObjA");
        Button btnB = virtObjRoot.Q<Button>("BtnVirtualObjB");


        ColorButtons(btnA, Color.yellow);
        btnA.clicked += () => { OnSwitchVirtualObject?.Invoke(this, 0);  ColorButtons(btnA, Color.yellow); ColorButtons(btnB, Color.white); };
        btnB.clicked += () => { OnSwitchVirtualObject?.Invoke(this, 1); ColorButtons(btnA, Color.white); ColorButtons(btnB, Color.yellow); };
    }

    private void ColorButtons(Button btn, Color color)
    {
        btn.style.backgroundColor = color;
    }
}