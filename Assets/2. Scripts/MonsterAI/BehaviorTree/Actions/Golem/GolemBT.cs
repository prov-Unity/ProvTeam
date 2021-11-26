using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GolemBT : MonsterAI
{
    private Node _topNode;
    private static readonly int RoarHash = Animator.StringToHash("Roar");
    private static readonly int HpHash = Animator.StringToHash("Hp");
    private const int AttackPatternLength = 2;

    protected override void Awake()
    {
        base.Awake();
        monsterType = MonsterType.Golem;
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

    public void IncreaseAttackPower()
    {
        Anim.SetInteger(HpHash, currentHp);
        if (currentHp > 50)
            return;
        Anim.SetBool(RoarHash, true);
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].attackPower += 10;
        }
    }

    public void RoarStateChange()
    {
        Anim.SetBool(RoarHash, false);
    }
}