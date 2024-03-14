using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObject : MonoBehaviour
{
    private float _threshold = -25;

    private void Update()
    {
        if(this.transform.position.y < _threshold)
        {
            Debug.Log("Respawn");

            this.GetComponent<Rigidbody>().velocity = Vector3.zero;

            this.transform.localPosition = Vector3.up;
        }
    }
}
