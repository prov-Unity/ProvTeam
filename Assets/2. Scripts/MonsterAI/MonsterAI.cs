using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public class MonsterAI : MonoBehaviour
{
    [ReadOnly] public string name;
    [ReadOnly] public int hp;
    [ReadOnly] public float detectionDistance;
    [ReadOnly] public float attackDistance;
    [ReadOnly] public float traceDistance;
    [ReadOnly] public Vector3 targetPosition;
    public FindPlayerTime findPlayerTime;

    private Animator _animator;
    private MonsterData monsterData;
    private MonsterInfo monsterInfo;
    private static readonly int Hit1 = Animator.StringToHash("Hit");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        monsterData = Resources.Load<MonsterData>("Datas/MonsterData");
        monsterInfo = monsterData.monsterInfos[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GetDamage(other.GetComponent<Weapon>().attackPower));
        }
    }

    private IEnumerator GetDamage(int damage)
    {
        _animator.SetTrigger(Hit1);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0)
                                    .normalizedTime >= 0.8f);
        monsterInfo.hp -= damage;
        if (monsterInfo.hp <= 0)
        {
            StartCoroutine(Die());
        }
            
    }

    private IEnumerator Die()
    {
        
    }
}
