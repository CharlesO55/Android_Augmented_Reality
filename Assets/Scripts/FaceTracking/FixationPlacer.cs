using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FixationPlacer : MonoBehaviour
{
    private ARFace face;
    [SerializeField] private GameObject fixationPrefab;
    [SerializeField] private GameObject eyePrefab;
    private GameObject fixationObj;
    private GameObject eyeObjL;
    private GameObject eyeObjR;


    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<ARFace>();   
        if (fixationPrefab != null )
        {
            fixationObj = Instantiate(fixationPrefab, transform);
        }

        if (eyePrefab != null )
        {
            eyeObjL = Instantiate(eyePrefab, transform);
            eyeObjR = Instantiate(eyePrefab, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fixationObj != null)
        {
            fixationObj.transform.position = face.fixationPoint.transform.position;
            fixationObj.transform.rotation = face.fixationPoint.transform.rotation;
        }

        if (eyePrefab != null)
        {
            eyeObjL.transform.position = face.leftEye.transform.position;
            eyeObjL.transform.rotation = face.leftEye.transform.rotation;

            eyeObjR.transform.position = face.rightEye.transform.position;
            eyeObjR.transform.rotation = face.rightEye.transform.rotation;
        }
    }
}
