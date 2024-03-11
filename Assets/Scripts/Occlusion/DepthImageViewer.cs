using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(RawImage))]
public class DepthImageViewer : MonoBehaviour
{
    [SerializeField] private AROcclusionManager occlusionManager;
    [SerializeField] private RawImage rawImage;

    // Update is called once per frame
    void Update()
    {
        Texture2D envDepth = occlusionManager.environmentDepthTexture;
        if (envDepth != null)
            rawImage.texture = envDepth;
    }
}
