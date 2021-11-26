using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMiniBT : MonsterAI
{
    private Node _topNode;
    private const int AttackPatternLength = 2;

    protected override void Awake()
    {
        base.Awake();
        monsterType = MonsterType.GhostMini;
    }

    protected override void Start()
    {
        base.Start();
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        Attack attackNode = new Attack(Anim, monsterBehaviorState, AttackPatternLength);
        Range attackRangeNode = new Range(this, attackDistance);
        Range traceRangeNode = new Range(this, traceDistance);
        Trace traceNode = new Trace(Agent, Anim, GameManager.instance.player.transform, monsterBehaviorState);
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
            yield return new WaitForSeconds(0.2f);
            _topNode.Evaluate();
            if (_topNode.NodeState == NodeState.FAILURE)
            {
                AgentMoveControl(false);
            }
        }
    }

    public void AttackBlackHole()
    {
        GameObject blackHole = Resources.Load<GameObject>("Weapons/BlackHole");
        Instantiate(blackHole, transform.position + transform.forward * 3, Quaternion.identity);
    }

    public void AttackIceWheel()
    {
        Vector3 originPos = transform.position;
        GameObject iceWheel = Resources.Load<GameObject>("Weapons/IceWheel");
        Vector3 dir = PPAP.Instance.player.transform.position - originPos;
        Instantiate(iceWheel, originPos + transform.forward * 3, Quaternion.identity);
        
    }
}