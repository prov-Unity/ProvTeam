using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Node
{
    private MonsterAI monsterAI;
    private float range;

    public Range(MonsterAI monsterAI, float range)
    {
        this.monsterAI = monsterAI;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(monsterAI.targetPosition, monsterAI.transform.position);
        if (range > distance)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}