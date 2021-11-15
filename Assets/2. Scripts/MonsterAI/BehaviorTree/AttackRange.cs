using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : Node
{
    private MonsterAI monsterAI;

    public AttackRange(MonsterAI monsterAI)
    {
        this.monsterAI = monsterAI;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(monsterAI.targetPosition, monsterAI.transform.position);
        if (monsterAI.attackDistance > distance)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
