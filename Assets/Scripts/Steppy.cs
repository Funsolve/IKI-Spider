using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steppy : MonoBehaviour
{

    [SerializeField]
    private Transform m_HomeTransform;
    [SerializeField]
    private float m_DistanceBeforeStep;
    [SerializeField]
    private float m_StepSpeed;
    [SerializeField]
    private float m_StepOvershootFraction;

    public bool m_Moving;

    private float m_DistFromHome;

    void Start()
    {
        
    }

    // Update is called once per frame
    public bool UpdateLeg()
    {
        UpdateDistance();
        if (m_Moving)
            return true;

        if(m_DistFromHome > m_DistanceBeforeStep)
        {
            StartCoroutine(MoveToHomePos());
            return true;
        }
        return false;
    }
    public void UpdateDistance()
    {
        m_DistFromHome = Vector3.Distance(transform.position, m_HomeTransform.position);
    }
    public void SetHomePosition(Transform _pos)
    {
        m_HomeTransform = _pos;
    }

    IEnumerator MoveToHomePos()
    {
        m_Moving = true;

        Quaternion startRot = transform.rotation;
        Vector3 startPos = transform.position;

        Quaternion endRot = m_HomeTransform.rotation;

        Vector3 dirToHome = (m_HomeTransform.position - transform.position);
        float overShootDistance = m_DistanceBeforeStep * m_StepOvershootFraction;
        Vector3 overShootVector = dirToHome * overShootDistance;

        Vector3 endPos = m_HomeTransform.position + overShootVector;

        Vector3 centerPos = (startPos + endPos) / 2;
        centerPos += m_HomeTransform.up * Vector3.Distance(startPos, endPos) / 2f;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / m_StepSpeed;
            normalizedTime = Easing.Cubic.InOut(normalizedTime);

            //transform.position = Vector3.Lerp(startPos, endPos, normalizedTime);
            transform.position =
                Vector3.Lerp(
                    Vector3.Lerp(startPos, centerPos, normalizedTime),
                    Vector3.Lerp(centerPos, endPos, normalizedTime),
                    normalizedTime);

            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            yield return null;
        }
        while (timeElapsed < m_StepSpeed);

        m_Moving = false;
    }
}
