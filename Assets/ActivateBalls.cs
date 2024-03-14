using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBalls : MonoBehaviour
{
    [SerializeField] GameObject BallsContainer;


    private void Awake()
    {
        foreach (Rigidbody rb in BallsContainer.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = false;
        }
    }
    public void StartAllBallsRB()
    {
        //this.BallsContainer.SetActive(true);

        foreach (Rigidbody rb in BallsContainer.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = true;
        }
    }
}
