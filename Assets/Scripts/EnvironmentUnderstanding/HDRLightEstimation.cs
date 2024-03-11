using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class HDRLightEstimation : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager m_CameraManager;

    [SerializeField] private Light directionalLight;

    public bool isActive = true;

    public float? averageBrightness
    {
        get; private set;
    }

    public float? averageColorTemperature
    {
        get; private set;
    }
    public float? averageIntensityInLumens
    {
        get; private set;
    }

    public float? averageMainLightBrightness
    {
        get; private set;
    }

    public Color? colorCorrection
    {
        get; private set;
    }

    public Color? mainLightColor
    {
        get; private set;
    }

    public Vector3? mainLightDirection
    {
        get; private set;
    }

    public float? mainLightIntensityLumens
    {
        get; private set;
    }

    public SphericalHarmonicsL2? sphericalHarmonics
    {
        get; private set;
    }


    private void Start()
    {
        this.directionalLight = GetComponent<Light>();
    }

    void OnEnable()
    {
        m_CameraManager.frameReceived += CameraFrameUpdated;
    }

    private void OnDisable()
    {
        m_CameraManager.frameReceived -= CameraFrameUpdated;
    }

    private void CameraFrameUpdated(ARCameraFrameEventArgs args)
    {
        if (!isActive)
        {
            directionalLight.transform.eulerAngles = Vector3.zero;
            directionalLight.color = Color.white;
            directionalLight.intensity = 1f;
            return;
        }

        averageBrightness = args.lightEstimation.averageBrightness;
        averageColorTemperature = args.lightEstimation.averageColorTemperature;
        averageIntensityInLumens = args.lightEstimation.averageIntensityInLumens;
        averageMainLightBrightness = args.lightEstimation.averageMainLightBrightness;
        colorCorrection = args.lightEstimation.colorCorrection;
        mainLightColor = args.lightEstimation.mainLightColor;
        mainLightDirection = args.lightEstimation.mainLightDirection;
        mainLightIntensityLumens = args.lightEstimation.mainLightIntensityLumens;
        sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;

        if (args.lightEstimation.averageBrightness.HasValue)
        {
            Debug.Log($"averageBrightness: {args.lightEstimation.averageBrightness.Value}");
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            Debug.Log($"averageColorTemperature: {args.lightEstimation.averageColorTemperature.Value}");
            directionalLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.averageIntensityInLumens.HasValue)
        {
            Debug.Log($"averageIntensityInLumens: {args.lightEstimation.averageIntensityInLumens.Value}");
        }

        if (args.lightEstimation.averageMainLightBrightness.HasValue)
        {
            Debug.Log($"averageMainLightBrightness: {args.lightEstimation.averageMainLightBrightness.Value}");
            directionalLight.intensity = args.lightEstimation.averageMainLightBrightness.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            Color c = args.lightEstimation.colorCorrection.Value;
            Debug.Log($"colorCorrection (R,G,B): ({c.r},{c.g},{c.b})");
        }

        if (args.lightEstimation.mainLightColor.HasValue)
        {
            Color c = args.lightEstimation.mainLightColor.Value;
            Debug.Log($"mainLightColor (R,G,B): ({c.r},{c.g},{c.b})");

            directionalLight.color = c;
        }

        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            Vector3 v = args.lightEstimation.mainLightDirection.Value;
            Debug.Log($"mainLightDirection (Vector3): ({v.x},{v.y},{v.z})");

            directionalLight.transform.rotation = Quaternion.LookRotation( v );
        }

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            Debug.Log($"mainLightIntensityLumens: {args.lightEstimation.mainLightIntensityLumens.Value}");
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            SphericalHarmonicsL2 sh = args.lightEstimation.ambientSphericalHarmonics.Value;
            Debug.Log($"Spherical Harmonics: Yes");
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = sh;
        }
    }
}
