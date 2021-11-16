using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSlaveBT : MonsterAI
{
    private Node topNode;

    protected override void Awake()
    {
        base.Awake();
        monsterTypeIdx = (int)MonsterType.SkeletonSlave;
    }

    protected override void Start()
    {
        base.Start();
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        print(Agent);
        print(Anim);
        Attack attackNode = new Attack(Anim, targetPosition, Agent);
        Range attackRangeNode = new Range(this, attackDistance);
        Range traceRangeNode = new Range(this, traceDistance);
        Trace traceNode = new Trace(Agent, Anim, targetPosition);
        Sequence attackSequence = new Sequence(new List<Node>{attackRangeNode, attackNode});
        Sequence traceSequence = new Sequence(new List<Node> {traceRangeNode, traceNode});

        topNode = new Selector(new List<Node> {attackSequence, traceSequence});
    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.NodeState == NodeState.FAILURE)
        {
            Agent.isStopped = true;
        }
    }

    public override void Action()
    {
        base.Action();
        
    }
}
