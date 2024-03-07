using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    Rigidbody m_rb;


    [SerializeField] private float m_Speed = 0.5f;
    [SerializeField] private Vector3 m_Direction = Vector3.forward;


    private void Start()
    {
        this.m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.m_rb.velocity = m_Direction * m_Speed * Time.deltaTime;
        this.m_rb.angularVelocity = Vector3.up * m_Speed * 8* Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered " + other.gameObject.name);

        if (other.CompareTag("Projectile"))
        {
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("Turret"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}