using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Attack : Node
{
    private Random random;
    private Animator animator;
    private int attackIndex;
    private int attackPatternLength;
    private MonsterBehaviorState monsterBehaviorState;
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int AttackIndexHash = Animator.StringToHash("AttackIndex");

    public Attack(Animator animator, MonsterBehaviorState monsterBehaviorState, int attackPatternLength)
    {
        random = new Random();
        this.animator = animator;
        this.monsterBehaviorState = monsterBehaviorState;
        this.attackPatternLength = attackPatternLength;
    }

    public override NodeState Evaluate()
    {
        if (monsterBehaviorState.isAttack)
            return NodeState.FAILURE;
        Debug.Log("Attack Node 실행됨");
        attackIndex = random.Next(attackPatternLength);
        Debug.Log(attackIndex);
        animator.SetFloat(AttackIndexHash, attackIndex);
        animator.SetBool(AttackHash, true);
        return NodeState.SUCCESS;
    }
    
}
