using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariousTranslateMove : MonoBehaviour {

    public float m_power;
    public float m_reduceTime;
    public bool m_fowardMove;
    public bool m_rightMove;
    public bool m_upMove;
    public float m_changedFactor;
    float m_Time;
    public Vector3 dir;
    public Vector3 target;
    private bool isSetPos;

    private void Start()
    {
        m_Time = Time.time;
        target = PPAP.Instance.player.transform.position;

    }

    private void Update () {
        m_changedFactor = VariousEffectsScene.m_gaph_scenesizefactor;
        if (!isSetPos)
        {
            setTargetPosition();
        }
        transform.Translate(dir * 2f);
        // if (m_fowardMove)
        //     transform.Translate(transform.forward * m_power * m_changedFactor);
        // if (m_rightMove)
        //     transform.Translate(transform.right * m_power* m_changedFactor);
        // if (m_upMove)
        //     transform.Translate(transform.up * m_power* m_changedFactor);


        if (m_Time + m_reduceTime < Time.time && m_reduceTime != 0)
        {
            m_power -= Time.deltaTime/10;
            m_power = Mathf.Clamp01(m_power);
        }
    }

    private void setTargetPosition()
    {
        dir = (target - transform.position).normalized;
        transform.LookAt(dir);
    }
    
}
