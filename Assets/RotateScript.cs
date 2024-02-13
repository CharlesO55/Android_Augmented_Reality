using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    private void LateUpdate()
    {
        this.transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
}
