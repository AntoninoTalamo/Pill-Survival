using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        PlayerData.instance.ResetPlayer();
        UIManager.instance.ResetTimer();
        UIManager.instance.menuState = UIManager.MenuState.GAMEPLAY;
    }
}
