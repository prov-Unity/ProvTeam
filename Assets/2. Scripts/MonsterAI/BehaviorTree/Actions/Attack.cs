using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : Node
{
    private Animator animator;
    private Transform target;
    private NavMeshAgent agent;
    private MonsterBehaviorState monsterBehaviorState;
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    public Attack(Animator animator, Transform target, NavMeshAgent agent, MonsterBehaviorState monsterBehaviorState)
    {
        this.animator = animator;
        this.target = target;
        this.agent = agent;
        this.monsterBehaviorState = monsterBehaviorState;
    }

    public override NodeState Evaluate()
    {
        if (monsterBehaviorState.isAttack)
            return NodeState.FAILURE;
        Debug.Log("Attack Node 실행됨");
        animator.SetBool(AttackHash, true);
        return NodeState.SUCCESS;
    }
    
}
