using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class FindPlayerInfo
{
    [SerializeField, ReadOnly] private float lastFindTime;
    public Player player;

    public FindPlayerInfo(Player player)
    {
        this.player = player;
        lastFindTime = Time.time;
    }
    
    /// <summary>
    /// 마지막으로 본 시간 업데이트
    /// </summary>
    public void UpdateFindTime()
    {
        Debug.Log("시간 업데이트 됨");
        lastFindTime = Time.time;
    }

    /// <summary>
    /// 잊혀져야 하는지 체크
    /// </summary>
    public bool CheckExpire(float forgetTime)
    {
        return (Time.time - lastFindTime) >= forgetTime;
    }
}
public class MeleeSensor : MonoBehaviour
{
        [Header("Sensor Setting")] 
        public float range;
        public float AttackDistance = 2f;
        public Transform sensorTransform;
        public float sensorCheckCycle = 0.1f;
    
        /// <summary>
        /// FindTargets에 들어간 애들은 해당 시간이 지나면 잊혀진다.
        /// 잊혀지는 기준은 더 이상 시야각에 보이지 않을 떄부터 시간이 흐른다.
        /// </summary>
        public float timeToForget = 3f;

        private Player target;
        private Transform targetTransform;
        private FindPlayerInfo currentTarget;
        private MonsterState monsterState;
    
        private void Start()
        {
            monsterState = GetComponent<MonsterState>();
            StartCoroutine(CheckSensor());
            range = monsterState.monsterInfo.traceDistance;
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
            Gizmos.color = new Color(0f, 1f, 1f, 0.16f);
            Gizmos.DrawSphere(transform.position, range); 
            
        }

        private void CheckFindPlayerExpire()
        {
            if (currentTarget != null && currentTarget.CheckExpire(timeToForget))
            {
                currentTarget = null;
                Debug.Log("TimeToForget IDLE");
                monsterState.state = State.IDLE;
            }
        }
    
        private IEnumerator CheckSensor()
        {
            while (true)
            {
                if (monsterState.state == State.DEATH) yield break;
                yield return new WaitForSeconds(sensorCheckCycle);
                CheckFindPlayerExpire();
                Collider[] checkObjs = Physics.OverlapSphere(transform.position, range, 
                                                            LayerMask.GetMask("Player"));
                foreach (var checkObj in checkObjs)
                {
                    if (!checkObj.CompareTag("Player"))
                        continue;
                    target = checkObj.GetComponent<Player>();
                    targetTransform = target.neckTransform;
                    monsterState.targetTransform = targetTransform;
                    MonsterAction();
                }
            }
        }

        public void MonsterAction()
        {
            if (currentTarget == null)
            {
                print("Trace");
                monsterState.state = State.TRACE;
                currentTarget = new FindPlayerInfo(target);
            }
            else
            {
                float distance = Vector3.Distance(transform.position, targetTransform.position); 
                if (distance <= AttackDistance)
                {
                    Debug.Log("Attack");
                    monsterState.state = State.ATTACK;
                }                
                else if (monsterState.state == State.IDLE)
                    monsterState.state = State.TRACE;
                currentTarget.UpdateFindTime();
            }
        }
        
}
