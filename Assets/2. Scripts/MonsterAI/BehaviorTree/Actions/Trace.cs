using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trace : Node
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform target;
    private MonsterBehaviorState monsterBehaviorState;
    private static readonly int TraceHash = Animator.StringToHash("Trace");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    public Trace(NavMeshAgent agent, Animator animator, Transform target, MonsterBehaviorState monsterBehaviorState)
    {
        this.agent = agent;
        this.animator = animator;
        this.target = target;
        this.monsterBehaviorState = monsterBehaviorState;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("TraceNode 실행됨");

        if (monsterBehaviorState.isAttack)
            return NodeState.FAILURE;
        animator.SetBool(AttackHash, false);
        agent.isStopped = false;
        animator.SetBool(TraceHash, true);
        agent.SetDestination(target.position);
        return NodeState.SUCCESS;
    }
}
