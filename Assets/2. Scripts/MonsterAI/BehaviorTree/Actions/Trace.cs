using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trace : Node
{
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 target;
    private static readonly int Trace1 = Animator.StringToHash("Trace");

    public Trace(NavMeshAgent agent, Animator animator, Vector3 target)
    {
        this.agent = agent;
        this.animator = animator;
        this.target = target;
    }

    public override NodeState Evaluate()
    {
        agent.SetDestination(target);
        animator.SetBool(Trace1, true);
        return NodeState.SUCCESS;
    }
}
