using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : Node
{
    private Animator animator;
    private Vector3 target;
    private NavMeshAgent agent;
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public Attack(Animator animator, Vector3 target,  NavMeshAgent agent)
    {
        this.animator = animator;
        this.target = target;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        agent.transform.rotation = Quaternion.LookRotation(target, Vector3.forward);
        animator.SetTrigger(Attack1);
        return NodeState.SUCCESS;
    }
}
