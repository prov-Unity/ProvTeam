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
            
        curWeapon = Instantiate(WeaponManager.instance.weaponPrefabs[((int)weaponType)], player.playerWeaponSpawnPoints[(int)weaponType].transform.position, player.playerWeaponSpawnPoints[(int)weaponType].transform.rotation);
        curWeapon.transform.parent = gameObject.transform;
        curWeapon.transform.localScale = new Vector3(100f, 100f, 100f);

        Weapon weapon = curWeapon.GetComponent<Weapon>();
        switch(weaponType) {
            case WeaponType.Fist_Left: weapon.InitializeWeapon(5, 100); break;
            case WeaponType.Bone_Right: weapon.InitializeWeapon(10, 7); break;
        }
    }

    public void DestroyCurWeapon() {
        if(curWeapon != null)
            Destroy(curWeapon);
    }
}