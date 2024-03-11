using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LightEstimationInfoPanel : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private HDRLightEstimation lightEstimationData;
    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI avgBrightnessTxt;
    [SerializeField] private TextMeshProUGUI avgColorTempTxt;
    [SerializeField] private TextMeshProUGUI avgIntensityTxt;
    [SerializeField] private TextMeshProUGUI avgMainLightBrightnessTxt;
    [SerializeField] private TextMeshProUGUI colorCorrectionTxt;
    [SerializeField] private TextMeshProUGUI mainLightColorTxt;
    [SerializeField] private TextMeshProUGUI mainLightDirTxt;
    [SerializeField] private TextMeshProUGUI mainLightIntensityTxt;
    [SerializeField] private TextMeshProUGUI sphericalHarmonicsTxt;

    // Update is called once per frame
    void Update()
    {
        if (lightEstimationData.averageBrightness.HasValue)
        {
            avgBrightnessTxt.text = $"{lightEstimationData.averageBrightness.Value}";
        }
        else
        {
            avgBrightnessTxt.text = "None";
        }

        if (lightEstimationData.averageColorTemperature.HasValue)
        {
            avgColorTempTxt.text = $"{lightEstimationData.averageColorTemperature.Value}";
        }
        else
        {
            avgColorTempTxt.text = "None";
        }

        if (lightEstimationData.averageIntensityInLumens.HasValue)
        {
            avgIntensityTxt.text = $"{lightEstimationData.averageIntensityInLumens.Value}";
        }
        else
        {
            avgIntensityTxt.text = "None";
        }

        if (lightEstimationData.averageMainLightBrightness.HasValue)
        {
            avgMainLightBrightnessTxt.text = $"{lightEstimationData.averageMainLightBrightness.Value}";
        }
        else
        {
            avgMainLightBrightnessTxt.text = "None";
        }

        if (lightEstimationData.colorCorrection.HasValue)
        {
            Color c = lightEstimationData.colorCorrection.Value;
            colorCorrectionTxt.text = $"({c.r},{c.g},{c.b})";
        }
        else
        {
            colorCorrectionTxt.text = "None";
        }

        if (lightEstimationData.mainLightColor.HasValue)
        {
            Color c = lightEstimationData.mainLightColor.Value;
            mainLightColorTxt.text = $"({c.r},{c.g},{c.b})";
        }
        else
        {
            mainLightColorTxt.text = "None";
        }

        if (lightEstimationData.mainLightDirection.HasValue)
        {
            Vector3 v = lightEstimationData.mainLightDirection.Value;
            mainLightDirTxt.text = $"({v.x},{v.y},{v.z})";
        }
        else
        {
            mainLightDirTxt.text = "None";
        }

        if (lightEstimationData.mainLightIntensityLumens.HasValue)
        {
            mainLightIntensityTxt.text = $"{lightEstimationData.mainLightIntensityLumens.Value}";
        }
        else
        {
            mainLightIntensityTxt.text = "None";
        }

        if (lightEstimationData.sphericalHarmonics.HasValue)
        {
            SphericalHarmonicsL2 sh = lightEstimationData.sphericalHarmonics.Value;
            sphericalHarmonicsTxt.text = "Yes";
        }
        else
        {
            sphericalHarmonicsTxt.text = "None";
        }
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
