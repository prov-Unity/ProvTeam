using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject _player;
    public GameObject _endscreen;

    // Update is called once per frame
    void Update()
    {
        _endscreen.SetActive(!_player.activeInHierarchy);
    }
}
