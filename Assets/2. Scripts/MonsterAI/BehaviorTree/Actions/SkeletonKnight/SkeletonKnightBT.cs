using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SkeletonKnightBT : MonsterAI
{
    private Node topNode;
    private bool isAppear;
    private bool AppearEnd;
    private Random random;
    private const int AttackPatternLength = 1;
    private static readonly int AppearIndex = Animator.StringToHash("AppearIndex");
    private const int MAXAppearIndex = 4;

    protected override void Awake()
    {
        base.Awake();
        random = new Random();
        monsterType = MonsterType.SkeletonKnight;
    }

    protected override void Start()
    {
        base.Start();
        target = PPAP.Instance.player.transform;
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

        topNode = new Selector(new List<Node> {attackSequence, traceSequence});
    }


    public override void StartAction()
    {
        base.StartAction();
        if (!isAppear)
        {
            Anim.SetFloat(AppearIndex, random.Next(MAXAppearIndex));
            Debug.Log(isAppear);
            isAppear = true;
        }        
        if (!isRunning && AppearEnd)
            StartCoroutine(Action());
    }

    public void ChangeAppearState()
    {
        AppearEnd = !AppearEnd;
    }

    public override IEnumerator Action()
    {

        isRunning = true;
        yield return StartCoroutine(base.Action());
        while (true)
        {
            CheckForgetTime();
            target.position = PPAP.Instance.player.transform.position;
            yield return new WaitForSeconds(0.2f);
            topNode.Evaluate();
            if (topNode.NodeState == NodeState.FAILURE)
            {
                AgentMoveControl(false);
            }
        }
    }


}