using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : MonoBehaviour
{
    private Player player;
    [ReadOnly] public GameObject curWeapon;

    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    public void SelectWeapon(WeaponType weaponType) {
        if(curWeapon != null)
            Destroy(curWeapon);
            
        curWeapon = Instantiate(WeaponManager.instance.weaponPrefabs[((int)weaponType)], player.playerWeaponSpawnPoints[(int)weaponType].transform.position, Quaternion.identity);
        curWeapon.transform.parent = gameObject.transform;
        curWeapon.transform.localScale = new Vector3(100f, 100f, 100f);
    }

    public void DestroyCurWeapon() {
        if(curWeapon != null)
            Destroy(curWeapon);
    }
}