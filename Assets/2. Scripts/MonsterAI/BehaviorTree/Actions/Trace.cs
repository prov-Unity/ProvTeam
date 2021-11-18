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
    private static readonly int Trace1 = Animator.StringToHash("Trace");

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
        agent.isStopped = false;
        agent.SetDestination(target.position);
        animator.SetBool("IsAttack", false);
        animator.SetBool(Trace1, true);
        return NodeState.SUCCESS;
    }
}
