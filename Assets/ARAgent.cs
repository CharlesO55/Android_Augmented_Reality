using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ARAgent : MonoBehaviour
{

    private NavMeshAgent m_Agent;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }


    public void MoveAgent(Vector3 position)
    {
        m_Agent.isStopped = false;
        m_Agent.destination = position;
    }

    public void StopAgent()
    {
        m_Agent.isStopped = true;
    }
}