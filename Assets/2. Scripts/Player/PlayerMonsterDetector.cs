using System.Collections;
using UnityEngine;

public class PlayerMonsterDetector : MonoBehaviour
{
    [ReadOnly, SerializeField] private Player player;
    [ReadOnly, SerializeField] private Collider[] colliders;

    [ReadOnly, SerializeField] private float detectRange;
    [ReadOnly, SerializeField] private float detectCycle;

    private void Awake() {
        player = GetComponent<Player>();

        detectRange = 7f;
        detectCycle = 0.1f;
    }

    private void Start() {
        StartCoroutine("DetectMonsters");
    }

    private IEnumerator DetectMonsters() {
        while (true) {
            colliders = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Monster"));
            foreach (Collider curCollider in colliders) {
                MonsterAI monsterAI = curCollider.GetComponent<MonsterAI>();
                monsterAI.StartAction();
            }
            yield return new WaitForSeconds(detectCycle);
        }
    }

    // this method would occur error before starting the game because the values which this method uses are initialized after the game begins
    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;

    //     Gizmos.DrawWireSphere(transform.position, detectRange);        
    // }  
}