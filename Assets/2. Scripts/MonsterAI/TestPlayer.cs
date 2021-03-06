using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Collider[] colliders;

    private static readonly int AssembleHash = Animator.StringToHash("Assemble");

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Detect());
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
        float inputY = Input.GetAxis("Vertical") * Time.deltaTime * 5f;
        transform.position += new Vector3(inputX, 0, inputY);
        transform.rotation = Quaternion.LookRotation(new Vector3(inputX, 0, inputY));
    }

    private IEnumerator Detect()
    {
        while (true)
        {
            colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(transform.position, 15f, colliders, LayerMask.GetMask("Monster"));
            foreach (Collider coll in colliders)
            {
                if (coll == null)
                    break;
                if (coll.CompareTag("Monster"))
                {
                    MonsterAI monsterAI = coll.GetComponent<MonsterAI>();
                    monsterAI.StartAction();
                }
                else if (coll.CompareTag("Golem"))
                {
                    coll.GetComponent<Animator>().SetTrigger(AssembleHash);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
