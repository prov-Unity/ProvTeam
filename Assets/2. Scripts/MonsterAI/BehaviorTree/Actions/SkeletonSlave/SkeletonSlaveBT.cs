using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSlaveBT : MonoBehaviour
{
    private MonsterAI monsterAI;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Node topNode;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void ConstructBehaviorTree()
    {
        Attack attackNode = new Attack(_animator, monsterAI.targetPosition, _agent);
        Range attackRangeNode = new Range(monsterAI, monsterAI.attackDistance);
        Range traceRangeNode = new Range(monsterAI, monsterAI.traceDistance);
        Trace traceNode = new Trace(_agent, _animator, monsterAI.targetPosition);
        Sequence attackSequence = new Sequence(new List<Node>{attackRangeNode, attackNode});
        Sequence traceSequence = new Sequence(new List<Node> {traceRangeNode, traceNode});

        topNode = new Selector(new List<Node> {attackSequence, traceSequence});
    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.NodeState == NodeState.FAILURE)
        {
            _agent.isStopped = true;
        }
    }
}
