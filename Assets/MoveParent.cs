using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveParent : MonoBehaviour
{
    
    [SerializeField] Transform m_Parent;

    private Vector3 m_StartPos;
    private Vector3 m_MoveDir;

    private float m_MaxDist = 5;

    void Start()
    {
        this.m_Parent = transform.parent;

        m_StartPos = m_Parent.position;

        m_MoveDir = new Vector3(1, 0, 1);
    }


    void Update()
    {
        if (m_Parent.position.x > m_StartPos.x + m_MaxDist)
        {
            m_MoveDir.x = -1;
        }
        else if (m_Parent.position.x < m_StartPos.x - m_MaxDist)
        {
            m_MoveDir.x = 1;
        }

        if (m_Parent.position.z > m_StartPos.z + m_MaxDist)
        {
            m_MoveDir.z = -1;
        }
        else if (m_Parent.position.z < m_StartPos.z - m_MaxDist)
        {
            m_MoveDir.z = 1;
        }

        m_Parent.position += m_MoveDir * Time.deltaTime;
    }
}
