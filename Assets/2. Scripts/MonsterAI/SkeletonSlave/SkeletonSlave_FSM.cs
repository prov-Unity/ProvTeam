using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSlave_FSM : MonoBehaviour
{
    [Header("SkeletonData")]
    public MonsterData monsterData;
    public int currentHp;
    public bool isHitting;
    
    private NavMeshAgent _agent;
    private MonsterState monsterState;
    private Animator _animator;
    private MonsterInfo monsterInfo;
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashTrace = Animator.StringToHash("Trace");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        monsterData = Resources.Load<MonsterData>("Datas/MonsterData");
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        monsterInfo = monsterData.monsterInfos[0];
        currentHp = monsterInfo.hp;
        monsterState = GetComponent<MonsterState>();
        StartCoroutine(MonsterAction());
    }

    
    public IEnumerator MonsterAction()
    {
        while (true)
        {
            switch (monsterState.state)
            {
                case State.IDLE:
                    _agent.isStopped = true;
                    _animator.SetBool(hashTrace, false);
                    break;
                case State.TRACE:
                    _animator.SetBool(hashTrace, true);
                    MoveToTarget();
                    break;
                case State.ATTACK:
                    StartCoroutine(Attack());
                    break;
                case State.DEATH:
                    StartCoroutine(Die());
                    break;
            }

            yield return new WaitForSeconds(0.4f);
        }
    }

    public IEnumerator Attack()
    {
        _agent.isStopped = true;
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _agent.velocity = Vector3.zero;
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0)
                                             .normalizedTime >=
                                         1.2f);
        
        _animator.SetTrigger(hashAttack);
        monsterState.state = State.IDLE;
    }

    public void MoveToTarget()
    {
        _agent.isStopped = false;
        _agent.updatePosition = true;
        _agent.updateRotation = true;
        _agent.SetDestination(monsterState.targetTransform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            OnDamage(other.GetComponent<Weapon>().attackPower);
        }
        
    }

    public void OnDamage(float damage)
    {
        _animator.SetTrigger(hashHit);
        currentHp -= (int) damage;
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            monsterState.state = State.DEATH;
            _animator.SetTrigger(hashDeath);
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3.0f);
        //Destroy(gameObject);
    }
}
