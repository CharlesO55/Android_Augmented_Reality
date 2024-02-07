using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ARAgent : MonoBehaviour
{

    private NavMeshAgent m_Agent;

    [SerializeField] private NavMeshSurface m_Surface;
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }


    public bool MoveAgent(Vector3 position)
    {
        //IGNORE WHEN ALREADY MOVING TO SAME DESTINATION
        if (m_Agent.destination == position)
        {
            return false;
        }

        m_Agent.isStopped = false;
        m_Agent.destination = position;

        return true;
    }

    public void StopAgent()
    {
        m_Agent.isStopped = true;
    }
}