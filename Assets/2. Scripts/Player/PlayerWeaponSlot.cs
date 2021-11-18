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
        curWeapon.transform.localScale = new Vector3(100f, 170f, 100f);

        Weapon weapon = curWeapon.GetComponent<Weapon>();
        
        weapon.weaponType = weaponType;
        weapon.attackPower = WeaponManager.instance.weaponAttackPowers[(int)weaponType];
        if(weaponType == WeaponType.Fist_Right) 
            weapon.durability = player.playerInfo.availableWeapons[(int)WeaponType.Fist_Left].durability;
        else
            weapon.durability = player.playerInfo.availableWeapons.Find(x => x.weaponType == weaponType).durability;
    }

    public void DestroyCurWeapon() {
        if(curWeapon != null)
            Destroy(curWeapon);
    }
}