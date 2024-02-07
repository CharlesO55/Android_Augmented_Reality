using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentManager : MonoBehaviour
{
    public static AgentManager INSTANCE;

    private List<ARAgent> m_Agents;

    private Transform m_Target;


    void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            INSTANCE = this;
        }
    }

    void Start()
    {
        m_Agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());
    }

    void Update()
    {
        //MoveAgentsClick();
    }


    private void MoveAgentsClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Plane"))
                    MoveAllAgents(hit.point);
            }
        }
    }

    public void MoveAllAgents(Vector3 position)
    {
        foreach (var agent in m_Agents)
        {
            if (!agent.MoveAgent(position))
            {
                return;
            }
        }
    }

    public void StopAllAgents()
    {
        foreach (var agent in m_Agents)
        {
            agent.StopAgent();
        }
    }
}
