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
    [HideInInspector]public MonsterData monsterData;
    [ReadOnly] public MonsterInfo monsterInfo;

    private void Awake()
    {
        monsterData = Resources.Load<MonsterData>("Datas/MonsterData");
        weapon = GetComponentInChildren<Weapon>();
        state = State.IDLE;
        monsterInfo = monsterData.monsterInfos[0];
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