using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private float inputX;
    [ReadOnly, SerializeField] private float inputY;
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    // this variable exists for debug purpose only
    bool isCreated = false;

    private void Update() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        player.playerMovement.Move(inputX, inputY);

        if(!isCreated && Input.GetKeyDown(KeyCode.E)) {
            // instantiate ornate_sword to right hand weapon slot
            player.playerRightWeaponSlot.SelectWeapon(WeaponType.Ornate_Sword);
            isCreated = true;

            player.playerCombat.SetWeapons(null, player.playerRightWeaponSlot.curWeapon.GetComponent<Weapon>());
        }

        if(Input.GetKeyDown(KeyCode.Space))
            player.playerMovement.Jump();

        if(Input.GetKeyDown(KeyCode.LeftShift))
            player.playerMovement.Roll();

        if(!player.playerInfo.isAttacking && Input.GetMouseButtonDown(0))
            player.playerCombat.Attack();
    }
}