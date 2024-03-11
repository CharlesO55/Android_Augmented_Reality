using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FaceInfoPanel : MonoBehaviour
{
    private static FaceInfoPanel instance;
    public static FaceInfoPanel Instance
    {
        get
        {
            return instance;
        }

    }

    [SerializeField] private TextMeshProUGUI facePosTxt;
    [SerializeField] private TextMeshProUGUI faceRotTxt;

    private void Awake()
    {
        instance = this;
    }

    public void SetFacePosition(Vector3 position)
    {
        facePosTxt.text = $"Face Position: ({position.x}, {position.y}, {position.z})";
    }

    public void SetFaceRotation(Vector3 rotation)
    {
        faceRotTxt.text = $"Face Rotation: ({rotation.x}, {rotation.y}, {rotation.z})";
    }
}
