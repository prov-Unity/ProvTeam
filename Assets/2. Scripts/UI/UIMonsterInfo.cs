using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMonsterInfo : MonoBehaviour
{
    [ReadOnly] public TextMeshProUGUI textMonsterName;
    [ReadOnly] public Image imageMonsterHealth;

    private void Awake() {
        textMonsterName = GetComponentInChildren<TextMeshProUGUI>();
        imageMonsterHealth = GetComponentInChildren<Image>();
    }
}