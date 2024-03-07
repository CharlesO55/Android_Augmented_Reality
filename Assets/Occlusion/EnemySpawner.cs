using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;


    [SerializeField] private float m_SpawmTime = 1;

    [SerializeField] private List<Vector3> m_SpawnPoints;

    [SerializeField] private Enemy m_Enemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        this.StartCoroutine(SpawnEnemy());
    }

    public void AddSpawnPoint(Vector3 newPos)
    {
        this.m_SpawnPoints.Add(newPos);
    }

    public void RemoveSpawnPoint(Vector3 pointToRemove)
    {
        if (this.m_SpawnPoints.Contains(pointToRemove))
        {
            this.m_SpawnPoints.Remove(pointToRemove);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SpawmTime);

            if(m_SpawnPoints.Count > 0)
            {
                int rng = UnityEngine.Random.Range(0, m_SpawnPoints.Count);

                Enemy newEnemy = GameObject.Instantiate(m_Enemy, m_SpawnPoints[rng], Quaternion.identity);
            }
        }
    }
}