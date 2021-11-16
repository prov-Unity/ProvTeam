using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSlaveBT : MonoBehaviour
{
    private MonsterAI monsterAI;
    private Node topNode;

    private void Awake()
    {
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        Attack attackNode = new Attack(monsterAI.Anim, monsterAI.targetPosition, monsterAI.Agent);
        Range attackRangeNode = new Range(monsterAI, monsterAI.attackDistance);
        Range traceRangeNode = new Range(monsterAI, monsterAI.traceDistance);
        Trace traceNode = new Trace(monsterAI.Agent, monsterAI.Anim, monsterAI.targetPosition);
        Sequence attackSequence = new Sequence(new List<Node>{attackRangeNode, attackNode});
        Sequence traceSequence = new Sequence(new List<Node> {traceRangeNode, traceNode});

        topNode = new Selector(new List<Node> {attackSequence, traceSequence});
    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.NodeState == NodeState.FAILURE)
        {
            monsterAI.Agent.isStopped = true;
        }
    }
}
