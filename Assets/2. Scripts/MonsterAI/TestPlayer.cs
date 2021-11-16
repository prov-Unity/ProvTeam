using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = new Collider[10];
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.position += new Vector3(inputX, 0, inputY);
        transform.rotation = Quaternion.LookRotation(new Vector3(inputX, 0, inputY));
    }

    private IEnumerator Detect()
    {
        while (true)
        {
            Physics.OverlapSphereNonAlloc(transform.position, 7f, colliders, LayerMask.GetMask("Monster"));
            foreach (Collider coll in colliders)
            {
                if (coll == null)
                    break;
                coll.GetComponent<MonsterAI>().Action();

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
