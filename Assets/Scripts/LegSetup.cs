using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DitzelGames.FastIK;

public class LegSetup : MonoBehaviour
{
    [SerializeField]
    private int m_LegIdentifier;

    [SerializeField]
    private FastIKFabric m_IKScript;

    [SerializeField]
    private GameObject m_LegTargetPrefab;

    [SerializeField]
    private GameObject m_LegTargetHomePrefab;

    private GameObject m_SpawnedLegTarget;
    private GameObject m_SpawnedLegTargetHome;

    private RobotBrain m_RobotBrain;

    private bool m_ReadyToWalk = false;

    void Awake()
    {
        if(m_RobotBrain == null)
        {
            m_RobotBrain = transform.parent.parent.parent.GetComponent<RobotBrain>();
        }

        if(m_LegTargetPrefab == null)
        {
            Debug.LogError("Didn't set the Leg Target Prefab on leg " + gameObject.name);
            return;
        }
        if(m_IKScript == null)
        {
            Debug.LogError("Leg not set up correctly");
            return;
        }
        // Spawn & set the object the leg follows.
        if(m_SpawnedLegTarget == null)
        {
            m_SpawnedLegTarget = GameObject.Instantiate(m_LegTargetPrefab, m_IKScript.transform.position, m_LegTargetPrefab.transform.rotation);
            m_SpawnedLegTarget.name += m_LegIdentifier;
            m_IKScript.Target = m_SpawnedLegTarget.transform;

        }
        // Spawn the object the target tries to be close to.
        if(m_SpawnedLegTargetHome == null)
        {
            m_SpawnedLegTargetHome = GameObject.Instantiate(m_LegTargetHomePrefab, m_IKScript.transform.position, m_LegTargetHomePrefab.transform.rotation, transform.parent.parent);
            m_SpawnedLegTargetHome.name = "LegTargetHome" + m_LegIdentifier;
            m_SpawnedLegTarget.GetComponent<Steppy>().SetHomePosition(m_SpawnedLegTargetHome.transform);
        }

        if (m_IKScript != null && m_SpawnedLegTarget != null && m_SpawnedLegTargetHome != null)
        {
            m_ReadyToWalk = true;
            m_RobotBrain.AddLeg(m_SpawnedLegTarget.GetComponent<Steppy>(), m_LegIdentifier - 1);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " has failed to setup.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
