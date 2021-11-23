using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [ReadOnly] public Weapon targetWeapon;
    [ReadOnly] public SavePoint targetSavePoint;
    [ReadOnly] public bool isNewWeapon;

    private Player player;
    private string targetName; 
    private int tempDurability;
    private Vector3 vectorForDroppedWeaponPosition;
    private Collider [] colliders;
    private int curIndex;
    private int targetIndex;

    private float distanceToInteract;
    private float yValueForInteractionBox;

    private void Awake() {
        targetWeapon = null;
        isNewWeapon = true;
        tempDurability = -1;
        vectorForDroppedWeaponPosition = new Vector3(0, 0.1f, 0);

        player = GetComponent<Player>();


        distanceToInteract = 0.15f;
        yValueForInteractionBox = 0.45f;
    }

    private void Update() {
        colliders = Physics.OverlapBox(player.transform.position + new Vector3(player.transform.forward.normalized.x, yValueForInteractionBox, player.transform.forward.normalized.z), 
                                        new Vector3(distanceToInteract, player.transform.lossyScale.y, distanceToInteract), Quaternion.identity, LayerMask.GetMask("Interactable"));
        
        if(colliders.Length > 0) {
            targetWeapon = colliders[0].GetComponent<Weapon>();
            targetSavePoint = colliders[0].GetComponent<SavePoint>();

            if(targetWeapon != null && targetWeapon.owner == null) {
                UIManager.instance.EnableInteractionPopup();
                
                targetName = targetWeapon.weaponType.ToString().Split(new char[] {'_'})[0];
                // check whether this weapon is the new weapon for the player
                if(player.playerInfo.availableWeapons.Find(x => x.weaponType == targetWeapon.weaponType) == null) {
                    // this branch is for the new weapon
                    UIManager.instance.SetInteractionPopupText($"Press E to pickup {targetName}({targetWeapon.durability})");
                    isNewWeapon = true;
                }
                else {
                    // this branch is for the existing weapon
                    UIManager.instance.SetInteractionPopupText($"Press E to replace {targetName}({targetWeapon.durability})");
                    isNewWeapon = false;
                }
            }
            else if(targetSavePoint != null) {
                UIManager.instance.EnableInteractionPopup();
                UIManager.instance.SetInteractionPopupText("Press E to save");
            }
            else {
                if(!UIManager.instance.isInteractionPopupDisabled) {
                    UIManager.instance.DisableInteractionPopup();
                    targetWeapon = null;
                    targetSavePoint = null;
                }
            }
        }
        else {
            if(!UIManager.instance.isInteractionPopupDisabled) {
                UIManager.instance.DisableInteractionPopup();
                targetWeapon = null;
                targetSavePoint = null;
            }
        }
    }

    public void PickupNewWeapon() {
        player.playerInfo.availableWeapons.Add(new AvailableWeapon(targetWeapon.weaponType, targetWeapon.durability));
        Destroy(targetWeapon.gameObject);

        UIManager.instance.EnableWeaponSelectionPopup();
        WeaponSelectionManager.instance.MoveWeaponSelectionBoxesLeftOnce();
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        UIManager.instance.DisableWeaponSelectionPopup();
    }

    public void ReplaceCurrentWeapon() {
        AvailableWeapon replacedWeapon = player.playerInfo.availableWeapons.Find(x => x.weaponType == targetWeapon.weaponType);
        tempDurability = replacedWeapon.durability;
        replacedWeapon.durability = targetWeapon.durability;
        targetWeapon.durability = tempDurability;

        curIndex = ((int)player.playerInfo.curWeapon.weaponType);
        targetIndex = ((int)replacedWeapon.weaponType);


        if(curIndex < targetIndex) {
            UIManager.instance.EnableWeaponSelectionPopup();
            for(; curIndex < targetIndex; curIndex++) {
                WeaponSelectionManager.instance.MoveWeaponSelectionBoxesLeftOnce();
            }
            WeaponSelectionManager.instance.SelectCurrentWeapon();
            UIManager.instance.DisableWeaponSelectionPopup();
        }
        else if(curIndex > targetIndex) {
            UIManager.instance.EnableWeaponSelectionPopup();
            for(; curIndex > targetIndex; curIndex--) {
                WeaponSelectionManager.instance.MoveWeaponSelectionBoxesRightOnce();
            }
            WeaponSelectionManager.instance.SelectCurrentWeapon();
            UIManager.instance.DisableWeaponSelectionPopup();
        }
    }

    public void DropCurrentWeapon() {
        AvailableWeapon droppedWeaponInfo = player.playerInfo.curWeapon;

        GameObject droppedWeapon = Instantiate(WeaponManager.instance.weaponPrefabs[((int)droppedWeaponInfo.weaponType)]);
        droppedWeapon.transform.position = player.transform.position + player.transform.forward.normalized + vectorForDroppedWeaponPosition;
        droppedWeapon.GetComponent<Weapon>().durability = droppedWeaponInfo.durability;

        player.playerInfo.availableWeapons.Remove(droppedWeaponInfo);

        UIManager.instance.EnableWeaponSelectionPopup();
        WeaponSelectionManager.instance.MoveWeaponSelectionBoxesRightOnce();
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        UIManager.instance.DisableWeaponSelectionPopup();
    }

    public void DestoryCurrentWeapon() {
        player.playerInfo.availableWeapons.Remove(player.playerInfo.curWeapon);

        UIManager.instance.EnableWeaponSelectionPopup();
        WeaponSelectionManager.instance.MoveWeaponSelectionBoxesRightOnce();
        WeaponSelectionManager.instance.SelectCurrentWeapon();
        UIManager.instance.DisableWeaponSelectionPopup();
    }

    // // this method would occur error before starting the game because the values which this method uses are initialized after the game begins
    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;
        
    //     Gizmos.DrawWireCube(player.transform.position + new Vector3(player.transform.forward.normalized.x, yValueForInteractionBox, player.transform.forward.normalized.z), 
    //                         new Vector3(distanceToInteract, player.transform.lossyScale.y, distanceToInteract));
    // }    
}