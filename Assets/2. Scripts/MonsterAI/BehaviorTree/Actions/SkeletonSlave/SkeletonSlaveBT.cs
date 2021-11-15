using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[Serializable]
public class FindPlayerTime
{
    [SerializeField, ReadOnly] private float lastFindTime;

    public FindPlayerTime()
    {
        lastFindTime = Time.time;
    }
    
    /// <summary>
    /// 마지막으로 본 시간 업데이트
    /// </summary>
    public void UpdateFindTime()
    {
        Debug.Log("시간 업데이트 됨");
        lastFindTime = Time.time;
    }

    /// <summary>
    /// 잊혀져야 하는지 체크
    /// </summary>
    public bool CheckExpire(float forgetTime)
    {
        return (Time.time - lastFindTime) >= forgetTime;
    }
}

public class SkeletonSlaveBT : MonoBehaviour
{
    [ReadOnly] public int hp;
    [ReadOnly] public float detectionDistance;
    [ReadOnly] public float attackDistance;
    [ReadOnly] public float traceDistance;
    [ReadOnly] public Vector3 targetPosition;
    public FindPlayerTime findPlayerTime;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    private MonsterData monsterData;
    private MonsterInfo monsterInfo;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        findPlayerTime = new FindPlayerTime();
        monsterData = Resources.Load<MonsterData>("Datas/MonsterData");
        monsterInfo = monsterData.monsterInfos[0];
    }
}
