using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


[Serializable]
public class FindPlayerTime
{
    [SerializeField, ReadOnly] private float lastFindTime;

    public FindPlayerTime(float time)
    {
        lastFindTime = time;
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

public enum MonsterType
{
    SkeletonSlave,
    SkeletonKnight,
    Ghost,
    Spider
}

[Serializable]
public class MonsterBehaviorState
{
    public bool isAttack;
    public bool isHitting;
    public bool isDead;
}

public abstract class MonsterAI : MonoBehaviour
{
    [ReadOnly] public int currentHp;
    [ReadOnly] public float attackDistance;
    [ReadOnly] public float traceDistance;
    [ReadOnly] public float forgetTime;
    [ReadOnly] public Transform target;
    [ReadOnly] public FindPlayerTime findPlayerTime;
    [ReadOnly] public MonsterType monsterType;
    [ReadOnly] public MonsterBehaviorState monsterBehaviorState;
    public MonsterData monsterData;
    public MonsterInfo monsterInfo;
    
    protected bool isRunning;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    private Weapon weapon;
    private Weapon hitWeapon;
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int TraceHash = Animator.StringToHash("Trace");

    public NavMeshAgent Agent
    {
        get => _agent;
    }

    public Animator Anim
    {
        get => _animator;
    }

    protected virtual void Awake()
    {
        findPlayerTime = new FindPlayerTime(Time.time);
        monsterBehaviorState = new MonsterBehaviorState();
        weapon = GetComponentInChildren<Weapon>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        monsterData = Resources.Load<MonsterData>("Datas/MonsterData");
    }

    protected virtual void Start()
    {
        monsterInfo = monsterData.monsterInfos[(int)monsterType];
        currentHp = monsterInfo.maxHp;
        attackDistance = monsterInfo.attackDistance;
        traceDistance = monsterInfo.traceDistance;
        forgetTime = monsterInfo.forgetTime;
    }

    public void CheckForgetTime()
    {
        if (findPlayerTime.CheckExpire(forgetTime))
        {
            Debug.Log("모두 멈춰!");
            StopAllCoroutines();
            isRunning = false;
            _animator.SetBool(TraceHash, false);
            AgentMoveControl(false);
        }
    }

    public virtual void StartAction()
    {
        findPlayerTime.UpdateFindTime();
    }

    public virtual IEnumerator Action()
    {
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Weapon"))
            return;
        hitWeapon = other.GetComponent<Weapon>();
        if (hitWeapon != null && hitWeapon.owner.Equals("Player"))
        {
            if (!monsterBehaviorState.isHitting)
                StartCoroutine(GetDamage(hitWeapon.attackPower));
        }
    }

    private IEnumerator GetDamage(int damage)
    {
        _animator.SetTrigger(HitHash);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0)
                                    .normalizedTime >= 0.9f);
        monsterBehaviorState.isHitting = !monsterBehaviorState.isHitting;
        currentHp -= damage;
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            UIManager.instance.DisableMonsterInfo();
            StartCoroutine(Die());
        }
        else
        {
            UIManager.instance.EnableMonsterInfo();
            UIManager.instance.UpdateMonsterInfo(this);
        }
    }

    private IEnumerator Die()
    {
        _animator.SetTrigger(Dead);
        yield return new WaitForSeconds(3.0f);
        if (monsterType == MonsterType.Ghost || 
            monsterType == MonsterType.Spider)
            yield break;
        string weaponName = weapon.weaponType.ToString().Split('_')[0];
        Debug.Log(weaponName);
        GameObject weaponItem = Resources.Load<GameObject>("Weapons/"+weaponName);
        Weapon dropWeapon = weaponItem.GetComponent<Weapon>();
        dropWeapon.durability = WeaponManager.instance.weaponInitialDurabilities[(int) dropWeapon.weaponType];
        Instantiate(weaponItem, transform.position, Quaternion.identity);
        Destroy(gameObject);    
    }
    
    public void TurnOnWeaponCollider()
    {
        weapon.EnableCollider(true);
    }

    public void TurnOffWeaponCollider()
    {
        weapon.EnableCollider(false);
    }

    public void ChangeAttackState()
    {
        monsterBehaviorState.isAttack = !monsterBehaviorState.isAttack;
        AgentMoveControl(monsterBehaviorState.isAttack);
    }

    public void ChangeHitState()
    {
        monsterBehaviorState.isHitting = !monsterBehaviorState.isHitting;
        AgentMoveControl(monsterBehaviorState.isHitting);
    }

    public void ChangeDeadState()
    {
        monsterBehaviorState.isDead = !monsterBehaviorState.isDead;
        AgentMoveControl(monsterBehaviorState.isDead);
    }

    public void AgentMoveControl(bool canMove)
    {
        if (canMove)
        {
            _agent.isStopped = false;
        }
        else
        {
            _agent.isStopped = true;
        }
    }
}
