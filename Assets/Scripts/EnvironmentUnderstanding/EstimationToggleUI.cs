using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EstimationToggleUI : MonoBehaviour
{
    [SerializeField] private HDRLightEstimation lightEstimation;
    [SerializeField] private TextMeshProUGUI tmGUI;

    // Start is called before the first frame update
    void Start()
    {
        if (tmGUI == null)
            tmGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        if (lightEstimation.isActive)
        {
            tmGUI.text = "Turn ON Light Estimation";
            lightEstimation.isActive = false;
        }
        else
        {
            tmGUI.text = "Turn OFF Light Estimation";
            lightEstimation.isActive = true;
        }
    }


}
