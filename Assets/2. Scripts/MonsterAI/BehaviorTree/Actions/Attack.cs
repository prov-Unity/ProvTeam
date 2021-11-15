using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    private Animator animator;
    private Transform target;
    private Transform origin;
    private float attackDistance;
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public Attack(Animator animator, Transform target, Transform origin, float attackDistance)
    {
        this.animator = animator;
        this.target = target;
        this.origin = origin;
        this.attackDistance = attackDistance;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(origin.position, target.position);
        if (distance < attackDistance)
        {
            animator.SetTrigger(Attack1);
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
