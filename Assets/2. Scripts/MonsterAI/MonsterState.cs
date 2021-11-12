using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IDLE,
    TRACE,
    ATTACK,
    HIT,
    DEATH
}
public class MonsterState : MonoBehaviour
{
    [ReadOnly] public State state;
    [ReadOnly] public Transform targetTransform;
    [ReadOnly] public Weapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        state = State.IDLE;
    }

    public void TurnOnWeaponCollider()
    {
        weapon.EnableCollider(true);
    }

    public void TurnOffWeaponCollider()
    {
        weapon.EnableCollider(false);
    }
    

}