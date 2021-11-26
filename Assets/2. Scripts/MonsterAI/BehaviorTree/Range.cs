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
        if (range >= 2f && range <= 5f)
            Debug.Log("TraceRange 실행됨");
        else if (range <= 2f)
            Debug.Log("AttackRange 실행됨");

        float distance = Vector3.Distance(GameManager.instance.player.transform.position, monsterAI.transform.position);
        if (range > distance)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}