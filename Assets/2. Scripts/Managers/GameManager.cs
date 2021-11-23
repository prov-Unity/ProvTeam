using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public static GameManager instance;
    [ReadOnly] public Player player;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        player = FindObjectOfType<Player>();
    }

    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * timeScale;
    }

    public void EnablePlayerInput() {
        player.playerInput.enabled = true;
    }

    public void DisablePlayerInput() {
        player.playerInput.enabled = false;
    }
}