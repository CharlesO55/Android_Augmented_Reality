using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 1f;


    void Start()
    {
        StartCoroutine(LifetimeUpdate());
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator LifetimeUpdate()
    {
        yield return new WaitForSeconds(m_lifetime);

        Destroy(this.gameObject);
    }
}
