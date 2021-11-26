using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectDestroy : MonoBehaviour
{
    private Weapon _weapon;
    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        StartCoroutine(EffectDestory());
    }

    private IEnumerator EffectDestory()
    {
        _weapon.owner = "Monster";
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
