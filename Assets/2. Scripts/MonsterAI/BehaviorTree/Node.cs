using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;

    public NodeState NodeState
    {
        get => _nodeState;
    }

    public abstract NodeState Evaluate();
}
