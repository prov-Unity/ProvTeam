using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMonsterInfo : MonoBehaviour
{
    [ReadOnly] public TextMeshProUGUI textMonsterName;
    [ReadOnly] public Image imageMonsterHealth;
    private void Awake() {
        textMonsterName = GetComponentInChildren<TextMeshProUGUI>();
        imageMonsterHealth = GetComponentsInChildren<Image>()[1];
    }

    public void UpdateMonsterInfo(MonsterAI targetMonster) {
        textMonsterName.text = targetMonster.monsterData.monsterInfos[(int)targetMonster.monsterType].name;
        imageMonsterHealth.fillAmount = (float)targetMonster.currentHp / targetMonster.monsterData.monsterInfos[(int)targetMonster.monsterType].maxHp;
    }
}