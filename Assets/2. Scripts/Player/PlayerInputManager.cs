using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private float inputX;
    [ReadOnly, SerializeField] private float inputY;
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        player.playerMovement.Move(inputX, inputY);

        // this code section would be gone to different script of which name might be PlayerWeaponSelector or so 
        if(Input.GetKeyDown(KeyCode.Alpha1) && player.playerInfo.weaponType != WeaponType.Fist_Left) {
            // switch to fist
            player.playerLeftWeaponSlot.SelectWeapon(WeaponType.Fist_Left);
            player.playerRightWeaponSlot.SelectWeapon(WeaponType.Fist_Right);

            player.playerCombat.SetWeapons(player.playerLeftWeaponSlot.curWeapon.GetComponent<Weapon>(), player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

            player.playerInfo.weaponType = WeaponType.Fist_Left;
            player.playerInfo.attackIndex = 0;
            player.playerAnimation.ChangeMoveToFist();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && player.playerInfo.weaponType != WeaponType.TwoHand_Sword_Right) {
            // switch to sword
            player.playerLeftWeaponSlot.DestroyCurWeapon();
            player.playerRightWeaponSlot.SelectWeapon(WeaponType.TwoHand_Sword_Right);

            player.playerCombat.SetWeapons(null, player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());

            player.playerInfo.weaponType = WeaponType.TwoHand_Sword_Right;
            player.playerInfo.attackIndex = 0;
            player.playerAnimation.ChangeMoveTo2Hand();
        }


        if(Input.GetKeyDown(KeyCode.Space))
            player.playerMovement.Jump();

        if(Input.GetKeyDown(KeyCode.LeftShift))
            player.playerMovement.Roll();

        if(!player.playerInfo.isAttacking && Input.GetMouseButtonDown(0))
            player.playerCombat.Attack();
    }
}