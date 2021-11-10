using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IDLE,
    TRACE,
    ATTACK,
    DEATH
}
public class SkeletonSlave_State : MonoBehaviour
{
    public State state;
    public Transform targetTransform;
}
