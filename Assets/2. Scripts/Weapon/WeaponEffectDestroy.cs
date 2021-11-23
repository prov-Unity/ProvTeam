using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectDestroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(EffectDestory());
    }

    private IEnumerator EffectDestory()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
