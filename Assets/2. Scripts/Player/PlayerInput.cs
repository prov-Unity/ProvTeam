using UnityEngine;

public class PlayerInput : MonoBehaviour
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

        if(Input.GetKeyDown(KeyCode.Space))
            player.playerMovement.Jump();

        if(Input.GetKeyDown(KeyCode.LeftShift))
            player.playerMovement.Roll(inputX, inputY);

        if(Input.GetMouseButtonDown(0))
            player.playerCombat.Attack();

        if(Input.GetKeyDown(KeyCode.G) && player.playerInfo.curWeapon.weaponType != WeaponType.Fist_Left)
            player.playerInteractionManager.DropCurrentWeapon();

        // this code would be deleted right after implementing the interaction between savepoint and player
        if(Input.GetKeyDown(KeyCode.K))
            UIManager.instance.EnableSavePopup();

    }
}