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
        public float range = 4f;
        public float AttackDistance = 2f;
        public float fov = 65f;
        public Transform sensorTransform;
        public float sensorCheckCycle = 0.4f;
    
        /// <summary>
        /// FindTargets에 들어간 애들은 해당 시간이 지나면 잊혀진다.
        /// 잊혀지는 기준은 더 이상 시야각에 보이지 않을 떄부터 시간이 흐른다.
        /// </summary>
        public float timeToForget = 3f;

        private FindPlayerInfo currentTarget;
        private MonsterState monsterState;
    
        private void Start()
        {
            monsterState = GetComponent<MonsterState>();
            StartCoroutine(CheckSensor());
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
            Gizmos.color = new Color(0f, 1f, 1f, 0.16f);
            Gizmos.DrawSphere(transform.position, range); 
            
            Vector3 leftDir = Quaternion.Euler(0f, -fov * 0.5f, 0f) * sensorTransform.forward;
            Vector3 rightDir = Quaternion.Euler(0f, fov * 0.5f, 0f) * sensorTransform.forward;
            
            Gizmos.color = new Color(0.2f, 1f, 0.37f, 1f);
            Gizmos.DrawRay(sensorTransform.position, leftDir * range);
            Gizmos.DrawRay(sensorTransform.position, rightDir * range);
        }

        private void CheckFindPlayerExpire()
        {
            if (currentTarget != null && currentTarget.CheckExpire(timeToForget))
            {
                currentTarget = null;
                monsterState.state = State.IDLE;
            }
        }
    
        private IEnumerator CheckSensor()
        {
            while (true)
            {
                yield return new WaitForSeconds(sensorCheckCycle);
                CheckFindPlayerExpire();
                Collider[] checkObjs = Physics.OverlapSphere(transform.position, range, 
                                                            LayerMask.GetMask("Player"));
                foreach (var checkObj in checkObjs)
                {
                    Player target = checkObj.GetComponent<Player>();
                    Transform targetTransform = target.neckTransform;
                    monsterState.targetTransform = targetTransform;
                    Vector3 dir = (targetTransform.position - transform.position).normalized;
                    float dot = Vector3.Dot(dir, transform.forward);
                    if (dot >= Mathf.Cos(fov * 0.5f))
                    {
                        Debug.Log($"{checkObj}를 시야각 안에서 찾았따");
                        if (Physics.Linecast(sensorTransform.transform.position, targetTransform.position,
                            LayerMask.GetMask("Default", "Ground"), QueryTriggerInteraction.Ignore))
                        {
                            Debug.Log($"{checkObj}를 찾았는데 시야각에 안에 있는데 안보인다.");
                        }
                        else
                        {
                            if (currentTarget == null)
                            {
                                print("aaa");
                                monsterState.state = State.TRACE;
                                currentTarget = new FindPlayerInfo(target);
                            }
                            else
                            {
                                float distance = Vector3.Distance(transform.position, targetTransform.position);
                                if (monsterState.state == State.DEATH)
                                    yield break;
                                else if (distance <= 2f)
                                    monsterState.state = State.ATTACK;
                                else if (monsterState.state != State.ATTACK)
                                    monsterState.state = State.TRACE;
                                currentTarget.UpdateFindTime();
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"{checkObj}를 찾았는데 시야각에 없다.");
                    }
                }
            }
        }
}
