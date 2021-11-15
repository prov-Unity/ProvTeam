using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trace : Node
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform target;
    private static readonly int Trace1 = Animator.StringToHash("Trace");

    public Trace(NavMeshAgent agent, Animator animator, Transform target)
    {
        this.agent = agent;
        this.animator = animator;
        this.target = target;
    }

    public override NodeState Evaluate()
    {
        agent.SetDestination(target.position);
        animator.SetBool(Trace1, true);
        return NodeState.SUCCESS;
    }
}
