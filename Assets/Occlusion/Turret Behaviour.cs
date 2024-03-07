using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] private Bullet m_Bullet;
    [SerializeField] private int m_BulletSpeed = 10;


    [SerializeField] private float m_updateTime = 0.5f;
    private ETurretState m_State = ETurretState.IDLE;



    private Vector3 m_SpawnPoint;
    void Start()
    {
        this.m_Animator = GetComponent<Animator>();

        StartCoroutine("TurretUpdate");


        m_SpawnPoint = this.transform.position + new Vector3(0, 0, 1.5f);
        EnemySpawner.Instance.AddSpawnPoint(m_SpawnPoint);
    }

    public void Shoot()
    {
        this.m_State = ETurretState.SHOOT;
    }


    private IEnumerator TurretUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_updateTime);


            bool isShooting = this.m_State == ETurretState.SHOOT;

            this.m_Animator.SetBool("IsShooting", isShooting);

            if (isShooting)
            {
                Bullet bullet = GameObject.Instantiate(m_Bullet, this.transform.position + this.transform.forward * .3f, Quaternion.identity);
                
                bullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * m_BulletSpeed, ForceMode.Impulse);

                this.m_State = ETurretState.IDLE;
            }
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.Instance.RemoveSpawnPoint(m_SpawnPoint);
    }
}