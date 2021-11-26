using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBT : MonsterAI
{
    private Node _topNode;
    private const int AttackPatternLength = 1;

    protected override void Awake()
    {
        base.Awake();
        monsterType = MonsterType.SkeletonSlave;
    }

    protected override void Start()
    {
        base.Start();
        target = GameManager.instance.player.transform;
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        Attack attackNode = new Attack(Anim, monsterBehaviorState, AttackPatternLength);
        Range attackRangeNode = new Range(this, attackDistance);
        Range traceRangeNode = new Range(this, traceDistance);
        Trace traceNode = new Trace(Agent, Anim, target, monsterBehaviorState);
        Sequence attackSequence = new Sequence(new List<Node>{attackRangeNode, attackNode});
        Sequence traceSequence = new Sequence(new List<Node> {traceRangeNode, traceNode});

        _topNode = new Selector(new List<Node> {attackSequence, traceSequence});
    }


    public override void StartAction()
    {
        base.StartAction();
        if (!isRunning)
            StartCoroutine(Action());
    }

    public override IEnumerator Action()
    {

        isRunning = true;
        yield return StartCoroutine(base.Action());
        while (true)
        {
            CheckForgetTime();
            target.position = GameManager.instance.player.transform.position;
            yield return new WaitForSeconds(0.2f);
            _topNode.Evaluate();
            if (_topNode.NodeState == NodeState.FAILURE)
            {
                AgentMoveControl(false);
            }
        }
    }


}