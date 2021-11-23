using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private SaveSlots saveSlots;

    private void Awake() {
        saveSlots = GetComponentInChildren<SaveSlots>();
    }

    
}
