using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/Data", order = 0)]
public class MonsterData : ScriptableObject
{
    public List<MonsterInfo> monsterInfos;
}

[System.Serializable]
public class MonsterInfo
{
    public string name;
    public int hp;
    public int speed;
    public float traceDistance;
    public float attackDistance;
    public float forgetTime;
}
