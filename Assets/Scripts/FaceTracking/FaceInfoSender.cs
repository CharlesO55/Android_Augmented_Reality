using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceInfoSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FaceInfoPanel.Instance != null)
        {
            FaceInfoPanel.Instance.SetFacePosition(transform.position);
            FaceInfoPanel.Instance.SetFaceRotation(transform.eulerAngles);
        }
    }
}
