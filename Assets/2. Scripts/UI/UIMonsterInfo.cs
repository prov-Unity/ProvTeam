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

    public void UpdateMonsterInfo(MonsterAI targetMonster) {
        textMonsterName.text = targetMonster.monsterData.monsterInfos[(int)targetMonster.monsterType].name;
        imageMonsterHealth.fillAmount = targetMonster.currentHp / targetMonster.monsterData.monsterInfos[(int)targetMonster.monsterType].maxHp;
    }
}