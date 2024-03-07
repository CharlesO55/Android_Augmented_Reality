using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(RawImage))]
public class DepthImageViewer : MonoBehaviour
{
    [SerializeField] private AROcclusionManager m_OcclusionManager;
    [SerializeField] private RawImage m_RawImage;
    
    // Update is called once per frame
    void Update()
    {
        Texture2D envDepth = m_OcclusionManager.environmentDepthTexture;
        if (envDepth != null)
        {
            this.m_RawImage.texture = envDepth;
        }
    }
}
