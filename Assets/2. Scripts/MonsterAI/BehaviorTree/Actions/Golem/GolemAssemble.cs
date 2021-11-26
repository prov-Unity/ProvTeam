using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAssemble : MonoBehaviour
{
    public void CreateGolem()
    {
        Debug.Log("aa");
        GameObject golem = Resources.Load<GameObject>("Monster/GolemLava_Legacy");
        Instantiate(golem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
