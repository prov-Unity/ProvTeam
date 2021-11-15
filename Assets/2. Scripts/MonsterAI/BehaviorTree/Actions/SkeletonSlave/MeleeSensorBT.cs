using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeSensorBT : Node
{
    private SkeletonSlaveBT skeletonSlaveBT;
    private Transform origin;
    private NavMeshAgent agent;
    private Collider[] colliders;

    public MeleeSensorBT(SkeletonSlaveBT skeletonSlaveBt, Transform origin, NavMeshAgent agent)
    {
        skeletonSlaveBT = skeletonSlaveBt;
        this.origin = origin;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Physics.OverlapSphereNonAlloc(origin.position, skeletonSlaveBT.detectionDistance, colliders);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                agent.isStopped = false;
                skeletonSlaveBT.findPlayerTime.UpdateFindTime();
                skeletonSlaveBT.targetPosition = collider.transform.position;
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
