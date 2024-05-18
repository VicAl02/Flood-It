using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
