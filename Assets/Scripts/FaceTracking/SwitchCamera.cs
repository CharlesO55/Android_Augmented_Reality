using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private ARCameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraManager == null)
            cameraManager = GameObject.FindFirstObjectByType<ARCameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCameraMode()
    {
        CameraFacingDirection dir = cameraManager.currentFacingDirection;
        if (dir == CameraFacingDirection.World)
            cameraManager.requestedFacingDirection = CameraFacingDirection.User;
        else
            cameraManager.requestedFacingDirection = CameraFacingDirection.World;
    }
}
