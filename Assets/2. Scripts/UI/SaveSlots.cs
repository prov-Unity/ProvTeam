using UnityEngine;

public class SaveSlots : MonoBehaviour
{
    [ReadOnly] public SaveSlot [] saveSlots;

    private void Awake() {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }
}
