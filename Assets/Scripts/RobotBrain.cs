using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBrain : MonoBehaviour
{
    private Steppy[] m_LegPos;
    private bool[] m_UpdatedLegs;

    private void Awake()
    {
        if(m_LegPos == null)
        {
            m_LegPos = new Steppy[6];
        }
        if(m_UpdatedLegs == null)
        {
            m_UpdatedLegs = new bool[3];
            m_UpdatedLegs[0] = false;
            m_UpdatedLegs[1] = false;
            m_UpdatedLegs[2] = false;
        }
    }

    public void AddLeg(Steppy _newLeg, int _arrPos)
    {
        if (_arrPos < m_LegPos.Length)
        {
            m_LegPos[_arrPos] = _newLeg;
        }
        else
        {
            Debug.LogWarning("Trying to add too many legs?");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_LegPos[0].m_Moving && !m_LegPos[2].m_Moving && !m_LegPos[4].m_Moving)
        {
            for(int i = 0; i < m_UpdatedLegs.Length; i++)
            {
                if(!m_UpdatedLegs[i])
                {
                    if(m_LegPos[i*2].UpdateLeg())
                    {
                        m_LegPos[(i * 2) + 1].UpdateLeg();
                        m_UpdatedLegs[i] = true;
                        break;
                    }
                }
            }
            
            if(m_UpdatedLegs[0] && m_UpdatedLegs[1] && m_UpdatedLegs[2])
            {
                for(int i = 0; i < m_UpdatedLegs.Length; i++)
                {
                    m_UpdatedLegs[i] = false;
                }
            }
        }
    }


}
