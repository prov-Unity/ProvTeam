using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSlave_FSM : MonoBehaviour
{
    [Header("SkeletonData")] 
    [ReadOnly] public int hp;
    [ReadOnly] public int damage;
    [ReadOnly] public int speed;
    public MonsterData monsterData;

    private SkeletonSlave_State skeletonSlaveState;
    private Animator _animator;
    private MonsterInfo monsterInfo;
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashTrace = Animator.StringToHash("Trace");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    
    
    private void Awake()
    {
        monsterData = Resources.Load<MonsterData>("/Datas/MonsterData");
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        monsterInfo = monsterData.monsterInfos[0];
        skeletonSlaveState = GetComponent<SkeletonSlave_State>();
        StartCoroutine(MonsterAction(skeletonSlaveState.state));
    }

    
    public IEnumerator MonsterAction(State state)
    {
        switch (state)
        {
            case State.IDLE:
                _animator.SetBool(hashTrace, false);
                break;
            case State.TRACE:
                _animator.SetBool(hashTrace, true);
                MoveToTarget();
                break;
            case State.ATTACK:
                _animator.SetTrigger(hashAttack);
                break;
            case State.DEATH:
                _animator.SetTrigger(hashDeath);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(0.4f);
    }


    public void MoveToTarget()
    {
        Vector3 dir = (skeletonSlaveState.targetTransform.position - transform.position).normalized;
        transform.Translate(dir * speed);
        float angle = Vector3.SignedAngle(transform.forward, skeletonSlaveState.targetTransform.position,
                                                        Vector3.up);
        transform.Rotate(0f, Mathf.Lerp(0f, angle, 0.5f), 0f);
    }

    
    public void OnDamage()
    {
        _animator.SetTrigger(hashHit);
        hp -= damage;
        if (hp <= 0)
        {
            skeletonSlaveState.state = State.DEATH;
            _animator.SetTrigger(hashDeath);
        }
    }
    

    public void MonsterDestroy()
    {
        Destroy(this.gameObject);
    }
}
