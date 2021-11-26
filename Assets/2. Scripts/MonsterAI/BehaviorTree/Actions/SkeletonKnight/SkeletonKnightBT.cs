using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SkeletonKnightBT : MonsterAI
{
    private Node _topNode;
    private bool _isAppear;
    private bool _appearEnd;
    private Random _random;
    private const int AttackPatternLength = 1;
    private static readonly int AppearIndex = Animator.StringToHash("AppearIndex");
    private const int MAXAppearIndex = 4;

    protected override void Awake()
    {
        base.Awake();
        _random = new Random();
        monsterType = MonsterType.SkeletonKnight;
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
        if (!_isAppear)
        {
            Anim.SetFloat(AppearIndex, _random.Next(MAXAppearIndex));
            Debug.Log(_isAppear);
            _isAppear = true;
        }        
        if (!isRunning && _appearEnd)
            StartCoroutine(Action());
    }

    public void ChangeAppearState()
    {
        _appearEnd = !_appearEnd;
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