using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSlave_FSM : MonoBehaviour
{
    [Header("SkeletonData")]
    public int currentHp;
    public bool isHitting;
    
    private NavMeshAgent _agent;
    private MonsterState monsterState;
    private Animator _animator;
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashTrace = Animator.StringToHash("Trace");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        monsterState = GetComponent<MonsterState>();
        currentHp = monsterState.monsterInfo.maxHp;
        _agent.speed = monsterState.monsterInfo.speed;
        StartCoroutine(MonsterAction());
    }

    
    public IEnumerator MonsterAction()
    {
        Debug.Log("현재 상태 : " + monsterState.state);
        while (true)
        {
            switch (monsterState.state)
            {
                case State.IDLE:
                    Debug.Log("IDLE");
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
                    yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Attack()
    {
        AgentStop();
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0)
                                             .normalizedTime >= 0.8f);
        if (monsterState.state != State.DEATH)
            _animator.SetTrigger(hashAttack);
        monsterState.state = State.IDLE;
    }

    public void AgentStop()
    {
        _agent.isStopped = true;
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _agent.velocity = Vector3.zero;
    }

    public void AgentMove()
    {
        _agent.isStopped = false;
        _agent.updatePosition = true;
        _agent.updateRotation = true;
    }

    public void MoveToTarget()
    {
        AgentMove();
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
            _animator.SetBool(hashDeath, true);
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject weaponItem = Resources.Load<GameObject>("Weapons/Bone");
        Instantiate(weaponItem, transform.position, Quaternion.identity);
        AgentStop();
        Destroy(gameObject);
    }
}
